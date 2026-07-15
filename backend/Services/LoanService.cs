using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.LoanDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class LoanService : ILoanService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;

    public LoanService(ApplicationDatabaseCtx dbCtx, IMapper mapper)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
    }

    public async Task<List<LoanResponseDto>> GetAllLoansAsync()
    {
        var loans = await _dbCtx.Loan.ToListAsync();
        return _mapper.Map<List<LoanResponseDto>>(loans);
    }

    public async Task<LoanResponseDto?> GetLoanByIdAsync(int id)
    {
        var loan = await _dbCtx.Loan.FindAsync(id);
        return loan == null ? null : _mapper.Map<LoanResponseDto>(loan);
    }

    public async Task<LoanResponseDto> CreateLoanAsync(LoanCreateDto dto)
    {
        var entity = _mapper.Map<Loan>(dto);
        await _dbCtx.Loan.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<LoanResponseDto>(entity);
    }

    public async Task<LoanResponseDto?> UpdateLoanAsync(int id, UpdateLoanDto dto)
    {
        var existing = await _dbCtx.Loan.FindAsync(id);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<LoanResponseDto>(existing);
    }

    public async Task<bool> DeleteLoanAsync(int id)
    {
        var loan = await _dbCtx.Loan.FindAsync(id);
        if (loan == null)
        {
            return false;
        }

        _dbCtx.Loan.Remove(loan);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}