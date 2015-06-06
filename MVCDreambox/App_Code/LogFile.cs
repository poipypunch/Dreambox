using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace MVCDreambox.App_Code
{
    public class LogFile
    {
        #region"Property"
        string path = "c:\\logFile";
        string timeformat = "HH:mm:ss";
        string fileformat = "yyyyMMdd";
        string spit = "-----";
        string fileType = ".txt";
        #endregion
        public static void writeLogFile(DateTime dateTime, string pageName, string messageLog)
        {
            new LogFile(dateTime,pageName,messageLog);
        }
        public LogFile(DateTime dateTime, string pageName, string messageLog)
		{			
			//this.CreateNewFolder(path);
            this.writeLogfile(dateTime.ToString(fileformat), dateTime.ToString(timeformat), pageName, messageLog);			
		}
        private void CreateNewFolder(string path)
		{			
			try
			{
				if (System.IO.Directory.Exists(path)== false)
				{
					System.IO.Directory.CreateDirectory(path);
				}
			}
			catch(Exception e)
			{
				e.ToString();
				// ไม่สามารถสร้าง folder ได้เนื่องจาก path อาจจะผิดหรือไม่ถูกต้อง
			}
		}
        private void writeLogfile(string filename, string timeformat, string pagename, string log)
        {
            if (log.IndexOf("Thread was being aborted.") < 0)
            {
                path = ConfigurationSettings.AppSettings["LogPath"].ToString();
                if (path.Trim() == string.Empty)
                {
                    path = "c:\\logFile";
                }
                this.CreateNewFolder(path);
                
                filename = path + "\\" + ConfigurationSettings.AppSettings["LogName"] + "_" + filename + fileType;

                StreamWriter write = null;                
                try
                {
                    if (File.Exists(filename) == false)
                    {
                        FileStream file;
                        file = File.Create(filename);
                        file.Close();
                    }
                    write = new StreamWriter(filename, true);
                    //write.WriteLine(fulltextwrite);
                    write.WriteLine(timeformat);
                    write.Write(spit);
                    write.WriteLine(pagename);
                    write.Write(spit);
                    write.WriteLine(log);
                    write.WriteLine();
                }
                catch (Exception es)
                {
                    //ไม่สามารถบันทึก Log ได้ เนื่องจาก user อาจจะใส่ path ผิด หรือ path ไม่ถูกต้อง
                    es.ToString();
                }
                finally
                {
                    if (write != null)
                        write.Close();
                }
            }
        }      

    }
}
