using System.Security.Cryptography;
using System.Text;
using StellarDotnetSdk;
using StellarDotnetSdk.Accounts;
using StellarDotnetSdk.Assets;
using StellarDotnetSdk.Memos;
using StellarDotnetSdk.Operations;
using StellarDotnetSdk.Responses;
using StellarDotnetSdk.Transactions;
using ApacStellar2026.Models.Enums;
using ApacStellar2026.Settings;
using Microsoft.Extensions.Options;

namespace ApacStellar2026.Service;

public class StellarService
{
    private readonly StellarSettings _settings;

    public StellarService(IOptions<StellarSettings> settings)
    {
        _settings = settings.Value;
    }

    public (string publicKey, string secretSeed) GenerateKeypair()
    {
        var keypair = KeyPair.Random();
        return (keypair.AccountId, keypair.SecretSeed ?? string.Empty);
    }

    public string EncryptSecret(string secret, string encryptionKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32).Substring(0, 32));
        var plaintextBytes = Encoding.UTF8.GetBytes(secret);

        using var aes = new AesGcm(keyBytes, AesGcm.TagByteSizes.MaxSize);
        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);
        var ciphertext = new byte[plaintextBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);

        var result = new byte[nonce.Length + tag.Length + ciphertext.Length];
        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length, tag.Length);
        Buffer.BlockCopy(ciphertext, 0, result, nonce.Length + tag.Length, ciphertext.Length);

        return Convert.ToBase64String(result);
    }

    public string DecryptSecret(string encrypted, string encryptionKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32).Substring(0, 32));
        var data = Convert.FromBase64String(encrypted);

        var nonce = data[..AesGcm.NonceByteSizes.MaxSize];
        var tag = data[AesGcm.NonceByteSizes.MaxSize..(AesGcm.NonceByteSizes.MaxSize + AesGcm.TagByteSizes.MaxSize)];
        var ciphertext = data[(AesGcm.NonceByteSizes.MaxSize + AesGcm.TagByteSizes.MaxSize)..];

        using var aes = new AesGcm(keyBytes, AesGcm.TagByteSizes.MaxSize);
        var plaintextBytes = new byte[ciphertext.Length];
        aes.Decrypt(nonce, ciphertext, tag, plaintextBytes);

        return Encoding.UTF8.GetString(plaintextBytes);
    }

    private string GetHorizonUrl(StellarNetwork network)
    {
        return network switch
        {
            StellarNetwork.Mainnet => _settings.MainnetUrl,
            _ => _settings.TestnetUrl
        };
    }

    public async Task<AccountResponse?> GetAccountDetails(string publicKey, StellarNetwork network)
    {
        try
        {
            var server = new Server(GetHorizonUrl(network));
            var account = await server.Accounts.Account(publicKey);
            return account;
        }
        catch
        {
            return null;
        }
    }

    public async Task<decimal> GetBalance(string publicKey, StellarNetwork network)
    {
        try
        {
            var server = new Server(GetHorizonUrl(network));
            var account = await server.Accounts.Account(publicKey);
            var xlmBalance = account.Balances
                .FirstOrDefault(b => b.AssetType == "native");
            return xlmBalance != null ? decimal.Parse(xlmBalance.BalanceString) : 0m;
        }
        catch
        {
            return 0m;
        }
    }

    public async Task<string> SubmitPayment(string secretSeed, string destination, decimal amount, string? memo, StellarNetwork network)
    {
        var server = new Server(GetHorizonUrl(network));
        var sourceKeypair = KeyPair.FromSecretSeed(secretSeed);
        var destinationKeypair = KeyPair.FromAccountId(destination);

        var sourceAccount = await server.Accounts.Account(sourceKeypair.AccountId);

        var paymentOp = new PaymentOperation(destinationKeypair, new AssetTypeNative(), amount.ToString(), null);

        var transactionBuilder = new TransactionBuilder(sourceAccount)
            .AddOperation(paymentOp);

        if (!string.IsNullOrEmpty(memo))
        {
            transactionBuilder.AddMemo(new MemoText(memo));
        }

        var transaction = transactionBuilder.Build();
        transaction.Sign(sourceKeypair);

        var response = await server.SubmitTransaction(transaction);
        return response?.Hash ?? string.Empty;
    }
}