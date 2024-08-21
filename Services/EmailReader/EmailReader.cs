using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MailKit.Search;
using MimeKit;
using System;
using FOBOS_API.Utils;
using FOBOS_API.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using System.Text;

namespace FOBOS_API.Services.EmailReader
{
    public class EmailReader
    {
        private ImapClient client;
        private static string path = ServerApp.MapPath("Services/EmailReader/TempFiles/");

        public EmailReader(string mailServer, int port, bool ssl, string login, string password)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            client = new ImapClient();
            client.Connect(mailServer, port, SecureSocketOptions.SslOnConnect);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(login, password);

            client.Inbox.Open(FolderAccess.ReadWrite);
        }

        public IList<Statement> ReadAttachment(int idInterCard, int idNubankCard)
        {
            
            
            var files = Directory.GetFiles(path);
            var completePathFile = files.FirstOrDefault();

            var isInterStatement = completePathFile.Contains("Inter");

            if (isInterStatement)
            {
                return this.ReadInterStatement(completePathFile, idInterCard);
            }
            else
            {
                return this.ReadNubankStatement(completePathFile, idNubankCard);
            }

        }

        private IList<Statement> ReadInterStatement(string completePathFile, int idInterCard)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {
                IList<Statement> statements = new List<Statement>();
                using (var reader = new StreamReader(completePathFile, Encoding.GetEncoding(1252)))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (i > 7)
                        {
                            Statement statement = new Statement();
                            dynamic values = line.Split(';');

                            statement.fkCard = idInterCard;
                            statement.date = Convert.ToDateTime(values[0]);
                            statement.name = values[1];
                            var s = values[2].Split("R$ ")[0];
                            decimal value = (values[2].Split("R$ ")[0].Contains("-")?
                                Convert.ToDecimal(values[2].Split("R$ ")[0]) * -1 :
                                Convert.ToDecimal(values[2].Split("R$ ")[0]));
                            statement.value = value;
                            statement.balance = Convert.ToDecimal(values[3]);

                            statements.Add(statement);
                        }
                        i = i + 1;
                    }
                }
                return statements;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }
        private IList<Statement> ReadNubankStatement(string completePathFile, int idNubankCard)
        {
            return new List<Statement>();
        }

        private void DeleteAllFile()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        public void DownloadAttachment()
        {
            this.DeleteAllFile();

            var query = SearchQuery.SubjectContains("Inter").Or(SearchQuery.SubjectContains("Nubank"));
            UniqueId uid = client.Inbox.Search(query).Last();

            MimeMessage message = client.Inbox.GetMessage(uid);

            bool interStatement = message.Subject.Contains("Inter"); // if interStatement is false, then statement is nubank

            MimeEntity attachment = message.Attachments.FirstOrDefault();

            if(attachment != null)
            {
                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;

                using (var stream = File.Create(path + (interStatement ? "Inter - " : "Nubank - ") + fileName))
                {
                    if (attachment is MessagePart)
                    {
                        var rfc822 = (MessagePart)attachment;

                        rfc822.Message.WriteTo(stream);
                    }
                    else
                    {
                        var part = (MimePart)attachment;

                        part.Content.DecodeTo(stream);
                    }
                }
            }
        }
    }
}
