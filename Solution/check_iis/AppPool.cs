using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoringPluginsForWindows
{
    class AppPool
    {
        public String Name { get; private set; }
        public Boolean AutoStart { get; private set; }
        public Boolean Enable32bitAppOnWin64 { get; private set; }
        public Boolean IsLocallyStored { get; private set; }
        public String ManagedPipelineMode { get; private set; }
        public String ManagedRuntimeVersion { get; private set; }
        public long QueueLength { get; private set; }
        public String State { get; private set; }
        public String ProcessModelIdentityType { get; private set; }
        public TimeSpan ProcessModelIdleTimeout { get; private set; }
        public Boolean ProcessModelLoadUserProfile { get; private set; }
        public long ProcessModelMaxProcesses { get; private set; }
        public Boolean ProcessModelPingingEnabled { get; private set; }
        public Int32 ProcessModelPingInterval { get; private set; }
        public long ProcessModelPingResponseTime { get; private set; }
        public String ProcessModelUserName { get; private set; }
        public Boolean CpuSmpAffinitized { get; private set; }
        public String FailureLoadBalancerCapabilities { get; private set; }
        public Array WorkerProcesses { get; private set; }

        public AppPool(String name, Boolean autoStart, Boolean enable32bitAppOnWin64, Boolean isLocallyStored, 
            String managedPipelineMode, String managedRuntimeVersion, long queueLength, String state, 
            String processModelIdentityType, TimeSpan processModelIdleTimeout,
            Boolean processModelLoadUserProfile, long processModelMaxProcesses, Boolean processModelPingingEnabled,
            String processModelUserName, Boolean cpuSmpAffinitized, String failureLoadBalancerCapabilities,
            Array workerProcesses)
        {
            Name = name;
            AutoStart = autoStart;
            Enable32bitAppOnWin64 = enable32bitAppOnWin64;
            IsLocallyStored = isLocallyStored;
            ManagedPipelineMode = managedPipelineMode;
            ManagedRuntimeVersion = managedRuntimeVersion;
            QueueLength = queueLength;
            State = state;
            ProcessModelIdentityType = processModelIdentityType;
            ProcessModelIdleTimeout = processModelIdleTimeout;
            ProcessModelLoadUserProfile = processModelLoadUserProfile;
            ProcessModelMaxProcesses = processModelMaxProcesses;
            ProcessModelPingingEnabled = processModelPingingEnabled;
            ProcessModelPingResponseTime = ProcessModelPingResponseTime;
            ProcessModelUserName = processModelUserName;
            CpuSmpAffinitized = cpuSmpAffinitized;
            FailureLoadBalancerCapabilities = failureLoadBalancerCapabilities;
            WorkerProcesses = workerProcesses;
        }
    }

    class AppPoolWorkerProcesses
    {
        public String AppPoolName { get; private set; }
        public Boolean IsLocallyStored { get; private set; }
        public String State { get; private set; }
        public Array ApplicationDomains { get; private set; }

        public AppPoolWorkerProcesses(String appPoolName, Boolean isLocallyStored, String state, Array applicationDomains)
        {
            AppPoolName = appPoolName;
            IsLocallyStored = isLocallyStored;
            State = state;
            ApplicationDomains = applicationDomains;
        }
    }

    class AppPoolWPAppDomains
    {
        public String Id { get; private set; }
        public Int32 Idle { get; private set; }
        public Boolean IsLocallyStored { get; private set; }
        public String PhysicalPath { get; private set; }
        public String VirtualPath { get; private set; }
        public Array AppPoolWorkerProcesses { get; private set; }

        public AppPoolWPAppDomains(String id, Int32 idle, Boolean isLocallyStored, String physicalPath, String virtualPath,
            Array appPoolWorkerProcesses)
        {
            Id = id;
            Idle = idle;
            IsLocallyStored = isLocallyStored;
            PhysicalPath = physicalPath;
            VirtualPath = virtualPath;
            AppPoolWorkerProcesses = appPoolWorkerProcesses;
        }
    }

}
