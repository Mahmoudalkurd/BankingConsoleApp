using BankingConsoleApp.Database;

class Program
{
    static void Main()
    {
        DbInitializer.CreateDatabase();
        DbInitializer.CreateTables();


        DbInitializer.InsertSampleData();

        Console.WriteLine("🎉 Setup complete. Press any key to exit.");
        Console.ReadKey();
    }
}
