using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.TransactionDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;

    public TransactionService(ApplicationDatabaseCtx dbCtx, IMapper mapper)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
    }

    public async Task<List<TransactionResponseDto>> GetTransactionsByUserIdAsync(string userId)
    {
        var transactions = await _dbCtx.Transaction
            .Include(t => t.Loan).ThenInclude(l => l.Account)
            .Include(t => t.Investment).ThenInclude(i => i.Account)
            .Where(t => (t.Loan != null && t.Loan.Account.ApplicationUserId == userId)
                     || (t.Investment != null && t.Investment.Account.ApplicationUserId == userId))
            .ToListAsync();
        return _mapper.Map<List<TransactionResponseDto>>(transactions);
    }

    public async Task<TransactionResponseDto?> GetByIdAndUserIdAsync(int id, string userId)
    {
        var transaction = await _dbCtx.Transaction
            .Include(t => t.Loan).ThenInclude(l => l.Account)
            .Include(t => t.Investment).ThenInclude(i => i.Account)
            .FirstOrDefaultAsync(t => t.TransactionId == id
                && ((t.Loan != null && t.Loan.Account.ApplicationUserId == userId)
                 || (t.Investment != null && t.Investment.Account.ApplicationUserId == userId)));
        return transaction == null ? null : _mapper.Map<TransactionResponseDto>(transaction);
    }

    public async Task<TransactionResponseDto?> CreateAsync(TransactionCreateDto dto, string userId)
    {
        var accountOwnerMatch = await _dbCtx.Loan
            .Include(l => l.Account)
            .Where(l => l.LoanId == dto.LoanId && l.Account.ApplicationUserId == userId)
            .AnyAsync()
            || await _dbCtx.Investment
            .Include(i => i.Account)
            .Where(i => i.InvestmentId == dto.InvestmentId && i.Account.ApplicationUserId == userId)
            .AnyAsync();

        if (!accountOwnerMatch)
        {
            return null;
        }

        var entity = _mapper.Map<Transaction>(dto);
        await _dbCtx.Transaction.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<TransactionResponseDto>(entity);
    }

    public async Task<TransactionResponseDto?> UpdateAsync(int id, UpdateTransactionDto dto, string userId)
    {
        var existing = await _dbCtx.Transaction
            .Include(t => t.Loan).ThenInclude(l => l.Account)
            .Include(t => t.Investment).ThenInclude(i => i.Account)
            .FirstOrDefaultAsync(t => t.TransactionId == id
                && ((t.Loan != null && t.Loan.Account.ApplicationUserId == userId)
                 || (t.Investment != null && t.Investment.Account.ApplicationUserId == userId)));
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<TransactionResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var transaction = await _dbCtx.Transaction
            .Include(t => t.Loan).ThenInclude(l => l.Account)
            .Include(t => t.Investment).ThenInclude(i => i.Account)
            .FirstOrDefaultAsync(t => t.TransactionId == id
                && ((t.Loan != null && t.Loan.Account.ApplicationUserId == userId)
                 || (t.Investment != null && t.Investment.Account.ApplicationUserId == userId)));
        if (transaction == null)
        {
            return false;
        }

        _dbCtx.Transaction.Remove(transaction);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}