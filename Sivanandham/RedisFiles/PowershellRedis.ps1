param
(
    [Parameter(Mandatory = $true)]
    [String]
    $RedisCacheInstance,

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

#Import RedisPopulate#
Import-Module "$ScriptRoot\RedisPopulate.dll"
$CacheItemValue = [IO.File]::ReadAllText($FilePathForCacheItemValue)
PopulateRedis -Rediscacheinstance $RedisCacheInstance -Keystring $CacheItemKey -Valuestring $CacheItemValue -PortNumber $PortNumber