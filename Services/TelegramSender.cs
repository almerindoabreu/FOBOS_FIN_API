using FOBOS_API.Utils;
using System;
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
                  "5168058859:AAEQ2JeCs_UyxDLDO5JsBZ2ElOo6-590U_E"
                );

        public TelegramSender()
        {
        }

        public async Task sendMenssage(string image, string message)
        {
            try {
                using (Stream stream = System.IO.File.OpenRead(ServerApp.MapPath("Services/OverviewGenerator/Script/"+ image + ".png")))
                {
                    await bot.SendPhotoAsync(
                       chatId: -607479766,
                       photo: stream,
                       caption: message
                   );
                }
            }catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {
                    var text = update.Message.Text;
                    var id = update.Message.Chat.Id;
                    string? username = update.Message.Chat.Username;

                    Console.WriteLine($" {username} | {id} | {text}");
                }
            }
        }
    }
}
