using System;
using System.IO;
using System.Security.Cryptography;

namespace Terminal-Hash-Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter the file path: ");
                string filePath = Console.ReadLine();
                string userHash;

                if (filePath.StartsWith('"') && filePath.EndsWith('"'))
                {
                    filePath = filePath.Substring(1, filePath.Length - 2);
                }

                if (File.Exists(filePath))
                {


                    using (var md5 = MD5.Create())
                    {
                        using (var stream = File.OpenRead(filePath))
                        {
                            byte[] hash = md5.ComputeHash(stream);
                            string md5Hash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                            Console.Write("Enter the MD5 hash: ");
                            userHash = Console.ReadLine();

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
                }
                else
                {
                    Console.WriteLine($"Directory {filePath} does not exist!");
                }

                Console.WriteLine("Press r to restart, q to quit the program.");

                var key = Console.ReadKey().KeyChar;

                if (key == 'q')
                {
                    break;
                }
                if (key == 's')
                {
                    
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
