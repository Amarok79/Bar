
$account = Get-AzStorageAccount

Set-AzStorageBlobContent -Context $account.Context -Container drinks -File data\drinks.xml -Force
