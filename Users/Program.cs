using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Users
{
    internal class Program
    {
        public static string ConnectionString = "server=localhost;database=users;user=root;password=";
        public static MySqlConnection Conn = new MySqlConnection(ConnectionString);

        public static void InsertData(string name, string email, string password)

        {
            Conn.Open();

            string sql = "INSERT INTO `data`(`Name`, `Email`, `Password`) VALUES(@name, @email, @password)";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        public static void ReadData()
        {
            Conn.Open();

            string sql = "SELECT * FROM data";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);

            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine($"{dr.GetInt32(0)}" +
                    $" {dr.GetString(1)}" +
                    $" {dr.GetString(2)} " +
                    $" {dr.GetString(3)}" +
                    $" {dr.GetDateTime(5)}");
            }

            Console.ReadKey();
            Conn.Close();
        }

        public static void DeleteUser(int id)
        {
            Conn.Open();

            string sql = "DELETE FROM data WHERE Id = @id";

            MySqlCommand cmd = new MySqlCommand( sql, Conn);

            cmd.Parameters.AddWithValue("@id", id);
            
            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        public static void UpdateUser(int Id, string Name, string Email, string Password)
        {
            Conn.Open();

            string sql = "UPDATE `data` SET `Name`=@name, `Email`=@email, `Password`=@password WHERE `Id`=@id";

            MySqlCommand cmd =new MySqlCommand( sql, Conn);

            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@password", Password);
            cmd.Parameters.AddWithValue("@id", Id);

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Válassz menüpontot");
            Console.WriteLine("1. Lekérdezés");
            Console.WriteLine("2. Beszúrás");
            Console.WriteLine("3. Módosítás");
            Console.WriteLine("4. Törlés");
            byte menu;
            do
            {
                menu = byte.Parse(Console.ReadLine());
            } while (menu < 1 || menu > 4);
            switch(menu)
            {
                case 1: 
                    ReadData();
                    break;

                case 2:
                    Console.WriteLine("Kérem a nevet: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Kérem az emailt: ");
                    string email = Console.ReadLine();
                    Console.WriteLine("Kérem a jelszót: ");
                    string password = Console.ReadLine();  

                    InsertData(name, email, password);
                    break;

                case 3:
                    Console.WriteLine("Kérem az azonosítót: ");
                    int id1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Kérem a nevet: ");
                    string Name = Console.ReadLine();
                    Console.WriteLine("Kérem az emailt: ");
                    string Email = Console.ReadLine();
                    Console.WriteLine("Kérem a jelszót: ");
                    string Password = Console.ReadLine();

                    UpdateUser(id1, Name, Email, Password);
                    break;

                case 4:
                        Console.WriteLine("Kérem a törlendő user Id-jét: ");
                        int id = int.Parse(Console.ReadLine());
                        DeleteUser(id);
                        break;
            }

            

           
        }
    }
}
