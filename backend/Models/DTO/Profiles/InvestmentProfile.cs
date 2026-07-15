using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.InvestmentDto;

namespace ApacStellar2026.Dto.Profiles;

public class InvestmentProfile : Profile
{
    public InvestmentProfile()
    {
        CreateMap<Investment, InvestmentResponseDto>();

        CreateMap<InvestmentCreateDto, Investment>()
            .ForMember(dest => dest.InvestmentId, opt => opt.Ignore())
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap<UpdateInvestmentDto, Investment>()
            .ForMember(dest => dest.InvestmentId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
    }
}