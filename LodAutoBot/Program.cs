using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace LodAutoBot
{
    static class Program
    {
        static string AutoRegCom(string strCmd)
        {
         
            string rInfo;
            try
            {
                Process myProcess = new Process();
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("cmd.exe");
                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.CreateNoWindow = true;
                myProcessStartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo = myProcessStartInfo;
                myProcessStartInfo.Arguments = "/c " + strCmd;
                myProcess.Start();
                StreamReader myStreamReader = myProcess.StandardOutput;
                rInfo = myStreamReader.ReadToEnd();
                myProcess.Close();
                rInfo = strCmd + "\r\n" + rInfo;
                return rInfo;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string dllPath = Path.Combine(Application.StartupPath, "op_x86.dll"+ " /s");
            AutoRegCom("regsvr32 "+ dllPath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
