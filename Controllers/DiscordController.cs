using FOBOS_API.Repositories.Interfaces;
using FOBOS_API.Services.OverviewGenerator;
using FOBOS_API.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FOBOS_API.Models;
using System.Linq;
using ClosedXML;

namespace FOBOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscordController
    {
        private readonly IStatementRepository statementRepository;
        private readonly ICategoryRepository categoryRepository;
        public DiscordController(IStatementRepository statementRepository, ICategoryRepository categoryRepository)
        {

            this.statementRepository = statementRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("discord/sendMessages")]
        public async Task sendMessages()
        {
            DiscordAPI discordAPI = new DiscordAPI();

            IList<Statement> statements = await statementRepository.GetStatementsActivated();
            statements = statements.Where(st => st.fkCategory == null).ToList();

            IList<Category> categories = await categoryRepository.GetCategoriesActivated();

            string categoriesSelected = "";

            for (int i = 0; i < categories.Count; i++)
            {
                categoriesSelected = categoriesSelected + ("#" + categories[i].CategoryType.id + "." + categories[i].id + "# " + categories[i].CategoryType.name + " > " + categories[i].name + "\n");
            }

            for (int i = 0; i < 2; i++)
            {
                string massage = "#" + statements[i].id + "# " + statements[i].name + " - Valor: " + statements[i].value + "\n\n" + categoriesSelected + "\n\n ----------------------------------------------" ;
                await discordAPI.sendMessage(massage);
            }
        }

        [HttpGet]
        [Route("discord/categorizationStatement")]
        public async Task<string> categorizationStatement()
        {
            DiscordAPI discordAPI = new DiscordAPI();

            IList<Statement> statements = await statementRepository.GetStatementsActivated();

            List<Statement> statementsDefined = await discordAPI.readStatementsDefined();
            int i = 0;
            foreach (var statementDefined in statementsDefined)
            {
                Statement statement = statements.Where(s => s.id == statementDefined.id).FirstOrDefault();

                if (statement != null)
                {
                    statement.fkCategory = statementDefined.fkCategory;
                    bool saved = false;
                    saved = await statementRepository.SaveStatement(statement);
                    if (saved) { i++; }
                }
            }

            return "Foram registrados " + i + " novos extratos categorizados.";
        }

        [HttpGet]
        [Route("discord/clearMessages")]
        public async Task cleanMessages()
        {
            DiscordAPI discordAPI = new DiscordAPI();

            IList<Statement> statements = await statementRepository.GetStatementsActivated();

            await discordAPI.cleanMessages();
            
        }
    }
}
