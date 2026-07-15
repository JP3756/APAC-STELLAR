using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.FinancialInstitutionDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class FinancialInstitutionService : IFinancialInstitutionService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;

    public FinancialInstitutionService(ApplicationDatabaseCtx dbCtx, IMapper mapper)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
    }

    public async Task<List<FinancialInstitutionResponseDto>> GetAllAsync()
    {
        var institutions = await _dbCtx.FinancialInstitution.ToListAsync();
        return _mapper.Map<List<FinancialInstitutionResponseDto>>(institutions);
    }

    public async Task<FinancialInstitutionResponseDto?> GetByIdAsync(int id)
    {
        var institution = await _dbCtx.FinancialInstitution.FindAsync(id);
        return institution == null ? null : _mapper.Map<FinancialInstitutionResponseDto>(institution);
    }

    public async Task<FinancialInstitutionResponseDto> CreateAsync(FinancialInstitutionCreateDto dto)
    {
        var entity = _mapper.Map<FinancialInstitution>(dto);
        await _dbCtx.FinancialInstitution.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<FinancialInstitutionResponseDto>(entity);
    }

    public async Task<FinancialInstitutionResponseDto?> UpdateAsync(int id, UpdateFinancialInstitutionDto dto)
    {
        var existing = await _dbCtx.FinancialInstitution.FindAsync(id);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<FinancialInstitutionResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var institution = await _dbCtx.FinancialInstitution.FindAsync(id);
        if (institution == null)
        {
            return false;
        }

        _dbCtx.FinancialInstitution.Remove(institution);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}