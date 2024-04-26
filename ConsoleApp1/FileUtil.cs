using System.Text.RegularExpressions;
namespace ConsoleApp1
{
    public static class FileUtil
    {
        private const long MaxFileSize = 10 * 1024 * 1024; //10MB

        public static int CountFilenameOccurencesInFile(string filepath) 
        {
            var fileInfo = new FileInfo(filepath);

            if (!fileInfo.Exists) 
            {
                throw new ValidationException($"File not found for path {filepath}");
            }

            if (fileInfo.Length > MaxFileSize) 
            {
                throw new ValidationException($"File is too big ({fileInfo.Length.DisplayAsMb()} MB). Max filesize is {MaxFileSize.DisplayAsMb()} MB.");
            }

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);

            return CountStrOccurencesInFile(fileNameWithoutExtension, fileInfo);
        }

        private static int CountStrOccurencesInFile(string searchString, FileInfo fileInfo) 
        {
            Regex regex = new Regex(searchString);

            int counter = 0;

            try
            {	
                using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        while (!sr.EndOfStream)
                        {
                            counter += regex.Matches(sr.ReadLine()).Count;
                        }
                    }
                }
            }
            catch (IOException)
            {
                throw new ValidationException($"File {fileInfo.FullName} unavailable, make sure the file is not beeing used by another process.");
            }

            return counter;
        }

        private static double DisplayAsMb(this long bytes) 
        {
            return Math.Round(bytes * 1.0 / 1024 / 1024, 2);
        }
    }
}