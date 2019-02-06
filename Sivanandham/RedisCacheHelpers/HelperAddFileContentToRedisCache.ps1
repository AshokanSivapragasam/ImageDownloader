#Input parameters#
param
(
    [Parameter(Mandatory = $true)]
    [String]
    $RedisCacheContainerConnectionString,

    [Parameter(Mandatory = $true)]
    [String]
    $CacheItemKey,

    [Parameter(Mandatory = $true)]
    [String]
    $FilePathForCacheItemValue,

    [Parameter(Mandatory = $false)]
    [String]
    $PortNumber
)

#Get working directory#
$ScriptRoot = (Split-Path -parent $MyInvocation.MyCommand.Definition)

#Import the class libary, 'RedisPopulate'#
Import-Module "$ScriptRoot\RedisPopulate.dll"

#Get content of the json file#
$CacheItemValue = [IO.File]::ReadAllText($FilePathForCacheItemValue)

#Add key-value pair to Redis Cache Container#
PopulateRedis -Rediscacheinstance $RedisCacheContainerConnectionString -Keystring $CacheItemKey -Valuestring $CacheItemValue -PortNumber $PortNumber