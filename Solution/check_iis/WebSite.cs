using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoringPluginsForWindows
{
    class WebSite
    {
        public long Id { get; private set; }
        public String Name { get; private set; }
        public Boolean ServerAutoStart { get; private set; }
        public Boolean IsLocallyStored { get; private set; }
        public String State { get; private set; }
        public String LogFileDirectory { get; private set; }
        public Boolean LogFileEnabled { get; private set; }
        public Array Bindings { get; private set; }
        public Array Applications { get; private set; }

        public WebSite(long id, String name, Boolean serverAutoStart, Boolean isLocallyStored, String state, 
            String logFileDirectory, Boolean logFileEnabled, Array bindings, Array applications)
        {
            Id = id;
            Name = name;
            ServerAutoStart = serverAutoStart;
            IsLocallyStored = isLocallyStored;
            State = state;
            LogFileDirectory = logFileDirectory;
            LogFileEnabled = logFileEnabled;
            Bindings = bindings;
            Applications = applications;
        }
    }

    class SiteBinding
    {
        public String Protocol { get; private set; }
        public String BindingInformation { get; private set; }
        public String Host { get; private set; }
        public String CertificateHash { get; private set; }
        public String CertificateStoreName { get; private set; }
        public Boolean UseDsMapper { get; private set; }
        public Boolean IsIPPortHostBinding { get; private set; }
        public Boolean IsLocallyStored { get; private set; }

        public SiteBinding(String protocol, String bindingInformation, String host, String certificateHash,
            String certificateStoreName, Boolean useDsMapper, Boolean isIpPortHostBinding, Boolean isLocallyStored)
        {
            Protocol = protocol;
            BindingInformation = bindingInformation;
            Host = host;
            CertificateHash = certificateHash;
            CertificateStoreName = certificateStoreName;
            UseDsMapper = useDsMapper;
            IsIPPortHostBinding = isIpPortHostBinding;
            IsLocallyStored = isLocallyStored;
        }
    }

    class SiteApplications
    {
        public String ApplicationPoolName { get; private set; }
        public String EnabledProtocols { get; private set; }
        public Boolean IsLocallyStored { get; private set; }
        public String Path { get; private set; }
        public Array VirtualDirectories { get; private set; }

        public SiteApplications(String applicationPoolName, String enabledProtocols, Boolean isLocallyStored,
            String path, Array virtualDirectories)
        {
            ApplicationPoolName = applicationPoolName;
            EnabledProtocols = enabledProtocols;
            IsLocallyStored = isLocallyStored;
            Path = path;
            VirtualDirectories = virtualDirectories;
        }
    }

    class SiteAppVirtualDirectories
    {
        public String Path { get; private set; }
        public String PhysicalPath { get; private set; }
        public Boolean IsLocallyStored { get; private set; }
        public String LogonMethod { get; private set; }
        public String UserName { get; private set; }

        public SiteAppVirtualDirectories(String path, String physicalPath, Boolean isLocallyStored,
            String logonMethod, String userName)
        {
            Path = path;
            PhysicalPath = physicalPath;
            IsLocallyStored = isLocallyStored;
            LogonMethod = logonMethod;
            UserName = userName;
        }
    }
}
