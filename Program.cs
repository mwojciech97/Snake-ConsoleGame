using Snake;
using System.ComponentModel;

#region variables
List<Player> players = new List<Player>();
int playerID = 0;
Board board = new Board();
ConsoleKeyInfo keyInfo;
ConsoleKey consoleKey;
#endregion

InitializeGame();

void InitializeGame()
{
    string playersFile = @"C:\Repos\Snake\PlayersFile.txt";
    string scoreFile = @"C:\Repos\Snake\SnakeScores.txt";
    Console.CursorVisible = false;
    CreateFile(scoreFile);
    CreateFile(playersFile);
    ReadPlayers(playersFile);
    Console.Clear();
    Menu();
    MenuLoop(playersFile, scoreFile);
}
void MenuLoop(string playersFile, string scoreFile)
{
    do
    {
        keyInfo = Console.ReadKey(true);
        consoleKey = keyInfo.Key;
        switch (consoleKey)
        {
            case ConsoleKey.D1:
                Console.Clear();
                CreateGame(scoreFile);
                Menu();
                break;
            case ConsoleKey.D2:
                Console.Clear();
                ReadScores(scoreFile);
                break;
            case ConsoleKey.D3:
                Console.Clear();
                Settings(playersFile);
                break;
            case ConsoleKey.D4:
                break;
            default:
                break;
        }
    } while (consoleKey != ConsoleKey.D4);
}
void Menu()
{
    Console.WriteLine("Hello " + players[playerID].PlayerName + ". It is nice to see you again!");
    Console.WriteLine("1. Start game");
    Console.WriteLine("2. Top 10 scroces");
    Console.WriteLine("3. Settings");
    Console.WriteLine("4. Exit");
}
void Settings(string playersFile)
{
    Console.WriteLine("1. Change player");
    Console.WriteLine("2. Create new player");
    Console.WriteLine("3. Change board size");
    Console.WriteLine("4. Back");
    keyInfo = Console.ReadKey(true);
    switch (keyInfo.Key)
    {
        case ConsoleKey.D1:
            Console.Clear();
            playerID = ChoosePlayer();
            Menu();
            break;
        case ConsoleKey.D2:
            CreatePlayer(playersFile);
            SavePlayers(playersFile);
            Console.Clear();
            Settings(playersFile);
            break;
        case ConsoleKey.D3:
            ChangeBoardSize();
            Console.Clear();
            Menu();
            break;
        case ConsoleKey.D4:
            Console.Clear();
            Menu();
            break;
        default:
            Console.Clear();
            Settings(playersFile);
            break;
    }
}
void ChangeBoardSize()
{
    string? temp;
    int tempV = 0;
    bool parseSuccess = false;
    do
    {
        Console.Clear();
        Console.WriteLine("Choose your size of the board. It cannot be less than 15 and more than 25!");
        temp = Console.ReadLine();
        parseSuccess = int.TryParse(temp, out tempV);
        board.Width = tempV;
        board.Height = tempV;
    } while (!parseSuccess || tempV < 14 || tempV > 26);
}
void CreateGame(string scoreFile)
{
    Game game = new Game(board.Width, board.Height);
    game.SetStartingPositions();
    board.CreateBoard();

    while (!game.Lost)
    {
        game.PlayerInput();
        game.GameLogic();
        Console.SetCursorPosition(0, board.Height + 2);
        Console.WriteLine("Your score: " + game.Score);
    }
    Console.WriteLine("Game Over!");
    Console.WriteLine("Press enter to continue");
    SaveScore(game, scoreFile);
    do
    {
        keyInfo = Console.ReadKey(true);
        consoleKey = keyInfo.Key;
    } while(!consoleKey.Equals(ConsoleKey.Enter));
    
    Console.Clear();
}
void CreateFile(string fileName)
{
    if (!File.Exists(fileName))
    {
        File.Create(fileName).Close();
    }
}
void ReadScores(string scoreFile)
{
    int i = 1;
    string[] scores = File.ReadAllLines(scoreFile);
    if (scores.Count() == 0)
    {
        Console.WriteLine("No games were played or no points were acquired during the game!");
    }
    else
    {
        foreach(string str in scores)
        {
            Console.WriteLine(i + ". " + str.Split(',')[0] + " " + str.Split(',')[1] + " " + str.Split(',')[2]);
            i++;
        }
    }
    Console.ReadKey(true);
    Console.Clear();
    Menu();
}
void SaveScore(Game game, string scoreFile)
{
    List<string> scores = File.ReadAllLines(scoreFile).ToList();
    scores.Add(game.Score.ToString() + "," + players[playerID].PlayerName + "," + board.Width + "x" + board.Height);
    scores = scores.OrderByDescending(score => Int16.Parse(score.Split(',')[0])).ToList();
    if(scores.Count() > 10 || scores[scores.Count - 1].Split(',')[0] == "0") scores.RemoveAt(scores.Count() - 1);
    var scoresToSave = string.Join("\n", scores);
    File.WriteAllText(scoreFile, scoresToSave);
}
void SavePlayers(string playersFile)
{
    var playersToSave = string.Join("\n", players.Select(player => player.PlayerName));
    File.WriteAllText(playersFile, playersToSave);
}
void ReadPlayers(string playersFile)
{
    string[] playersFromFile = File.ReadAllLines(playersFile);
    if (playersFromFile.Count() == 0)
    {
        CreatePlayer(playersFile);
    }
    else
    {
        foreach (string player in playersFromFile)
        {
            players.Add(new Player(player));
        }
        playerID = ChoosePlayer();
    }
}
int ChoosePlayer()
{
    int i = 0;
    int tempPlayerID = 0;
    string? userInput;
    bool parseSuccess = false;
    Console.WriteLine("Choose your player name to continue:");
    foreach (Player player in players)
    {
        i++;
        Console.WriteLine(i + ". " + player.PlayerName);
    }
    do
    {
        userInput = Console.ReadLine();
        if (userInput != null) userInput.Trim();
        parseSuccess = int.TryParse(userInput, out tempPlayerID);
    }
    while (!parseSuccess || tempPlayerID > i);
    Console.Clear();
    return tempPlayerID - 1;
}
void CreatePlayer(string playersFile)
{
    string? name;
    do
    {
        Console.Clear();
        Console.WriteLine("Create a new player!");
        Console.Write("Type your name: ");
        name = Console.ReadLine();
    }
    while (name == null ||
    name.Trim() == "" ||
    players.Any(player => player.PlayerName == name));
    
    players.Add(new Player(name.Trim()));
    SavePlayers(playersFile);
}