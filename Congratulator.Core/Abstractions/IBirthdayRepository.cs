using Congratulator.Domain.Birthday;

namespace Congratulator.Core.Abstractions;

public interface IBirthdayRepository
{
    Task<Guid> Create(Birthday birthday);
    Task<Guid> Delete(Guid id);
    Task<List<Birthday>> Get();
    Task<Guid> Update(Guid id, string name, string description, DateTime date);
}