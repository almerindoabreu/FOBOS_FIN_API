using Discord;
using Org.BouncyCastle.Asn1.Crmf;
using Discord.Rest;
using System;
using System.Linq;
using Discord.WebSocket;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;
using FOBOS_API.Models;

namespace FOBOS_API.Services
{
    public class DiscordAPI
    {
        static string token = "TOKEN";
        static ulong channelId = 0; // Substitua pelo ID do canal onde deseja enviar a mensagem
        DiscordRestClient client;
        public DiscordAPI()
        {
            //client = new RestClient(token);
        }

        public async Task sendMessage(string msg)
        {
            using (var client = new HttpClient())
            {
                // Configurando o cabeçalho de autorização com o token do bot
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);

                // Criando o conteúdo da mensagem no formato JSON
                var payload = new
                {
                    content = msg // Conteúdo da mensagem
                };

                // Convertendo o objeto payload para JSON
                var jsonPayload = JsonConvert.SerializeObject(payload);

                // Criando o conteúdo HTTP com o tipo application/json
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Definindo a URL para o endpoint de envio de mensagem
                var url = $"https://discord.com/api/v10/channels/{channelId}/messages";

                // Enviando a requisição POST para o endpoint
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Mensagem enviada com sucesso!");
                }
                else
                {
                    Console.WriteLine($"Erro ao enviar a mensagem: {response.StatusCode} - {response.ReasonPhrase}");
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Detalhes do erro: {errorResponse}");
                }
            }
        }

        public async Task<List<Statement>> readStatementsDefined()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);

                var url = $"https://discord.com/api/v10/channels/{channelId}/messages?limit=100"; // Altere a versão da API conforme necessário
                var response = await client.GetAsync(url);
                List<Statement> statementsDefined = new List<Statement>();

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var messages = JArray.Parse(jsonResponse);

                    foreach (var message in messages)
                    {
                        if(message["type"].ToString() == "19")
                        {
                            string idStatement = message["referenced_message"]["content"].ToString().Split("#")[1];
                            string idCategory = message["content"].ToString().Split(".")[1];

                            statementsDefined.Add(new Statement { id = int.Parse(idStatement), fkCategory = int.Parse(idCategory) });
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Erro ao obter mensagens: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return statementsDefined;
            }
        }

        public async Task readPoll()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);

                var url = $"https://discord.com/api/v10/channels/{channelId}/messages?limit=100"; // Obter as últimas 100 mensagens
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var messages = JArray.Parse(jsonResponse);

                    foreach (var message in messages)
                    {
                        Console.WriteLine($"Mensagem Completa: {message.ToString()}");

                        // Verifica se a mensagem tem conteúdo ou embeds
                        if (!string.IsNullOrEmpty(message["content"]?.ToString()) || (message["embeds"] != null && message["embeds"].HasValues))
                        {
                            Console.WriteLine($"Enquete: {message["content"]}");

                            if (message["reactions"] != null && message["reactions"].HasValues)
                            {
                                foreach (var reaction in message["reactions"])
                                {
                                    var emoji = reaction["emoji"]["name"].ToString(); // Nome do emoji
                                    var count = reaction["count"].ToObject<int>(); // Número de votos
                                    Console.WriteLine($" - Opção: {emoji}, Total de Votos: {count}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("A mensagem não possui conteúdo visível.");
                        }
                        Console.WriteLine(); // Linha em branco para separação
                    }
                }
                else
                {
                    Console.WriteLine($"Erro ao obter mensagens: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }

        public async Task cleanMessages()
        {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);

                    bool hasMoreMessages = true;

                    while (hasMoreMessages)
                    {
                        // Obtenha as últimas 100 mensagens
                        var url = $"https://discord.com/api/v10/channels/{channelId}/messages?limit=100";
                        var response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var messages = JArray.Parse(jsonResponse);

                            if (messages.Count == 0)
                            {
                                hasMoreMessages = false; // Sem mensagens
                                break;
                            }

                            // Deleta cada mensagem
                            foreach (var message in messages)
                            {
                                string messageId = message["id"].ToString();
                                var deleteUrl = $"https://discord.com/api/v10/channels/{channelId}/messages/{messageId}";

                                var deleteResponse = await client.DeleteAsync(deleteUrl);
                                if (!deleteResponse.IsSuccessStatusCode)
                                {
                                    Console.WriteLine($"Erro ao deletar mensagem {messageId}: {deleteResponse.StatusCode} - {deleteResponse.ReasonPhrase}");
                                }

                                // Aguarde um tempo para evitar ultrapassar os limites de rate limit
                                await Task.Delay(1000); // 1 segundo de atraso por precaução
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Erro ao obter mensagens: {response.StatusCode} - {response.ReasonPhrase}");
                            break;
                        }
                    }
            }

        }
    }

    internal class StamentCategorized
    {
        public int idStatement;
        public int idCategory;
    }
}
