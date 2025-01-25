namespace intergalactic_archives
{
    public class Archive
    {
        private Artifact[] artifactArray = new Artifact[0];
        private void SortArtifactArrayByDecodedName()
        {
            int n = artifactArray.Length;

            for (int i = 1; i < n; i++)
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
        public void Insert(Artifact artifactToInsert)
        {
            if (IsArtifact(artifactToInsert.DecodedName))
            {
                Console.WriteLine($"Artifact with decodedName '{artifactToInsert.DecodedName}' already exists.");
                return;
            }

            ResizeArray();
            artifactArray[artifactArray.Length - 1] = artifactToInsert;

            Console.WriteLine($"Artifact with DecodedName '{artifactToInsert.DecodedName}'");
        }
        private bool IsArtifact(string decodedName)
        {
            return artifactArray.Any(artifact => artifact.DecodedName.Equals(decodedName, StringComparison.OrdinalIgnoreCase));
        }
        private void ResizeArray()
        {
            int newLength = artifactArray.Length + 1;
            Array.Resize(ref artifactArray, newLength);
        }
        public void SearchByDecodedName(string decodedName)
        {
            int index = BinarySearchByName(decodedName);

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
        private int BinarySearchByName(string decodedName)
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
