using BloodBankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Net.Cache;

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


        // Getting All Entries
        [HttpGet]
        public ActionResult<IEnumerable<BloodBankEntry>> GetAllEntries()
        {
            if (bloodEntries == null || !bloodEntries.Any())
            {
                return NoContent();
            }
            return Ok(bloodEntries);
        }

        // Get Blood Entry By Id

        [HttpGet("{Id}")]
        public ActionResult<BloodBankEntry> GetEntryById(int Id)
        {
            var queryBloodEntry = bloodEntries.Find(currEntry => currEntry.Id == Id);

            if (queryBloodEntry == null)
            { return NoContent(); }
            return Ok(queryBloodEntry);
        }

        // Creating a new Entry
        [HttpPost]
        public IActionResult AddBloodEntryById(BloodBankEntry newBloodEntry)
        {
            if (newBloodEntry == null)
            {
                return BadRequest("Entry should not be null.");
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
            if (string.IsNullOrEmpty(newBloodEntry.BloodType))
            {
                return BadRequest("Blood Type Should not be Null");
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
                return NoContent();
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
    }
}
