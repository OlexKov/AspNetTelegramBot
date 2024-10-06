﻿
using Ardalis.Specification.EntityFrameworkCore;
using BusinessLogic.DTO;
using Newtonsoft.Json;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;


Console.WriteLine("Hello, World!");
string token = "7862031062:AAH2LFTmXTDRi0kFHTPBw1A_x5YWlyul2T0";
List<long> adminsChatId = [5254681094];
var client = new TelegramBotClient(token);
client.StartReceiving(Update,Error);
Console.ReadLine();
await client.CloseAsync();
async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
{
    throw new NotImplementedException();
}

async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
{

    var message = update.Message;

    if (message != null) 
    {
        if (!adminsChatId.Contains(message.Chat.Id))
        {
            //User commands
            switch (message.Type)
            {
                case MessageType.Text:

                    Console.WriteLine($"{message.Chat.Username} |  {message.Text}");
                    switch (message?.Text?.ToLower())
                    {
                        case "/start":
                            await client.SendTextMessageAsync(message.Chat.Id, "Будь ласка, поділіться своїм контактом", cancellationToken: token);
                            break;

                        default:
                            await client.SendTextMessageAsync(message.Chat.Id, "Для початку поділіться своїм контактом", replyToMessageId: message.MessageId, cancellationToken: token);
                            break;
                    }
                    break;

                case MessageType.Contact:
                    if (message.Contact != null) 
                    {
                        long newUserId = await CreateUserAsync(message.Contact,message.Chat.Username);
                        if (newUserId != 0) 
                        { 
                        }
                        var phoneNumber = message?.Contact?.PhoneNumber;
                        Console.WriteLine($"Отримано номер телефону: {phoneNumber}");
                        await client.SendTextMessageAsync(message.Chat.Id, $"Дякуємо! Ваш номер телефону: {phoneNumber}", cancellationToken: token);
                        foreach (var adminId in adminsChatId)
                        {
                            await client.SendTextMessageAsync(adminId, $"Новий користувач: {message?.From?.Username}, номер телефону: {phoneNumber}", cancellationToken: token);
                        }
                    }
                    
                    break;

                default:
                    break;
            }
        }
        else 
        {
            //Admin commands
            long adminChatId = message.Chat.Id;
            switch (message.Type)
            {
                case MessageType.Text:
                    adminConsoleWriteLine($"{message.Chat.Username} |  {message.Text}");
                    switch (message?.Text?.ToLower())
                    {
                        case "/start":
                            await client.SendTextMessageAsync(adminChatId, "Привіт, адміністраторе! Ви отримали доступ до бота.", cancellationToken: token);
                            break;

                        default:
                            break;
                    }
                    break;
                   
                case MessageType.Contact:
                    var newAdminChatId = message?.Contact?.UserId;
                    if (newAdminChatId != null) 
                    {
                        adminConsoleWriteLine($"Додано новаго адміністратора: {message?.Contact?.FirstName} {message?.Contact?.LastName}");
                        adminsChatId.Add((long)newAdminChatId);
                        await client.SendTextMessageAsync(newAdminChatId, "Привіт, адміністраторе! Ви отримали доступ до бота.", cancellationToken: token);
                    }
                    break;
                default:
                    break;
            }
        }
        
    }
   
}

void adminConsoleWriteLine(string messsage)
{
    ConsoleColor consoleColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(messsage);
}

async Task<long> CreateUserAsync(Contact contact,string? username,bool admin = false) 
{
    var user = new TelegramUserDto()
    {
        Id = contact.UserId ?? 0,
        UserName = username,
        LastName = contact.LastName,
        FirstName = contact.FirstName,
        Vcard = contact.FirstName,
        PhoneNumber = contact.PhoneNumber
    };
    var url = "http://localhost:15251/api/user/";
    url += admin ? "createadmin" : "createuser";
    using var client = new HttpClient();
    string json = JsonConvert.SerializeObject(user);
    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
    HttpResponseMessage response = await client.PostAsync(url,content);
    if (response.IsSuccessStatusCode)
    {
        string responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<long>(responseData);
    }
    return 0;
}

async Task<IEnumerable<TelegramUserDto>> GetUsersAsync(bool admin =  false)
{
    var url = "http://localhost:15251/api/user/";
    url += admin ? "getadmins" : "getusers";
    using var client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(url);
    List<TelegramUserDto> result = [];
    if (response.IsSuccessStatusCode)
    {
        string responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<TelegramUserDto>>(responseData) ?? result;
    }
    return result;
}

async Task<TelegramUserDto?> GetUsersByIdAsync(long id)
{
    var url = $"http://localhost:15251/api/user/get{id}";
    using var client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(url);
    if (response.IsSuccessStatusCode)
    {
        string responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TelegramUserDto>(responseData);
    }
    return null;
}