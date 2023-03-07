using FOBOS_API.Services;
using FOBOS_API.Services.OverviewGenerator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FOBOS_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {

        private OverviewGenerator overView;
        public TelegramController()
        {
            overView = new OverviewGenerator();
        }

        [HttpGet]
        [Route("telegram/dailyOverview")]
        public async Task dailyOverview()
        {
            TelegramSender telegramSender = new TelegramSender();
            overView.DailyOverview();

            await telegramSender.sendMenssage("daily_overview", "Resumo da Semana...");
        }

        [HttpGet]
        [Route("telegram/invoiceOfTheMonth")]
        public async Task invoiceOfTheMonth()
        {
            TelegramSender telegramSender = new TelegramSender();
            await overView.InvoiceOfTheMonth();

            await telegramSender.sendMenssage("invoice_of_the_month", "Faturas Pagas por Mês");
        }

        [HttpGet]
        [Route("telegram/weeklyOverview")]
        public async Task weeklyOverview()
        {
            TelegramSender telegramSender = new TelegramSender();
            await overView.WeeklyOverview();

            await telegramSender.sendMenssage("weekly", "Gastos por Semana");
        }
    }
}
