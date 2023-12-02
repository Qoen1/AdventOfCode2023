namespace dag2;

public class Game
{
    public int Id { get; set; }
    public Dictionary<string, int> Dice = new Dictionary<string, int>()
    {
        {"red", 0},
        {"green", 0},
        {"blue", 0}
    };
}