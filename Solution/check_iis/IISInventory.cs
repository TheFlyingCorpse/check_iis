using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoringPluginsForWindows
{
    class IISInventory
    {
        public String IISVersion { get; private set; }
        public Array AppPools { get; private set; }
        public Array WebSites { get; private set; }
        public Array StartedAppPools { get; private set; }
        public Array StoppedAppPools { get; private set; }
        public Array StartedSites { get; private set; }
        public Array StoppedSites { get; private set; }
        public IISInventory(String iisVersion, Array appPools, Array webSites, Array startedAppPools, Array stoppedAppPools, Array startedSites, Array stoppedSites)
        {
            IISVersion = iisVersion;
            AppPools = appPools;
            WebSites = webSites;
            StartedAppPools = startedAppPools;
            StoppedAppPools = stoppedAppPools;
            StartedSites = startedSites;
            StoppedSites = stoppedSites;
        }
    }

}
