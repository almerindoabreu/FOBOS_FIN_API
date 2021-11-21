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
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeRepository categoryTypeRepository;

        public CategoryTypeController(ICategoryTypeRepository categoryTypeRepository)
        {
            this.categoryTypeRepository = categoryTypeRepository;
        }

        [HttpGet]
        [Route("categoryTypes")]
        public async Task<IList<CategoryType>> GetCategoryTypesActivated()
        {
            IList<CategoryType> categoryTypes = await categoryTypeRepository.GetCategoryTypesActivated();
            return categoryTypes;
        }

        [HttpGet]
        [Route("categoryTypes/ShowAll")]
        public async Task<IList<CategoryType>> GetCategoryTypes()
        {
            IList<CategoryType> categoryTypes = await categoryTypeRepository.GetCategoryTypes();
            return categoryTypes;
        }

        [HttpGet]
        [Route("categoryTypes/{id}")]
        public async Task<CategoryType> GetCategoryType([FromRoute] int id)
        {
            CategoryType categoryType = await categoryTypeRepository.GetCategoryType(id);
            return categoryType;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveCategoryType([FromBody] CategoryType categoryType)
        {
            Operators Op = ((categoryType.id == 0 || categoryType.id == null) ? Operators.Insert : Operators.Alter);
            Message message = new Message("Tipo de Categoria", Op);

            try
            {
                await categoryTypeRepository.SaveCategoryType(categoryType);
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
        public async Task<IActionResult> DeleteCategoryType([FromRoute] int id)
        {
            Message message = new Message("Tipo de Categoria", Operators.Delete);

            try
            {
                CategoryType categoryType = await categoryTypeRepository.GetCategoryType(id);
                categoryType.ativo = false;

                await categoryTypeRepository.SaveCategoryType(categoryType);

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
