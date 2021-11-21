using System;

namespace FOBOS_API.Utils.Message
{
  public class Message
  {
    private string EntityName { get; set; }
    private Exception Error { get; set; }
    private Operators Op { get; set; }
    private string Feedback { get; set; }
    private string Text { get; set; }
    private bool Show { get; set; }
    public Message(string entityName, Operators op)
    {
      EntityName = entityName;
      Op = op;
    }

    public void setMessageSuccess()
    {
      Show = true;
      Feedback = "success";
      Text = getMessageTextSuccess();
    }

    public void setMessageError(Exception ex)
    {
      Error = ex;
      Show = true;
      Feedback = "error";
      Text = getMessageTextError();
    }

    public string getFeedback()
    {
      return Feedback;
    }
    public bool getShow()
    {
      return Show;
    }
    public string getText()
    {
      return Text;
    }

    private string getMessageTextSuccess()
    {
      switch (Op)
      {
        case Operators.Insert:
          return "Inclusão realizada para " + EntityName + " com sucesso!";
        case Operators.Alter:
          return "Alteração realizada para " + EntityName + " com sucesso!";
        case Operators.Delete:
          return "Exclusão realizada para " + EntityName + " com sucesso!";
        default:
          return "Operação em massa realizada para " + EntityName + " com sucesso!";
      }
    }

    private string getMessageTextError()
    {
      switch (Op)
      {
        case Operators.Insert:
          return "Houve um erro na inclusão " + EntityName + "! Erro: ";
        case Operators.Alter:
          return "Houve um erro na alteração " + EntityName + "! Erro: ";
        case Operators.Delete:
          return "Houve um erro na exclusão " + EntityName + "! Erro: ";
        default:
          return "Houve um erro na operação em massa " + EntityName + "! Erro: ";
      }
    }
  }
}