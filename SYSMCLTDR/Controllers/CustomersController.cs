
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SYSMCLTDR.Models;

namespace SYSMCLTDR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
            
        }
       
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.Where(c => c.IsDeleted == false).ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customers>> CreateCustomer(Customers customer)
        {
            if (CustomerNumberExists(customer.CustomerNumber)) return BadRequest("CustomerNumber Exist alrady");
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpGet("fullinfo/{CustomerNumber}")]
        public async Task<IActionResult> GetFullCustomerInfo(string CustomerNumber)
        {
              var customer = await _context.Customers
        .FirstOrDefaultAsync(c => c.CustomerNumber == CustomerNumber);

    if (customer == null)
    {
        return NotFound();
    }

    var addresses = await _context.Addresses
        .Where(a => a.CustomerId == customer.Id)
        .ToListAsync();

    var contacts = await _context.Contacts
        .Where(c => c.CustomerId == customer.Id)
        .ToListAsync();

    return Ok(new { customer, addresses, contacts });
        }


        // Patch: api/Customers/5
        [HttpPatch("{CustomerNumber}")]
        public async Task<IActionResult> UpdateCustomer(string CustomerNumber,[FromBody] UpdateCustomerRequest requestBody)
        {
            var customerToUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == CustomerNumber);
            if (customerToUpdate == null)
            {
                return NotFound("CustomerNumber Not Exist");
            }
         
            customerToUpdate.Name = requestBody.Name;
            _context.Customers.Update(customerToUpdate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }



        // DELETE: api/Customers/5
        [HttpDelete("{CustomerNumber}")]
        public async Task<ActionResult<Customers>> DeleteCustomer(string CustomerNumber)
        {
            var customerToUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == CustomerNumber);
            if (customerToUpdate == null)
            {
                return NotFound("CustomerNumber Not Exist");
            }

            customerToUpdate.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        private bool CustomerNumberExists(string CustomerNumber)
        {
            return _context.Customers.Any(e => e.CustomerNumber == CustomerNumber);
        }
        public class UpdateCustomerRequest
        {
            public string Name { get; set; }
        }
        //// POST: api/Customers/5/contacts
        //[HttpPost]
        //public async Task<ActionResult<Contacts>> AddContactToCustomer(int customerId, [FromBody] Contacts contact)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var customer = await _context.Customers.FindAsync(customerId);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    contact.CustomerId = customerId;
        //    _context.Contacts.Add(contact);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction("AddContactToCustomer", new { contactId = contact.Id, customerId = customer.Id }, contact);
        //}

    }
    }

