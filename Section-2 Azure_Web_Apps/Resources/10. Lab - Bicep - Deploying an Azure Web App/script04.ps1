@description('Azure region for all resources')
param location string = resourceGroup().location

@description('App Service plan name')
param planName string = 'asp-dev-eus-01'

@description('Web App name (must be globally unique)')
param webAppName string = 'web-deb-eus-01'

@description('Deployment slot name')
param slotName string = 'staging'

@description('App Service plan SKU. Use Standard (S1) or higher for deployment slots.')
param skuName string = 'S1'

@description('Instance count (scale out)')
@minValue(1)
param workerCount int = 1

@description('Linux runtime stack for the Web App')
param linuxFxVersion string = 'DOTNETCORE|10.0'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: planName
  location: location
  sku: {
    name: skuName
    tier: 'Standard'
    size: skuName
    capacity: workerCount
  }
  properties: {
    // true = Linux plan
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2022-09-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: linuxFxVersion
    }
  }
}

resource webAppSlot 'Microsoft.Web/sites/slots@2022-09-01' = {
  name: '${webApp.name}/${slotName}'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: linuxFxVersion
    }
  }
}

