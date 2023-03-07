using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<IList<Card>> GetCards();
        Task<IList<Card>> GetCardsActivated(); 
         Task<Card> GetCard(int id);
         Task<Card> GetCardByBankName(string name);
        Task SaveCard(Card Cards);
    }
}
