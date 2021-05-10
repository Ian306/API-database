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
    public class EmployeeController : ControllerBase
    {
        private readonly NORTHWNDContext DBContext;

        public EmployeeController(NORTHWNDContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetEmployees")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            var List = await DBContext.Employees.Select(
                s => new EmployeeDTO
                {
                    EmployeeId = s.EmployeeId, 
                    LastName = s.LastName, 
                    FirstName = s.FirstName, 
                    Title = s.Title, 
                    TitleOfCourtesy = s.TitleOfCourtesy, 
                    Address = s.Address, 
                    City = s.City,
                    Region = s.Region, 
                    PostalCode = s.PostalCode, 
                    Country = s.Country,
                    HomePhone = s.HomePhone,
                    Notes = s.Notes,
                    ReportsTo = s.ReportsTo,
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

        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int Id)
        {
            EmployeeDTO Employee = await DBContext.Employees.Select(
                    s => new EmployeeDTO
                    {
                        EmployeeId = s.EmployeeId,
                        LastName = s.LastName,
                        FirstName = s.FirstName,
                        Title = s.Title,
                        TitleOfCourtesy = s.TitleOfCourtesy,
                        Address = s.Address,
                        City = s.City,
                        Region = s.Region,
                        PostalCode = s.PostalCode,
                        Country = s.Country,
                        HomePhone = s.HomePhone,
                        Notes = s.Notes,
                        ReportsTo = s.ReportsTo,
                    })
                .FirstOrDefaultAsync(s => s.EmployeeId == Id);

            if (Employee == null)
            {
                return NotFound();
            }
            else
            {
                return Employee;
            }
        }

        [HttpPost("InsertEmployee")]
        public async Task<HttpStatusCode> InsertEmployee(EmployeeDTO Employee)
        {
            var newEmployee = new Employee()
            {
                EmployeeId = Employee.EmployeeId,
                LastName = Employee.LastName,
                FirstName = Employee.FirstName,
                Title = Employee.Title,
                TitleOfCourtesy = Employee.TitleOfCourtesy,
                Address = Employee.Address,
                City = Employee.City,
                Region = Employee.Region,
                PostalCode = Employee.PostalCode,
                Country = Employee.Country,
                HomePhone = Employee.HomePhone,
                Notes = Employee.Notes,
                ReportsTo = Employee.ReportsTo,
            };

            DBContext.Employees.Add(newEmployee);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmployee")]
        public async Task<HttpStatusCode> UpdateEmployee(EmployeeDTO Employee)
        {
            var EmployeeToUpdate = await DBContext.Employees.FirstOrDefaultAsync(s => s.EmployeeId
            == Employee.EmployeeId);

            EmployeeToUpdate.EmployeeId = Employee.EmployeeId;
            EmployeeToUpdate.LastName = Employee.LastName;
            EmployeeToUpdate.FirstName = Employee.FirstName;
            EmployeeToUpdate.Title = Employee.Title;
            EmployeeToUpdate.TitleOfCourtesy = Employee.TitleOfCourtesy;
            EmployeeToUpdate.Address = Employee.Address;
            EmployeeToUpdate.City = Employee.City;
            EmployeeToUpdate.Region = Employee.Region;
            EmployeeToUpdate.PostalCode = Employee.PostalCode;
            EmployeeToUpdate.Country = Employee.Country;
            EmployeeToUpdate.HomePhone = Employee.HomePhone;
            EmployeeToUpdate.Notes = Employee.Notes;
            EmployeeToUpdate.ReportsTo = Employee.ReportsTo;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteEmployee/{Id}")]
        public async Task<HttpStatusCode> DeleteEmployee(int Id)
        {
            var EmployeeToDelete = new Employee()
            {
                EmployeeId = Id
            };
            DBContext.Employees.Attach(EmployeeToDelete);
            DBContext.Employees.Remove(EmployeeToDelete);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
