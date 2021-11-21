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
    public class BankController : ControllerBase
    {
        private readonly IBankRepository bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        [HttpGet]
        [Route("banks")]
        public async Task<IList<Bank>> GetBanksActivated()
        {
            IList<Bank> banks = await bankRepository.GetBanksActivated();
            return banks;
        }

        [HttpGet]
        [Route("banks/{id}")]
        public async Task<Bank> GetBank([FromRoute] int id)
        {
            Bank bank = await bankRepository.GetBank(id);
            return bank;
        }

        [HttpGet]
        [Route("banks/showAll")]
        public async Task<IList<Bank>> GetBanks()
        {
            IList<Bank> banks = await bankRepository.GetBanks();
            return banks;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveBank([FromBody] Bank bank)
        {
            Operators Op = ((bank.id == 0 || bank.id == null) ? Operators.Insert : Operators.Alter);
            Message message = new Message("Banco", Op);

            try
            {
                await bankRepository.SaveBank(bank);
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
        public async Task<IActionResult> DeleteBank([FromRoute] int id)
        {
            Operators Op = Operators.Delete;
            Message message = new Message("Banco", Op);

            try
            {
                Bank bank = await bankRepository.GetBank(id);
                bank.ativo = false;

                await bankRepository.SaveBank(bank);
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
