$ScriptRoot = (Split-Path -parent $MyInvocation.MyCommand.Definition)
$RedisCacheInstance="$env:ExternalSettingsCache";

[hashtable] $settings;
$settings =@{};

$settings.Add("AllocadiaTransform","$env:getAllInxpoTenants");
$settings.Add("DataPlatform_AADInstance","$env:DataPlatform_AADInstance");
$settings.Add("IdealTimeForInxpoDeltapullEntity","$env:IdealTimeForInxpoDeltapullEntity");
$settings.Add("IdealTimeForInxpoFullpullEntity","$env:IdealTimeForInxpoFullpullEntity");

 foreach ($settingKey in $settings.Keys) 
 {
     $value=$settings."$settingKey"
 
     Write-Output "$value = $settingKey --> $RedisCacheInstance"

    & "$ScriptRoot\PowershellRedis.ps1" -RedisCacheInstance "$RedisCacheInstance" -Key "$settingKey" -Value "$value" -PortNumber "6380"
 }

Import-Module "$ScriptRoot\RedisPopulate.dll"

 ##################################### Encrypt External Variables #########################################
$settings = @{};

$settings.Add("InxpoBaseUrl","$env:InxpoBaseUrl");
$settings.Add("DataPlatform_RestEndPointBaseURL","$env:DataPlatform_RestEndPointBaseURL");
$settings.Add("DataPlatform_ClientID","$env:DataPlatform_ClientID");
$settings.Add("DataPlatform_AppKey","$env:DataPlatform_AppKey");
$settings.Add("DataPlatform_Tenant","$env:DataPlatform_Tenant");
$settings.Add("DataPlatform_APIResourceID","$env:DataPlatform_APIResourceID");

foreach ($settingKey in $settings.Keys) 
 {
     $value=$settings."$settingKey"
 
     Write-Output "$value = $settingKey --> $RedisCacheInstance"

      PopulateRedisEncrypted -Rediscacheinstance $RedisCacheInstance -Keystring $settingKey -Valuestring $value -PortNumber "6380" -KeyId "$env:KeyVaultKey" -ClientID "$env:KeyVault_ClientID" -ClientSecret "$env:KeyVault_AppKey"
 }

 ###################################### Json Updating #####################################################
$settings =@{};

#badgeAwards
$settings.Add("getbadgeData","$ScriptRoot\json\InxpoBadgeAwardsToEDP\Configurations\pullInxpoBadgeAwards.json");
$settings.Add("InxpoBadgeAwardToMip","$ScriptRoot\json\InxpoBadgeAwardsToEDP\Configurations\InxpoBadgeAwardToMip.json");

#Chatroomvisits
$settings.Add("InxpoChatroomvisitsToEDP","$ScriptRoot\json\InxpoChatroomvisitsToEDP\Configurations\InxpoChatroomvisitsToEDP.json");
$settings.Add("ChatroomvisitsTransformationEDP","$ScriptRoot\json\InxpoChatroomvisitsToEDP\Configurations\ChatroomvisitsTransformationEDP.json");

#doclinkactivities
$settings.Add("InxpoDocLinkActivitiesToEDP","$ScriptRoot\json\InxpoDocLinkActivitiesToEDP\Configurations\InxpoDocLinkActivitiesToEDP.json");
$settings.Add("DocLinkActivitysTransformationEDP","$ScriptRoot\json\InxpoDocLinkActivitiesToEDP\Configurations\DocLinkActivitysTransformationEDP.json");

#eventratings
$settings.Add("InxpoEventRatingsToEDP","$ScriptRoot\json\InxpoEventRatingsToEDP\Configurations\InxpoEventRatingsToEDP.json");
$settings.Add("EventRatingsTransformationEDP","$ScriptRoot\json\InxpoEventRatingsToEDP\Configurations\EventRatingsTransformationEDP.json");

#privatechatsengaged
$settings.Add("InxpoPrivateChatsEngagedToEDP","$ScriptRoot\json\InxpoPrivateChatsEngagedToEDP\Configurations\InxpoPrivateChatsEngagedToEDP.json");
$settings.Add("PrivateChatsEngagedTransformationEDP","$ScriptRoot\json\InxpoPrivateChatsEngagedToEDP\Configurations\PrivateChatsEngagedTransformationEDP.json");

#CommunicationCenterRecaps
$settings.Add("InxpoCommunicationCenterRecapsToEDP","$ScriptRoot\json\InxpoCommunicationCenterRecapsToEDP\Configurations\InxpoCommunicationCenterRecapsToEDP.json");
$settings.Add("CommunicationCenterRecapsTransformationEDP","$ScriptRoot\json\InxpoCommunicationCenterRecapsToEDP\Configurations\CommunicationCenterRecapsTransformationEDP.json");


#eventDates
$settings.Add("pushInxpoEventDatesToEDP","$ScriptRoot\json\InxpoEventDateToEDP\Configurations\pullInxpoEventDates.json");
$settings.Add("InxpoEventDateToMip","$ScriptRoot\json\InxpoEventDateToEDP\Configurations\InxpoEventDateToMip.json");

#showkeys
$settings.Add("pushInxpoShowsToEDP","$ScriptRoot\json\InxpoShowsToEDP\Configurations\pullInxpoShows.json");
$settings.Add("InxpoShowsToMip","$ScriptRoot\json\InxpoShowsToEDP\Configurations\InxpoShowsToMip.json");

