using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.StellarPaymentDto;

namespace ApacStellar2026.Dto.Profiles;

public class StellarPaymentProfile : Profile
{
    public StellarPaymentProfile()
    {
        CreateMap<StellarPayment, StellarPaymentResponseDto>();

        CreateMap<StellarPaymentCreateDto, StellarPayment>()
            .ForMember(dest => dest.StellarPaymentId, opt => opt.Ignore())
            .ForMember(dest => dest.StellarWallet, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionHash, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore());
    }
}