using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace TerminalHashCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string calculatedHash = "";
            bool runSucsess = false;
            bool hashMatch = false;
            string userHash = "";
            var key = '0';
            while (true)
            {
                runSucsess = false;
                Console.Clear();
                Console.Write("Enter the file path: ");
                string filePath = Console.ReadLine();
                Console.Write($"Supported hash calculation methods:\nmd5\nsha-256\nsha-512\nEnter the file calculation method: ");
                string calculationMethod = Console.ReadLine().ToLower();
                if (filePath.StartsWith('"') && filePath.EndsWith('"'))
                {
                    filePath = filePath.Substring(1, filePath.Length - 2);
                }

                if (File.Exists(filePath))
                {
                    runSucsess = true;
                    if (calculationMethod == "md5")
                    {
                        var md5 = MD5.Create();
                        var stream = File.OpenRead(filePath);
                        byte[] hash = md5.ComputeHash(stream);
                        calculatedHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                    else if (calculationMethod == "sha-256" || calculationMethod == "sha256")
                    {
                        using (FileStream stream = File.OpenRead(filePath))
                        {
                            SHA256 sha256 = SHA256.Create();

                            byte[] hash = sha256.ComputeHash(stream);

                            calculatedHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
                        }
                    }
                    else if (calculationMethod == "sha-512" || calculationMethod == "sha512")
                    {
                        using (FileStream stream = File.OpenRead(filePath))
                        {
                            SHA512 sha512 = SHA512.Create();

                            byte[] hash = sha512.ComputeHash(stream);

                            calculatedHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Calculation method was not found!");
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
                    Console.Write($"Enter the {calculationMethod} hash: ");
                    userHash = Console.ReadLine();
                    if (calculatedHash == userHash)
                    {
                        Console.WriteLine("The hashes match.");
                    }
                    else
                    {
                        Console.WriteLine("The hashes do not match.");
                        Console.WriteLine($"Calculated hash: {calculatedHash}");
                        Console.WriteLine($"User input hash: {userHash}");
                        hashMatch = false;
                    }
                }
                else
                {
                    Console.WriteLine($"Directory {filePath} does not exist!");
                }

                Console.WriteLine("Press r to restart, q to quit the program or s to save it.");

                key = Console.ReadKey().KeyChar;

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
                        File.WriteAllText(saveLocation, "For file " + '"' + filePath + '"' + ' ' + calculationMethod + " hashes matched!" + "\n" + "The calculated hash was: " + calculatedHash);
                    }
                    else if (!hashMatch)
                    {
                        File.WriteAllText(saveLocation, "For file " + '"' + filePath + '"' + ' ' + calculationMethod + " hashes did not match" + "\n" + "The calculated hash was: " + calculatedHash + "\n" + "User input hash was: " + userHash);
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
