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

        public async Task<List<Birthday>> Get(string intervalTime)
        {
            
            static int GetDayOfYear(DateTime date)
            {
                TimeSpan difference = date - new DateTime(date.Year, 1, 1);
                
                return difference.Days + 1;
            }
            
            var birthdayEntities = await _context.Birthdays
                .AsNoTracking()
                .ToListAsync();
            
            DateTime today = DateTime.Today;
            int dayOfYear = GetDayOfYear(today);

            switch (intervalTime)
            {
                case "today": 
                    return birthdayEntities.Select(birthday => new Birthday
                    {
                        Id = birthday.Id,
                        Name = birthday.Name,
                        Description = birthday.Description,
                        Date = birthday.Date,
                    })
                    .Where(x=>(x.Date.Day == today.Day) && (x.Date.Month == today.Month))
                    .ToList();
                
                case "tomorrow" :
                    return birthdayEntities.Select(birthday => new Birthday
                    {
                        Id = birthday.Id,
                        Name = birthday.Name,
                        Description = birthday.Description,
                        Date = birthday.Date,
                    })
                    .Where(x=>(GetDayOfYear(x.Date) - dayOfYear == 1))
                    .OrderBy(x=>x.Name)
                    .ToList();
                
                case "10 days":
                    return birthdayEntities.Select(birthday => new Birthday
                        {
                            Id = birthday.Id,
                            Name = birthday.Name,
                            Description = birthday.Description,
                            Date = birthday.Date,
                        })
                        .Where(x=>((GetDayOfYear(x.Date) - dayOfYear <= 10) && (GetDayOfYear(x.Date) - dayOfYear >= 0 )))
                        .OrderBy(x=>x.Date.Day)
                        .ToList();

                case "this month":
                    return birthdayEntities.Select(birthday => new Birthday
                        {
                            Id = birthday.Id,
                            Name = birthday.Name,
                            Description = birthday.Description,
                            Date = birthday.Date,
                        })
                        .Where(x=>(x.Date.Month == today.Month))
                        .OrderBy(x=>x.Date.Day)
                        .ToList();
                
                default: 
                    return birthdayEntities
                    .Select(birthday => new Birthday
                    {
                        Id = birthday.Id,
                        Name = birthday.Name,
                        Description = birthday.Description,
                        Date = birthday.Date,
                    })
                    .OrderBy(x=> x.Name)
                    .ToList();
            }
        }
        
        

        public async Task<Guid> Create(Birthday birthday)
        {
            var birthdayEntity = new Birthday
            {
                Id = birthday.Id,
                Name = birthday.Name,
                Description = birthday.Description,
                Date = birthday.Date,
            };

            await _context.Birthdays.AddAsync(birthdayEntity);
            await _context.SaveChangesAsync();

            return birthdayEntity.Id;
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