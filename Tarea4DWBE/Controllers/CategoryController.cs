using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tarea3DWBE.DTO;
using Tarea4DWBE.Models;

namespace Tarea3DWBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly NORTHWNDContext DBContext;

        public CategoryController(NORTHWNDContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            var List = await DBContext.Categories.Select(
                s => new CategoryDTO
                {
                    CategoryId = s.CategoryId, 
                    CategoryName = s.CategoryName, 
                    Description = s.Description, 
                    Picture = s.Picture,                   
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

        [HttpGet("GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int Id)
        {
            CategoryDTO Category = await DBContext.Categories.Select(
                    s => new CategoryDTO
                    {
                        CategoryId = s.CategoryId,
                        CategoryName = s.CategoryName,
                        Description = s.Description,
                        Picture = s.Picture,
                    })
                .FirstOrDefaultAsync(s => s.CategoryId == Id);

            if (Category == null)
            {
                return NotFound();
            }
            else
            {
                return Category;
            }
        }

        [HttpPost("InsertCategory")]
        public async Task<HttpStatusCode> InsertCustomer(CategoryDTO Category)
        {
            var newCategory = new Category()
            {
                CategoryId = Category.CategoryId,
                CategoryName = Category.CategoryName,
                Description = Category.Description,
                Picture = Category.Picture,
            };

            DBContext.Categories.Add(newCategory);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateCategory")]
        public async Task<HttpStatusCode> UpdateCustomer(CategoryDTO Category)
        {
            var CategoryToUpdate = await DBContext.Categories.FirstOrDefaultAsync(s => s.CategoryId
            == Category.CategoryId);

            CategoryToUpdate.CategoryId = Category.CategoryId;
            CategoryToUpdate.CategoryName = Category.CategoryName;
            CategoryToUpdate.Description = Category.Description;
            CategoryToUpdate.Picture = Category.Picture;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteCategory/{Id}")]
        public async Task<HttpStatusCode> DeleteCategory(int Id)
        {
            var CategoryToDelete = new Category()
            {
                CategoryId = Id
            };
            DBContext.Categories.Attach(CategoryToDelete);
            DBContext.Categories.Remove(CategoryToDelete);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
