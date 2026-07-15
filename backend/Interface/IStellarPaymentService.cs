using ApacStellar2026.Dto.StellarPaymentDto;

namespace ApacStellar2026.Interface;

public interface IStellarPaymentService
{
    Task<List<StellarPaymentResponseDto>> GetByUserIdAsync(string userId);
    Task<StellarPaymentResponseDto?> GetByIdAndUserIdAsync(int id, string userId);
    Task<List<StellarPaymentResponseDto>> GetByWalletIdAndUserIdAsync(int walletId, string userId);
    Task<StellarPaymentResponseDto?> SendPaymentAsync(StellarPaymentCreateDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}