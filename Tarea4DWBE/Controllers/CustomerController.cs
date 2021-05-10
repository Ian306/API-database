using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tarea4DWBE.DTO;
using Tarea4DWBE.Models;

namespace Tarea4DWBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly NORTHWNDContext DBContext;

        public CustomerController(NORTHWNDContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            var List = await DBContext.Customers.Select(
                s => new CustomerDTO
                {
                    CustomerId = s.CustomerId,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    Address = s.Address, 
                    City = s.City, 
                    Region = s.Region, 
                    PostalCode = s.PostalCode, 
                    Country = s.Country, 
                    Phone = s.Phone, 
                    Fax = s.Fax,
                    //Orders = s.Orders, 
                    //CustomerCustomerDemos = s.CustomerCustomerDemos, 

                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetCustomerById")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(string Id)
        {
            CustomerDTO Customer = await DBContext.Customers.Select(
                    s => new CustomerDTO
                    {
                        CustomerId = s.CustomerId,
                        CompanyName = s.CompanyName,
                        ContactName = s.ContactName,
                        ContactTitle = s.ContactTitle,
                        Address = s.Address,
                        City = s.City,
                        Region = s.Region,
                        PostalCode = s.PostalCode,
                        Country = s.Country,
                        Phone = s.Phone,
                        Fax = s.Fax,
                        //Orders = s.Orders,
                        //CustomerCustomerDemos = s.CustomerCustomerDemos,
                    })
                .FirstOrDefaultAsync(s => s.CustomerId == Id);

            if (Customer == null)
            {
                return NotFound();
            }
            else
            {
                return Customer;
            }
        }

        [HttpPost("InsertCustomer")]
        public async Task<HttpStatusCode> InsertCustomer(CustomerDTO Customer)
        {
            var newCustomer = new Customer()
            {
                CustomerId = Customer.CustomerId,
                CompanyName = Customer.CompanyName,
                ContactName = Customer.ContactName,
                ContactTitle = Customer.ContactTitle,
                Address = Customer.Address,
                City = Customer.City,
                Region = Customer.Region,
                PostalCode = Customer.PostalCode,
                Country = Customer.Country,
                Phone = Customer.Phone,
                Fax = Customer.Fax,
                //Orders = Customer.Orders,
                //CustomerCustomerDemos = Customer.CustomerCustomerDemos,
            };

            DBContext.Customers.Add(newCustomer);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateCustomer")]
        public async Task<HttpStatusCode> UpdateCustomer(CustomerDTO Customer)
        {
            var CustomerToUpdate = await DBContext.Customers.FirstOrDefaultAsync(s => s.CustomerId
            == Customer.CustomerId);

            CustomerToUpdate.CustomerId = Customer.CustomerId;
            CustomerToUpdate.CompanyName = Customer.CompanyName;
            CustomerToUpdate.ContactName = Customer.ContactName;
            CustomerToUpdate.ContactTitle = Customer.ContactTitle;
            CustomerToUpdate.Address = Customer.Address;
            CustomerToUpdate.City = Customer.City;
            CustomerToUpdate.Region = Customer.Region;
            CustomerToUpdate.PostalCode = Customer.PostalCode;
            CustomerToUpdate.Country = Customer.Country;
            CustomerToUpdate.Phone = Customer.Phone;
            CustomerToUpdate.Fax = Customer.Fax;
            //CustomerToUpdate.Orders = Customer.Orders;
            //CustomerToUpdate.CustomerCustomerDemos = Customer.CustomerCustomerDemos;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteCustomer/{Id}")]
        public async Task<HttpStatusCode> DeleteCustomer(string Id)
        {
            var CustomerToDelete = new Customer()
            {
                CustomerId = Id
            };
            DBContext.Customers.Attach(CustomerToDelete);
            DBContext.Customers.Remove(CustomerToDelete);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
