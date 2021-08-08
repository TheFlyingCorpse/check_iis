# check_iis
Internet Information Server check plugin for Icinga2, Icinga, Centreon, Shinken, Naemon and other nagios like systems.

## General Requirements

* Windows OS with .NET Framework 3.5 SP1 or newer installed, this plugin comes in flavours for both 3.5 and 4.0+
* IIS 7.0 or higher, this means Windows Server 2008 or newer.
* This plugin can only check Sites and AppPools locally on the same machine it runs from. Expects Icinga2 agent, NSCP or similar to execute it.
* To run this the agent/executor service must run as admin, user rights is not enough due to requiring admin with elevated rights to read the configuration store from IIS.
* Case sensitivity in matches for Sites and Application Pools.
* Use only the named switches, the single character shortcuts might change in the future or disappear completely.

## Assumptions
This executable assumes the following:
* That the plugin is able to run as SYSTEM or with a user that has Administrative privileges.
* When you run inventory with the switch to only return running objects, this assumes that the Sites and AppPools are in their correct states. The output from this can be used to correctly configure the arguments.
* If you run this plugin via Icinga2 and you have issues escaping, try to supply --split-by and separate multiple AppPools or Sites with a comma, ex ".Net v2.0,.Net v4.5".
* Some monitoring solutions really dont like long verbose output, so if your monitoring solution is one of these, try to use the switch --hide-long-output so it only prints the summary of the --check-service.

## Usage:	

	check_iis.exe - Windows Service Status plugin for Icinga2, Icinga, Centreon, Shinken, Naemon and other nagios like systems
        Version: 0.10.5690.38449

        a:inventory-websites            Switch to use to provide inventory of Sites
        A:inventory-apppools            Switch to use to provide inventory of AppPools
        b:check-websites                Switch to use to check the health status of the local Sites
        B:check-apppools                Switch to use to check the health status of the local AppPools
        E:inv-level                     Argument to change the level of output. Default is 'normal', available options are 'normal','full'
        f:inv-format                    Argument to provide output of the inventory in other formats, valid options are 'readable', 'i2conf' and 'json'
        F:excluded-sites                Excludes Sites from checks and inventory
        G:included-sites                Includes Sites to check while all other Sites are excluded, affects both checks and inventory
        h:stopped-sites                 The specified Sites are checked that they are stopped
        H:warn-sites                    These specified Sites will return Warning if they are not in the expected state
        i:perfcounter-sites             Switch to use to get perfcounters from Sites
		M:perfdata-mbytes               Switch to use to get perfdata as MBytes rather than bytes
        I:excluded-apppools             Excludes AppPools from checks and inventory
        J:included-apppools             Includes AppPools to check while all other AppPools are excluded, affects both checks and inventory
        k:stopped-apppools              The specified AppPools are checked that they are stopped
        K:warn-apppools                 The specified AppPools will return Warning if they are not in the expected state
        l:perfcounter-apppools          Switch to use to get perfcounters from AppPools
        L:skip-empty-apppools           Switch which sets do not check or inventory AppPools which are empty
        T:hide-long-output              Switch to hide the long service output, only prints the summary output and any Sites or AppPools deviating from 'OK'
        u:expected-state                Argument to provide the expected State of the AppPool or Site, used together with --single-check
        w:icinga2                       Switch used in the Icinga2 CommandDefinition, returns output and perfcounter to the correct class. Do not use via command line.
        W:single-check                  Switch used together with the Icinga2 Auto Apply rules, this is set when there is a single Site or AppPool to check. Do take great care if you use this outside of the auto apply rules
        x:split-by                      Argument used to specify what splits all Sites and AppPool arguments. Default is a single space, ' '
        X:inv-hide-empty                Switch to hide empty vars from inventory output
        y:inv-running-only              Switch to only inventory running Sites and/or AppPools, depending on what has been selected for inventory
        z:verbose                       Switch to use when trying to figure out why a Site or an AppPool is not included, excluded or similarly when the returned output is not as expected
        Z:debug                         Switch to to get maximum verbosity (for debugging)

