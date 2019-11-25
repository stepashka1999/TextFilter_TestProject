using System.Collections.Generic;
using System.IO;

namespace TextFilter_TestProject
{
    class TextFragmentDeleter
    {
        private StreamReader streamReader;
        private char[] PunctMark = {'.', ',', '-', '—', '!', '?', ' '};
        private List<string[]> tempFile = new List<string[]>();

        public TextFragmentDeleter(string[] FileName)
        {
            foreach(var file in FileName)
            {
                streamReader = new StreamReader(file);
                var temp = streamReader.ReadToEnd().Split("\n");
                tempFile.Add( temp);
            }
        }

        public List<string> DeletFromFile(int numOfSimbols, bool delMark = false)
        {
            var file = new List<string>();

            foreach (var fileFromList in tempFile)
            { 
                file.Add( GetText(fileFromList, numOfSimbols, delMark) );
            }
            return file;
        }


        private string GetText(string[] tempLine, int numOfSimbols, bool delMark)
        {
            string file = "";
            bool lastIsMark = false;

            List<string[]> listOfWords = new List<string[]>();

            for (int i = 0; i < tempLine.Length; i++)
            {
                if (delMark) listOfWords.Add(tempLine[i].Split(PunctMark));
                else listOfWords.Add(tempLine[i].Split(" "));
            }

            foreach (var line in listOfWords)
            {
                foreach (var word in line)
                {
                    if (delMark)
                    {
                        if (word.Length >= numOfSimbols) continue;
                        else file = file + word + " ";
                    }
                    else if (!delMark)
                    {
                        foreach (var simbol in PunctMark)
                        {
                            if (word.Length > 0 && word[0] != '\r') 
                            {
                                if (word[word.Length - 1] == '\r') 
                                {
                                    if (simbol == word[word.Length - 2])
                                    {
                                        if (word.Length - 2 >= numOfSimbols) file = file + word[word.Length - 2].ToString() + " ";
                                        else file = file + word + " ";

                                        lastIsMark = true;
                                        break;
                                    }
                                }
                                else if (simbol == word[word.Length - 1])
                                {
                                    if (word.Length - 1 >= numOfSimbols) file = file + word[word.Length - 1].ToString() + " ";
                                    else file = file + word + " ";

                                    lastIsMark = true;
                                    break;
                                }
                            }
                        }
                        if (!lastIsMark)
                        {
                            if (word.Length >= numOfSimbols) file += " ";
                            else file = file + word + " ";
                            continue;
                        }
                        else if (lastIsMark)
                        {
                            lastIsMark = false;
                            continue;
                        }
                    }
                }
                file += "\n";
            }
            return file;
        }
    }
}
