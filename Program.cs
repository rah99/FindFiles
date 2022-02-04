using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindJSFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*****This application will look for the specified file in the provided path's folder and subfolders******");
            List<string> results;
            string prefix = null;
            string file;
            string ext;

            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n");
                Console.Write("\nEnter the folder path: ");
                Console.ForegroundColor = ConsoleColor.White;
                string path = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                while (!Directory.Exists(path)) { Console.WriteLine("\nPath not found, please reenter the correct path." + "\n"); Console.ForegroundColor = ConsoleColor.White; path = Console.ReadLine(); }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Does your file/s have a prefix: y/n: ");
                Console.ForegroundColor = ConsoleColor.White;
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.WriteLine("\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Please enter the prefix without the final dot: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    prefix = Console.ReadLine();
                    prefix = prefix.ToLower() + ".";
                } else
                {
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter the file name, without the extension, to find (use of wildcards permitted (?, *)): ");
                Console.ForegroundColor = ConsoleColor.White;
                file = Console.ReadLine();
                file = file.ToLower();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Please enter the extension type of the files to search for, without the dot: ");
                Console.ForegroundColor = ConsoleColor.White;
                ext = Console.ReadLine();
                ext = "." + ext.ToLower();
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(prefix))
                {
                    file = prefix + file + ext;
                }
                else
                {
                    file += ext;
                }
                results = GetFiles(path, file, SearchOption.AllDirectories);
                List<string> files = new List<string>(results);
                Console.ForegroundColor = ConsoleColor.Blue;
                if (files.Count() == 0)
                {
                    Console.WriteLine("No matching files.");
                }
                else if (files.Count() > 1)
                {
                    Console.WriteLine(files.Count + " files found at:\n" + string.Join("\n", files));
                }
                else
                {
                    Console.WriteLine(files.Count + " file found at:\n" + string.Join("\n", files));
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nWould you like to find more files? y/n: ");
                Console.ForegroundColor = ConsoleColor.White;
            } while (Console.ReadKey().Key == ConsoleKey.Y);

            Console.WriteLine();
        }

        private static List<string> GetFiles(string sourceFolder, string filters, SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(sourceFolder) || string.IsNullOrWhiteSpace(filters))
            {
                return null;
            }

            return filters.Split('|').SelectMany(filter => Directory.GetFiles(sourceFolder, filter, searchOption)).ToList();
        }
    }
}
