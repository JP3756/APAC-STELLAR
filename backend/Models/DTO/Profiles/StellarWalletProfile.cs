using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.StellarWalletDto;

namespace ApacStellar2026.Dto.Profiles;

public class StellarWalletProfile : Profile
{
    public StellarWalletProfile()
    {
        CreateMap<StellarWallet, StellarWalletResponseDto>();

        CreateMap<StellarWalletCreateDto, StellarWallet>()
            .ForMember(dest => dest.StellarWalletId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PublicKey, opt => opt.Ignore())
            .ForMember(dest => dest.SecretKeyEncrypted, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Assets, opt => opt.Ignore());

        CreateMap<UpdateStellarWalletDto, StellarWallet>()
            .ForMember(dest => dest.StellarWalletId, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PublicKey, opt => opt.Ignore())
            .ForMember(dest => dest.SecretKeyEncrypted, opt => opt.Ignore())
            .ForMember(dest => dest.Network, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Assets, opt => opt.Ignore());
    }
}