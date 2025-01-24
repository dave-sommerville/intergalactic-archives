﻿namespace intergalactic_archives
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] orderedChar = new string[26]
            { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string[] charKey = new string[26]
            { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P"};

            string galacticVault = "D3|X2|C6|V5|J3|O4|K3|V1 U2|O4|X6|L5|K3|P3 U2|N4|J5|H6,Galaxara-IX,Galactic Cycle 4X-142,Sector H-21 - Unit 7,A" +
                " slender wand that vibrates subtly in the presence of spatial anomalies. Tests have shown it can slightly alter local spacetime fabric;" +
                " handle with care. Its origin remains a mystery fueling speculations about its use in wormhole creation or manipulation." +
                "Y4|H2|V3|G3|A2|C4|H4|R2|O3|V4 A2|H3|Z4|X1|A3|V3|W2|I2|Z4|Y4,Starithia-III,Galactic Cycle 3Y-517,Sector F-07 - Unit 10,Elegant eyewear with " +
                "lenses that reveal cosmic energy patterns when worn. Believed to have been used for studying astral phenomena or for interstellar navigation. " +
                "The frames adjust to fit any wearer suggesting a highly adaptive design.";

 

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
        private static void PrintSearchResult(Artifact artwork)
        {
            if (artwork != null)
            {
                Console.WriteLine("Search result:");
                Console.WriteLine(artwork.PrintArtifact());
            }
            else
            {
                Console.WriteLine("No matching artwork found.");
            }
        }
        private static void PrintArtifact(Artifact[] artArray)
        {
            string[] arrayDisplay = new string[artArray.Length];
            for (int i = 0; i < artArray.Length; i++)
            {
                Console.WriteLine(artArray[i].PrintArtifact());
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
             
        private static Artifact StringSplitter(ref string userInput, Artifact[] newArray, int count)
        {
            string[] inputStringArr = userInput.Split(",", 6);
            string[] objectInput = new string[5];


            for (int i = 0; i < 6; i++)
            {
                inputStringArr[i] = objectInput[i];
            }
            userInput = inputStringArr[5];
            newArray[count] = CreateObject(objectInput);
            
        }

        private static Artifact CreateObject(string[] splitInput)
        {
            if (splitInput.Length != 5)
            {
                Console.WriteLine("Array must have exactly five elements");
            }
            string[] nameArray = splitInput[0].Split("|");
            Artifact newArtifact = new Artifact(nameArray, splitInput[1], splitInput[2], splitInput[3], splitInput[4]);
            return newArtifact;
        }
    }
}
