using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginSystem;

namespace Puppetmaster
{
    public class Sys
    {

        public static RichTextBox box = null;
        public bool _isExit = false;
        public static bool _isRunning = false;
        public readonly object _sync = new object();
        

        public static void Start()
        {
            _isRunning = true;
            LoadModule();
        }


        public static void LoadModule()
        {
            Plugins.Init();
            Log("Loaded " + Plugins.PluginList.Count + " Plugin's\n");
       }

        public static void Stop()
        {
            _isRunning = false;
        }

        public bool IsExit()
        {
            _isRunning = false;
            bool result = true;
            lock (_sync)
            {
                result = _isExit;
            }
            return result;
        }




        public static void Log(string s)
        {
            if (box == null) return;
            try
            {
                box.Invoke(new Action(delegate
                {
                    string stamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " : ";
                    box.AppendText(stamp + s + "\n");
                    box.SelectionStart = box.Text.Length;
                    box.ScrollToCaret();
                }));
            }
            catch { }
        }


    }
}
