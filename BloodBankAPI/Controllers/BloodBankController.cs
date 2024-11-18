using BloodBankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController : ControllerBase
    {
        public static List<BloodBankEntry> bloodEntries = new List<BloodBankEntry>{};
        [HttpGet]

        // Getting all Blood Bank Entries..............
        public ActionResult<IEnumerable<BloodBankEntry>> getAllEntries()
        {
            if(bloodEntries == null || !bloodEntries.Any())
            {
                return NoContent();
            }
            return Ok(bloodEntries);
        }
    }
}
