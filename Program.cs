namespace ConsoleAppSquareMaster
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //WorldBuilder wb=new WorldBuilder();
            World world = new World();
            var w=world.BuildWorld2(100,100);
            for (int i = 0; i < w.GetLength(1); i++)
            {
                for(int j = 0; j < w.GetLength(0); j++)
                {
                    char ch;
                    if (w[j, i]) ch = '*'; else ch = ' ';
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
        }
    }
}
