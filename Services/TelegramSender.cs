using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using FOBOS_API.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOBOS_API.Services
{
    public class TelegramSender
    {

        static TelegramBotClient bot = new TelegramBotClient(
                  "TOKEN"
                );
        CancellationTokenSource cts;
        CancellationToken cancellationToken;

        public TelegramSender()
        {
            cts = new CancellationTokenSource();
            cancellationToken = cts.Token;
        }

        public async Task sendMenssage(List<string> options, string question)
        {
            try {
                await bot.SendPollAsync(
                    chatId: -607479766,
                    question: question,
                    options: options,
                    isAnonymous: true,
                    cancellationToken: cancellationToken
                );
                
            }catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        public async Task readMessages()
        {
            int offset = 0;

            while (true)
            {
                var updates = await bot.GetUpdatesAsync(offset: offset);
                DateTime fromDate = new DateTime(2024, 8, 1);

                foreach (var update in updates)
                {
                    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                    {
                        var message = update.Message;

                        // Verifica se a mensagem foi enviada após a data especificada
                        if (message.Date >= fromDate)
                        {
                            // Aqui você pode processar a mensagem, verificar comandos, etc.
                            Console.WriteLine($"Nova mensagem de {message.From.Username}: {message.Text}");

                            // Verifica se a mensagem contém um comando específico
                            if (message.Text != null && message.Text.StartsWith("/comando"))
                            {
                                Console.WriteLine($"Comando encontrado: {message.Text}");
                                // Processa o comando conforme necessário
                            }
                        }
                    }

                    // Atualiza o offset para não processar as mesmas mensagens novamente
                    offset = update.Id + 1;
                }
                offset = offset + 1;
                // Opcional: adicionar um delay para não sobrecarregar a API do Telegram
                await Task.Delay(1000);
            }

        }
    }
}
