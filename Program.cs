namespace intergalactic_archives
{
    public class Program
    {
        private static readonly string[] orderedChar = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static readonly string[] charKey = { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P" };
        private static readonly string[] reversedChar = { "Z", "Y", "X", "W", "V", "U", "T", "S", "R", "Q", "P", "O", "N", "M", "L", "K", "J", "I", "H", "G", "F", "E", "D", "C", "B", "A" };
        static void Main(string[] args)
        {
            const string GALACTIC_VAULT = "data/galactic_vault.txt";
            const string SAVED_SUMMARY = "data/expedition_summary.txt";
            Artifact[] summaries = new Artifact[0];
            try
            {
                string[] vaultProcessor = ResearchDrone.ReadFile(GALACTIC_VAULT);
                string[] artifactArchive = ResearchDrone.ReadFile(SAVED_SUMMARY);
                summaries = PopulateFromArchives(artifactArchive);

                if (vaultProcessor != null)
                {
                    for (int i = 0; i < vaultProcessor.Length; i++)
                    {
                        StringDecryptor(vaultProcessor[i], ref summaries);
                    }
                }

                Console.WriteLine("Welcome Back Ranger");
                bool isRunning = true;
                while (isRunning)
                {
                    Console.WriteLine("Please choose one of the following options:\n1) Search/Add Artifacts by Name\n2) List Artifact Names\n3) Save Journal and Exit\n");
                    int decision = PrintMenu(3);
                    switch (decision)
                    {
                        case 1:
                            string targetArtifact = ReturnValidString("Enter the artifact name: ");
                            SearchByName(targetArtifact, ref summaries, out bool isInSumArr);
                            if (!isInSumArr)
                            {
                                Console.WriteLine("Would you like to add a new Artifact?\n1) Yes\n2) No\n");
                                int addDecision = PrintMenu(2);
                                if (addDecision == 1)
                                {
                                    Artifact newArtifact = UserEnteredArtifact();
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
                            try
                            {
                                ResearchDrone.WriteFile(SAVED_SUMMARY, summaries);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error saving file: {e.Message}");
                            }
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
        //      RETREIVE EXPEDITION SUMMARIES 
        public static Artifact[] PopulateFromArchives(string[] archiveInput)
        {
            Artifact[] archivedSummaries = new Artifact[archiveInput.Length];
            for (int i = 0; i < archivedSummaries.Length; i++)
            {
                string[] splitLineInput = archiveInput[i].Split(",");
                archivedSummaries[i] = new Artifact(splitLineInput[0], splitLineInput[1], splitLineInput[2], splitLineInput[3], splitLineInput[4]);
            }
            return archivedSummaries;
        }
        //      USER INTERFACE
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
        private static Artifact UserEnteredArtifact()
        {
            string name = ReturnValidString("Please enter item name:");
            string planet = ReturnValidString("Please enter planet of origin:");
            string date = ReturnValidString("Please enter date of discovery:");
            string location = ReturnValidString("Please enter location of discovery:");
            string description = ReturnValidString("Please enter description of item:");
            Artifact userEntry = new Artifact(name, planet, date, location, description);
            return userEntry;
        }
        //  ENCRYPTION PROCESSING
        private static void StringDecryptor(string userInput, ref Artifact[] summaryArray)
        {
            string[] inputStringArr = userInput.Split(",", 6);
            if (inputStringArr.Length < 5) return;

            Artifact newArtifact = CreateDecryptedArtifact(inputStringArr);
            summaryArray = InsertArtifact(newArtifact, ref summaryArray);
        }
        private static string DecodeChar(string codeChar, int cycle)
        {
            string letterPart = codeChar.Substring(0, 1);
            int numberPart = int.Parse(codeChar.Substring(1));
            if (cycle < numberPart)
            {
                for (int i = 0; i < orderedChar.Length; i++)
                {
                    if (orderedChar[i] == letterPart)
                    {
                        letterPart = charKey[i];
                        break;
                    }
                }
                return DecodeChar(letterPart + numberPart.ToString(), cycle + 1);
            }
            else
            {
                return letterPart;
            }
        }
        public static string DecodeName(string[] encodedInput)
        {
            string[] outputArr = new string[encodedInput.Length];
            for (int i = 0; i < encodedInput.Length; i++)
            {
                if (encodedInput[i].Contains(" "))
                {
                    string[] wordBrArr = encodedInput[i].Split(" ");
                    for (int j = 0; j < wordBrArr.Length; j++)
                    {
                        wordBrArr[j] = DecodeChar(wordBrArr[j], 1);
                    }
                    string output = string.Join(" ", wordBrArr);
                    outputArr[i] = output;
                }
                else
                {
                    outputArr[i] = DecodeChar(encodedInput[i], 1);
                }
            }
            string finalMapping = string.Join("", outputArr);
            return ReverseMap(finalMapping);
        }
        private static string ReverseMap(string strInput)
        {
            string[] charOutput = new string[strInput.Length];

            for (int i = 0; i < strInput.Length; i++)
            {
                char currentChar = strInput[i];
                if (currentChar == ' ')
                {
                    charOutput[i] = " ";
                }
                else
                {
                    for (int j = 0; j < orderedChar.Length; j++)
                    {
                        if (orderedChar[j].ToUpper() == currentChar.ToString().ToUpper())
                        {
                            charOutput[i] = reversedChar[j];
                            break;
                        }
                    }
                }
            }
            return string.Join("", charOutput); 
        }
        //      OBJECT CREATION FROM DECRYPTED INPUT
        private static Artifact CreateDecryptedArtifact(string[] splitInput)
        {
            if (splitInput.Length != 5)
            {
                throw new ArgumentException("Input array must have exactly five elements.");
            }

            string[] nameArray = splitInput[0].Split("|");
            return new Artifact(DecodeName(nameArray), splitInput[1], splitInput[2], splitInput[3], splitInput[4]);
        }
        public static Artifact[] InsertArtifact(Artifact artifactToInsert, ref Artifact[] summaryArray)
        {
            Artifact[] summaryUpload = new Artifact[summaryArray.Length + 1];
            int insertPos = BinarySearch(artifactToInsert.DecodedName, ref summaryArray);

            if (insertPos >= 0)
            {
                Console.WriteLine("Artifact already exists.");
                return summaryArray;
            }
            insertPos = ~insertPos;
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
        //  BINARY SEARCH HELPER FUNCTION
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
    }
}
