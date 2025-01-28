using System.Runtime.CompilerServices;

namespace intergalactic_archives
{

    public class Artifact
    {
        public string DecodedName;
        public string Planet;
        public string DiscoveryDate;
        public string StorageLocation;
        public string Description;
        public Artifact(string decodedName, string planet, string discoveryDate, string storageLocation, string description)
        {
            DecodedName = decodedName;
            Planet = planet;
            DiscoveryDate = discoveryDate;
            StorageLocation = storageLocation;
            Description = description;
        }
        public string PrintArtifact()
        {
            return $"Name: {DecodedName}\nPlanet: {Planet}\nDiscovery Date: {DiscoveryDate}\nStorage Location: {StorageLocation}\nDescription: {Description}\n\n";
        }
        public string PrintName()
        {
            return $"{DecodedName}\n";
        }
    }
}
