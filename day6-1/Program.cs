List<string> lines = File.ReadAllLines("input.txt").ToList();
for (int i=0;i<lines.Count;i++)
{
    lines[i] = lines[i].Split(":")[1];
}


string time = lines[0];
string distance = lines[1];

string[] timestrings = time.Split(" ", StringSplitOptions.RemoveEmptyEntries);
string[] distancestrings = distance.Split(" ", StringSplitOptions.RemoveEmptyEntries);
List<int> times = new List<int>();
foreach(string timestring in timestrings)
{
    times.Add(int.Parse(timestring));
}
List<int> distances = new List<int>();
foreach(string distancestring in distancestrings)
{
    distances.Add(int.Parse(distancestring));
}

int answer = 0;
for (int i = 0; i < distances.Count && i < times.Count; i++)
{
    int currTime = times[i];
    int currDistance = distances[i];
    int amtOfWinningStrats = 0;

    for (int seconds = 0; seconds < currTime; seconds++)
    {
        int calculatedDistance = seconds * (currTime - seconds);
        if (calculatedDistance > currDistance)
        {
            amtOfWinningStrats++;
        }
    }

    if (answer == 0)
    {
        answer += amtOfWinningStrats;
    }
    else
    {
        answer *= amtOfWinningStrats;
    }
}

Console.WriteLine($"answer: {answer}");