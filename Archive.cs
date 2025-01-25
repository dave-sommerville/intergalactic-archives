namespace intergalactic_archives
{
    public class Archive
    {
        private Artifact[] artifactArray = new Artifact[0];
        private void SortArtifact()
        {
            for (int i = 1; i < artifactArray.Length; i++)
            {
                Artifact key = artifactArray[i];
                string keyDecodedName = key.DecodedName;
                int insertPos = BinarySearchByName(keyDecodedName);
                for (int j = i - 1; j >= insertPos; j--)
                {
                    artifactArray[j + 1] = artifactArray[j];
                }
                artifactArray[insertPos] = key;
            }
        }
        public Artifact[] InsertArtifact(Artifact artifactToInsert)
        {
                ResizeArray(1);
                for (int i = 0; i < artifactArray.Length; i++)
            {
                if (artifactArray[i] != null && BinarySearch(artifactArray[i].DecodedName) > i)
                {

                }
            }
        }


        private Artifact[] ResizeArray(int byNum)
        {
            Artifact[] newArray = new Artifact[artifactArray.Length + byNum];
            return newArray;
        }
        public void SearchByDecodedName(string decodedName)
        {
            int index = BinarySearch(decodedName);

            if (index >= 0)
            {
                Console.WriteLine($"Artifact found: DecodedName = {artifactArray[index].DecodedName}");
            }
            else
            {
                Console.WriteLine($"Artifact with DecodedName '{decodedName}' not found.");
                Console.WriteLine("Would you like to create a new artifact with this name? (y/n)");
               // Print Menu 
            }
        }
        private int BinarySearch(string decodedName)
        {
            int low = 0;
            int high = artifactArray.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int comparison = string.Compare(artifactArray[mid].DecodedName, decodedName, StringComparison.OrdinalIgnoreCase);

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
