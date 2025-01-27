namespace intergalactic_archives
{
    public class Program
    {
        static void Main(string[] args)
        {
            //  Processing Galactic Vault 
            string galacticVault = "./galactic_vault";  //  Make Constants 
            Artifact[] summaries = new Artifact[0];
            string savedSummary = "expedition_summary.txt";
            string[] vaultProcessor = ResearchDrone.ReadFile(galacticVault);
            for (int i = 0; i < vaultProcessor.Length; i++)
            {
                StringSplitter(vaultProcessor[i], ref summaries);
            }

            Console.WriteLine("Welcome ranger");
            Console.WriteLine("Please choose one of the following options:\n1) Search/Add Artifacts by Name 2) List Artifact Names 3) Save Journal and Exit");
            int decision = PrintMenu(4);
            bool isRunning = true;
            bool isInSumArr = false;

            while (isRunning)
            {
                switch(decision)
                {
                    case 1:
                        string targetArtifact = ReturnValidString();
                        SearchByName(targetArtifact, ref summaries, ref isInSumArr);
                        if (!isInSumArr)
                        {
                            Console.WriteLine("Would you like to add a new Artifact?\n1) Yes 2) No");
                            int addDecision = PrintMenu(2);
                            if (addDecision == 1)
                            {
                                Artifact newArtifact = CreateArtifact(UserEnteredArtwork());
                                summaries = InsertArtifact(newArtifact, ref summaries);
                            } else
                            {
                                break;
                            }
                        }
                        break;
                    case 2:
                        for (int i = 0; i < summaries.Length; i++)
                        {
                            Console.WriteLine(summaries[i].PrintName);
                        }
                        break;
                    case 3:
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }
        /* TO DO
         * Design user experience  
         * Saving to txt file 
         * Error Throw and Catch 
        */
        //  _______________________________________________________
        //  MENU INTERFACE / DISPLAYS
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
        //  _______________________________________________________
        //  MANUAL INPUT 

        private static string ReturnValidString() // I think this needs a return if string invalid 
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
            propertyArray[0] = ReturnValidString();
            Console.WriteLine("Please enter Artist:");
            propertyArray[1] = ReturnValidString();
            Console.WriteLine("Please enter Year:");
            propertyArray[2] = ReturnValidString();
            Console.WriteLine("Please enter Medium:");
            propertyArray[3] = ReturnValidString();
            return propertyArray;
        }

        //  _______________________________________________________
        //  TXT PROCESSOR 

        private static void StringSplitter(string userInput, ref Artifact[] summaryArray)
        {
            string[] inputStringArr = userInput.Split(",", 6);
            if (inputStringArr.Length < 6) return;
            string[] objectInput = new string[5];
            for (int i = 0; i < objectInput.Length; i++)
            {
                objectInput[i] = inputStringArr[i];
            }
            userInput = inputStringArr[5];
            Artifact newArtifact = CreateArtifact(objectInput);
            summaryArray = InsertArtifact(newArtifact, ref summaryArray);
            if (!string.IsNullOrEmpty(userInput))
            {
                StringSplitter(userInput, ref summaryArray);
            }
        }
        private static Artifact CreateArtifact(string[] splitInput)
        {
            if (splitInput.Length != 5)
            {
                Console.WriteLine("Array must have exactly five elements");
            }
            string[] nameArray = splitInput[0].Split("|");
            Artifact newArtifact = new Artifact(nameArray, splitInput[1], splitInput[2], splitInput[3], splitInput[4]);
            return newArtifact;
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

            return -1; // Artifact not found
        }
        public static void SearchByName(string decodedName, ref Artifact[] summaryArray,ref bool isInSum)
        {
            int index = BinarySearch(decodedName, ref summaryArray);
            if (index >= 0)
            {
                Console.WriteLine($"Artifact found: Unencrypted Name = {summaryArray[index].PrintArtifact()}");
                isInSum = true;
            }
            else
            {
                Console.WriteLine($"Artifact with Name '{decodedName}' not found.");
            }
        }
    }
}
