using ApacStellar2026.Dto.ConnectedAccDto;

namespace ApacStellar2026.Interface;

public interface IConnectedAccService
{
    Task<List<ConnectedAccResponseDto>> GetAllByUserIdAsync(string userId);
    Task<ConnectedAccResponseDto?> GetByIdAndUserIdAsync(int id, string userId);
    Task<ConnectedAccResponseDto> CreateAsync(ConnectedAccCreateDto dto, string userId);
    Task<ConnectedAccResponseDto?> UpdateAsync(int id, UpdateConnectedAccDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}