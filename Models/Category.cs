
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Annotation = System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FOBOS_API.Models;
using FOBOS_API.Data;

namespace FOBOS_API.Models
{
  [Annotation.Table("FOBO_TB_CATEGORIES")]
  public class Category : _BaseModel
  {
    [Column(Name="NM_NAME")]
    public string name { get; set; }

    [Column(Name="FK_CATY_CODIGO")]
    public int fkCategoryType { get; set; }

    public virtual CategoryType CategoryType { get; set; }
    [JsonIgnore]
    public List<Statement> Statements { get; private set; } = new List<Statement>();

    public Category()
    {

    }
  }
}