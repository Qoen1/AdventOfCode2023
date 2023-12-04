namespace day4_1;

public class Card
{
    public int CardNumber { get; set; }
    public List<int> Numbers { get; set; } = new List<int>();
    public List<int> WinningNumbers { get; set; } = new List<int>();
}