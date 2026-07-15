using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.StellarAssetDto;

namespace ApacStellar2026.Dto.Profiles;

public class StellarAssetProfile : Profile
{
    public StellarAssetProfile()
    {
        CreateMap<StellarAsset, StellarAssetResponseDto>();

        CreateMap<StellarAssetCreateDto, StellarAsset>()
            .ForMember(dest => dest.StellarAssetId, opt => opt.Ignore())
            .ForMember(dest => dest.StellarWallet, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore());

        CreateMap<UpdateStellarAssetDto, StellarAsset>()
            .ForMember(dest => dest.StellarAssetId, opt => opt.Ignore())
            .ForMember(dest => dest.StellarWalletId, opt => opt.Ignore())
            .ForMember(dest => dest.StellarWallet, opt => opt.Ignore())
            .ForMember(dest => dest.AssetCode, opt => opt.Ignore())
            .ForMember(dest => dest.AssetIssuer, opt => opt.Ignore())
            .ForMember(dest => dest.IsIssuer, opt => opt.Ignore());
    }
}