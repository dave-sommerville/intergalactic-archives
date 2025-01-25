﻿namespace intergalactic_archives
{
    public class Artifact
    {
        public string[] EncodedName;
        public string DecodedName;
        public string Planet;
        public string DiscoveryDate;
        public string StorageLocation;
        public string Description;

        public Artifact(string[] encodedName, string decodedName, string planet, string discoveryDate, string storageLocation, string desciption)
        {
            string[] orderedChar = new string[26]
                { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string[] charKey = new string[26]
                { "H", "Z", "A", "U", "Y", "E", "K", "G", "O", "T", "I", "R", "J", "V", "W", "N", "M", "F", "Q", "S", "D", "B", "X", "L", "C", "P"};

            EncodedName = encodedName;
            DecodedName = decodedName; //(Must further split the encodedName array *
          Planet = planet;
            DiscoveryDate = discoveryDate;
            StorageLocation = storageLocation;
            Description = desciption;
        }

        public void WhiteSpaceAdjust()
        {
            for (int i = 0; i < EncodedName.Length; i++)
            {
                if (EncodedName[i].Contains(" "))
                {
                    ResizeArray(2);
                    string[] wordBreak = EncodedName[i].Split("");
                    for (int j = EncodedName.Length - 1; j > i + wordBreak.Length - 1; j--)
                    {
                        EncodedName[j] = EncodedName[j - wordBreak.Length - 1];
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        EncodedName[i + k] = wordBreak[k];
                    }
                    i += wordBreak.Length - 1;
                }
            }
        }
        private void ResizeArray(int byNum)
        {
            string[] newArray = new string[EncodedName.Length + byNum];
            EncodedName = newArray;
        }
        // I think this whole thing is whack 
        //public string DecodeItemName(string encodedInput, int cycle, int maxCycle, string[] startArr, string[] endArr)
        //{
        //    if (cycle < maxCycle)
        //    {
        //        for (int i = 0; i < startArr.Length; i++)
        //        {
        //            if (startArr[i] == encodedInput)
        //            {
        //                encodedInput = endArr[i];
        //            }
        //        }
        //        return DecodeItemName(encodedInput, cycle + 1, maxCycle, startArr, endArr);
        //    } else
        //    {
        //        return encodedInput;
        //    }
        //}

        public string PrintArtifact()
        {
            string newLine = $"{EncodedName} | {Planet} | {DiscoveryDate} | {StorageLocation} | {Description}";
            return newLine;
        }
    }
}
