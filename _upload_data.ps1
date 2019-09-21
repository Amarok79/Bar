
$account = Get-AzStorageAccount

Get-ChildItem -Path .\data | ForEach-Object {
    Set-AzStorageBlobContent -Context $account.Context -Container drinks -File $_ -Force
}
