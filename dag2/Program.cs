//read file input.txt


using System.Text.RegularExpressions;
using dag2;

Console.WriteLine("Hello World!");
List<string> lines = File.ReadAllLines("input.txt").ToList();

List<Game> games = new List<Game>();
int result = 0;

Regex numberRegex = new Regex(@"\d+");
Regex colorRegex = new Regex(@"red|green|blue");
Game controlGame = new Game()
{
    Dice = new Dictionary<string, int>()
    {
        {"red",12},
        {"green", 13},
        {"blue", 14}
    }
};

foreach (string line in lines)
{
    int id = int.Parse(numberRegex.Matches(line.Substring(0, line.IndexOf(':'))).Single().Value);
    string coloursString = line.Substring(line.IndexOf(':') + 1);
    
    string[] colours = coloursString.Split(new []{';',','}, StringSplitOptions.TrimEntries);
    
    Game game = new Game();

    foreach (string colour in colours)
    {
        string numberString = numberRegex.Matches(colour).Single().Value;
        string color = colorRegex.Matches(colour).Single().Value;
        int number = int.Parse(numberString);
        if (number > game.Dice[color])
        {
            game.Dice[color] = number;
        }
    }

    game.Id = id;

    if (GameIsPossible(game))
    {
        games.Add(game);
        result += id;
    }
}
Console.WriteLine($"result: {result}");

bool GameIsPossible(Game game)
{
    return game.Dice["red"] <= controlGame.Dice["red"] &&
           game.Dice["green"] <= controlGame.Dice["green"] &&
           game.Dice["blue"] <= controlGame.Dice["blue"];
}