namespace ConsoleApp1
{
    class Program
    {
        public static string[] args;

        Program(string[] args)
        {
            Program.args = args;
        }
        
        private void Run()
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Validation error: Please submit a filepath as argument.");
                return;
            }

            var filepath = args[0];

            try 
            {
                var strCount = FileUtil.CountFilenameOccurencesInFile(filepath); 
                Console.WriteLine($"Found {strCount} occurences of filename in file {filepath}.");  
            } 
            catch (ValidationException ve) 
            {
                Console.WriteLine($"Validation error: {ve.Message}"); 
            }
            catch (Exception e) 
            {
                Console.WriteLine($"Unknown error: {e.Message}"); 
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program(args);
            program.Run();

            // Console.WriteLine("Press any key to close program");
            // Console.Read();
        }
    }
}
