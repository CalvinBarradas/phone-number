using Microsoft.AspNetCore.Mvc;
using PhoneNumberService.Helpers;

namespace PhoneNumberService.Controllers
{
    [Route("api/format")]
    [ApiController]
    public class FormatPhoneNumberController : Controller
    {
        [HttpPost]
        [Consumes("text/plain")]
        public string Format([FromBody] string phoneNumber)
        {
            if (!PhoneNumberHelper.IsUKExtension(phoneNumber))
                return phoneNumber;

            return PhoneNumberHelper.FormatNumber(PhoneNumberHelper.RemoveUKExtension(phoneNumber));
        }
    }
}