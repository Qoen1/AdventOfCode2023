using System.Text.RegularExpressions;
using day3_1;

List<string> lines = File.ReadAllLines("input.txt").ToList();
int answer = 0;
string[,] map = new string[lines.Count, lines[0].Length];

//build map
for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = lines[i][j].ToString();
    }
}

string specialCharString = "~!@#$%^&*()_+=-`[];',/?><\":}{";

List<Coordinate> coordinates = new List<Coordinate>();
SearchDirection[] directions = new SearchDirection[]
{
    SearchDirection.TOP, SearchDirection.TOPRIGHT, SearchDirection.RIGHT, SearchDirection.BOTTOMRIGHT,
    SearchDirection.BOTTOM, SearchDirection.BOTTOMLEFT, SearchDirection.LEFT, SearchDirection.TOPLEFT
};
bool addToResult = false;
List<string> results = new List<string>();

//get the right numbers from the map
for (int i = 0; i <= map.GetUpperBound(0); i++)
{
    for (int j = 0; j <= map.GetUpperBound(1); j++)
    {
        Coordinate currentCoordinate = new Coordinate(i, j);
        if ("0123456789".Contains(map[i, j]))
        {
            coordinates.Add(currentCoordinate);
            //check if there is a special char
            foreach (SearchDirection direction in directions)
            {
                int x = i;
                int y = j;
                
                switch (direction)
                {
                    case SearchDirection.TOP:
                        y--;
                        break;
                    case SearchDirection.TOPRIGHT:
                        x++;
                        y--;
                        break;
                    case SearchDirection.RIGHT:
                        x++;
                        break;
                    case SearchDirection.BOTTOMRIGHT:
                        x++;
                        y++;
                        break;
                    case SearchDirection.BOTTOM:
                        y++;
                        break;
                    case SearchDirection.BOTTOMLEFT:
                        x--;
                        y++;
                        break;
                    case SearchDirection.LEFT:
                        x--;
                        break;
                    case SearchDirection.TOPLEFT:
                        x--;
                        y--;
                        break;
                }
                
                if (x > 0 && 
                    y > 0 && 
                    x < map.GetUpperBound(1) && 
                    y < map.GetUpperBound(0) && 
                    specialCharString.Contains(map[x, y]))
                {
                    addToResult = true;
                }
            }
        }
        else if (coordinates.Count > 0 && addToResult)
        {
            string value = "";
            foreach (Coordinate coordinate in coordinates)
            {
                value += map[coordinate.X, coordinate.Y];
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"accepted value: {value}");
            Console.BackgroundColor = ConsoleColor.Black;

            results.Add(value);
            coordinates.Clear();
            addToResult = false;
        }
        else if (coordinates.Count > 0)
        {
            string value = "";
            foreach (Coordinate coordinate in coordinates)
            {
                value += map[coordinate.X, coordinate.Y];
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"rejected value: {value}");
            Console.BackgroundColor = ConsoleColor.Black;
            coordinates.Clear();
            addToResult = false;
        }
        else
        {
            coordinates.Clear();
            addToResult = false;
        }
    }
}

//get result
foreach (string result in results)
{
    answer += int.Parse(result);
}

Console.WriteLine($"answer: {answer}");