using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.StellarPaymentDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;
using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Service;

public class StellarPaymentService : IStellarPaymentService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;
    private readonly StellarService _stellarService;
    private readonly IConfiguration _configuration;

    public StellarPaymentService(
        ApplicationDatabaseCtx dbCtx,
        IMapper mapper,
        StellarService stellarService,
        IConfiguration configuration)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
        _stellarService = stellarService;
        _configuration = configuration;
    }

    public async Task<List<StellarPaymentResponseDto>> GetByUserIdAsync(string userId)
    {
        var payments = await _dbCtx.StellarPayment
            .Include(p => p.StellarWallet)
            .Where(p => p.StellarWallet.ApplicationUserId == userId)
            .ToListAsync();
        return _mapper.Map<List<StellarPaymentResponseDto>>(payments);
    }

    public async Task<StellarPaymentResponseDto?> GetByIdAndUserIdAsync(int id, string userId)
    {
        var payment = await _dbCtx.StellarPayment
            .Include(p => p.StellarWallet)
            .FirstOrDefaultAsync(p => p.StellarPaymentId == id && p.StellarWallet.ApplicationUserId == userId);
        return payment == null ? null : _mapper.Map<StellarPaymentResponseDto>(payment);
    }

    public async Task<List<StellarPaymentResponseDto>> GetByWalletIdAndUserIdAsync(int walletId, string userId)
    {
        var payments = await _dbCtx.StellarPayment
            .Where(p => p.StellarWalletId == walletId && p.StellarWallet.ApplicationUserId == userId)
            .Include(p => p.StellarWallet)
            .ToListAsync();
        return _mapper.Map<List<StellarPaymentResponseDto>>(payments);
    }

    public async Task<StellarPaymentResponseDto?> SendPaymentAsync(StellarPaymentCreateDto dto, string userId)
    {
        var wallet = await _dbCtx.StellarWallet
            .FirstOrDefaultAsync(w => w.StellarWalletId == dto.StellarWalletId && w.ApplicationUserId == userId);
        if (wallet == null)
        {
            return null;
        }

        var assetCode = dto.AssetCode ?? "XLM";

        var entity = new StellarPayment
        {
            StellarWalletId = dto.StellarWalletId,
            DestinationAddress = dto.DestinationAddress,
            AssetCode = assetCode,
            Amount = dto.Amount,
            Memo = dto.Memo,
            Status = StellarPaymentStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _dbCtx.StellarPayment.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();

        try
        {
            var encryptionKey = _configuration["StellarSettings:EncryptionKey"]
                ?? throw new InvalidOperationException("EncryptionKey not configured.");
            var secretSeed = _stellarService.DecryptSecret(wallet.SecretKeyEncrypted, encryptionKey);

            var txHash = await _stellarService.SubmitPayment(
                secretSeed,
                dto.DestinationAddress,
                dto.Amount,
                dto.Memo,
                wallet.Network);

            entity.Status = StellarPaymentStatus.Success;
            entity.TransactionHash = txHash;
            entity.CompletedAt = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            entity.Status = StellarPaymentStatus.Failed;
            entity.ErrorMessage = ex.Message;
            entity.CompletedAt = DateTime.UtcNow;
        }

        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<StellarPaymentResponseDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var payment = await _dbCtx.StellarPayment
            .Include(p => p.StellarWallet)
            .FirstOrDefaultAsync(p => p.StellarPaymentId == id && p.StellarWallet.ApplicationUserId == userId);
        if (payment == null)
        {
            return false;
        }

        _dbCtx.StellarPayment.Remove(payment);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}