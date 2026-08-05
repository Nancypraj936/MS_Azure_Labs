$location = "EastUS"
$rgName   = "rg-az204-dev-01"

$planName = "asp-dev-eus-01"
$appName  = "web-dev-eus-01"

# 1) Create a resource group
New-AzResourceGroup -Name $rgName -Location $location

# 2) Create an Azure App Service (App Service Plan)
# Note: -Linux makes this a Linux plan (required for linuxFxVersion stacks like DOTNETCORE|x.x)
New-AzAppServicePlan `
  -Name $planName `
  -ResourceGroupName $rgName `
  -Location $location `
  -Tier "Basic" `
  -NumberofWorkers 1 `
  -Linux
  
 # 3) Create an Azure Web App (and set runtime to .NET 10)
# Create the web app in the plan
New-AzWebApp `
  -Name $appName `
  -ResourceGroupName $rgName `
  -Location $location `
  -AppServicePlan $planName
  
# Set the runtime stack for Linux by updating site config (linuxFxVersion)
# The runtime stack format for Linux App Service is: "DOTNETCORE|<major.minor>" (pipe, not colon). :contentReference[oaicite:0]{index=0}
# .NET 10 stack value: "DOTNETCORE|10.0". :contentReference[oaicite:1]{index=1}
Set-AzResource `
  -ResourceGroupName $rgName `
  -ResourceType "Microsoft.Web/sites/config" `
  -ResourceName "web-dev-eus-200/web" `
  -ApiVersion "2022-09-01" `
  -Properties @{ linuxFxVersion = "DOTNETCORE|10.0" } `
  -Force