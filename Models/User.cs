
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Annotation = System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FOBOS_API.Models;
using FOBOS_API.Data;

namespace FOBOS_API.Models
{
  [Annotation.Table("FOBO_TB_USERS")]
  public class User : _BaseModel
  {
    [Column(Name="NM_NAME")]
    public string name { get; set; }

    [JsonIgnore]
    public List<Card> Cards { get; private set; } = new List<Card>();

    [JsonIgnore]
    public List<Goal> Goals { get; private set; } = new List<Goal>();

    public User()
    {

    }
  }
}