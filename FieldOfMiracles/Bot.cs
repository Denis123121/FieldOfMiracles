using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
namespace FieldOfMiracles;

public class Bot
{
    private TelegramBotClient _botClient;
    
    private CancellationTokenSource _cancellationTokenSource; 

    public Bot()
    {
        _botClient = new TelegramBotClient("5402353079:AAGUBuJJ979Q23LP6qoKOwwE_HduLylxdtk");
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Start()
    {
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            BotHandlers.HandleUpdateAsync,
            BotHandlers.HandlePollingErrorAsync,
            receiverOptions,
            _cancellationTokenSource.Token
        );
    }

    public string GetBotName()
    {
        return _botClient.GetMeAsync().Result.Username;
    }
    
    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
}