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
            EncodedName = encodedName;
            Planet = planet;
            DiscoveryDate = discoveryDate;
            StorageLocation = storageLocation;
            Description = desciption;
        }
        //  Recursive function
        public string PrintArtifact()
        {
            string newLine = $"{EncodedName} | {Planet} | {DiscoveryDate} | {StorageLocation} | {Description}";
            return newLine;
        }
    }
}
