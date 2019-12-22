using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PluginSystem;

namespace Puppetmaster
{
    public class SysMain
    {
        public static RichTextBox box = null;
        public bool _isExit = false;
        public static bool _isRunning = false;
        public readonly object _sync = new object();
        public static bool Safety;

        public static void Start()
        {
            _isRunning = true;
            RunModule();
            // RunModul(0);  // Run One Specific Modul
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

        public static void RunModule()   //Start all Loaded Module
        {
            for (int i = 0; i < Plugins.PluginList.Count; i++)
            {
                IPlugin plugin = Plugins.PluginList[i];
                Log("Running Plugin [" + i + "] \"" + plugin.Name + "\"" + " Author: \"" + plugin.Author + "\"" + " Version:" + "\"" + plugin.Version + "\"");

                if (plugin.DoMain())
                    Log("Modul [" + i + "] Success\n");
                else
                    Log("Modul [" + i + "] Fail\n");
            }


        }

        public static void RunModul(int mod)  //Start One Specific Modul
        {
            if (Plugins.PluginList.Count < 0) return;

            for (int i = 0; i < Plugins.PluginList.Count; i++)
            {
                if (i == mod)
                {
                    Safety = true;
                    break;
                }
                else
                {
                    Safety = false;
                }

            }

            if (Safety == true)
            {
                IPlugin plugin = Plugins.PluginList[mod];
                Log("Running Plugin [" + mod + "] \"" + plugin.Name + "\"" + " Author: \"" + plugin.Author + "\"" + " Version:" + "\"" + plugin.Version + "\"");

                if (plugin.DoMain())
                    Log("Modul [" + mod + "] Success\n");
                else
                    Log("Modul [" + mod + "] Fail\n");
            }
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
