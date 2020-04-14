using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataWebApiIssue2126Repro01.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ODataWebApiIssue2126Repro01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private static readonly Collection<Customer> _customers = new Collection<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "Customer 1",
                DynamicProperties = new Dictionary<string, object>
                {
                    { "StreetAddress", new Address { AddressLine = "AL1", City = "C1" } },
                    { "OtherAddresses", new Collection<Address>
                        {
                            new Address { AddressLine = "AL2", City = "C2" },
                            new Address { AddressLine = "AL3", City = "C3" }
                        }
                    }
                }
            }
        };

        [EnableQuery]
        public IQueryable<Customer> Get()
        {
            return _customers.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Customer> Get([FromODataUri]int key)
        {
            var customer = _customers.SingleOrDefault(d => d.Id.Equals(key));

            return SingleResult.Create(new[] { customer }.AsQueryable());
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody]Delta<Customer> delta)
        {
            var customer = _customers.SingleOrDefault(d => d.Id.Equals(key));

            if (customer != null)
                delta.Patch(customer);

            await Task.Run(() =>
            {
                // ...            
            });

            return Ok();
        }
    }
}
