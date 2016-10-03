using UnityEngine;
using System;
using System.IO;
//using HoloToolkit.Unity;
using System.Threading;
using CloudCall;

namespace CloudCall
{
    public class Called
    {
        /*
        [PostProcessBuildAttribute]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject);
{
        }*/

        public static void precall(object manifest)
        {



            cmdloglin();
            wrtEnv();
            //writeLine("MANIFEST?++++++++++++++++++++++++++++++" + manifest);


            string curdir = Environment.CurrentDirectory;

            DirectoryInfo dir = new DirectoryInfo(curdir);

            string basePath = dir.Parent.Parent.FullName;
            //string basePath = Directory.GetCurrentDirectory();
            writeLine("++++++++++++++++++++++++++++++++++++++++++++++++CURRENT DIRECTORY: " + basePath);
            writeLine("++++++++++++++++++++++++++++++++++++++++++++++++regular DIRECTORY: " + Environment.CurrentDirectory);
            basePath =dir.Parent.FullName;
            readAllTargets(@"viveImport.txt",basePath);

        }
        public static void CallMe(string exportpath)
        {


            wrtEnv();
            writeLine("EXPORT PATH++++++++++++++++++++++++++++++" + exportpath);
            writeLine("FINISHED WITH ENV+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            //  cmdloglin();

        }
        public static void cmdloglin()
        {
            writeLine("++++++++++++++++++++++++++++++++++++++++++++++++PREBASH");
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo() { FileName = "/bin/bash", Arguments = " --version", };
            System.Diagnostics.Process proc = new System.Diagnostics.Process() { StartInfo = startInfo, };
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.Start();
            writeLine("++++++++++++++++++++++++++++++++++++++++++++++++POSTBASH");
            string output = proc.StandardOutput.ReadToEnd();
            Thread.Sleep(5000);
            proc.Close();
            proc.WaitForExit();

            writeLine(output);
        }
        public static void cmdlogWin()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "python --version";
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            writeLine(output);
        }
        public static void writeLine(string line)
        {
            //Debug.Log(line);
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
        /*
        public static void buildHolo()
        {

            string appName = "holoBuild";
            // First build SLN
            if (!BuildDeployTools.BuildSLN(BuildDeployPrefs.BuildDirectory, false))
            {
                return;
            }

            // Next, APPX
            if (!BuildDeployTools.BuildAppxFromSolution(
                appName,
                BuildDeployPrefs.MsBuildVersion,
                BuildDeployPrefs.ForceRebuild,
                BuildDeployPrefs.BuildConfig,
                BuildDeployPrefs.BuildDirectory,
                BuildDeployPrefs.IncrementBuildVersion))
            {
                return;
            }
          //  string[] buildList = Directory.GetDirectories(BuildDeployPrefs.AbsoluteBuildDirectory);
        }
        */
        public static void readAllTargets(string target,string basePath)

        {
            writeLine("STARTING READ.............................");

            try
            {
                string sourcePath = "";
                string targetPath = "";
                writeLine("CheckingExistance of............................." + basePath + '/' + target + "================" + File.Exists(basePath + '/' + target));
                
                string[] lines = System.IO.File.ReadAllLines(basePath + '/' + target);
                writeLine("post Read line===========================");
                for (int x = 0; x < lines.Length; x += 2)
                {
                    writeLine("starting " + x + " =========================== " + lines.Length);
                    sourcePath = basePath + lines[x];
                    targetPath = basePath + lines[x + 1];
                    writeLine("Now copying " + sourcePath + " to " + targetPath + "=========================");
                    DirectoryCopy(sourcePath, targetPath, true);
                }
            }
            catch(Exception e)
            {
                writeLine(e.ToString() +  " :     ================================ "  + e.Message);
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
            writeLine("DirectoryCopyStart"+ "=============================");
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
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                DateTime dt = File.GetLastWriteTime(temppath);

              //  if (dt.AddDays(7) > DateTime.Now)
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

        /// <summary>
        /// my initial copy directory not completed though
        /// </summary>
        /// <param name="currentdir"></param>
        /// <param name="targetdir"></param>
        public static void copyCurrentDir(string currentdir, string targetdir)
        {
            string fileName = "";
            string destFile = "";
            string sourcePath = currentdir;
            string targetPath = targetdir;



            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }





            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                string[] files2 = System.IO.Directory.GetFiles(targetPath);


                //check to make sure it exists
                if (files2.Length < files.Length)

                {
                    foreach (string s in files)
                    {
                        fileName = System.IO.Path.GetFileName(s);
                        destFile = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                    // Copy the files and overwrite destination files if they already exist.
                    foreach (string s in files)
                    {

                        // Use static Path methods to extract only the file name from the path.
                        fileName = System.IO.Path.GetFileName(s);
                        //DateTime dt = File.GetLastWriteTime(System.IO.Path.Combine(sourcePath, fileName));




                        destFile = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }
                else
                {
                    Console.WriteLine("Source path does not exist!");
                }


            }
        }
        static void copySingleFile()
        {
            string fileName = "test.txt";
            string fileName2 = "test2.txt";

            string sourcePath = @"C:\Users\Public\TestFolder";
            string targetPath = @"C:\Users\Public\TestFolder\SubDir";


            string currentdir = Directory.GetCurrentDirectory();

            sourcePath = currentdir;
            targetPath = currentdir;

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName2);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);

            // To copy all the files in one directory to another directory.
            // Get the files in the source folder. (To recursively iterate through
            // all subfolders under the current directory, see
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously
            //       in this code example.
            /*
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
            */


            // Keep console window open in debug mode.
            //Console.WriteLine("Press any key to exit.");
            // Console.ReadKey();
        }


        static void WalkDirectoryTree(System.IO.DirectoryInfo root, string basedir, string targetbasedir)
        {
            // Now find all the subdirectories under this directory.
            System.IO.DirectoryInfo[] subDirs = null;
            subDirs = root.GetDirectories();

            //  FileInfo f = root.GetFiles()[0];
            // string name = f.DirectoryName;

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                // Resursive call for each subdirectory.
                WalkDirectoryTree(dirInfo, basedir, targetbasedir);








            }
        }


        public static void testPython(string exportpath)
        {
            /*
             for(int x = 0; x< 20; x++)
                 writeLine("***********************************************************************************************************************************");

             writeLine(Environment.OSVersion.ToString());
             string currentdir = Directory.GetCurrentDirectory();

             writeLine(currentdir);
             */
            //  copyFiles();
            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Python.exe
            /*

                   // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "YOURBATCHFILE.bat";
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

                   */
        }



    }
}
