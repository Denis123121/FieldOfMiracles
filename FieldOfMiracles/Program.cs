using FieldOfMiracles;

Bot bot = new Bot();
bot.Start();

Console.WriteLine("Bot started");
Console.WriteLine($"Start listening for @{bot.GetBotName()}");
Console.WriteLine("Press enter for stop");
Console.ReadKey();

bot.Stop();