$location = "eastus"
$rg       = "rg-az204-dev-01"
$plan     = "asp-dev-web-01"
$app      = "web-dev-eus-300"
$slot     = "staging"

# 1) Create a resource group
az group create --name $rg --location $location

# 2) Create an App Service plan (use Standard tier so deployment slots are supported)
# --number-of-workers 1 = start with 1 instance
az appservice plan create `
  --resource-group $rg `
  --name $plan `
  --location $location `
  --sku B1 `
  --is-linux `
  --number-of-workers 1

# 3) Create the Web App and set the runtime to .NET 10 (Linux)
az webapp create --resource-group $rg --plan $plan --name $app --runtime "DOTNETCORE:10.0"

az appservice plan update --resource-group $rg --name $plan --sku S1

az webapp deployment slot create --resource-group $rg --name $app --slot $slot