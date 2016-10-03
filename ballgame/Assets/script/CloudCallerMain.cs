using UnityEngine;
using System.Collections;
using System.Threading;
using System.IO;
using System;
//using CloudCall;




public class CloudCallerMain
    {

        public static void readAllTargets(string targetFile)

        {
        string otherdir = @"D:\githubrepos\Demo_Project";

        string curdir = Environment.CurrentDirectory;
       
        DirectoryInfo dir = new DirectoryInfo(curdir);

        string basePath = dir.Parent.Parent.FullName;

        basePath = otherdir;
            string sourcePath = "";
            string targetPath = "";
            string[] lines = System.IO.File.ReadAllLines(basePath +@"\" + targetFile);
            for (int x = 0; x < lines.Length; x += 2)
            {
                sourcePath = basePath + lines[x];
            
                targetPath = basePath + lines[x + 1];
            writeLine("------------------Now copying " + sourcePath + " to " + targetPath );
           
            DirectoryCopy(sourcePath, targetPath, true);
            }
        }


        /// <summary>
        /// The microsoft directory copy edited to check last changes on source files
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

        // Get the files in the directory and copy them to the new location.
        int spot = 0;
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                spot++;
                writeLine("file " + spot + " :" + file.ToString());
                string temppath = Path.Combine(destDirName, file.Name);
                DateTime dt = File.GetLastWriteTime(temppath);
            
                //if (dt.AddDays(7).CompareTo( DateTime.Now)> 0)
                    file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    public static void writeLine(string line)
    {
        Debug.Log(line);
        Console.WriteLine(line);
        // Debug.LogWarning(line);
    }
    public static void wrtEnv()
    {
        try
        {
            for (int x = 0; x < 20; x++)
                writeLine("***********************************************************************************************************************************");

            writeLine(Environment.OSVersion.ToString());
            string currentdir = Directory.GetCurrentDirectory();
            string currentDataPath = Application.dataPath;
            writeLine("CURRENT DIR--------------------:" + currentdir);
            writeLine("DATA PATH---------------------:" + Application.dataPath);
        }
        catch (Exception e)
        {
            writeLine("ERROR:" + e.ToString());
        }


    }
}
