<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="SimuladorCooperativaVerde" generation="1" functional="0" release="0" Id="40aeb813-fe2c-4702-a73f-0f460ae2b697" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="SimuladorCooperativaVerdeGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="SimuladorCooperativaVerde.ServiceBusRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/MapSimuladorCooperativaVerde.ServiceBusRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaVerde.ServiceBusRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/MapSimuladorCooperativaVerde.ServiceBusRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SimuladorCooperativaVerde.ServiceBusRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/MapSimuladorCooperativaVerde.ServiceBusRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapSimuladorCooperativaVerde.ServiceBusRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/SimuladorCooperativaVerde.ServiceBusRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaVerde.ServiceBusRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/SimuladorCooperativaVerde.ServiceBusRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapSimuladorCooperativaVerde.ServiceBusRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/SimuladorCooperativaVerde.ServiceBusRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="SimuladorCooperativaVerde.ServiceBusRole" generation="1" functional="0" release="0" software="C:\@TFS\Tesis\SimuladorCooperativaVerde\SimuladorCooperativaVerde\csx\Debug\roles\SimuladorCooperativaVerde.ServiceBusRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SimuladorCooperativaVerde.ServiceBusRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;SimuladorCooperativaVerde.ServiceBusRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/SimuladorCooperativaVerde.ServiceBusRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/SimuladorCooperativaVerde.ServiceBusRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/SimuladorCooperativaVerde/SimuladorCooperativaVerdeGroup/SimuladorCooperativaVerde.ServiceBusRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="SimuladorCooperativaVerde.ServiceBusRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="SimuladorCooperativaVerde.ServiceBusRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="SimuladorCooperativaVerde.ServiceBusRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>