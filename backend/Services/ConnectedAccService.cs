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

    public async Task<List<ConnectedAccResponseDto>> GetAllByUserIdAsync(string userId)
    {
        var connectedAccounts = await _dbCtx.ConnectedAccount
            .Include(ca => ca.FinancialInstitution)
            .Where(ca => ca.ApplicationUserId == userId)
            .ToListAsync();
        return _mapper.Map<List<ConnectedAccResponseDto>>(connectedAccounts);
    }

    public async Task<ConnectedAccResponseDto?> GetByIdAndUserIdAsync(int id, string userId)
    {
        var connectedAccount = await _dbCtx.ConnectedAccount
            .Include(ca => ca.FinancialInstitution)
            .FirstOrDefaultAsync(ca => ca.AccountId == id && ca.ApplicationUserId == userId);
        return connectedAccount == null ? null : _mapper.Map<ConnectedAccResponseDto>(connectedAccount);
    }

    public async Task<ConnectedAccResponseDto> CreateAsync(ConnectedAccCreateDto dto, string userId)
    {
        var entity = _mapper.Map<ConnectedAccount>(dto);
        entity.ApplicationUserId = userId;
        await _dbCtx.ConnectedAccount.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();

        await _dbCtx.Entry(entity).Reference(ca => ca.FinancialInstitution).LoadAsync();
        return _mapper.Map<ConnectedAccResponseDto>(entity);
    }

    public async Task<ConnectedAccResponseDto?> UpdateAsync(int id, UpdateConnectedAccDto dto, string userId)
    {
        var existing = await _dbCtx.ConnectedAccount
            .Include(ca => ca.FinancialInstitution)
            .FirstOrDefaultAsync(ca => ca.AccountId == id && ca.ApplicationUserId == userId);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<ConnectedAccResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var connectedAccount = await _dbCtx.ConnectedAccount
            .FirstOrDefaultAsync(ca => ca.AccountId == id && ca.ApplicationUserId == userId);
        if (connectedAccount == null)
        {
            return false;
        }

        _dbCtx.ConnectedAccount.Remove(connectedAccount);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}