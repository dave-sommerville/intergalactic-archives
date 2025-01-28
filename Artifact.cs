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
            DecodedName = decodedName ?? throw new ArgumentNullException(nameof(decodedName), "Decoded name cannot be null");
            Planet = planet ?? throw new ArgumentNullException(nameof(planet), "Planet cannot be null");
            DiscoveryDate = discoveryDate ?? throw new ArgumentNullException(nameof(discoveryDate), "Discovery date cannot be null");
            StorageLocation = storageLocation ?? throw new ArgumentNullException(nameof(storageLocation), "Storage location cannot be null");
            Description = description ?? throw new ArgumentNullException(nameof(description), "Description cannot be null");
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
