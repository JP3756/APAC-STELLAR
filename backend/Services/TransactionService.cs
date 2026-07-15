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

    public async Task<List<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await _dbCtx.Transaction.ToListAsync();
        return _mapper.Map<List<TransactionResponseDto>>(transactions);
    }

    public async Task<TransactionResponseDto?> GetByIdAsync(int id)
    {
        var transaction = await _dbCtx.Transaction.FindAsync(id);
        return transaction == null ? null : _mapper.Map<TransactionResponseDto>(transaction);
    }

    public async Task<TransactionResponseDto> CreateAsync(TransactionCreateDto dto)
    {
        var entity = _mapper.Map<Transaction>(dto);
        await _dbCtx.Transaction.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<TransactionResponseDto>(entity);
    }

    public async Task<TransactionResponseDto?> UpdateAsync(int id, UpdateTransactionDto dto)
    {
        var existing = await _dbCtx.Transaction.FindAsync(id);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<TransactionResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _dbCtx.Transaction.FindAsync(id);
        if (transaction == null)
        {
            return false;
        }

        _dbCtx.Transaction.Remove(transaction);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}