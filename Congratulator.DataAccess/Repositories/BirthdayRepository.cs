﻿using Congratulator.Core.Abstractions;
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

        public async Task<List<Birthday>> Get(string intervalTime, string searchString)
        {
            var today = DateTime.Today;
            var dayOfYear = GetDayOfYear(today);
            var birthdayEntities = await _context.Birthdays.AsNoTracking().ToListAsync();

            return intervalTime switch
            {
                "all" => (searchString != null) ? birthdayEntities.Where(x=>x.Name.Contains(searchString)).ToList() : birthdayEntities,
                "today" => FilterBirthdays(birthdayEntities, searchString,x => x.Date.Day == today.Day && x.Date.Month == today.Month),
                "tomorrow" => FilterBirthdays(birthdayEntities, searchString,x => GetDayOfYear(x.Date) - dayOfYear == 1, x => x.Name),
                "10 days" => FilterBirthdays(birthdayEntities, searchString,x => GetDayOfYear(x.Date) - dayOfYear >= 0 && GetDayOfYear(x.Date) - dayOfYear <= 10, x => GetDayOfYear(x.Date)),
                "this month" => FilterBirthdays(birthdayEntities, searchString,x => x.Date.Month == today.Month, x => x.Date.Day),
                _ => FilterBirthdays(birthdayEntities, searchString,x => true, x => x.Name)
            };
            
            List<Birthday> FilterBirthdays(List<Birthday> birthdays, string searchString, Predicate<Birthday> predicate, Func<Birthday, object> ordering = null)
            {
                var filtered = birthdays.FindAll(predicate);
                if (ordering != null)
                {
                    filtered.Sort((x, y) => Comparer<object>.Default.Compare(ordering(x), ordering(y)));
                }

                if (searchString != null)
                {
                    filtered = filtered.Where(x => x.Name.Contains(searchString)).ToList();
                }
                return filtered;
            }
             
            int GetDayOfYear(DateTime date)
             {
                 TimeSpan difference = date - new DateTime(date.Year, 1, 1);
                 return difference.Days + 1;
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