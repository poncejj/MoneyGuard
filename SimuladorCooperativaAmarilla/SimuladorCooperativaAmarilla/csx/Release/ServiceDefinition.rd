<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="SimuladorCooperativaAmarilla" generation="1" functional="0" release="0" Id="b50ed4de-3eeb-43ef-a8ff-c27a4b31f281" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="SimuladorCooperativaAmarillaGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/LB:SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapCertificate|SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaAmarilla.ServiceBusRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/MapSimuladorCooperativaAmarilla.ServiceBusRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaAmarilla.ServiceBusRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="SimuladorCooperativaAmarilla.ServiceBusRole" generation="1" functional="0" release="0" software="C:\@TFS\Tesis\SimuladorCooperativaAmarilla\SimuladorCooperativaAmarilla\csx\Release\roles\SimuladorCooperativaAmarilla.ServiceBusRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SW:SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SimuladorCooperativaAmarilla.ServiceBusRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;SimuladorCooperativaAmarilla.ServiceBusRole&quot;&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="SimuladorCooperativaAmarilla.ServiceBusRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="SimuladorCooperativaAmarilla.ServiceBusRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="SimuladorCooperativaAmarilla.ServiceBusRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="5c1a0f37-1cbc-4101-be7d-480f639a1de3" ref="Microsoft.RedDog.Contract\ServiceContract\SimuladorCooperativaAmarillaContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="81c851c3-649a-4904-9b02-66e2587e6fd8" ref="Microsoft.RedDog.Contract\Interface\SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/SimuladorCooperativaAmarilla/SimuladorCooperativaAmarillaGroup/SimuladorCooperativaAmarilla.ServiceBusRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>