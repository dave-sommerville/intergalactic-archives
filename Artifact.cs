namespace intergalactic_archives
{
    public class Artifact
    {
        public string[] EncodedName;
        public string DecodedName;
        public string Planet;
        public string DiscoveryDate;
        public string StorageLocation;
        public string Description;
        public string[] orderedChar = new string[26]
            { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        public string[] charKey = new string[26]
            { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P"};
        public string[] reverseChar = new string[26]
            {"Z", "Y", "X", "W", "V", "U", "T", "S", "R", "Q", "P", "O", "N", "M", "L", "K", "J", "I", "H", "G", "F", "E", "D", "C", "B", "A" };


        public Artifact(string[] encodedName, string planet, string discoveryDate, string storageLocation, string desciption)
        {

            EncodedName = encodedName;
            DecodedName = DecodeName(EncodedName); 
            Planet = planet;
            DiscoveryDate = discoveryDate;
            StorageLocation = storageLocation;
            Description = desciption;


        }
        public string DecodeName(string[] encodedInput)
        {
            string[] outputArr = new string[encodedInput.Length];
            for (int i = 0; i < encodedInput.Length; i++)
            {
                if (encodedInput[i].Contains(" "))
                {
                    string[] wordBrArr = encodedInput[i].Split(" ");
                    for (int j = 0; j < wordBrArr.Length; j++) {
                        wordBrArr[j] = DecodeChar(wordBrArr[j], 1);
                    } 
                    string output = string.Join(" ", wordBrArr);
                    outputArr[i] = output;
                    break;
                } else
                {
                    outputArr[i] = encodedInput[i];
                    break;
                }
            }
            return string.Join("", outputArr);
        }
        public string DecodeChar(string codeChar, int cycle)
        {
            string[] splitCode = codeChar.Split("");
            string letterReturn = splitCode[0];
            int maxCycle = ReturnValidInt(splitCode[1]);
            if (cycle < maxCycle)
            {
                for (int i = 0; i < orderedChar.Length; i++)
                {
                    if (orderedChar[i] == letterReturn)
                    {
                        letterReturn = charKey[i];
                        break;
                    }
                }
                return DecodeChar(codeChar, cycle + 1);
            }
            else
            {
                return letterReturn;
            }
        }
        private static int ReturnValidInt(string input)
        {
            int intOutput;
            bool isValid;
            do
            {
                isValid = int.TryParse(input, out intOutput) && intOutput >= 1;
                if (!isValid)
                {
                    Console.WriteLine("Invalid input.");        // Throw error
                }
            } while (!isValid);
            return intOutput;
        }

        public string PrintArtifact()
        {
            string newLine = $"{EncodedName} | {Planet} | {DiscoveryDate} | {StorageLocation} | {Description}";
            return newLine;
        }
    }
}
