namespace ApacStellar2026.Dto.LoanDto;

public class LoanCreateDto
{
    public int AccountId { get; set; }
    public int Principal { get; set; }
    public int Interest { get; set; }
    public DateTime DueDate { get; set; }
}