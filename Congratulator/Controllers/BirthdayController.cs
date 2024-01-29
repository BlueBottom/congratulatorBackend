using Congratulator.Contracts.Contracts;
using Congratulator.Core.Abstractions;
using Congratulator.Domain.Birthday;
using Microsoft.AspNetCore.Mvc;

namespace Congratulator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BirthdaysController : ControllerBase
    {
        private readonly IBirthdayService _birthdayService;

        public BirthdaysController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BirthdayResponse>>> GetBirthdays() 
        {
            var birthdays = await _birthdayService.GetAllBirthdays();

            var response = birthdays.Select(b => new BirthdayResponse(b.Id, b.Name, b.Description, b.Date));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBirthday([FromBody] BirthdayRequest request)
        {

            var birthdayId = await _birthdayService.CreateBirthday(request);

            return Ok(birthdayId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBirthday(Guid id, [FromBody] BirthdayRequest request)
        {
            var birthdayId = await _birthdayService.UpdateBirthday(id, request.Name, request.Description, request.Date);

            return Ok(birthdayId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBirthday(Guid id)
        {
            return Ok(await _birthdayService.DeleteBirthday(id));
        }
    }
}
