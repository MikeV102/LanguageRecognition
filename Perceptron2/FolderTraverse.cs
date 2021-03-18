using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nai3
{
    class FolderTraverse
    {
       

        public static int NumberOfFolders(string root)
        {
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }

            dirs.Push(root);

            string[] subDirs = null;

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();

                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
              
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

            }
            return subDirs?.Length ?? -1;
        }

        public static List<string> GetLanugages(string root)
        {
            List<string> languages = new List<string>();
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }

            dirs.Push(root);

            string[] subDirs = null;

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();

                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
              
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

            }

            foreach (var subDir in subDirs)
            {
                
            DirectoryInfo dir_info = new DirectoryInfo(subDir);
            string directory = dir_info.Name;  // System32
            languages.Add(directory);
            }

            return languages;
        }
        
        
        
         public static  List<string> FilesPaths(string root)
         {
             // list with files paths to be returned 
             List<string> pathsList = new List<string>();
             // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }

            dirs.Push(root);

            string[] subDirs = null;

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();

                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
               
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                    string[] files = null;
                    try
                    {
                        files = System.IO.Directory.GetFiles(currentDir);
                    }
                
                    catch (UnauthorizedAccessException e)
                    {
                        
                        Console.WriteLine(e.Message);
                        continue;
                    }
                
                    catch (System.IO.DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    // Perform the required action on each file here.
                    // Modify this block to perform your required task.
                    foreach (string file in files)
                    {
                        try
                        {
                            
                            //stringBuilder for adding file paths to the arrayList

                            // Perform whatever action is required in your scenario.
                            System.IO.FileInfo fi = new System.IO.FileInfo(file);
                            //Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                            pathsList.Add(file);

                        }
                        catch (System.IO.FileNotFoundException e)
                        {
                            // If file was deleted by a separate application
                            //  or thread since the call to NumberOfFolders()
                            // then just continue.
                            Console.WriteLine(e.Message);
                            continue;
                        }
                    }
                
                    // Push the subdirectories onto the stack for traversal.
                    // This could also be done before handing the files.
                    foreach (string str in subDirs)
                    {
                        //Console.WriteLine(str);
                        dirs.Push(str);
                    }
                }

            return pathsList;
         }


            
        }
    }
