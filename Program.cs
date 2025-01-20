namespace intergalactic_archives
{
    public class Program
    {
        static void Main(string[] args)
        {
            
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
