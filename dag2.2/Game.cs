namespace dag2._2;

public class Game
{
    public int Id { get; set; }
    public int Power
    {
        get
        {
            int result = 1;
            foreach (int number in Dice.Values)
            {
                result *= number;
            }

            return result;
        }
    }

    public Dictionary<string, int> Dice = new Dictionary<string, int>()
    {
        {"red", 0},
        {"green", 0},
        {"blue", 0}
    };
}