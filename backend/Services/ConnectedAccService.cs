using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.ConnectedAccDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class ConnectedAccService : IConnectedAccService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;

    public ConnectedAccService(ApplicationDatabaseCtx dbCtx, IMapper mapper)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
    }

    public async Task<List<ConnectedAccResponseDto>> GetAllAsync()
    {
        var connectedAccounts = await _dbCtx.ConnectedAccount
            .Include(ca => ca.FinancialInstitution)
            .ToListAsync();
        return _mapper.Map<List<ConnectedAccResponseDto>>(connectedAccounts);
    }

    public async Task<ConnectedAccResponseDto?> GetByIdAsync(int id)
    {
        var connectedAccount = await _dbCtx.ConnectedAccount
            .Include(ca => ca.FinancialInstitution)
            .FirstOrDefaultAsync(ca => ca.AccountId == id);
        return connectedAccount == null ? null : _mapper.Map<ConnectedAccResponseDto>(connectedAccount);
    }

    public async Task<ConnectedAccResponseDto> CreateAsync(ConnectedAccCreateDto dto)
    {
        var entity = _mapper.Map<ConnectedAccount>(dto);
        await _dbCtx.ConnectedAccount.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();

        // Reload with navigation property for the response
        await _dbCtx.Entry(entity).Reference(ca => ca.FinancialInstitution).LoadAsync();
        return _mapper.Map<ConnectedAccResponseDto>(entity);
    }

    public async Task<ConnectedAccResponseDto?> UpdateAsync(int id, UpdateConnectedAccDto dto)
    {
        var existing = await _dbCtx.ConnectedAccount
            .Include(ca => ca.FinancialInstitution)
            .FirstOrDefaultAsync(ca => ca.AccountId == id);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<ConnectedAccResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var connectedAccount = await _dbCtx.ConnectedAccount.FindAsync(id);
        if (connectedAccount == null)
        {
            return false;
        }

        _dbCtx.ConnectedAccount.Remove(connectedAccount);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}