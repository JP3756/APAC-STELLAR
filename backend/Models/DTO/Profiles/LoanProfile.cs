using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.LoanDto;

namespace ApacStellar2026.Dto.Profiles;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, LoanResponseDto>();

        CreateMap<LoanCreateDto, Loan>()
            .ForMember(dest => dest.LoanId, opt => opt.Ignore())
            .ForMember(dest => dest.LoanNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Principal))
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap<UpdateLoanDto, Loan>()
            .ForMember(dest => dest.LoanId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.LoanNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore())
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
    }
}