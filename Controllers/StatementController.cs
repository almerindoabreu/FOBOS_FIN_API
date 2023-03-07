using Dapper;
using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories;
using FOBOS_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementRepository statementRepository;

        public StatementController(IStatementRepository statementRepository)
        {
            this.statementRepository = statementRepository;
        }

        [HttpGet]
        [Route("statements")]
        public async Task<IList<Statement>> GetStatementsActivated()
        {
            IList<Statement> statements = await statementRepository.GetStatementsActivated();
            return statements;
        }

        [HttpGet]
        [Route("Show/LastImport")]
        public async Task<IList<Statement>> LastImport()
        {
            IList<Statement> statements = await statementRepository.GetStatementsActivated();
            return statements;
        }

        [HttpGet]
        [Route("statements/ShowAll")]
        public async Task<IList<Statement>> GetStatements()
        {
            IList<Statement> statements = await statementRepository.GetStatements();
            return statements;
        }

        [HttpGet]
        [Route("statements/{id}")]
        public async Task<Statement> GetStatement([FromRoute] int id)
        {
            Statement statement = await statementRepository.GetStatement(id);
            return statement;
        }

        [HttpPost]
        [Route("save")]
        public async Task SaveStatement([FromBody] Statement statement)
        {
            await statementRepository.SaveStatement(statement);
        }

        [HttpPut]
        [Route("delete/{id}")]
        public async Task DeleteStatement([FromRoute] int id)
        {
            Statement statement = await statementRepository.GetStatement(id);
            statement.ativo = false;

            await statementRepository.SaveStatement(statement);
        }
    }
}
