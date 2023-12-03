using dag3_2;

List<string> lines = File.ReadAllLines("input.txt").ToList();
int answer = 0;
string[,] map = new string[lines.Count, lines[0].Length];

#region buildMap

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = lines[i][j].ToString();
    }
}

#endregion

#region getNumbers

string specialCharString = "~!@#$%^&*()_+=-`[];',/?><\":}{";

List<Coordinate> coordinates = new List<Coordinate>();
List<List<Coordinate>> numberCoordinatesList = new List<List<Coordinate>>();
SearchDirection[] directions = new SearchDirection[]
{
    SearchDirection.TOP, SearchDirection.TOPRIGHT, SearchDirection.RIGHT, SearchDirection.BOTTOMRIGHT,
    SearchDirection.BOTTOM, SearchDirection.BOTTOMLEFT, SearchDirection.LEFT, SearchDirection.TOPLEFT
};
bool addToResult = false;
Coordinate star = null;
List<string> results = new List<string>();
List<Coordinate> starList = new List<Coordinate>();

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
                    map[x, y].Contains('*'))
                {
                    addToResult = true;
                    star = new Coordinate(x, y);
                }
            }
        }
        else if (coordinates.Count > 0 && addToResult)
        {
            bool isNewStar = true;
            foreach (Coordinate coordinate in starList)
            {
                if(coordinate.CompareTo(star) == 0) isNewStar = false;
            }
            if(isNewStar) starList.Add(star);
            string value = "";
            foreach (Coordinate coordinate in coordinates)
            {
                value += map[coordinate.X, coordinate.Y];
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"accepted value: {value}");
            Console.BackgroundColor = ConsoleColor.Black;

            results.Add(value);
            numberCoordinatesList.Add(coordinates);
            coordinates = new List<Coordinate>();
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

#endregion

#region getResult

foreach (Coordinate starCoordinate in starList)
{
    List<List<Coordinate>> connectedNumbers = new List<List<Coordinate>>();
    Console.WriteLine($"starCoordinate: {starCoordinate.X}, {starCoordinate.Y}");

    foreach (SearchDirection direction in directions)
    {
        int x = starCoordinate.X;
        int y = starCoordinate.Y;
                
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
        Coordinate currentCoordinate = new Coordinate(x, y);

        foreach (List<Coordinate> numberCoordinates in numberCoordinatesList)
        {
            foreach (Coordinate coordinate in numberCoordinates)
            {
                if (coordinate.CompareTo(currentCoordinate) == 0 && !connectedNumbers.Contains(numberCoordinates))
                {
                    connectedNumbers.Add(numberCoordinates);
                }
            }
        }
    }

    if (connectedNumbers.Count == 2)
    {
        string firstNumber = ConvertCoordinateListToString(connectedNumbers[0]);
        string secondNumber = ConvertCoordinateListToString(connectedNumbers[1]); 
        
        answer += (int.Parse(firstNumber) * int.Parse(secondNumber));
    }
}

Console.WriteLine($"answer: {answer}");
                                       
#endregion

string ConvertCoordinateListToString(List<Coordinate> coordinates)
{
    string result = "";
    foreach (Coordinate coordinate in coordinates)
    {
        result += map[coordinate.X, coordinate.Y];
    }

    return result;
}
{
    
}