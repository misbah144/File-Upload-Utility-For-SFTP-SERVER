using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.Web.Hosting;
using System.Web.Configuration;
using System.Web.Services;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
//using System.Drawing;

namespace edituploadmerge
{
    class Program        ////////////// editing to avoid unnecessary characters and uploading the required file  functionalities are merged ////////                     ////////// properly working at 5:00 PM  12/5/2017  ////////////////
    {                    ////////// used to upload file  from local machune  to remote server /////////////////

       // [STAThread]                                    // used to enable file dialog option in console application 
        static void Main(string[] args)
        {

            try
            {
                while (true)
                {

                    string file_name;
                    //string fileName;
                    //OpenFileDialog fd = new OpenFileDialog();
                    //fd.ShowDialog();
                    //file_name = fd.FileName;
                    //string  file_name1 = fd.SafeFileName;
                    //Console.Write(file_name);
                    //SaveFileDialog sd = new SaveFileDialog();
                    //sd.ShowDialog();

                    //string sourcePath = @file_name;
                    //string targetPath = Path.Combine(Environment.CurrentDirectory, @"uploads\"+file_name1);

                    // File.Move(sourcePath, targetPath);        used as cut paste function

                    //// Use Path class to manipulate file and directory paths.
                    //string sourceFile = System.IO.Path.Combine(sourcePath, file_name);
                    // string destFile = System.IO.Path.Combine(targetPath,sourcePath);
                    //if (File.Exists(targetPath))
                    //{ File.Delete(targetPath); }
                    //System.IO.File.Copy(sourcePath, targetPath, true);         // ----used as copy paste function   
                    string host = ConfigurationManager.AppSettings["server"];
                    int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    // private readonly static int Port = 22;
                    string username = ConfigurationManager.AppSettings["username"];
                    string password = ConfigurationManager.AppSettings["password"];
                    string workingdirectory = ConfigurationManager.AppSettings["workingdirectory"];
                    // string uploadfile = HostingEnvironment.MapPath("~/upload_files/NAV.20160817.txt");   ////----///
                    //  string uploadfile = System.Web.Hosting.HostingEnvironment.MapPath("~/upload_files/NAV.20160810.txt");
                    string uploadfile = @"C:\Users\misbah.haque\Desktop\upload\";
                    
                    //    string upload_directory_lis = @"C:\Users\misbah.haque\Desktop\upload\upload_lis\";             // lis upload directory 

                    string upload_directory_msorder = @ConfigurationManager.AppSettings["upload_directory_msorder"];
                    string upload_directory_nav = @ConfigurationManager.AppSettings["upload_directory_nav"]; 
                    string archive_directory = @"C:\Users\misbah.haque\Desktop\upload\local_archive\";

                   //  string archive_lis = @"C:\Users\misbah.haque\Desktop\upload\upload_lis\archive_lis\";       //to archive  .lis files

                    string archive_nav = @ConfigurationManager.AppSettings["archive_nav"];       // to archive nav files 

                    string archive_msorders = @ConfigurationManager.AppSettings["archive_msorders"];  // to archive msorder files

                    //DirectoryInfo d_lis = new DirectoryInfo(upload_directory_lis);     // looking into directory for  .lis files 

                    ////////////////////////////////// for.lis files   ////////////////////////////////////////////

                    //FileInfo[] Files_lis = d_lis.GetFiles("*.lis"); //Getting Text files  
                    //foreach (FileInfo file in Files_lis)
                    //{
                    //    string str = "";
                    //    str = upload_directory_lis + file.Name;
                    //    string workingdirectory_root = ConfigurationManager.AppSettings["workingdirectory_root"];
                    //    workingdirectory = workingdirectory_root + "KSEPRICES.BP";

                    //    Console.WriteLine("Creating client and connecting");
                    //    using (var client = new SftpClient(host, port, username, password))
                    //    {
                    //        client.Connect();
                    //        Console.WriteLine("Connected to {0}", host);
                    //        client.ChangeDirectory(workingdirectory);
                    //        Console.WriteLine("Changed directory to {0}", workingdirectory);
                    //        //var listDirectory = client.ListDirectory(workingdirectory); /////////// to see the files in the directory 
                    //        //Console.WriteLine("Listing directory:");
                    //        //foreach (var fi in listDirectory)
                    //        //{
                    //        //    Console.WriteLine(" - " + fi.Name);
                    //        //}
                    //        string text = string.Empty;
                    //        using (System.IO.StreamReader file1 = new System.IO.StreamReader(@str))
                    //        {
                    //            text = file1.ReadToEnd();    // 
                    //            //  text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  
                    //            // System.IO.File.WriteAllText(@uploadfile, text);  // can not executed b/c files are used by another process 
                    //        }
                    //        string ext = Path.GetExtension(@str);
                    //        if (Path.GetExtension(ext).Equals(".txt"))   // to filter text files from ( .lis files )
                    //        {
                    //            text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  /// use to avoid unnnessceary charaters during file transfer i.e. "~OD"
                    //            System.IO.File.WriteAllText(@str, text);
                    //        }
                    //        //   string mystring = text;
                    //        //   Console.Write(mystring);
                    //        using (var fileStream = new FileStream(str, FileMode.Open))
                    //        {
                    //            Console.WriteLine("Uploading {0} ({1:N0} bytes)", str, fileStream.Length);
                    //            client.BufferSize = 4 * 1024; // bypass Payload error large files
                    //            client.UploadFile(fileStream, Path.GetFileName(str));
                    //            Console.WriteLine(DateTime.Now);         /////      
                    //            // used as cut paste function                            
                    //        }
                    //        string sourcePath = str;
                    //        string targetPath = archive_lis + file.Name;
                    //        if (File.Exists(targetPath))
                    //        { File.Delete(targetPath); }
                    //        File.Move(sourcePath, targetPath);

                    //    }
                    //}

                    //////////////////////////////// for.lis files   ////////////////////////////////////////////

                    //////////////////////////////// for NAV  files   ////////////////////////////////////////////

                    DirectoryInfo d_nav = new DirectoryInfo(upload_directory_nav);          // looking into directory for nav files 
                    FileInfo[] Files_nav = d_nav.GetFiles("*.txt"); //Getting Text files  
                    foreach (FileInfo file in Files_nav)
                    {
                        string filter_file = file.Name;
                        string filter = filter_file.Substring(0,3);                     
                        if((String.Compare(filter,"MSO") == 0))
                        {
                            Console.WriteLine("EQUALS FOR MSO ");
                                 // WE CAN MANIPULATE  WORKING DIRECTORY PATH HERE BASED ON  FILE NAME 
                                // string workingdirectory_root = ConfigurationManager.AppSettings["workingdirectory_root"];
                                //  workingdirectory = workingdirectory_root + "";
                        
                        }
                        if ((String.Compare(filter, "NAV") == 0))
                        {
                            Console.WriteLine("EQUALS FOR NAV ");

                            string workingdirectory_root = ConfigurationManager.AppSettings["workingdirectory_root"];
                            workingdirectory = workingdirectory_root + "DAILY.NAV";

                            // WE CAN MANIPULATE  WORKING DIRECTORY PATH HERE BASED ON  FILE NAME 
                        }
                        string str = "";
                        str = upload_directory_nav + file.Name;
                        Console.WriteLine("Creating client and connecting");
                        using (var client = new SftpClient(host, port, username, password))
                        {
                            client.Connect();
                            Console.WriteLine("Connected to {0}", host);
                            client.ChangeDirectory(workingdirectory);
                            Console.WriteLine("Changed directory to {0}", workingdirectory);
                            //var listDirectory = client.ListDirectory(workingdirectory); /////////// to see the files in the directory 
                            //Console.WriteLine("Listing directory:");
                            //foreach (var fi in listDirectory)
                            //{
                            //    Console.WriteLine(" - " + fi.Name);
                            //}
                            string text = string.Empty;
                            using (System.IO.StreamReader file1 = new System.IO.StreamReader(@str))
                            {
                                text = file1.ReadToEnd();    // 
                                //  text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  
                                // System.IO.File.WriteAllText(@uploadfile, text);  // can not executed b/c files are used by another process 
                            }
                            string ext = Path.GetExtension(@str);
                            if (Path.GetExtension(ext).Equals(".txt"))   // to filter text files from ( .lis files )
                            {
                                text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  /// use to avoid unnnessceary charaters during file transfer i.e. "~OD"
                                System.IO.File.WriteAllText(@str, text);
                            }
                         //   string mystring = text;
                         //   Console.Write(mystring);
                            using (var fileStream = new FileStream(str, FileMode.Open))
                            {
                                Console.WriteLine("Uploading {0} ({1:N0} bytes)", str, fileStream.Length);
                                client.BufferSize = 4 * 1024; // bypass Payload error large files
                                client.UploadFile(fileStream, Path.GetFileName(str));
                                Console.WriteLine(DateTime.Now);         /////                                                                                           
                            }
                            string sourcePath = str;
                            string targetPath = archive_nav + file.Name;
                            if (File.Exists(targetPath))
                            { File.Delete(targetPath); }
                            File.Move(sourcePath, targetPath);          // used as cut paste function



                        }

                    }

                    //////////////////////////////// for NAV  files   ////////////////////////////////////////////             

                    //////////////////////////////// for MSORDER  files   ////////////////////////////////////////////  

                    DirectoryInfo d_msorder = new DirectoryInfo(upload_directory_msorder);     // looking into directory for msorder files 
                    FileInfo[] Files_msorder = d_msorder.GetFiles("*.txt"); //Getting Text files  
                    foreach (FileInfo file in Files_msorder)
                    {
                        string filter_file = file.Name;
                        string filter = filter_file.Substring(0, 3);
                        if ((String.Compare(filter, "MSO") == 0))
                        {
                            Console.WriteLine("EQUALS FOR MSO ");
                            // WE CAN MANIPULATE  WORKING DIRECTORY PATH HERE BASED ON  FILE NAME 
                            string workingdirectory_root = ConfigurationManager.AppSettings["workingdirectory_root"];
                            workingdirectory = workingdirectory_root + "MS.ORDERS.IN";
                        }
                        if ((String.Compare(filter, "NAV") == 0))
                        {
                            Console.WriteLine("EQUALS FOR NAV ");
                            string workingdirectory_root = ConfigurationManager.AppSettings["workingdirectory_root"];
                            workingdirectory = workingdirectory_root + "DAILY.NAV";
                            // WE CAN MANIPULATE  WORKING DIRECTORY PATH HERE BASED ON  FILE NAME 
                        }
                        string str = "";
                        str = upload_directory_msorder + file.Name;
                        Console.WriteLine("Creating client and connecting");
                        using (var client = new SftpClient(host, port, username, password))
                        {
                            client.Connect();
                            Console.WriteLine("Connected to {0}", host);
                            client.ChangeDirectory(workingdirectory);
                            Console.WriteLine("Changed directory to {0}", workingdirectory);
                            //var listDirectory = client.ListDirectory(workingdirectory); /////////// to see the files in the directory 
                            //Console.WriteLine("Listing directory:");
                            //foreach (var fi in listDirectory)
                            //{
                            //    Console.WriteLine(" - " + fi.Name);
                            //}
                            string text = string.Empty;
                            using (System.IO.StreamReader file1 = new System.IO.StreamReader(@str))
                            {
                                text = file1.ReadToEnd();    // 
                                //  text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  
                                // System.IO.File.WriteAllText(@uploadfile, text);  // can not executed b/c files are used by another process 
                            }
                            string ext = Path.GetExtension(@str);
                            if (Path.GetExtension(ext).Equals(".txt"))   // to filter text files from ( .lis files )
                            {
                                text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  /// use to avoid unnnessceary charaters during file transfer i.e. "~OD"
                                System.IO.File.WriteAllText(@str, text);
                            }
                            //   string mystring = text;
                            //   Console.Write(mystring);
                            using (var fileStream = new FileStream(str, FileMode.Open))
                            {
                                Console.WriteLine("Uploading {0} ({1:N0} bytes)", str, fileStream.Length);
                                client.BufferSize = 4 * 1024; // bypass Payload error large files
                                client.UploadFile(fileStream, Path.GetFileName(str));
                                Console.WriteLine(DateTime.Now);         /////      
                                // used as cut paste function                            
                            }
                            string sourcePath = str;
                            string targetPath = archive_msorders + file.Name;
                            if (File.Exists(targetPath))
                            { File.Delete(targetPath); }
                            File.Move(sourcePath, targetPath);

                        }

                    }
                    //////////////////////////////// for MSORDER  files   ////////////////////////////////////////////  
                
                }
                 //  Console.Read();
            }

            catch (Exception e) 
            {

                Console.WriteLine(e.ToString());
                Console.Read();
            }
                // string uploadfile = Path.Combine(Environment.CurrentDirectory, @"uploads\" + file_name1);
            //string uploadfile = System.Web.HttpContext.Current.Server.MapPath("~/upload_files/NAV.20160810.txt");
            // const string host = "172.30.101.32";
            // const string username = "amtest";
            //  const string password = "123456";
            //  const string workingdirectory = "/temenos/bnk/bnk.run/DAILY.NAV";                       //always check diectory path of the file where to be updated  
            //  const string uploadfile = @"C:\Users\misbah.haque\Desktop\upload\NAV.20160810.txt";    // always check the  file path
            //   const string upload_directory = @"C:\Users\misbah.haque\Desktop\myfolder";            //    

            //////////////////////////////////////////////   orignal code ////////////////////////////////////////////////// --- represented with three lines 
            // int port = 22;

            //--        Console.WriteLine("Creating client and connecting");
            //--        using (var client = new SftpClient(host, port, username, password))
            //--       {
            //--        client.Connect();
            //--      Console.WriteLine("Connected to {0}", host);

            //--                client.ChangeDirectory(workingdirectory);
            //--            Console.WriteLine("Changed directory to {0}", workingdirectory);

            //--        var listDirectory = client.ListDirectory(workingdirectory); /////////// to see the files in the directory 
            //--      Console.WriteLine("Listing directory:");
            //--       foreach (var fi in listDirectory)
            //--        {
            //--        Console.WriteLine(" - " + fi.Name);
            //--      }

            //   DirectoryInfo d = new DirectoryInfo(upload_directory);//Assuming Test is your Folder
            /// FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            //string str = "";
            //foreach (FileInfo file in Files)
            // {
            //str = str + ", " + file.Name;
            //Console.WriteLine(" files to be uploaded "+"__"+file.FullName);
            //  Console.ReadLine();

            //}

            /////////////////////////////////////////////// to find characters in text files //////////////

            /*      int counter = 0;
                  string line;

              //    Console.Write("Input your search text: ");
                //  var text = "~";

                  System.IO.StreamReader myfile =
                      new System.IO.StreamReader(@"C:\Users\misbah.haque\Desktop\sharedsftp\NAV.20160818.txt");

                  while ((line = myfile.ReadLine()) != null)
                  {
                      if (line.Contains(text))
                      {
                          Console.WriteLine("hidden character is present ");

                          break;
                      }
                      else
                      { Console.WriteLine("not found "); }
                      counter++;
                  }

                  Console.WriteLine("Line number: {0}", counter);
                  myfile.Close();
                  Console.ReadLine();    */

            //  using (StreamReader sr = new StreamReader(@"C:\Users\misbah.haque\Desktop\sharedsftp\expnav\NAV.20160818.txt", Encoding.ASCII))
            // {
            // string line = sr.ReadLine();
            // Console.WriteLine("checking chracters to find" + line);
            //--        string text = string.Empty;
            //--        using (System.IO.StreamReader file = new System.IO.StreamReader(@uploadfile))
            //--        {

            //--        text = file.ReadToEnd();    // 
            //  text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  
            // System.IO.File.WriteAllText(@uploadfile, text);  // can not executed b/c files are used by another process 
            //--    }

            //--        string ext = Path.GetExtension(@uploadfile);
            //--        if (Path.GetExtension(ext).Equals(".txt"))   // to filter text files from ( .lis files )
            //--       {
            //--       text = System.Text.RegularExpressions.Regex.Replace(text, @"\r", string.Empty);  /// use to avoid unnnessceary charaters during file transfer i.e. "~OD"
            //--       System.IO.File.WriteAllText(@uploadfile, text);
            //--       }

            //--        string mystring = text; 
            //--        Console.Write(mystring);

            // string[] lines = text.Split('\~');
            //  foreach (string s in lines)
            //{
            //  Console.WriteLine("console write line "+ s);

            //  }

            //}

            /////////////////////////////////////////////////////////////////////////////////////////////////////////         
            ////////////////// used to upload all the files of  directory on sftp server  ///////////////////////////////////////
            ////  it can also be used to  download a single file  we just have to remove for each loop ////////////////////

            // /*    foreach (FileInfo file1 in Files)
            // {

            //--        using (var fileStream = new FileStream(uploadfile, FileMode.Open))
            //--       {
            //--        Console.WriteLine("Uploading {0} ({1:N0} bytes)", uploadfile, fileStream.Length);
            //--        client.BufferSize = 4 * 1024; // bypass Payload error large files
            //--        client.UploadFile(fileStream, Path.GetFileName(uploadfile));
            //--        Console.WriteLine(DateTime.Now);         /////      
            //--      }  

            //////////////////////////////////////////////////////////////////  orignal code  ////////////////////////////////////   
            // } */

            //--   }

            //--     }

        }

    }


}
