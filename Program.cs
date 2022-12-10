using System;
using System.IO;
using System.Security.Cryptography;

namespace Hash_Calculator_c_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                // Ask the user for the file path
                Console.Write("Enter the file path: ");
                string filePath = Console.ReadLine();
                string userHash;

                // Check if the file path starts and ends with " and remove them if they exist
                if (filePath.StartsWith('"') && filePath.EndsWith('"'))
                {
                    filePath = filePath.Substring(1, filePath.Length - 2);
                }

                // Calculate the MD5 hash of the file
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        byte[] hash = md5.ComputeHash(stream);
                        string md5Hash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                        // Ask the user for the MD5 hash
                        Console.Write("Enter the MD5 hash: ");
                        userHash = Console.ReadLine();

                        // Compare the two hashes
                        if (md5Hash == userHash)
                        {
                            Console.WriteLine("The hashes match.");
                        }
                        else
                        {
                            Console.WriteLine("The hashes do not match.");
                            Console.WriteLine($"Calculated hash: {md5Hash}");
                            Console.WriteLine($"User input hash: {userHash}");
                        }
                    }
                }

                // Wait for the user to type q to exit the program, or r to clear the console and restart the program
                Console.WriteLine("Press r to restart, q to quit the program.");

                var key = Console.ReadKey().KeyChar;

                if (key == 'q')
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
