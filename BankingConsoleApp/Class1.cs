using System;
using System.Data.SqlClient;

namespace BankingConsoleApp.Database
{
    public static class DbInitializer
    {
        public static void CreateDatabase()
        {
            string connectionString = "Server=(localdb)\\BankDbInstance;Integrated Security=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("IF DB_ID('BankDb') IS NULL CREATE DATABASE BankDb;", connection);
                command.ExecuteNonQuery();
                Console.WriteLine("✅ Database 'BankDb' created (or already exists).");
            }
        }

        public static void CreateTables()
        {
            string connectionString = "Server=(localdb)\\BankDbInstance;Database=BankDb;Integrated Security=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    IF OBJECT_ID('Kunder', 'U') IS NULL
                    CREATE TABLE Kunder (
                        kunder_id INT PRIMARY KEY IDENTITY(1,1),
                        namn NVARCHAR(100) NOT NULL
                    );

                    IF OBJECT_ID('Konton', 'U') IS NULL
                    CREATE TABLE Konton (
                        konton_id INT PRIMARY KEY IDENTITY(1,1),
                        kunder_id INT,
                        saldo DECIMAL(18,2),
                        FOREIGN KEY (kunder_id) REFERENCES Kunder(kunder_id)
                    );

                    IF OBJECT_ID('Transaktioner', 'U') IS NULL
                    CREATE TABLE Transaktioner (
                        transaktion_id INT PRIMARY KEY IDENTITY(1,1),
                        from_konton_id INT,
                        to_konton_id INT,
                        amount DECIMAL(18,2),
                        date DATETIME DEFAULT GETDATE(),
                        FOREIGN KEY (from_konton_id) REFERENCES Konton(konton_id),
                        FOREIGN KEY (to_konton_id) REFERENCES Konton(konton_id)
                    );";

                var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("✅ Tables created (if not already existing).");
            }
        }

        public static void InsertSampleData()
        {
            string connectionString = "Server=(localdb)\\BankDbInstance;Database=BankDb;Integrated Security=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
            IF NOT EXISTS (SELECT 1 FROM Kunder WHERE namn = 'Alice')
            BEGIN
                INSERT INTO Kunder (namn) VALUES ('Alice'), ('Bob');

                INSERT INTO Konton (kunder_id, saldo) VALUES (1, 5000.00), (1, 3000.00), (2, 1000.00);

                INSERT INTO Transaktioner (from_konton_id, to_konton_id, amount)
                VALUES (1, 3, 500.00), (2, 1, 1000.00), (3, 2, 200.00);
            END
        ";

                var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("✅ Sample data inserted (if not already existing).");
            }
        }

    }
}
