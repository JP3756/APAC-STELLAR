namespace ApacStellar2026.Dto.LoanDto;

public class LoanResponseDto
{
    public int LoanId { get; set; }
    public int AccountId { get; set; }
    public int LoanNumber { get; set; }
    public int Principal { get; set; }
    public int Balance { get; set; }
    public int Interest { get; set; }
    public DateTime DueDate { get; set; }
}