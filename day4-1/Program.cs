using day4_1;

#region gatherData

List<string> lines = File.ReadAllLines("input.txt").ToList();
List<Card> cards = new List<Card>();

for (int i=0;i<lines.Count;i++)
{
    string line = lines[i];
    Card card = new Card();

    string cardNumberString = line.Split(":", StringSplitOptions.RemoveEmptyEntries)[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
    card.CardNumber = int.Parse(cardNumberString);

    line = line.Split(":")[1];
    
    string[] parts = line.Split("|");
    
    string[] numbers = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    foreach (string number in numbers)
    {
        card.Numbers.Add(int.Parse(number));
    }
    string[] winningNumbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    foreach (string number in winningNumbers)
    {
        card.WinningNumbers.Add(int.Parse(number));
    }
    cards.Add(card);
}

#endregion

#region determineAnswer

int answer = 0;

foreach (Card card in cards)
{
    int points = 0;
    for (int i = 0; i < card.Numbers.Count && i < card.WinningNumbers.Count; i++)
    {
        if (card.WinningNumbers.Contains(card.Numbers[i]))
        {
            if(points == 0) points = 1;
            else points *= 2;
        }
    }

    answer += points;
}

#endregion

Console.WriteLine($"Answer: {answer}");