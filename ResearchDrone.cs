using System;


namespace intergalactic_archives
{
    public class ResearchDrone
    {
        public static string[] ReadFile(string filepath)
        {
            try
            {
                return File.ReadAllLines(filepath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The specified file was not found");
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to access this file");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                throw;
            }
        }

        public static void WriteFile(string filepath, Artifact[] artifacts)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    foreach (Artifact artifact in artifacts)
                    {
                        // Write the encoded name (split into parts using "|") and other artifact details
                        string encodedName = string.Join("|", artifact.EncodedName);
                        writer.WriteLine($"{encodedName},{artifact.Planet},{artifact.DiscoveryDate},{artifact.StorageLocation},{artifact.Description}");
                    }
                }
                Console.WriteLine("Artifacts have been successfully saved.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to write to this file.");
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The directory was not found.");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                throw;
            }
        }
    }
}
