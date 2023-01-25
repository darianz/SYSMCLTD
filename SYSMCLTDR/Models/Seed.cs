using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace SYSMCLTDR.Models
{
    public class Seed
    {
        public static async Task SeedCustomers(AppDbContext context)
        {
            try
            {

                // reset the Contacts Table
                await context.Contacts.ExecuteDeleteAsync();
                await context.Addresses.ExecuteDeleteAsync();
                await context.Customers.ExecuteDeleteAsync();
                await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Customers', RESEED, 0);");
                await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Addresses', RESEED, 0);");
                await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Contacts', RESEED, 0);");

                var customersData = await File.ReadAllTextAsync("./Seeds/CustomersSeed.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var customers = JsonSerializer.Deserialize<List<Customers>>(customersData);

                foreach (var customer in customers)
                {
                    context.Customers.Add(customer);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static async Task SeedAddresses(AppDbContext context)
        {
            try
            {

                var addressesData = await File.ReadAllTextAsync("./Seeds/AddressesSeed.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var addresses = JsonSerializer.Deserialize<List<Addresses>>(addressesData);

                foreach (var address in addresses)
                {
                    context.Addresses.Add(address);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static async Task SeedContacts(AppDbContext context)
        {
            try
            {

             

                var contactsData = await File.ReadAllTextAsync("./Seeds/ContactsSeed.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var contacts = JsonSerializer.Deserialize<List<Contacts>>(contactsData);

                foreach (var contact in contacts)
                {
                    context.Contacts.Add(contact);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
