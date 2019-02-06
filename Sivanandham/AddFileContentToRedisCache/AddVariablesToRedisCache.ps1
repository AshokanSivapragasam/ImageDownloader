#Get working directory#
$scriptRoot = (Split-Path -parent $MyInvocation.MyCommand.Definition)

#Get Connection string for Redis Cache Container #
$redisCacheContainerConnectionString = "$env:RedisCacheContainerConnectionString";

#Create a hash table#
[hashtable] $cacheItems;
$cacheItems = @{};

#Add any number of key-contentof(file) pair to following hashtable#
$cacheItems.Add("AllocadiaSubscriberMapping", "$env:AllocadiaSubscriberMappingJsonFilePath");

 foreach ($cacheItemKey in $cacheItems.Keys) 
 {
	#Prepare the complete path for json file#
	$filePathForCacheItemValue = $scriptRoot + $cacheItems."$cacheItemKey"

	#Add key-contentof(file) pair to Redis Cache Container#
	& "$ScriptRoot\HelperAddFileContentToRedisCache.ps1" -RedisCacheContainerConnectionString "$redisCacheContainerConnectionString" -CacheItemKey "$cacheItemKey" -FilePathForCacheItemValue "$filePathForCacheItemValue" -PortNumber "6380"
 }

 #Clear hashtable#
$cacheItems = @{};
