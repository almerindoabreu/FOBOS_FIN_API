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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IList<User>> GetUsersActivated()
        {
            IList<User> users = await userRepository.GetUsersActivated();
            return users;
        }

        [HttpGet]
        [Route("users/ShowAll")]
        public async Task<IList<User>> GetUsers()
        {
            IList<User> users = await userRepository.GetUsers();
            return users;
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<User> GetUser([FromRoute] int id)
        {
            User user = await userRepository.GetUser(id);
            return user;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveUser([FromBody] User user)
        {
            Operators Op = ((user.id == 0 || user.id == null) ? Operators.Insert : Operators.Alter);
            Message message = new Message("Usuário", Op);

            try
            {
                await userRepository.SaveUser(user);
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
        public async Task<IActionResult> DeleteCard([FromRoute] int id)
        {
            Message message = new Message("Usuário", Operators.Delete);

            try
            {
                User user = await userRepository.GetUser(id);
                user.ativo = false;

                await userRepository.SaveUser(user);

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
