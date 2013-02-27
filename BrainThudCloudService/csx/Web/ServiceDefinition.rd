<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="BrainThudCloudService" generation="1" functional="0" release="0" Id="730d26ca-cf21-4213-b1cb-dca055adc909" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="BrainThudCloudServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="BrainThud.Web:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/LB:BrainThud.Web:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="BrainThud.Web:Endpoint2" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/LB:BrainThud.Web:Endpoint2" />
          </inToChannel>
        </inPort>
        <inPort name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/LB:BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="BrainThud.Web:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="BrainThud.WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapBrainThud.WebInstances" />
          </maps>
        </aCS>
        <aCS name="Certificate|BrainThud.Web:BrainThud Self Signed" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapCertificate|BrainThud.Web:BrainThud Self Signed" />
          </maps>
        </aCS>
        <aCS name="Certificate|BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/MapCertificate|BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:BrainThud.Web:Endpoint1">
          <toPorts>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:BrainThud.Web:Endpoint2">
          <toPorts>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Endpoint2" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapBrainThud.Web:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/DataConnectionString" />
          </setting>
        </map>
        <map name="MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapBrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapBrainThud.WebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.WebInstances" />
          </setting>
        </map>
        <map name="MapCertificate|BrainThud.Web:BrainThud Self Signed" kind="Identity">
          <certificate>
            <certificateMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/BrainThud Self Signed" />
          </certificate>
        </map>
        <map name="MapCertificate|BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BrainThud.Web" generation="1" functional="0" release="0" software="C:\Drop\Dropbox\Projects\BrainThud\BrainThud\BrainThudCloudService\csx\Web\roles\BrainThud.Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Endpoint2" protocol="https" portRanges="8080">
                <certificate>
                  <certificateMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/BrainThud Self Signed" />
                </certificate>
              </inPort>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/SW:BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BrainThud.Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BrainThud.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Endpoint2&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0BrainThud Self Signed" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/BrainThud Self Signed" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="BrainThud Self Signed" />
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.WebInstances" />
            <sCSPolicyUpdateDomainMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.WebUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="BrainThud.WebUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="BrainThud.WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="BrainThud.WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="4db13847-8c77-4775-a14e-b9698f5d316b" ref="Microsoft.RedDog.Contract\ServiceContract\BrainThudCloudServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="1d18d9d1-6aa4-4e1d-8d3a-f7bbd99ba047" ref="Microsoft.RedDog.Contract\Interface\BrainThud.Web:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="170fa917-73a5-441c-a70f-c34ec9390719" ref="Microsoft.RedDog.Contract\Interface\BrainThud.Web:Endpoint2@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web:Endpoint2" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="4e3d8c0c-d223-4083-ab51-d316b8f38cdc" ref="Microsoft.RedDog.Contract\Interface\BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BrainThudCloudService/BrainThudCloudServiceGroup/BrainThud.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>