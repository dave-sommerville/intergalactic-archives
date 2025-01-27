namespace intergalactic_archives
{
    public class Archive()
    {
        public Artifact[] ArtifactArray;
        public Archive(Artifact[] artifactArray) { 
            ArtifactArray = artifactArray;
        }
        //public void SearchByDecodedName(string decodedName)
        //{
        //    int index = BinarySearch(decodedName);

        //    if (index >= 0)
        //    {
        //        Console.WriteLine($"Artifact found: DecodedName = {artifactArray[index].DecodedName}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Artifact with DecodedName '{decodedName}' not found.");
        //        Console.WriteLine("Would you like to create a new artifact with this name? (y/n)");
        //       // Print Menu 
        //    }
        //}


    }
}
