using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("****************************");
        Console.WriteLine("First Name       | Last Name     |");
        string filepath = "example.txt";
        Console.Write("Enter First Name : ");
        string name = Console.ReadLine();
        //Write content to file 
        File.WriteAllText(filepath, name);
        Console.Write("File written successfuly");

        //Read content from file 
        if (File.Exists(filepath))
            {
            string content = File.ReadAllText(filepath);
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }
}