using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Dto.StellarAssetDto;
using ApacStellar2026.Interface;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class StellarAssetService : IStellarAssetService
{
    private readonly ApplicationDatabaseCtx _dbCtx;
    private readonly IMapper _mapper;

    public StellarAssetService(ApplicationDatabaseCtx dbCtx, IMapper mapper)
    {
        _dbCtx = dbCtx;
        _mapper = mapper;
    }

    public async Task<List<StellarAssetResponseDto>> GetByUserIdAsync(string userId)
    {
        var assets = await _dbCtx.StellarAsset
            .Include(a => a.StellarWallet)
            .Where(a => a.StellarWallet.ApplicationUserId == userId)
            .ToListAsync();
        return _mapper.Map<List<StellarAssetResponseDto>>(assets);
    }

    public async Task<StellarAssetResponseDto?> GetByIdAndUserIdAsync(int id, string userId)
    {
        var asset = await _dbCtx.StellarAsset
            .Include(a => a.StellarWallet)
            .FirstOrDefaultAsync(a => a.StellarAssetId == id && a.StellarWallet.ApplicationUserId == userId);
        return asset == null ? null : _mapper.Map<StellarAssetResponseDto>(asset);
    }

    public async Task<List<StellarAssetResponseDto>> GetByWalletIdAndUserIdAsync(int walletId, string userId)
    {
        var assets = await _dbCtx.StellarAsset
            .Where(a => a.StellarWalletId == walletId && a.StellarWallet.ApplicationUserId == userId)
            .Include(a => a.StellarWallet)
            .ToListAsync();
        return _mapper.Map<List<StellarAssetResponseDto>>(assets);
    }

    public async Task<StellarAssetResponseDto?> CreateAsync(StellarAssetCreateDto dto, string userId)
    {
        var wallet = await _dbCtx.StellarWallet
            .FirstOrDefaultAsync(w => w.StellarWalletId == dto.StellarWalletId && w.ApplicationUserId == userId);
        if (wallet == null)
        {
            return null;
        }

        var entity = _mapper.Map<StellarAsset>(dto);
        await _dbCtx.StellarAsset.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<StellarAssetResponseDto>(entity);
    }

    public async Task<StellarAssetResponseDto?> UpdateAsync(int id, UpdateStellarAssetDto dto, string userId)
    {
        var existing = await _dbCtx.StellarAsset
            .Include(a => a.StellarWallet)
            .FirstOrDefaultAsync(a => a.StellarAssetId == id && a.StellarWallet.ApplicationUserId == userId);
        if (existing == null)
        {
            return null;
        }

        _mapper.Map(dto, existing);
        await _dbCtx.SaveChangesAsync();
        return _mapper.Map<StellarAssetResponseDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var asset = await _dbCtx.StellarAsset
            .Include(a => a.StellarWallet)
            .FirstOrDefaultAsync(a => a.StellarAssetId == id && a.StellarWallet.ApplicationUserId == userId);
        if (asset == null)
        {
            return false;
        }

        _dbCtx.StellarAsset.Remove(asset);
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}