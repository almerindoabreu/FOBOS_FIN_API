using ActiveUp.Net.Mail;
using FOBOS_API.Models;
using FOBOS_API.Repositories.Interfaces;
using FOBOS_API.Services;
using FOBOS_API.Services.EmailReader;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOBOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailReaderController : ControllerBase
    {

        private EmailReader emailReader;
        private readonly IStatementRepository statementRepository;
        private readonly ICardRepository cardRepository;
        public EmailReaderController(IStatementRepository statementReposiory, ICardRepository cardRepository)
        {
            this.emailReader = new EmailReader(
                "imap.gmail.com",
                993,
                true,
                "controle.gasto.alcl@gmail.com",
                "zylumrqitygtjwgg");
            this.statementRepository = statementReposiory;
            this.cardRepository = cardRepository;
        }

        [HttpGet]
        [Route("email/helloWord")]
        public async Task helloWord()
        {
            TelegramSender telegramSender = new TelegramSender();

            await telegramSender.sendMenssage("", "");
        }
            
        [HttpGet]
        [Route("email/showAll")]
        public async Task<string> VerifyAllEmail()
        {
            try { 
            Card cardInter = await cardRepository.GetCardByBankName("Inter");
            Card cardNubank = await cardRepository.GetCardByBankName("Nubank");
            emailReader.DownloadAttachment();
            IList<Statement> statements = emailReader.ReadAttachment((int)cardInter.id, (int)cardNubank.id);
            //await statementRepository.BulkStatement(statements);

            int i = 0;
            foreach (Statement statement in statements)
            {
                bool isSaved = await statementRepository.SaveStatement(statement);
                i = (isSaved ? i + 1 : i);
            }

            return "Foram registrados " + i + " novas movimentações do extrato.";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return "Error: " + ex;
            }
        }
    }
}
