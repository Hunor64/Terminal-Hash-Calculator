using System;
using System.IO;
using System.Security.Cryptography;

namespace TerminalHashCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string md5Hash = "";
            bool runSucsess = false;
            bool hashMatch = false;
            string userHash = "";
            while (true)
            {
                runSucsess = false;
                Console.Clear();
                Console.Write("Enter the file path: ");
                string filePath = Console.ReadLine();

                if (filePath.StartsWith('"') && filePath.EndsWith('"'))
                {
                    filePath = filePath.Substring(1, filePath.Length - 2);
                }

                if (File.Exists(filePath))
                {
                    runSucsess = true;
                    var md5 = MD5.Create();
                    var stream = File.OpenRead(filePath);
                    byte[] hash = md5.ComputeHash(stream);
                    md5Hash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                    Console.Write("Enter the MD5 hash: ");
                    userHash = Console.ReadLine();

                    if (md5Hash == userHash)
                    {
                        Console.WriteLine("The hashes match.");
                        hashMatch = true;
                    }
                    else
                    {
                        Console.WriteLine("The hashes do not match.");
                        Console.WriteLine($"Calculated hash: {md5Hash}");
                        Console.WriteLine($"User input hash: {userHash}");
                        hashMatch = false;
                    }
                }
                else
                {
                    Console.WriteLine($"Directory {filePath} does not exist!");
                }

                Console.WriteLine("Press r to restart, q to quit the program or s to save it.");

                var key = Console.ReadKey().KeyChar;

                if (key == 'q')
                {
                    break;
                }
                else if (key == 's' && runSucsess == true)
                {
                    Console.WriteLine();
                    Console.Write("Enter save location: ");
                    var saveLocation = Console.ReadLine();
                    if (hashMatch)
                    {
                        File.WriteAllText(saveLocation, "For file " + '"' + filePath + '"' + " the hashes matched!" + "\n" + "The calculated hash was: " + md5Hash);
                    }
                    else if (!hashMatch)
                    {
                        File.WriteAllText(saveLocation, "For file " + '"' + filePath + '"' + " the hashes did not match" + "\n" + "The calculated hash was: " + md5Hash + "\n" + "User input hash was: " + userHash);
                    }

                    Console.WriteLine("Saved!");
                    Console.WriteLine("Press r to restart, q to quit the program.");
                    key = Console.ReadKey().KeyChar;
                    if (key == 'q')
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }

                }
                else if (key == 's' && runSucsess == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("No hash was calculated!");
                    Console.WriteLine("Press r to restart, q to quit the program.");
                    key = Console.ReadKey().KeyChar;
                    if (key == 'q')
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

            }
        }
    }
}
