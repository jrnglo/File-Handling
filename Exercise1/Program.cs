class Program
{
    static void Main()
    {
        string filepath = "example.txt";

        Console.WriteLine("************************Choose Action************************");
        Console.WriteLine("1. Create     | 2. Read     | 3. Update     | 4. Delete     |");
        Console.WriteLine("*************************************************************");

        Console.Write("\nEnter choice: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("First Name : ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name : ");
            string lastName = Console.ReadLine();

            int entryNumber = 1;
            if (File.Exists(filepath))
            {
                var content = File.ReadAllText(filepath);
                var blocks = content.Split(new String[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                int Count = 0;
                foreach (var b in blocks)
                    if (b.Contains("First Name"))
                        Count++;
                entryNumber = Count + 1;
            }
            string entry = $"First Name : {firstName}{Environment.NewLine}Last Name {lastName}{Environment.NewLine}{Environment.NewLine}";
            File.AppendAllText(filepath, entry);
            Console.WriteLine("File Saved Successfully");
        }
        else if (choice == "2")
        {
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
        else if (choice == "3")
        {
            // Update
            if (!File.Exists(filepath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            var content = File.ReadAllText(filepath);
            var blocks = content.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (blocks.Length == 0)
            {
                Console.WriteLine("No entries to update.");
                return;
            }

            Console.WriteLine($"There are {blocks.Length} entries.");
            for (int i = 0; i < blocks.Length; i++)
                Console.WriteLine($"{i + 1}. {GetPreview(blocks[i])}");

            Console.Write("Enter entry number to update: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > blocks.Length)
            {
                Console.WriteLine("Invalid entry number.");
            }
            else
            {
                Console.Write("Enter new First Name : ");
                string newFirst = Console.ReadLine();
                Console.Write("Enter new Last Name : ");
                string newLast = Console.ReadLine();

                blocks[idx - 1] = $"Entry {idx}{Environment.NewLine}First Name : {newFirst}{Environment.NewLine}Last Name : {newLast}";
                var newContent = string.Join(Environment.NewLine + Environment.NewLine, blocks) + Environment.NewLine + Environment.NewLine;
                File.WriteAllText(filepath, newContent);
                Console.WriteLine("Entry updated.");
            }
        }
        else if (choice == "4")
        {
            // Delete
            if (!File.Exists(filepath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            var content = File.ReadAllText(filepath);
            var blocks = content.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (blocks.Length == 0)
            {
                Console.WriteLine("No entries to delete.");
                return;
            }

            Console.WriteLine($"There are {blocks.Length} entries.");
            for (int i = 0; i < blocks.Length; i++)
                Console.WriteLine($"{i + 1}. {GetPreview(blocks[i])}");

            Console.Write("Enter entry number to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > blocks.Length)
            {
                Console.WriteLine("Invalid entry number.");
            }
            else
            {
                var list = new List<string>(blocks);
                list.RemoveAt(idx - 1);

                // Renumber remaining entries and preserve their First/Last lines
                for (int i = 0; i < list.Count; i++)
                {
                    var b = list[i];
                    string withoutHeader = b;
                    if (withoutHeader.StartsWith("Entry "))
                    {
                        var nl = withoutHeader.IndexOf(Environment.NewLine, StringComparison.Ordinal);
                        if (nl >= 0)
                            withoutHeader = withoutHeader.Substring(nl + Environment.NewLine.Length);
                    }
                    list[i] = $"Entry {i + 1}{Environment.NewLine}{withoutHeader}";
                }

                var newContent = string.Join(Environment.NewLine + Environment.NewLine, list);
                if (!string.IsNullOrEmpty(newContent))
                    newContent += Environment.NewLine + Environment.NewLine;
                File.WriteAllText(filepath, newContent);
                Console.WriteLine("Entry deleted.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }

        // Local helper to show a short preview of an entry
        static string GetPreview(string block)
        {
            var lines = block.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return lines.Length > 0 ? lines[0] : block;
        }
    }
}
