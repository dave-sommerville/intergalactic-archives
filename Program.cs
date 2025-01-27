namespace intergalactic_archives
{
    public class Program
    {
        static void Main(string[] args)
        {
            //  Processing Galactic Vault 
            string galacticVault = "./galactic_vault";
            Artifact[] summaries = new Artifact[1];
            Archive artifactArchive = new Archive(summaries);
            string savedSummary = "expedition_summary.txt";
            string[] vaultProcessor = ResearchDrone.ReadFile(galacticVault);
            for (int i = 0; i < vaultProcessor.Length; i++)
            {

            }

            Console.WriteLine("Welcome ranger");
            Console.WriteLine("Please choose one of the following options:\n1) Search Artifacts by Name 2) List Artifact Names 3) Add New Artifact From Field 4) Save Journal and Exit");
            int decision = PrintMenu(4);
            bool is_Running = true;


            while (is_Running)
            {
                switch(decision + 1)
                {
                    case 1:
                        //  Search prompt 
                        //  Show results 
                        //  Print main menu again
                        break;
                    case 2:
                        //  Call to static method 
                        //  Print main menu
                        break;
                    case 3:
                        //  Entry prompts 
                        //  Print artifact 
                        //  Print Main Menu
                        break;
                    case 4:
                        is_Running = false;
                        break;
                    default:
                        //  Print main menu 
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
            string[] objectInput = new string[5];

                    //  Might want to make this a constant 
            for (int i = 0; i < 6; i++)
            {
                inputStringArr[i] = objectInput[i];
            }
            userInput = inputStringArr[5];
            Artifact newArtifact = CreateArtifact(objectInput);
            summaryArray = InsertArtifact(newArtifact,ref summaryArray);
            StringSplitter(userInput, ref summaryArray); // Recurrsion, I think it will work 
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
        //private void SortArtifact()
        //{
        //    for (int i = 1; i < artifactArray.Length; i++)
        //    {
        //        Artifact key = artifactArray[i];
        //        string keyDecodedName = key.DecodedName;
        //        int insertPos = BinarySearch(keyDecodedName);
        //        for (int j = i - 1; j >= insertPos; j--)
        //        {
        //            artifactArray[j + 1] = artifactArray[j];
        //        }
        //        artifactArray[insertPos] = key;
        //    }
        //}
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
    }
}
