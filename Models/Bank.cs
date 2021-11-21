
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FOBOS_API.Data;
using FOBOS_API.Models;
using Dapper.Contrib.Extensions;

namespace FOBOS_API.Models
{
  [Table("FOBO_TB_BANKS")]
  public class Bank : _BaseModel
  {
    [Column(Name = "NM_NAME")]
    public string name { get; set; }
    public Bank()
    {

    }
  }
}