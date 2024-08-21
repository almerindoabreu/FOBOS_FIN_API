using FOBOS_API.Models;
using FOBOS_API.Repositories;
using FOBOS_API.Repositories.Interfaces;
using FOBOS_API.Services;
using FOBOS_API.Services.OverviewGenerator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOBOS_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {

        private OverviewGenerator overView;
        private readonly IStatementRepository statementRepository;
        private readonly ICategoryRepository categoryRepository;
        public TelegramController(IStatementRepository statementRepository, ICategoryRepository categoryRepository)
        {
            overView = new OverviewGenerator();

            this.statementRepository = statementRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("telegram/categorizationStatement")]
        public async Task categorizationStatement()
        {
            TelegramSender telegramSender = new TelegramSender();

            IList<Statement> statements = await statementRepository.GetStatementsActivated();
            IList<Category> categories = await categoryRepository.GetCategoriesActivated();

            List<string> categoriesSelected = new List<string>();

            for(int i = 0; i < 10; i++)
            {
                categoriesSelected.Add("#" + categories[i].id + "# " + categories[i].CategoryType.name + " > " + categories[i].name);
            }

            for (int i = 0; i < 2; i++)
            {
                await telegramSender.sendMenssage(categoriesSelected, statements[i].name + " - Valor: " + statements[i].value);  
            }
        }

        [HttpGet]
        [Route("telegram/readMessages")]
        public async Task readMessages()
        {
            TelegramSender telegramSender = new TelegramSender();
            await telegramSender.readMessages();
            //await telegramSender.sendMenssage("daily_overview", "Resumo da Semana...");
        }

        [HttpGet]
        [Route("telegram/dailyOverview")]
        public async Task dailyOverview()
        {
            TelegramSender telegramSender = new TelegramSender();
            overView.DailyOverview();

            //await telegramSender.sendMenssage("daily_overview", "Resumo da Semana...");
        }

        [HttpGet]
        [Route("telegram/invoiceOfTheMonth")]
        public async Task invoiceOfTheMonth()
        {
            TelegramSender telegramSender = new TelegramSender();
            await overView.InvoiceOfTheMonth();

            //await telegramSender.sendMenssage("invoice_of_the_month", "Faturas Pagas por Mês");
        }

        [HttpGet]
        [Route("telegram/weeklyOverview")]
        public async Task weeklyOverview()
        {
            TelegramSender telegramSender = new TelegramSender();
            await overView.WeeklyOverview();

            //await telegramSender.sendMenssage("weekly", "Gastos por Semana");
        }
    }
}
