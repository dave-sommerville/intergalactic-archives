namespace intergalactic_archives
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] orderedChar = new string[26]
            { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string[] charKey = new string[26]
            { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P"};
        }
        private static int PrintMenu(int options)
        {
            int intDecision;
            bool isValid;
            do
            {
                string decision = Console.ReadLine();
                isValid = int.TryParse(decision, out intDecision) && intDecision >= 1 && intDecision <= options;
                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!isValid);
            return intDecision;
        }
        private static void PrintSearchResult(Artwork artwork)
        {
            if (artwork != null)
            {
                Console.WriteLine("Search result:");
                Console.WriteLine(artwork.PrintArtwork());
            }
            else
            {
                Console.WriteLine("No matching artwork found.");
            }
        }
        private static void PrintArray(Artwork[] artArray)
        {
            string[] arrayDisplay = new string[artArray.Length];
            for (int i = 0; i < artArray.Length; i++)
            {
                Console.WriteLine(artArray[i].PrintArtwork());
            }
        }
        private static string ValidatedString()
        {
            string input;
            bool isValid;

            do
            {
                input = Console.ReadLine()?.Trim();
                isValid = !string.IsNullOrEmpty(input);

            } while (!isValid);
            return input;
        }

        private static string[] UserEnteredArtwork()
        {
            string[] propertyArray = new string[4];
            Console.WriteLine("Please enter Title:");
            propertyArray[0] = ValidatedString();
            Console.WriteLine("Please enter Artist:");
            propertyArray[1] = ValidatedString();
            Console.WriteLine("Please enter Year:");
            propertyArray[2] = ValidatedString();
            Console.WriteLine("Please enter Medium:");
            propertyArray[3] = ValidatedString();
            return propertyArray;
        }

        private static string[] SplitLine(string line, string splitChar, int splitSize)
        {
            string[] splitText = line.Split(splitChar);
            for (int i = 0; i < splitSize; i++)
            {
                splitText[i] = splitText[i].Trim();
            }
            return splitText;
        }
        private static Artifact CreateObject(string[] splitInput)
        {
            if (splitInput.Length != 5)
            {
                Console.WriteLine("Array must have exactly five elements");
            }
            string[] nameArray = SplitLine(splitInput[0], "|", 2);
            Artifact newArtifact = new Artifact(nameArray, splitInput[1], splitInput[2], splitInput[3], splitInput[4]);
            return newArtifact;
        }
    }
}
