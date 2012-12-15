%windir%\system32\inetsrv\appcmd set config -section:applicationPools -applicationPoolDefaults.processModel.loadUserProfile:true
%windir%\system32\inetsrv\appcmd set config -section:applicationPools -applicationPoolDefaults.processModel.idleTimeout:00:00:00
%windir%\system32\inetsrv\appcmd set config -section:applicationPools -applicationPoolDefaults.recycling.periodicRestart.time:96:00:00
exit /b 0