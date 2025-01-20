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

        public Artifact(string[] encodedName, string planet, string discoveryDate, string storageLocation, string desciption)
        {
            string[] orderedChar = new string[26]
                { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string[] charKey = new string[26]
                { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P"};

            EncodedName = encodedName;                          //  Will need to 
            DecodedName = DecodeItemName(EncodedName[0], 1, EncodedName[1], orderedChar, charKey);
            Planet = planet;
            DiscoveryDate = discoveryDate;
            StorageLocation = storageLocation;
            Description = desciption;
        }
        //  Recursive function
        //  Need an int parse prior to using int (must be inside recursive func
        //  Need to loop through the name array and create a char for each entry 
        //  Join them into a regular word
        public string DecodeItemName(string encodedInput, int cycle, int maxCycle, string[] startArr, string[] endArr)
        {
            if (cycle < maxCycle)
            {
                for (int i = 0; i < startArr.Length; i++)
                {
                    if (startArr[i] == encodedInput)
                    {
                        encodedInput = endArr[i];
                    }
                }
                return DecodeItemName(encodedInput, cycle + 1, maxCycle, startArr, endArr);
            } else
            {
                return encodedInput;
            }
        }

        public string PrintArtifact()
        {
            string newLine = $"{EncodedName} | {Planet} | {DiscoveryDate} | {StorageLocation} | {Description}";
            return newLine;
        }
    }
}
