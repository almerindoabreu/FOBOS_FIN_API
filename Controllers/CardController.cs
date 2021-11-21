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
    public class CardController : ControllerBase
    {
        private readonly ICardRepository cardRepository;

        public CardController(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }

        [HttpGet]
        [Route("cards")]
        public async Task<IList<Card>> GetCardsActivated()
        {
            IList<Card> cards = await cardRepository.GetCardsActivated();
            return cards;
        }

        [HttpGet]
        [Route("cards/showAll")]
        public async Task<IList<Card>> GetCards()
        {
            IList<Card> cards = await cardRepository.GetCards();
            return cards;
        }

        [HttpGet]
        [Route("cards/{id}")]
        public async Task<Card> GetCard([FromRoute] int id)
        {
            Card card = await cardRepository.GetCard(id);
            return card;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveCard([FromBody] Card card)
        {
            Operators Op = ((card.id == 0 || card.id == null) ? Operators.Insert : Operators.Alter);
            Message message = new Message("Cartão", Op);

            try
            {
                await cardRepository.SaveCard(card);
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
            Message message = new Message("Cartão", Operators.Delete);

            try
            {
                Card card = await cardRepository.GetCard(id);
                card.ativo = false;

                await cardRepository.SaveCard(card);

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
