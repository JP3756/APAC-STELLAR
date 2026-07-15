using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.FinancialInstitutionDto;

namespace ApacStellar2026.Dto.Profiles;

public class FinancialInstitutionProfile : Profile
{
    public FinancialInstitutionProfile()
    {
        CreateMap<FinancialInstitution, FinancialInstitutionResponseDto>();

        CreateMap<FinancialInstitutionCreateDto, FinancialInstitution>()
            .ForMember(dest => dest.FinancialInstitutionId, opt => opt.Ignore())
            .ForMember(dest => dest.ConnectedAccounts, opt => opt.Ignore());

        CreateMap<UpdateFinancialInstitutionDto, FinancialInstitution>()
            .ForMember(dest => dest.FinancialInstitutionId, opt => opt.Ignore())
            .ForMember(dest => dest.ConnectedAccounts, opt => opt.Ignore());
    }
}