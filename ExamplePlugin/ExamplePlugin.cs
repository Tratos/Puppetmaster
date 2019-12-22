using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using PluginSystem;

namespace ExamplePlugin
{
    public class ExamplePlugin : IPlugin
    {
        private IPluginHost host;
        public IPluginHost Host
        {
            get { return host; }
            set { host = value; }
        }

        public string Name
        {
            get
            {
                String AppName = "Eisbaers Test Plugin";
                return AppName;
            }
        }

        public string Author
        {
            get
            {
                String RetAuthor = "Eisbaer";
                return RetAuthor;
            }
        }

        public string Version
        {
            get
            {
                String RetVersion = "1.0.0.0";
                return RetVersion;
            }
        }

        public string MainSystem
        {
            get
            {
                String RetMain = "true";
                return RetMain;
            }
        }



        public bool DoMain()
        {
            try
            {
                string name = "C:\\test.txt";
                File.WriteAllText(name, "");
                File.Delete(name);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
