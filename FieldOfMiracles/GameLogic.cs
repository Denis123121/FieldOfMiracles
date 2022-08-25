using System.Net.Sockets;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace FieldOfMiracles;

public class GameLogic
{
    private List<string> _words = new List<string>(){"самообороноспособность", "водоворотозасососпособность", 
       "гиппопотомомонстросесквиппедалиофобия","бумагопрядильный","раболепствующий"};
    private List<string> _hints = new List<string>(){"это когда тебя бьют, а ты бьешь в ответ, плачешь, материшься или зовешь мать", 
        "тут даже я в шоке, что-то с водой и способностью","страх длинных и сложных слов ;)", "относящийся до прядения хлопчатки, не спрашивайте, что это значит","тот кто ведет себя раболепно"};
    private List<string> _userLetters = new List<string>();
    private Random _random = new Random();
    private string _currentWord;
    private string _currentEncryptedWord;
    private int _randomNumber;
  
    public string ProcessTextMessage(string messageText)
    {
        if (messageText == "/start")
            return StartGame();
        if (messageText.StartsWith("/letter"))
            return CheckLetter(messageText);
        if (messageText == "/word")
            return ShowWord();
        if (messageText == "/hint")
            return GiveHint();
        
        return  "нажмите /start, чобы начать игру,\n/word, чтобы просмотреть слово,\n/hint, если нужна посдказка";
    }

    private string StartGame()
    {
        _randomNumber = _random.Next(0, _words.Count + 1);
        _currentEncryptedWord = "";
        _currentWord = _words[_randomNumber];
        
        for (int i = 0; i < _currentWord.Length; i++)
        {
            _currentEncryptedWord = _currentEncryptedWord + "*";
        }

        return "я загадал новое слово";
    }

    private string CheckLetter(string messageText)
    {
        _currentEncryptedWord = "";
        string userLetter = messageText.Substring(messageText.IndexOf(' ') + 1);
        
        for (int i = 0; i < _currentWord.Length; i++)
        {
            char letter = _currentWord[i];
            
            if (userLetter == letter.ToString())
            {
                _userLetters.Add(userLetter);
                
                _currentEncryptedWord = _currentEncryptedWord + userLetter;
            }
            else
            {
                _currentEncryptedWord = _currentEncryptedWord + "*";
            }
        }

        return "чтобы посмотреть слово нажмите /word";
    }
    
    private string ShowWord()
    {
        
        return _currentEncryptedWord;
    }
    
    private string GiveHint()
    {
        return _hints[_randomNumber];
    }
}