using Congratulator.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Congratulator.Domain.Birthday;

namespace Congratulator.DataAccess.Repositories
{
    public class BirthdayRepository : IBirthdayRepository
    {
        private readonly CongratulatorDbContext _context;

        public BirthdayRepository(CongratulatorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Birthday>> Get()
        {
            var birthdayEntities = await _context.Birthdays
                .AsNoTracking()
                .ToListAsync();

            var birthdays = birthdayEntities
                .Select(birthday => new Birthday
                {
                    Id = birthday.Id,
                    Name = birthday.Name,
                    Description = birthday.Description,
                    Date = birthday.Date,
                })
                .ToList();

            return birthdays;
        }
        
        

        public async Task<Guid> Create(Birthday birthday)
        {
            await _context.Birthdays.AddAsync(birthday);
            await _context.SaveChangesAsync();

            return birthday.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string description, DateTime date)
        {
            await _context.Birthdays
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Date, b => date));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Birthdays
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }


}
