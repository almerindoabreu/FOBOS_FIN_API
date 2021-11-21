
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Annotation = System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FOBOS_API.Models;
using FOBOS_API.Data;

namespace FOBOS_API.Models
{
  [Annotation.Table("FOBO_TB_CATEGORY_TYPES")]
  public class CategoryType : _BaseModel
  {
    [Column(Name="NM_NAME")]
    public string name { get; set; }
    [Column(Name="TP_STATEMENT")]
    public string typeStatement { get; set; }
    [JsonIgnore]
    public List<Category> Categories { get; set; } = new List<Category>();
    CategoryType()
    {

    }
  }
}