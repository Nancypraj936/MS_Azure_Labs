$slotName = "staging"

Set-AzAppServicePlan `
  -ResourceGroupName $rgName `
  -Name $planName `
  -Tier "Standard" `
  -WorkerSize "Small" `
  -NumberofWorkers 2
  
 # 2) Create a deployment slot for the Web App
New-AzWebAppSlot `
  -ResourceGroupName $rgName `
  -Name $appName `
  -Slot $slotName