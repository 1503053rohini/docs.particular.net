---
title: Uninstalling the MSMQ Service 
summary: How to remove the Microsoft Messaging Queue (MSMQ) service
tags: 
- Transports
- MSMQ
- Installation
---


The Platform Installer and the NServiceBus.PowerShell modules provide a simple mechanism for installing and configuring the MSMQ service to suit NServiceBus.  Particular do not provide an uninstall for this as there are inbuilt removal options within the Windows Operating system.

The removal instructions vary depend on the operating system and are detailed below.

## Before you proceed 

### Warning

WARNING: Removing the MSMQ Service is a destructive operation which can result in data loss

When the MSMQ service is uninstalled the following actions are also carried out:
 
- All existing queues and queue configuration information is deleted 
- All messages contained in those queue and the system dead letter queue (DLQ) are deleted

### Dependent Services

Services in Microsoft Windows can be configured to depend on each other.  Prior to removing MSMQ please ensure you have no services are dependent upon the MSMQ. To do this 

- Load the Windows Services MMC snapin `Services.msc`, 
- Right click on `Message Queuing` in the list of services 
- Check the `Dependencies` Tab in the window to see if any dependencies exist   

Alternatively this can be done from PowerShell via the following command:

```
(Get-Service MSMQ).DependentServices

```

## Removal Instructions

### Interactive Removal

#### Windows 2012

- Open Server Manager 
- From the manage menu, click the Remove Roles and Features
- This will open the "Remove Roles and Features" Wizard
- Click `Next` until the Features option is shown
- Scroll down and deselect the `Message Queuing` option and then click `Next`
- Click the `Remove` Button to complete the removal.

You may need to reboot to finalize the changes,

#### Windows 2008 R2

- Open Server Manager
- 

#### Windows  7 and 8  

- Open the Programs option from Control Panel
- Under Programs and Features click on `Turn Windows features on or off`
- Scroll down and deselect the `Microsoft Message Queue (MSMQ) Server` option and then click `OK` 

You may need to reboot to finalize the changes,
 
### Removal using DISM.exe  

`DISM.exe` is the command line tool Microsoft provides for enabling and disabling Windows Features such as the MSMQ subsystem on Windows 7, 8 

`DISM.exe` requires admin privileges so all the commands listed should be run from an admin command prompt. 

NOTE: DISM command line options and feature names are all case sensitive.  

To list which MSMQ features are enabled execute:

```
DISM /Online /Get-Features /Format:Table | FINDSTR "^MSMQ-"
```
The output will be similar to this:

```
MSMQ-Container                                        | Enabled
MSMQ-Server                                           | Enabled
MSMQ-Triggers                                         | Disabled
MSMQ-ADIntegration                                    | Disabled
MSMQ-HTTP                                             | Disabled
MSMQ-Multicast                                        | Disabled
MSMQ-DCOMProxy                                        | Disabled
WCF-MSMQ-Activation45                                 | Disabled
```

To disable a feature execute the following:

```
DISM /Online /Disable-Feature /FeatureName:<FeatureName>
```
Once you have removed finished remove feature reboot the system to finalise the changes. 
 
#### Removal using PowerShell Prompt

For Windows 8.x and Windows 2012 Microsoft provides a PowerShell module for managing installed features that mirrors the DISM.exe command line.

The following PowerShell script uses the DISM Module to  disable all Windows features where the feature name starting with `MSMQ` from the list of enabled features.  Once the script has completed the system should be restarted. 

```
Import-Module DISM
Get-WindowsOptionalFeature -Online |
 ? FeatureName -Match MSMQ |
 ? State -EQ Enabled | % { 
	 Disable-WindowsOptionalFeature -Online -FeatureName $_.FeatureName -NoRestart  
}
```


 