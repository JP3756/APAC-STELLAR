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

    public async Task<List<LoanResponseDto>> GetLoansByUserIdAsync(string userId)
    {
        var loans = await _dbCtx.Loan
            .Include(l => l.Account)
            .Where(l => l.Account.ApplicationUserId == userId)
            .ToListAsync();
        return _mapper.Map<List<LoanResponseDto>>(loans);
    }

    public async Task<LoanResponseDto?> GetLoanByIdAndUserIdAsync(int id, string userId)
    {
        var loan = await _dbCtx.Loan
            .Include(l => l.Account)
            .FirstOrDefaultAsync(l => l.LoanId == id && l.Account.ApplicationUserId == userId);
        return loan == null ? null : _mapper.Map<LoanResponseDto>(loan);
    }

    public async Task<LoanResponseDto?> CreateLoanAsync(LoanCreateDto dto, string userId)
    {
        var account = await _dbCtx.ConnectedAccount
            .FirstOrDefaultAsync(ca => ca.AccountId == dto.AccountId && ca.ApplicationUserId == userId);
        if (account == null)
        {
            return null;
        }

        var entity = _mapper.Map<Loan>(dto);
        await _dbCtx.Loan.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<LoanResponseDto>(entity);
    }

    public async Task<LoanResponseDto?> UpdateLoanAsync(int id, UpdateLoanDto dto, string userId)
    {
        var existing = await _dbCtx.Loan
            .Include(l => l.Account)
            .FirstOrDefaultAsync(l => l.LoanId == id && l.Account.ApplicationUserId == userId);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<LoanResponseDto>(existing);
    }

    public async Task<bool> DeleteLoanAsync(int id, string userId)
    {
        var loan = await _dbCtx.Loan
            .Include(l => l.Account)
            .FirstOrDefaultAsync(l => l.LoanId == id && l.Account.ApplicationUserId == userId);
        if (loan == null)
        {
            return false;
        }

        _dbCtx.Loan.Remove(loan);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}