<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="SimuladorServiciosBasicos" generation="1" functional="0" release="0" Id="0126d820-8b6a-4c1a-a75a-07d29b2447dd" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="SimuladorServiciosBasicosGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="ServiceBusRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/MapServiceBusRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ServiceBusRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/MapServiceBusRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapServiceBusRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/ServiceBusRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapServiceBusRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/ServiceBusRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ServiceBusRole" generation="1" functional="0" release="0" software="C:\@TFS\Tesis\SimuladorServiciosBasicos\SimuladorServiciosBasicos\csx\Debug\roles\ServiceBusRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ServiceBusRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ServiceBusRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/ServiceBusRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/ServiceBusRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/SimuladorServiciosBasicos/SimuladorServiciosBasicosGroup/ServiceBusRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="ServiceBusRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="ServiceBusRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="ServiceBusRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>