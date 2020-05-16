
$account = Get-AzStorageAccount

Get-ChildItem -Path .\data -Filter *.xml | ForEach-Object {
    Set-AzStorageBlobContent -Context $account.Context -Container drinks -File $_ -Force
}
