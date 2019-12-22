using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem
{
    public interface IPlugin
    {
        IPluginHost Host { get; set; }
        string Name { get; }
        string Author { get; }
        string Version { get; }
        string MainSystem { get; }

        bool DoMain();
    }

    public interface IPluginHost
    {
        //Stuff you wanna provide to plugin
    }
}
