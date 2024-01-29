using Congratulator.Contracts.Contracts;
using Congratulator.Core.Abstractions;
using Congratulator.Domain.Birthday;

namespace Congratulator.Application.Services
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IBirthdayRepository _birthdayRepository;
        public BirthdayService(IBirthdayRepository birthdayRepository)
        {
            _birthdayRepository = birthdayRepository;
        }

        public async Task<List<Birthday>> GetAllBirthdays(string intervalTime = "", string searchString = "")
        {
            return await _birthdayRepository.Get(intervalTime, searchString);
        }

        public async Task<Guid> CreateBirthday(BirthdayRequest birthdayRequest)
        {
            var birthday = new Birthday
            {
                Name = birthdayRequest.Name,
                Description = birthdayRequest.Description,
                Date = birthdayRequest.Date,
            };

            return await _birthdayRepository.Create(birthday);
        }

        public async Task<Guid> UpdateBirthday(Guid id, string name, string description, DateTime date)
        {
            return await _birthdayRepository.Update(id, name, description, date);
        }

        public async Task<Guid> DeleteBirthday(Guid id)
        {
            return await _birthdayRepository.Delete(id);
        }
    }
}