using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.ConnectedAccDto;

namespace ApacStellar2026.Dto.Profiles;

public class ConnectedAccProfile : Profile
{
    public ConnectedAccProfile()
    {
        CreateMap<ConnectedAccount, ConnectedAccResponseDto>()
            .ForMember(dest => dest.FinancialInstitutionName, opt => opt.MapFrom(src => src.FinancialInstitution != null ? src.FinancialInstitution.Name : null));

        CreateMap<ConnectedAccCreateDto, ConnectedAccount>()
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.FinancialInstitution, opt => opt.Ignore())
            .ForMember(dest => dest.Loans, opt => opt.Ignore())
            .ForMember(dest => dest.Investments, opt => opt.Ignore());

        CreateMap<UpdateConnectedAccDto, ConnectedAccount>()
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.InstitutionId, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.FinancialInstitution, opt => opt.Ignore())
            .ForMember(dest => dest.Loans, opt => opt.Ignore())
            .ForMember(dest => dest.Investments, opt => opt.Ignore());
    }
}