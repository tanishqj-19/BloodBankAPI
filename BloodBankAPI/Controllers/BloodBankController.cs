using BloodBankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController : ControllerBase
    {
        public static List<BloodBankEntry> bloodEntries = new List<BloodBankEntry>
            {
                new BloodBankEntry { Id = 1, DonorName = "John Doe", Age = 25, BloodType = "O+", ContactInfo = "1234567890", Quantity = 1.5m, CollectionDate = new DateTime(2024, 3, 15), ExpirationDate = new DateTime(2024, 9, 15), Status = "Available" },
                new BloodBankEntry { Id = 2, DonorName = "Alice Smith", Age = 30, BloodType = "A-", ContactInfo = "9876543210", Quantity = 1.2m, CollectionDate = new DateTime(2024, 6, 1), ExpirationDate = new DateTime(2024, 12, 1), Status = "Available" },
                new BloodBankEntry { Id = 3, DonorName = "Bob Johnson", Age = 40, BloodType = "B+", ContactInfo = "5555555555", Quantity = 1.0m, CollectionDate = new DateTime(2024, 5, 10), ExpirationDate = new DateTime(2024, 11, 10), Status = "Available" },
                new BloodBankEntry { Id = 4, DonorName = "Clara Lee", Age = 28, BloodType = "AB-", ContactInfo = "4444444444", Quantity = 1.3m, CollectionDate = new DateTime(2024, 2, 20), ExpirationDate = new DateTime(2024, 8, 20), Status = "Available" },
                new BloodBankEntry { Id = 5, DonorName = "Ethan Hunt", Age = 35, BloodType = "O-", ContactInfo = "3333333333", Quantity = 2.0m, CollectionDate = new DateTime(2024, 4, 18), ExpirationDate = new DateTime(2024, 10, 18), Status = "Requested" },
                new BloodBankEntry { Id = 6, DonorName = "Fiona Green", Age = 22, BloodType = "A+", ContactInfo = "2222222222", Quantity = 1.5m, CollectionDate = new DateTime(2024, 1, 5), ExpirationDate = new DateTime(2024, 7, 5), Status = "Available" },
                new BloodBankEntry { Id = 7, DonorName = "George Hill", Age = 29, BloodType = "B-", ContactInfo = "1111111111", Quantity = 1.8m, CollectionDate = new DateTime(2024, 7, 10), ExpirationDate = new DateTime(2024, 12, 10), Status = "Available" },
                new BloodBankEntry { Id = 8, DonorName = "Hannah Rose", Age = 38, BloodType = "AB+", ContactInfo = "9999999999", Quantity = 1.1m, CollectionDate = new DateTime(2024, 8, 25), ExpirationDate = new DateTime(2025, 2, 25), Status = "Available" },
                new BloodBankEntry { Id = 9, DonorName = "Ian White", Age = 32, BloodType = "O+", ContactInfo = "8888888888", Quantity = 1.7m, CollectionDate = new DateTime(2024, 9, 5), ExpirationDate = new DateTime(2025, 3, 5), Status = "Available" },
                new BloodBankEntry { Id = 10, DonorName = "Jack Black", Age = 45, BloodType = "A-", ContactInfo = "7777777777", Quantity = 1.4m, CollectionDate = new DateTime(2024, 1, 10), ExpirationDate = new DateTime(2024, 5, 10), Status = "Expired" }
            };
        private static Regex BloodTypeRegex = new Regex(@"^(A|B|AB|O)[+-]$");





        // Getting All Entries
        [HttpGet]
        public ActionResult<IEnumerable<BloodBankEntry>> GetAllEntries()
        {
            if (bloodEntries == null || !bloodEntries.Any())
            {
                return NotFound("No blood entries found.");
            }
            return Ok(bloodEntries);
        }

        // Get Blood Entry By Id

        [HttpGet("{Id}")]
        public ActionResult<BloodBankEntry> GetEntryById(int Id)
        {
            var queryBloodEntry = bloodEntries.Find(currEntry => currEntry.Id == Id);

            if (queryBloodEntry == null)
            { return NotFound($"Entry with ID {Id} not found."); }
            return Ok(queryBloodEntry);
        }

        // Creating a new Entry
        [HttpPost]
        public IActionResult AddBloodEntryById(BloodBankEntry newBloodEntry)
        {
            if (newBloodEntry == null )
            {
                return BadRequest("Entry should not be null.");
            }

            if (!BloodTypeRegex.IsMatch(newBloodEntry.BloodType))
            {
                return BadRequest("Invalid blood type. Valid types are A+, A-, B+, B-, AB+, AB-, O+, O-.");
            }
            if (newBloodEntry.Age < 18 || newBloodEntry.Age > 65)
            {
                return BadRequest("Minimum Age should be 18 and maximum age should be 65 for blood donation....");
            }

            if (newBloodEntry.ExpirationDate <= newBloodEntry.CollectionDate)
            {
                return BadRequest("Expiration Date should be greater than collection Date....");
            }
            if (string.IsNullOrEmpty(newBloodEntry.DonorName))
            {
                return BadRequest("Name should not be empty");
            }

            if (newBloodEntry.Quantity <= 0)
            {
                return BadRequest("Quanity should be Greater than 0");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            newBloodEntry.Id = bloodEntries.Count + 1;
            bloodEntries.Add(newBloodEntry);

            return CreatedAtAction(nameof(GetEntryById), new { Id = newBloodEntry.Id }, newBloodEntry);


        }

        // Updating an Entry

        [HttpPut("{Id}")]
        public IActionResult UpdateBloodEntryById(int Id,BloodBankEntry entryToBeUpdated)
        {
            var queryEntry = bloodEntries.Find(entry => entry.Id == Id);
            if (queryEntry == null)
            {
                return NotFound($"Entry with ID {Id} not found.");
            }
            

            if (!BloodTypeRegex.IsMatch(entryToBeUpdated.BloodType))
            {
                return BadRequest("Invalid blood type. Valid types are A+, A-, B+, B-, AB+, AB-, O+, O-.");
            }
            queryEntry.DonorName = entryToBeUpdated.DonorName;
            queryEntry.Quantity = entryToBeUpdated.Quantity;
            queryEntry.Age = entryToBeUpdated.Age;
            queryEntry.ExpirationDate = entryToBeUpdated.ExpirationDate;
            queryEntry.CollectionDate = entryToBeUpdated.CollectionDate;
            queryEntry.Status = entryToBeUpdated.Status;
            queryEntry.BloodType = entryToBeUpdated.BloodType;
           
            return CreatedAtAction(nameof(GetEntryById), new {Id =  queryEntry.Id}, queryEntry);

        }

        // Delete a Blood Entry......
        [HttpDelete("{Id}")]
        public IActionResult DeleteBloodEntryById(int Id)
        {
            var entryToBeDeleted = bloodEntries.Find(entry => entry.Id == Id);

            if(entryToBeDeleted == null)
            {
                return NotFound($"No Entry Found with id {Id}");
            }
            bloodEntries.Remove(entryToBeDeleted);

            return Ok($"Blood Entry with id {Id} is successfully deleted.....");
        }

        // Pagination

        [HttpGet("page")]
        public ActionResult<IEnumerable<BloodBankEntry>> BloodEntryPagination(int pageNumber, int pageSize)
        {
            if(pageSize <= 0 || pageNumber <= 0)
            {
                return BadRequest("Size and page should be greater than 0...");
            }
            var paginatedEntries = bloodEntries.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return Ok(paginatedEntries);
        }

        // Search Functionality By Blood
        [HttpGet("searchByBlood")]
        public ActionResult<IEnumerable<BloodBankEntry>> SearchByBloodType(string bloodType)
        {
            var filteredBloodEntries = bloodEntries.AsQueryable();
            if (string.IsNullOrEmpty(bloodType))
            {
                return NotFound();
            }

            if (!BloodTypeRegex.IsMatch(bloodType))
            {
                return BadRequest("Invalid blood type. Valid types are A+, A-, B+, B-, AB+, AB-, O+, O-.");
            }
            filteredBloodEntries = filteredBloodEntries.Where(entry => entry.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase));

            if (filteredBloodEntries == null)
            {
                return Content($"No entries exist with blood type {bloodType}");
            }


            return Ok(filteredBloodEntries.ToList());
            

        }
        [HttpGet("searchByStatus")]
        // Search Functionality By status
        public ActionResult<IEnumerable<BloodBankEntry>> SearchByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return NotFound();
            }
            var filteredBloodEntries = bloodEntries.AsQueryable();

            filteredBloodEntries = filteredBloodEntries.Where(entry => entry.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            
            if(filteredBloodEntries == null)
            {
                return Content($"No entries exist with status {status}");   
            }

            return Ok(filteredBloodEntries.ToList());
        }

        // Search Functionality By Donar Name
        [HttpGet("searchByDonorName")]
        public ActionResult<IEnumerable<BloodBankEntry>> SearchByDonarName(string donorName)
        {
            if (string.IsNullOrEmpty(donorName))
            {
                return NotFound();
            }

            var filteredBloodEntries = bloodEntries.AsQueryable();


            filteredBloodEntries = filteredBloodEntries.Where(entry => entry.DonorName.StartsWith(donorName, StringComparison.OrdinalIgnoreCase));

            if(filteredBloodEntries == null)
            {
                return Content($"No entries exist with donar name ${donorName}" );
            }

            return Ok(filteredBloodEntries.ToList());
            
        }

        // =============================== Bonus Task ===============================


        // sort
        [HttpGet("sort")]
        public ActionResult<IEnumerable<BloodBankEntry>> sortBloodEntry(string? by = null, string order = "asc")
        {
            var sortedList = bloodEntries.AsQueryable();
            if (by != null && by.Equals("bloodtype", StringComparison.OrdinalIgnoreCase))
            {
                
                if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    sortedList = sortedList.OrderByDescending(entry => entry.BloodType);
                }
                else
                {
                    sortedList = sortedList.OrderBy(entry => entry.BloodType);
                }

                return Ok(sortedList);

            } else if (by != null && by.Equals("CollectionDate", StringComparison.OrdinalIgnoreCase))
            {
                if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    sortedList = sortedList.OrderByDescending(entry => entry.CollectionDate);
                }
                else
                {
                    sortedList = sortedList.OrderBy(entry => entry.CollectionDate);
                }

                return Ok(sortedList);
            }


            return BadRequest("Not a valid type of sorting...");
        }

        // Filter Functionality......

        [HttpGet("filter")]

        public ActionResult<IEnumerable<BloodBankEntry>> FilterBloodEntry(string? bloodType = null, string? status = null, string? donorName = null)
        {
            
            var filteredBloodEntries = bloodEntries.AsQueryable();

            
            if (!string.IsNullOrEmpty(bloodType))
            {
                if (!BloodTypeRegex.IsMatch(bloodType))
                {
                    return BadRequest("Invalid blood type. Valid types are A+, A-, B+, B-, AB+, AB-, O+, O-.");
                }

                filteredBloodEntries = filteredBloodEntries.Where(entry =>
                    entry.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(status))
            {
                filteredBloodEntries = filteredBloodEntries.Where(entry =>
                    entry.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(donorName))
            {
                filteredBloodEntries = filteredBloodEntries.Where(entry =>
                    entry.DonorName.StartsWith(donorName, StringComparison.OrdinalIgnoreCase));
            }

            
            var result = filteredBloodEntries.ToList();
            if (!result.Any())
            {
                return NotFound("No matching blood entries found.");
            }

            return Ok(result);
        }

    }
}
