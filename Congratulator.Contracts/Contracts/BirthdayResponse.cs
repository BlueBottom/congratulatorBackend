namespace Congratulator.Contracts.Contracts
{
    public record BirthdayResponse(
        Guid Id, 
        string Name,
        string Description,
        DateTime Date);
}
