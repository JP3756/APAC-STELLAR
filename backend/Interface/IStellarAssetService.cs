using ApacStellar2026.Dto.StellarAssetDto;

namespace ApacStellar2026.Interface;

public interface IStellarAssetService
{
    Task<List<StellarAssetResponseDto>> GetByUserIdAsync(string userId);
    Task<StellarAssetResponseDto?> GetByIdAndUserIdAsync(int id, string userId);
    Task<List<StellarAssetResponseDto>> GetByWalletIdAndUserIdAsync(int walletId, string userId);
    Task<StellarAssetResponseDto?> CreateAsync(StellarAssetCreateDto dto, string userId);
    Task<StellarAssetResponseDto?> UpdateAsync(int id, UpdateStellarAssetDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}