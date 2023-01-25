
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
            return await _context.Customers.ToListAsync();
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


        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customers customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerToUpdate = await _context.Customers.FindAsync(id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }
            _context.Entry(customerToUpdate).CurrentValues.SetValues(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customers>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.IsDeleted = true;
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

