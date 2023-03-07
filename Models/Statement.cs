
using System.Collections.Generic;
using Annotation = System.ComponentModel.DataAnnotations.Schema;
using System;
using FOBOS_API.Models;
using FOBOS_API.Data;

namespace FOBOS_API.Models
{
  [Annotation.Table("FOBO_TB_STATEMENTS")]
  public class Statement : _BaseModel
  {
    [Column(Name="STAT_NM_NAME")]
    public string name { get; set; } = "";
    [Column(Name="STAT_NR_VALUE")]
    public decimal value { get; set; }
    [Column(Name="STAT_NR_BALANCE")]
    public decimal balance { get; set; }
    [Column(Name="STAT_DS_DESCRIPTION")]    
    public string description { get; set; }
    [Column(Name="STAT_DT_DATE")]
    public DateTime date { get; set; }

    [Column(Name= "STAT_FK_CARD_CODIGO")]

    public int fkCard { get; set; }
    [Column(Name="STAT_FK_CATE_CODIGO")]
    public int? fkCategory { get; set; }

    public virtual Card Card { get; set; }
    public virtual Category Category { get; set; }

    public Statement()
        {

        }
    public Statement(string _name, decimal _value, string _description, DateTime _date, int _fkCard)
    {
      name = _name;
      value = _value;
      description = _description;
      date = _date;
      fkCard = _fkCard;
    }

    Statement(Card card)
    {
      Card = card;
    }

    Statement(Card card, Category category)
    {
      Category = category;
    }
  }
}