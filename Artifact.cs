using System.Runtime.CompilerServices;

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

        private static readonly string[] orderedChar = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static readonly string[] charKey = { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P" };

        public Artifact(string[] encodedName, string planet, string discoveryDate, string storageLocation, string description)
        {
            EncodedName = encodedName;
            DecodedName = DecodeName(encodedName);
            Planet = planet;
            DiscoveryDate = discoveryDate;
            StorageLocation = storageLocation;
            Description = description;
        }

        public string DecodeName(string[] encodedInput)
        {
            return DecodeNameRecursive(encodedInput, 0);
        }

        private string DecodeNameRecursive(string[] encodedInput, int index)
        {
            if (index >= encodedInput.Length)
                return "";

            string word = encodedInput[index];
            string decodedWord = DecodeWordRecursive(word, 0);

            return decodedWord + " " + DecodeNameRecursive(encodedInput, index + 1);
        }
        //  Needs the shotgun here 

        private string DecodeWordRecursive(string word, int index)
        {
            if (index >= word.Length)
                return "";

            int charIndex = Array.IndexOf(charKey, word[index].ToString().ToUpper());
            string decodedChar = charIndex >= 0 ? orderedChar[charIndex] : word[index].ToString();

            return decodedChar + DecodeWordRecursive(word, index + 1);
        }

        public string PrintArtifact()
        {
            return $"Name: {DecodedName}\nPlanet: {Planet}\nDiscovery Date: {DiscoveryDate}\nStorage Location: {StorageLocation}\nDescription: {Description}";
        }

        public string PrintName()
        {
            return DecodedName;
        }
    }
}
