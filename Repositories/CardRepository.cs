using FOBOS_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using FOBOS_API;
using System;
using Dapper;
using System.Linq;
using FOBOS_API.Models;

namespace FOBOS_API.Repositories
{
  public class CardRepository : _BaseRepository, ICardRepository
  {

    public async Task<IList<Card>> GetCards()
    {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CARDS ";
                IList<Card> cards = (await db.getSQLConnection().QueryAsync<Card>(sql)).ToList();

                db.FecharConexao();
                return cards;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

    public async Task<Card> GetCard(int id)
    {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CARDS c"
                            + " JOIN FOBO_TB_BANKS b"
                            + " ON b.BANK_SQ_CODIGO = c.CARD_FK_BANK_CODIGO"
                             + " JOIN FOBO_TB_USER u"
                            + " ON u.USER_SQ_CODIGO = c.CARD_FK_USER_CODIGO"
                            + " WHERE c.CARD_SQ_CODIGO = @id";

                Card card = (await db.getSQLConnection().QueryAsync<Card, Bank, User, Card>(sql,
                    (c, b, u) =>
                    {
                        c.Bank = b;
                        c.User = u;
                        return c;
                    }, 
                    splitOn: "CARD_SQ_CODIGO,BANK_SQ_CODIGO,USER_SQ_CODIGO",
                    param: new {id = id })).First();

                db.FecharConexao();
                return card;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<IList<Card>> GetCardsActivated()
        {
            try
            {
                db.AbrirConexao();

                string sql = @"SELECT * FROM FOBO_TB_CARDS c"
                            + " JOIN FOBO_TB_BANKS b"
                            + " ON b.BANK_SQ_CODIGO = c.CARD_FK_BANK_CODIGO"
                            + " JOIN FOBO_TB_USER u"
                            + " ON u.USER_SQ_CODIGO = c.CARD_FK_USER_CODIGO"
                            + " WHERE c.CARD_BL_ATIVO = 1 ";

                IList<Card> cards = (await db.getSQLConnection().QueryAsync<Card, Bank, User, Card>(sql,
                     (c, b, u) =>
                     {
                         c.Bank = b;
                         c.User = u;
                         return c;
                     },
                    splitOn: "CARD_SQ_CODIGO,BANK_SQ_CODIGO,USER_SQ_CODIGO")).ToList();

                db.FecharConexao();
                return cards;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task SaveCard(Card card)
        {
            try
            {
                db.AbrirConexao();

                if (card.id == null || card.id == 0)
                {
                    await Insert(card);
                }
                else
                {
                    await Update(card);
                }

                db.FecharConexao();
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        private async Task Insert(Card card)
        {
            try
            {
                string sql = @" INSERT INTO FOBO_TB_CARDS "
                            + "("
                                + " CARD_NM_NAME,"
                                + " CARD_CD_AGENCY,"
                                + " CARD_CD_ACCOUNT,"
                                + " CARD_FK_BANK_CODIGO,"
                                + " CARD_FK_USER_CODIGO,"
                                + " CARD_DT_CREATED_AT,"
                                + " CARD_DT_UPDATED_AT,"
                                + " CARD_BL_ATIVO"
                            + " ) VALUES ("
                                + " @name, "
                                + " @agency, "
                                + " @account, "
                                + " @fkBank, "
                                + " @fkUser, "
                                + " @createdAt,"
                                + " @now,"
                                + " 1 "
                            + " )";

                await db.getSQLConnection().ExecuteAsync(sql, card);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }
        }

        private async Task Update(Card card)
        {
            try
            {
                string sql = @" UPDATE FOBO_TB_CARDS SET"
                                + " CARD_NM_NAME = @name, "
                                + " CARD_CD_AGENCY = @agency, "
                                + " CARD_CD_ACCOUNT = @account, "
                                + " CARD_FK_BANK_CODIGO = @fkBank, "
                                + " CARD_FK_USER_CODIGO = @fkUser, "
                                + " CARD_DT_CREATED_AT = @createdAt,"
                                + " CARD_DT_UPDATED_AT = @now,"
                                + " CARD_BL_ATIVO = @ativo"
                                + " WHERE CARD_SQ_CODIGO = @id";

                await db.getSQLConnection().ExecuteAsync(sql, card);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }
        }
    }

}