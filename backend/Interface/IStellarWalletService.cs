using ApacStellar2026.Dto.StellarWalletDto;

namespace ApacStellar2026.Interface;

public interface IStellarWalletService
{
    Task<List<StellarWalletResponseDto>> GetByUserIdAsync(string userId);
    Task<StellarWalletResponseDto?> GetByIdAndUserIdAsync(int id, string userId);
    Task<StellarWalletResponseDto> CreateAsync(StellarWalletCreateDto dto, string userId);
    Task<StellarWalletResponseDto?> UpdateAsync(int id, UpdateStellarWalletDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
    Task<decimal> GetBalanceAsync(int walletId);
}