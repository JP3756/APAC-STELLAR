using ApacStellar2026.Dto.ConnectedAccDto;

namespace ApacStellar2026.Interface;

public interface IConnectedAccService
{
    Task<List<ConnectedAccResponseDto>> GetAllAsync();
    Task<ConnectedAccResponseDto?> GetByIdAsync(int id);
    Task<ConnectedAccResponseDto> CreateAsync(ConnectedAccCreateDto dto);
    Task<ConnectedAccResponseDto?> UpdateAsync(int id, UpdateConnectedAccDto dto);
    Task<bool> DeleteAsync(int id);
}