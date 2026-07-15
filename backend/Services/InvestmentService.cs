using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.InvestmentDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class InvestmentService : IInvestmentService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;

    public InvestmentService(ApplicationDatabaseCtx dbCtx, IMapper mapper)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
    }

    public async Task<List<InvestmentResponseDto>> GetAllAsync()
    {
        var investments = await _dbCtx.Investment.ToListAsync();
        return _mapper.Map<List<InvestmentResponseDto>>(investments);
    }

    public async Task<InvestmentResponseDto?> GetByIdAsync(int id)
    {
        var investment = await _dbCtx.Investment.FindAsync(id);
        return investment == null ? null : _mapper.Map<InvestmentResponseDto>(investment);
    }

    public async Task<InvestmentResponseDto> CreateAsync(InvestmentCreateDto dto)
    {
        var entity = _mapper.Map<Investment>(dto);
        await _dbCtx.Investment.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<InvestmentResponseDto>(entity);
    }

    public async Task<InvestmentResponseDto?> UpdateAsync(int id, UpdateInvestmentDto dto)
    {
        var existing = await _dbCtx.Investment.FindAsync(id);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<InvestmentResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var investment = await _dbCtx.Investment.FindAsync(id);
        if (investment == null)
        {
            return false;
        }

        _dbCtx.Investment.Remove(investment);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}