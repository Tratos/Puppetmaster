using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginSystem;

namespace Puppetmaster
{
    public static class Plugins
    {
        public static List<IPlugin> PluginList = new List<IPlugin>();
        public static IPluginHost Host;
        public static RichTextBox box = null;

        public class PluginHost : IPluginHost
        {
        }


        public static void Init()
        {
            try
            {
                Host = new PluginHost();
                if (!Directory.Exists("plugins"))
                    return;
                string[] files = Directory.GetFiles("plugins\\", "*.dll", SearchOption.AllDirectories);
                ICollection<Assembly> assemblies = new List<Assembly>();
                foreach (string file in files)
                {
                    try
                    {
                        AssemblyName an = AssemblyName.GetAssemblyName(file);
                        Assembly assembly = Assembly.Load(an);
                        assemblies.Add(assembly);
                    }
                    catch { }
                }
                Type pluginType = typeof(IPlugin);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();
                        foreach (Type type in types)
                            if (type.IsInterface || type.IsAbstract)
                                continue;
                            else
                                if (type.GetInterface(pluginType.FullName) != null)
                                pluginTypes.Add(type);
                    }
                foreach (Type type in pluginTypes)
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                    plugin.Host = Host;
                    PluginList.Add(plugin);
                }
            }
            catch (Exception ex)
            {
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