## QuickStart - Icinga2

### Recommended setup to start with

To get quickly started with this plugin, these are the steps needed to quickly get going:

#### Copy the neccesary files

To make available the plugin for icinga2:
* Copy the two files "check_iis.exe" and "check_iis.exe.config" to the sbin folder of icinga2
** Default path on a 32bit OS: C:\Program Files\ICINGA2\sbin
** Default path on a 64bit OS: C:\Program Files (x86)\ICINGA2\sbin

* Copy the configuration file "check_iis.conf" into a place where it is easy for you to modify this using Notepad++ or your favourite text editor, like your Documents

** Note, this is only the command definition 
* Make a decision if you want to monitor each AppPool separately or as a group. 
* Make a decision if you want to monitor each Site separately or as a group.
#### Inventory Sites
* Skip this step if you are going to monitor the Sites as a group.
* Copy the output from this command to hosts.conf inside the brackets of the Host object (must be run from within icinga2's sbin folder, where you copied check_iis.exe and check_iis.exe.config)

	check_iis.exe --inventory-websites --inv-format i2conf
	
#### Monitoring Inventoried Sites
* Skip this step if you are going to monitor the Sites as a group.
* Uncomment the apply Service rule called "Monitoring Inventoried AppPools" in the check_iis.conf

#### Monitoring Sites as a group
* Skip this step if you are going to monitor Sites based on the inventory.
* Uncomment the apply Service rule called "Monitoring Sites as group" in the check_iis.conf

#### Inventory AppPools
* Skip this step if you are going to monitor the Application Pools as a group.

* Copy the output from this command to hosts.conf inside the brackets of the Host object (must be run from within icinga2's sbin folder, where you copied check_iis.exe and check_iis.exe.config)

	check_iis.exe --inventory-apppools --inv-format i2conf
	
#### Monitoring Inventoried AppPools
* Skip this step if you are going to monitor the Application Pools as a group.
* Uncomment the apply Service rule called "Monitoring Inventoried AppPools" in the check_iis.conf

#### Monitoring AppPools as a group
* Skip this step if you are going to monitor AppPools based on the inventory.
* Uncomment the apply Service rule called "Monitoring AppPools as group" in the check_iis.conf

#### Copying the configuration file to icinga

Copy the configuration file "check_iis.conf" into your ICINGA2\etc\icinga2\conf.d folder from where you modified a copy of the default.

#### Applying the configuration change

Restart icinga2 agent on the local server/client to read the changed configuration.

Wait 1-2 minutes, then on the master, run "icinga2 node update-config" before restarting the icinga2 service to read the "discovered" services from the agent(s).

## Examples

### Monitoring

#### Application Pools
Monitor Application Pools

	check_iis.exe --check-apppools

Monitor Application Pools, only monitor Application Pool ".NET v4.5"

	check_iis.exe --check-apppools --included-apppools ".NET v4.5"

Monitor Application Pools, all should be started but for ".NET v4.5", which is expected to be stopped

	check_iis.exe --check-apppools --stopped-apppools ".NET v4.5"

Monitor Application Pools, all should be started, if AppPool ".NET v2.0" is in a different state it should only warn

	check_iis.exe --check-apppools --warn-apppools ".NET v2.0"

Monitor Application Pools, monitor all AppPools but for ".NET v2.0"

	check_iis.exe --check-apppools --excluded-apppools ".NET v2.0"

Monitor Application Pools, monitor all but for ".NET 2.0" and".NET v4.5", in addition AppPool ".NET v4.5 Classic" should be stopped instead of running

	check_iis.exe --check-apppools --excluded_apppools ".NET v2.0" ".NET v4.5" --stopped-apppools ".NET v4.5 Classic"

Monitor Application Pools, all AppPools should be running, with performance data

	check_iis.exe --check-apppools --perfcounter-apppools

Monitor Application Pools, all AppPools should be running, excluded AppPool ".NET v2.0", with performance data

	check_iis.exe --check-apppools --perfcounter-apppools --excluded-apppools ".NET v2.0"

Monitor Application Pools, all AppPools should be running, excluded AppPools ".NET v2.0" and ".NET v4.5", default --split-by

	check_iis.exe --check-apppools --perfcounter-apppools --excluded-apppools ".NET v2.0" ".NET v4.5"
	
Monitor Application Pools, all AppPools should be running, excluded AppPools ".NET v2.0" and ".NET v4.5", comma separates multiple values

	check_iis.exe --check-apppools --perfcounter-apppools --excluded-apppools ".NET v2.0",".NET v4.5" --split-by ","

#### Sites
Monitor Sites

	check_iis.exe --check-websites

Monitor Sites, only monitor Site ".NET v4.5"

	check_iis.exe --check-websites --included-sites ".NET v4.5"

Monitor Sites, all should be started but for ".NET v4.5", which is expected to be stopped

	check_iis.exe --check-websites --stopped-sites ".NET v4.5"

Monitor Sites, all should be started, if AppPool ".NET v2.0" is in a different state it should only warn

	check_iis.exe --check-websites --warn-sites ".NET v2.0"

Monitor Sites, monitor all Sites but for ".NET v2.0"

	check_iis.exe --check-websites --excluded-sites ".NET v2.0"

Monitor Sites, monitor all but for ".NET 2.0" and".NET v4.5", in addition AppPool ".NET v4.5 Classic" should be stopped instead of running

	check_iis.exe --check-websites --excluded-sites ".NET v2.0" ".NET v4.5" --stopped-sites ".NET v4.5 Classic"

Monitor Sites, all Sites should be running, with performance data

	check_iis.exe --check-websites --perfcounter-sites

Monitor Sites, all Sites should be running, excluded AppPool ".NET v2.0", with performance data

	check_iis.exe --check-websites --perfcounter-sites --excluded-sites ".NET v2.0"

#### AppPools and Sites
Monitor Application Pools and Sites (all expected running)

	check_iis.exe --check-websites --apppools
	
Monitor Application Pools and Sites with perfcounter

	check_iis.exe --apppools --perfcounter-apppools --check-websites --perfcounter-sites

Monitor Application Pools (excluded ".NET v2.0") and Sites (excluded "WsusPool")

	check_iis.exe --apppools --perfcounter-apppools --check-websites --perfcounter-sites --excluded-apppools ".NET v2.0" --excluded-sites "WsusPool"
	
### Inventory
#### AppPools
Inventory AppPools

	check_iis.exe --inventory-apppools

Inventory AppPools, only inventory "DefaultAppPool"

	check_iis.exe --inventory-apppools --included-apppools "DefaultAppPool"

Inventory AppPools, only return the running AppPools

	check_iis.exe --inventory-apppools --inv-running-only
	
Inventory AppPools, only return the running AppPools, do not print empty arrays or keys with empty values

	check_iis.exe --inventory-apppools --inv-running-only --inv-hide-empty

Inventory AppPools, all but ".NET v4.5"

	check_iis.exe --inventory-apppools --excluded-apppools ".NET v4.5"

#### Sites
Inventory Sites

	check_iis.exe --inventory-websites

Inventory Sites, only return the running Sites

	check_iis.exe --inventory-websites --inv-running-only

Inventory Sites, all but "WsusPool"

	check_iis.exe --inventory-sites --excluded-sites "WsusPool"

Inventory Sites, only inventory "Default Web Site", full inventory 

	check_iis.exe --inventory-websites --included-sites "Default Web Site" --inv-level full
	
Inventory Sites, only return the running Sites, do not print empty arrays or keys with empty values

	check_iis.exe --inventory-websites --inv-running-only --inv-hide-empty
	
Inventory Sites, do not print empty arrays or keys with empty values, return it as icinga2 format configuration.

	check_iis.exe --inventory-websites --inv-hide-empty --inv-format i2conf
	
	
#### AppPools and Sites

Inventory AppPools and Sites

	check_iis.exe --inventory-apppools --inventory-websites
	
Inventory AppPools and Sites, only running

	check_iis.exe --inventory-apppools --inventory-websites --inv-running-only

Inventory AppPools and Sites with all recognized details, as json

	check_iis.exe --inventory-apppools --inventory-websites --inv-level full --inv-format json

Inventory AppPools and Sites with all recognized details, as json, only running

	check_iis.exe --inventory-apppools --inventory-websites --inv-level full --inv-format json --inv-running-only
	
Inventory AppPools and Sites with all recognized details, as i2conf output, only running

	check_iis.exe --inventory-apppools --inventory-websites --inv-level full --inv-format i2conf --inv-running-only
	
Inventory AppPools and Sites with all recognized details, as readable output, only running, hide empty vars

	check_iis.exe --inventory-apppools --inventory-websites --inv-level full --inv-format readable --inv-running-only --inv-hide-empty


## Configuration

To make this plugin work with Icinga2, NSCP or others, it needs to be configured. 
Below are some configuration examples on how to configure this plugin with Icinga2 (agent) and NSCP/NSClient++.

### Icinga2 agent

All configuration samples below assumes local configuration on agent, configuration for central use to come.

#### Command Definition

	object CheckCommand "check_iis" {
		import "plugin-check-command"

		command = [ PluginDir + "/check_iis.exe" ]

		arguments = {
			"--inventory-websites" = {
				set_if = "$iis_inventory_sites$"	
				description = "Switch to use to provide inventory of Sites"
			}
			"--inventory-apppools" = {
				set_if = "$iis_inventory_apppools$"	
				description = "Switch to use to provide inventory of AppPools"
			}
			"--check-websites" = {
				set_if = "$iis_check_sites$"
				description = "Switch to use to check the health status of the local Sites"
			}
			"--check-apppools" = {
				set_if = "$iis_check_apppools$"
				description = "Switch to use to check the health status of the local Sites"
			}
			"--inv-level" = {
				value = "$iis_inventory_level$"
				description = "Argument to change the level of output. Default is 'normal', available options are 'normal','full'"
			}
			"--inv-format" = {
				value = "$iis_inventory_format$"
				description = "Argument to provide output of the inventory in other formats, valid options are 'readable', 'i2conf' and 'json'"
			}
			"--excluded-sites" = {
				value = "$iis_excluded_sites$"
				description = "Excludes Sites from checks and inventory"
			}
			"--included-sites" = {
				value = "$iis_included_sites$"
				description = "Includes Sites to check while all other Sites are excluded, affects both checks and inventory"
			}
			"--stopped-sites" = {
				value = "$iis_stopped_sites$"
				description = "The specified Sites are checked that they are stopped"
			}
			"--warn-sites" = {
				value = "$iis_warn_sites$"
				description = "These specified Sites will return Warning if they are not in the expected state"
			}
			"--perfcounter-sites" = {
				set_if = "$iis_perfcounter_sites$"	
				description = "Switch to use to get perfcounters from Sites"
			}
			"--perfdata-bytes" = {
			set_if = "$iis_perfdata_mbytes$"	
			description = "Switch to use to get perfdata as MBytes rather than bytes"
			}
			"--excluded-apppools" = {
				value = "$iis_excluded_apppools$"
				description = "Excludes AppPools from checks and inventory"
			}
			"--included-apppools" = {
				value = "$iis_included_apppools$"
				description = "Includes AppPools to check while all other AppPools are excluded, affects both checks and inventory"
			}
			"--stopped-apppools" = {
				value = "$iis_stopped_apppools$"
				description = "The specified AppPools are checked that they are stopped"
			}
			"--warn-apppools" = {
				value = "$iis_warn_apppools$"
				description = "The specified AppPools will return Warning if they are not in the expected state"
			}
			"--perfcounter-apppools" = {
				set_if = "$iis_perfcounter_apppools$"	
				description = "Switch to use to get perfcounters from AppPools"
			}
			"--skip-empty-apppools" = {
				set_if = "$iis_skip_empty_apppools$"	
				description = "Switch which sets do not check or inventory AppPools which are empty"
			}
			"--hide-long-output" = {
				set_if = "$iis_hide_long_output$"	
				description = "Switch to hide the long service output, only prints the summary output and any Sites or AppPools deviating from 'OK'"
			}
			"--expected-state" = {
				set_if = "$iis_single_check$"	
				value = "$iis_expected_state$"
				description = "Argument to provide the expected State of the AppPool or Site, used together with --single-check"
			}
			"--icinga2" = {
				set_if = "$iis_icinga2$"	
				description = "Switch used in the Icinga2 CommandDefinition, returns output and perfcounter to the correct class. Do not use via command line"
			}
			"--single-check" = {
				set_if = "$iis_single_check$"	
				description = "Switch used together with the Icinga2 Auto Apply rules, this is set when there is a single Site or AppPool to check. Do take great care if you use this outside of the auto apply rules"
			}
			"--split-by" = {
				value = "$iis_split_by$"
				description = "Argument used to specify what splits all Sites and AppPool arguments. Default is a single space, ' '"
			}
			"--inv-hide-empty" = {
				set_if = "$inv_hide_empty_vars$"	
				description = "Switch to hide empty vars from inventory output."
			}
			"--inv-running-only" = {
				set_if = "$iis_inv_running_only$"	
				description = "Switch to only inventory running Sites and/or AppPools, depending on what has been selected for inventory"
			}
			"--verbose" = {
				set_if = "$iis_verbose$"
				description = "Switch to use when trying to figure out why a service is not included, excluded or similarly when the returned output is not as expected"
			}
		}
		vars.iis_inventory_sites = false
		vars.iis_inventory_apppools = false
		vars.iis_check_sites = false
		vars.iis_check_apppools = false
		//vars.iis_inventory_level = "normal"
		//vars.iis_inventory_format = "i2conf"
		//vars.iis_excluded_sites = ""
		//vars.iis_included_sites = ""
		//vars.iis_stopped_sites = ""
		//vars.iis_warn_sites = ""
		vars.iis_perfcounter_sites = false
		vars.iis_perfdata_mbytes = false
		//vars.iis_excluded_apppools = ""
		//vars.iis_included_apppools = ""
		//vars.iis_stopped_apppools = ""
		//vars.iis_warn_apppools = ""
		vars.iis_perfcounter_apppools = false
		vars.iis_skip_empty_apppools = false
		vars.iis_hide_long_output = false
		vars.iis_expected_state = "Started"
		vars.iis_icinga2 = false
		vars.iis_single_check = false
		vars.iis_split_by = false
		vars.iis_inv_hide_empty_vars = false
		vars.iis_inv_running_only = false
		vars.iis_verbose = false
		
	}

#### AppPool Monitoring
Monitor all AppPools, does not need any data from inventory. It checks that all available AppPools are "Started" by default.

	apply Service "IIS AppPools"{
	  import "generic-service"

	  check_command = "check_iis"

	  vars.iis_check_apppools = true
	  assign where host.name == NodeName
	}

Monitor all AppPools, same as above, just added with perfcounter option and excluded AppPools ".NET v2.0" and ".NET v4.5"

	apply Service "IIS AppPools"{
	  import "generic-service"

	  check_command = "check_iis"

	  assign where host.name == NodeName

	  vars.iis_check_apppools = true
	  vars.iis_perfcounter_apppools = true
	  vars.iis_excluded_apppools = ".NET v2.0,.NET v4.5"
	  vars.iis_split_by = ","
	  vars.iis_verbose = true
	}

Monitors AppPools based on inventory data (stored in the host object), this specific apply rule only adds running apppools to monitoring, with perfcounters

	apply Service "IIS AppPoolR " for (name => config in host.vars.inv.iis.apppool) {
	  import "generic-service"

	  check_command = "check_iis"

	  vars += config
	  vars.iis_check_apppools = true
	  vars.iis_included_apppools = vars.Name
	  vars.iis_perfcounter_apppools = true
	  vars.iis_split_by = ","
	  assign where config.State == "Started"
	}

Monitors AppPools based on inventory data (stored in the host object), this specific apply rule only adds stopped apppools to monitoring, with perfcounters

	apply Service "IIS AppPoolS " for (name => config in host.vars.inv.iis.apppool) {
	  import "generic-service"

	  check_command = "check_iis"

	  vars += config
	  vars.iis_check_apppools = true
	  vars.iis_included_apppools = vars.Name
	  vars.iis_stopped_apppools = vars.Name
	  vars.iis_perfcounter_apppools = true
	  vars.iis_split_by = ","
	  assign where config.State == "Stopped"
	}
	
Recommended method: Monitors AppPools based on inventory data (stored in the host object), this specific apply rule adds uses the state from inventory, with perfcounters and hides long output

	apply Service "IIS AppPool " for (name => config in host.vars.inv.iis.apppool) {
	  import "generic-service"

	  check_command = "check_iis"

	  vars += config
	  vars.iis_check_apppools = true
	  vars.iis_included_apppools = vars.Name
	  vars.iis_perfcounter_apppools = true
	  vars.iis_expected_state = vars.State
	  vars.iis_single_check = true
	  vars.iis_split_by = ","
	}
	

#### Monitor Sites
Monitors all Sites, does not use inventory data.

	apply Service "IIS Sites"{
	  import "generic-service"

	  check_command = "check_iis"

	  vars.iis_check_sites = true
	  assign where host.name == NodeName
	}

Monitors all Sites, does not use inventory data, the same as above, just added perfcounters

	apply Service "IIS Sites"{
	  import "generic-service"

	  check_command = "check_iis"

	  vars.iis_check_sites = true
	  vars.iis_perfcounter_sites = true
	  assign where host.name == NodeName
	}
	
Monitors Sites based on inventory data (stored in the host object), this specific apply rule only adds running sites to monitoring, with perfcounters

	apply Service "IIS SiteR " for (name => config in host.vars.inv.iis.site) {
	  import "generic-service"

	  check_command = "check_iis"

	  vars += config
	  vars.iis_check_sites = true
	  vars.iis_included_sites = vars.Name
	  vars.iis_perfcounter_sites = true
	  assign where config.State == "Started"
	}

Monitors Sites based on inventory data (stored in the host object), this specific apply rule only adds stopped Sites to monitoring, with perfcounters

	apply Service "IIS SiteS " for (name => config in host.vars.inv.iis.site) {
	  import "generic-service"

	  check_command = "check_iis"

	  vars += config
	  vars.iis_check_sites = true
	  vars.iis_included_sites = vars.Name
	  vars.iis_stopped_sites = vars.Name
	  vars.iis_perfcounter_sites = true
	  vars.iis_split_by = ","
	  assign where config.State == "Stopped"
	}
	
Recommended method: Monitors Sites based on inventory data (stored in the host object), this specific apply rule adds uses the state from inventory, with perfcounters

	apply Service "IIS Site " for (name => config in host.vars.inv.iis.site) {
	  import "generic-service"

	  check_command = "check_iis"

	  vars += config
	  vars.iis_check_sites = true
	  vars.iis_included_sites = vars.Name
	  vars.iis_perfcounter_sites = true
	  vars.iis_expected_state = vars.State
	  vars.iis_split_by = ","
	  vars.iis_single_check = true
	}

### NSCP / NSClient++
ToDo

Add in nsclient.ini

Test via check_nrpe or similar to verify.