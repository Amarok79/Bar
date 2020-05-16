
$account = Get-AzStorageAccount

Get-ChildItem -Path .\data -Filter *.jpg | ForEach-Object {
    Set-AzStorageBlobContent -Context $account.Context -Container drinks -File $_ -Force
}
