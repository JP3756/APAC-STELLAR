namespace ApacStellar2026.Settings;

public class StellarSettings
{
    public string TestnetUrl { get; set; } = "https://horizon-testnet.stellar.org";
    public string MainnetUrl { get; set; } = "https://horizon.stellar.org";
    public string Network { get; set; } = "Testnet";
    public string EncryptionKey { get; set; } = string.Empty;
}