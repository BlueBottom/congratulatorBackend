using Congratulator.Contracts.Contracts;
using Congratulator.Domain.Birthday;

namespace Congratulator.Core.Abstractions
{
    public interface IBirthdayService
    {
        Task<Guid> CreateBirthday(BirthdayRequest birthdayRequest);
        Task<Guid> DeleteBirthday(Guid id);
        Task<List<Birthday>> GetAllBirthdays();
        Task<Guid> UpdateBirthday(Guid id, string name, string description, DateTime date);
    }
}