namespace intergalactic_archives
{
    public class Program
    {
        static void Main(string[] args)
        {
            string galacticVault = "data/galactic_vault.txt";
            Artifact[] summaries = new Artifact[0];
            string savedSummary = "expedition_summary.txt";

            try
            {
                string[] vaultProcessor = ResearchDrone.ReadFile(galacticVault);
                for (int i = 0; i < vaultProcessor.Length; i++)
                {
                    StringSplitter(vaultProcessor[i], ref summaries);
                }

                Console.WriteLine("Welcome ranger");
                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("Please choose one of the following options:\n1) Search/Add Artifacts by Name\n2) List Artifact Names\n3) Save Journal and Exit");
                    int decision = PrintMenu(3);

                    switch (decision)
                    {
                        case 1:
                            string targetArtifact = ReturnValidString("Enter the artifact name: ");
                            SearchByName(targetArtifact, ref summaries, out bool isInSumArr);
                            if (!isInSumArr)
                            {
                                Console.WriteLine("Would you like to add a new Artifact?\n1) Yes\n2) No");
                                int addDecision = PrintMenu(2);
                                if (addDecision == 1)
                                {
                                    Artifact newArtifact = CreateArtifact(UserEnteredArtwork());
                                    summaries = InsertArtifact(newArtifact, ref summaries);
                                }
                            }
                            break;
                        case 2:
                            foreach (Artifact artifact in summaries)
                            {
                                Console.WriteLine(artifact.PrintName());
                            }
                            break;
                        case 3:
                            ResearchDrone.WriteFile(savedSummary, summaries);
                            isRunning = false;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
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

        private static string ReturnValidString(string prompt)
        {
            Console.WriteLine(prompt);
            string input;
            do
            {
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrEmpty(input));
            return input;
        }

        private static string[] UserEnteredArtwork()
        {
            string[] propertyArray = new string[4];
            propertyArray[0] = ReturnValidString("Please enter Title:");
            propertyArray[1] = ReturnValidString("Please enter Artist:");
            propertyArray[2] = ReturnValidString("Please enter Year:");
            propertyArray[3] = ReturnValidString("Please enter Medium:");
            return propertyArray;
        }

        private static void StringSplitter(string userInput, ref Artifact[] summaryArray)
        {
            string[] inputStringArr = userInput.Split(",", 6);
            if (inputStringArr.Length < 5) return;

            Artifact newArtifact = CreateArtifact(inputStringArr);
            summaryArray = InsertArtifact(newArtifact, ref summaryArray);
        }

        private static Artifact CreateArtifact(string[] splitInput)
        {
            if (splitInput.Length != 5)
            {
                throw new ArgumentException("Input array must have exactly five elements.");
            }

            string[] nameArray = splitInput[0].Split("|");
            return new Artifact(nameArray, splitInput[1], splitInput[2], splitInput[3], splitInput[4]);
        }

        public static Artifact[] InsertArtifact(Artifact artifactToInsert, ref Artifact[] summaryArray)
        {
            Artifact[] summaryUpload = new Artifact[summaryArray.Length + 1];
            int insertPos = BinarySearch(artifactToInsert.DecodedName, ref summaryArray);

            if (insertPos < 0) insertPos = ~insertPos;

            for (int i = 0; i < insertPos; i++)
            {
                summaryUpload[i] = summaryArray[i];
            }

            summaryUpload[insertPos] = artifactToInsert;

            for (int i = insertPos; i < summaryArray.Length; i++)
            {
                summaryUpload[i + 1] = summaryArray[i];
            }

            return summaryUpload;
        }

        private static int BinarySearch(string decodedName, ref Artifact[] summaryArray)
        {
            int low = 0;
            int high = summaryArray.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int comparison = string.Compare(summaryArray[mid].DecodedName, decodedName, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    return mid;
                }
                else if (comparison < 0)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return ~low;
        }

        public static void SearchByName(string decodedName, ref Artifact[] summaryArray, out bool isInSum)
        {
            int index = BinarySearch(decodedName, ref summaryArray);
            isInSum = index >= 0;

            if (isInSum)
            {
                Console.WriteLine($"Artifact found:\n{summaryArray[index].PrintArtifact()}");
            }
            else
            {
                Console.WriteLine($"Artifact with Name '{decodedName}' not found.");
            }
        }
    }
}