#boothvisits
$settings.Add("pushBoothVisitsToEdp","$ScriptRoot\json\InxpoBoothVisitsToEDP\Configurations\pullInxpoBoothVisits.json");
$settings.Add("InxpoBoothVisitToMip","$ScriptRoot\json\InxpoBoothVisitsToEDP\Configurations\InxpoBoothVisitsToMip.json");

#boothtabVisits
$settings.Add("pushInxpoBoothTabVisitsToEDP","$ScriptRoot\json\InxpoBoothTabVisitsToEDP\Configurations\pullInxpoBoothTabVisits.json");
$settings.Add("InxpoBoothTabVisitToMip","$ScriptRoot\json\InxpoBoothTabVisitsToEDP\Configurations\InxpoBoothTabVisitsToMip.json");

#socialmediaActivitys
$settings.Add("pushInxpoSocialMediaActivityToEDP","$ScriptRoot\json\InxpoSocialMediaActivitiesToEDP\Configurations\pullInxpoSocialMediaAcitivities.json");
$settings.Add("InxpoSocialMediaActivityToMip","$ScriptRoot\json\InxpoSocialMediaActivitiesToEDP\Configurations\InxpoSocialMediaAcitivitiesToMip.json");

#ShowFloorVisits
$settings.Add("pushShowFloorVisitsToEdp","$ScriptRoot\json\InxpoShowFloorVisitsToEDP\Configurations\pullInxpoShowFloorVisits.json");
$settings.Add("InxpoShowFloorVisitToMip","$ScriptRoot\json\InxpoShowFloorVisitsToEDP\Configurations\InxpoShowFloorVisitsToMip.json");

#EventLogins
$settings.Add("Workflow_PullInxpoEventLogins","$ScriptRoot\json\InxpoEventLoginsToEDP\Configurations\Workflow_PullInxpoEventLogins.json");
$settings.Add("Transformation_InxpoEventLoginsToEDP","$ScriptRoot\json\InxpoEventLoginsToEDP\Configurations\Transformation_InxpoEventLoginsToEDP.json");

#EventSurveyResponsesToEDP
$settings.Add("pullInxpoEventSurveyResponses","$ScriptRoot\json\InxpoEventSurveyResponsesToEDP\Configurations\pullInxpoEventSurveyResponses.json");
$settings.Add("InxpoEventSurveyResponsesToMIP","$ScriptRoot\json\InxpoEventSurveyResponsesToEDP\Configurations\InxpoEventSurveyResponsesToMIP.json");

#ProfileDetails
$settings.Add("InxpoProfilesToEdp","$ScriptRoot\json\InxpoProfileDetailsToEDP\Configurations\pullInxpoProfiles.json");
$settings.Add("InExpoProfileToMipProfileTransformation","$ScriptRoot\json\InxpoProfileDetailsToEDP\Configurations\InxpoProfilesToMip.json");
$settings.Add("InExpoProfileShowSurveyQuestionEntityToEDPTransformation", "$ScriptRoot\json\InxpoProfileDetailsToEDP\Configurations\InXpoProfileShowSurveyQuestion-Transformation.json");

#Presentations
$settings.Add("InxpoPresentationsToEDP","$ScriptRoot\json\InxpoPresentationsToEDP\Configurations\InxpoPresentationsToEDP.json");
$settings.Add("PresentationsTransformationEDP","$ScriptRoot\json\InxpoPresentationsToEDP\Configurations\PresentationsTransformationEDP.json");

#eventVisits
$settings.Add("INXPOEventVisitsToEDP","$ScriptRoot\json\InxpoEventVisitsToEDP\Configurations\INXPOEventVisitsToEDP.json");
$settings.Add("InxpoEventVisitsTransformationToEDP","$ScriptRoot\json\InxpoEventVisitsToEDP\Configurations\InxpoEventVisitsTransformationToEDP.json");

#ShowSurvey
$settings.Add("InxpoShowSurveysToEDP","$ScriptRoot\json\InxpoShowSurveyToEDP\Configurations\InxpoShowSurveyToEDP-workflow.json");
$settings.Add("InxpoShowSurveyToEDPTransformation", "$ScriptRoot\json\InxpoShowSurveyToEDP\Configurations\InxpoShowSurveyToEDPTransformation.json");
$settings.Add("InxpoShowSurveyQuestionAnswersToEDPTransformation", "$ScriptRoot\json\InxpoShowSurveyToEDP\Configurations\InxpoShowSurveyQuestionAnswersToEDPTransformation.json");

$ScriptRoot = (Split-Path -parent $MyInvocation.MyCommand.Definition)



$RedisCacheInstanceTransform="$env:TransformationSettingsCache";
$RedisCacheInstanceWorkFlow ="$env:WorkFlowSettingsCache";

foreach ($settingKey in $settings.Keys) 
{
    $value=$settings."$settingKey"
 
    Write-Output "$value = $settingKey --> Json"

PopulateRedisJson -RediscacheinstanceWorkFlow "$RedisCacheInstanceWorkFlow" -RediscacheinstanceTransform "$RedisCacheInstanceTransform" -Keystring "$settingKey" -Jsonfilepath "$value" -PortNumber "6380"


 }
 