
using System.Collections.Generic;
using Annotation = System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FOBOS_API.Data;
using FOBOS_API.Models;

namespace FOBOS_API.Models
{
  [Annotation.Table("FOBO_TB_CARDS")]
  public class Card : _BaseModel
  {
    [Column(Name="NM_NAME")]
    public string name { get; set; }

    [Column(Name="CD_AGENCY")]
    public string agency { get; set; }

    [Column(Name="CD_ACCOUNT")]
    public string account { get; set; }

    [Column(Name="FK_BANK_CODIGO")]
    public int fkBank { get; set; }

    [Column(Name="FK_USER_CODIGO")]
    public int fkUser { get; set; }

    public virtual Bank Bank { get; set; }
    public virtual User User { get; set; }

    [JsonIgnore]
    public List<Statement> Statements { get; private set; } = new List<Statement>();

    public Card(Bank bank, User user)
    {
      Bank = bank;
      User = user;
    }
    public Card()
    {
    }
  }

}