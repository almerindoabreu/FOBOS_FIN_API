using Dapper;
using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories;
using FOBOS_API.Repositories.Interfaces;
using FOBOS_API.Utils.Message;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IList<Category>> GetCategoriesActivated()
        {
            IList<Category> categories = await categoryRepository.GetCategoriesActivated();
            return categories;
        }

        [HttpGet]
        [Route("categories/showAll")]
        public async Task<IList<Category>> GetCategories()
        {
            IList<Category> categories = await categoryRepository.GetCategories();
            return categories;
        }

        [HttpGet]
        [Route("categories/{id}")]
        public async Task<Category> GetCategory([FromRoute] int id)
        {
            Category category = await categoryRepository.GetCategory(id);
            return category;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveCategory([FromBody] Category category)
        {
            Operators Op = ((category.id == 0 || category.id == null) ? Operators.Insert : Operators.Alter);
            Message message = new Message("Categoria", Op);

            try
            {
                await categoryRepository.SaveCategory(category);
                message.setMessageSuccess();

                return Ok(new
                {
                    feedback = message.getFeedback(),
                    show = message.getShow(),
                    text = message.getText()
                });
            }
            catch (Exception ex)
            {
                message.setMessageError(ex);
                return NotFound(message);
            }
        }

        [HttpPut]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            Message message = new Message("Categoria", Operators.Delete);

            try
            {
                Category category = await categoryRepository.GetCategory(id);
                category.ativo = false;

                await categoryRepository.SaveCategory(category);

                message.setMessageSuccess();

                return Ok(new
                {
                    feedback = message.getFeedback(),
                    show = message.getShow(),
                    text = message.getText()
                });
            }
            catch (Exception ex)
            {
                message.setMessageError(ex);
                return NotFound(message);
            }
        }
    }
}
