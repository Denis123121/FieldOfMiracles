using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace FieldOfMiracles;

public class BotHandlers
{
    private static GameLogic _logic = new GameLogic();

    public async static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;
        string responce = _logic.ProcessTextMessage(messageText);

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: responce,
            cancellationToken: cancellationToken);
    }

    public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}