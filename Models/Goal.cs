
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Annotation = System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FOBOS_API.Models;
using FOBOS_API.Data;

namespace FOBOS_API.Models
{
  [Annotation.Table("FOBO_TB_GOALS")]
  public class Goal : _BaseModel
  {
    [Column(Name="NM_TITLE")]
    public string title { get; set; }
    [Column(Name="DS_DESCRIPTION")]
    public string description { get; set; }
    [Column(Name="DT_DEADLINE")]
    public DateTime deadline { get; set; }
    [Column(Name="NM_STATUS")]
    public string status { get; set; }
    [Column(Name="FK_USER_CODIGO")]
    public int fkUser { get; set; }

    public virtual User User { get; set; }

    public Goal()
    {

    }
  }
}