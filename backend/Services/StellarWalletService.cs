using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.StellarWalletDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;
using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Service;

public class StellarWalletService : IStellarWalletService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;
    private readonly StellarService _stellarService;
    private readonly IConfiguration _configuration;

    public StellarWalletService(
        ApplicationDatabaseCtx dbCtx,
        IMapper mapper,
        StellarService stellarService,
        IConfiguration configuration)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
        _stellarService = stellarService;
        _configuration = configuration;
    }

    public async Task<List<StellarWalletResponseDto>> GetByUserIdAsync(string userId)
    {
        var wallets = await _dbCtx.StellarWallet
            .Where(w => w.ApplicationUserId == userId)
            .ToListAsync();
        return _mapper.Map<List<StellarWalletResponseDto>>(wallets);
    }

    public async Task<StellarWalletResponseDto?> GetByIdAndUserIdAsync(int id, string userId)
    {
        var wallet = await _dbCtx.StellarWallet
            .FirstOrDefaultAsync(w => w.StellarWalletId == id && w.ApplicationUserId == userId);
        return wallet == null ? null : _mapper.Map<StellarWalletResponseDto>(wallet);
    }

    public async Task<StellarWalletResponseDto> CreateAsync(StellarWalletCreateDto dto, string userId)
    {
        var network = dto.Network ?? StellarNetwork.Testnet;
        var (publicKey, secretSeed) = _stellarService.GenerateKeypair();

        var encryptionKey = _configuration["StellarSettings:EncryptionKey"]
            ?? throw new InvalidOperationException("EncryptionKey not configured.");
        var encryptedSecret = _stellarService.EncryptSecret(secretSeed, encryptionKey);

        var entity = new StellarWallet
        {
            ApplicationUserId = userId,
            PublicKey = publicKey,
            SecretKeyEncrypted = encryptedSecret,
            Network = network,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Balance = 0m
        };

        await _dbCtx.StellarWallet.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<StellarWalletResponseDto>(entity);
    }

    public async Task<StellarWalletResponseDto?> UpdateAsync(int id, UpdateStellarWalletDto dto, string userId)
    {
        var existing = await _dbCtx.StellarWallet
            .FirstOrDefaultAsync(w => w.StellarWalletId == id && w.ApplicationUserId == userId);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<StellarWalletResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var wallet = await _dbCtx.StellarWallet
            .FirstOrDefaultAsync(w => w.StellarWalletId == id && w.ApplicationUserId == userId);
        if (wallet == null)
        {
            return false;
        }

        _dbCtx.StellarWallet.Remove(wallet);
        await _dbCtx.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetBalanceAsync(int walletId)
    {
        var wallet = await _dbCtx.StellarWallet.FindAsync(walletId);
        if (wallet == null)
        {
            return 0m;
        }

        var balance = await _stellarService.GetBalance(wallet.PublicKey, wallet.Network);

        wallet.Balance = balance;
        await _dbCtx.SaveChangesAsync();

        return balance;
    }
}