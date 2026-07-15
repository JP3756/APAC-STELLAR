using AutoMapper;
using ApacStellar2026.Models;
using ApacStellar2026.Dto.TransactionDto;

namespace ApacStellar2026.Dto.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionResponseDto>();

        CreateMap<TransactionCreateDto, Transaction>()
            .ForMember(dest => dest.TransactionId, opt => opt.Ignore())
            .ForMember(dest => dest.Loan, opt => opt.Ignore())
            .ForMember(dest => dest.Investment, opt => opt.Ignore());

        CreateMap<UpdateTransactionDto, Transaction>()
            .ForMember(dest => dest.TransactionId, opt => opt.Ignore())
            .ForMember(dest => dest.Loan, opt => opt.Ignore())
            .ForMember(dest => dest.Investment, opt => opt.Ignore());
    }
}