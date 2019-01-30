/*
 * Registering certificate thumbprint for client, 'AzureSql_PROD' in interchangeuser table
 */
 
if (not exists (select 1 from dbo.InterchangeUser where [CertificateThumbprint] = 'D465D8E7BC9D768A815FB801930F06F614B03146' ))
begin
INSERT INTO [dbo].[InterchangeUser]
           ([Alias]
           ,[CertificateThumbprint]
           ,[ValidTriggeredSendTypes]
           ,[APIAccessEnabled]
           ,[TriggeredRequestEnabled]
           ,[ClassificationId]
           ,[AccountId]
           ,[ThumbprintExpirationDate])
     VALUES
           ('AzureSql_PROD'
           ,'D465D8E7BC9D768A815FB801930F06F614B03146'
           ,'Batch,NoDelay'
           ,1
           ,1
           ,3
           ,'96540'
           ,null)
END


/*
 * Add preferences for new client, 'AzureSql_PROD' in bulk-send configuration table
 */
declare @maxBulkSendConfigurationId int;
declare @interchangeuserid int;
select @maxBulkSendConfigurationId = max(BulkSendConfigurationId)+1 from [dbo].[BulkSendConfiguration]

select @interchangeuserid = interchangeuserid from [dbo].[InterchangeUser] where [CertificateThumbprint] = 'D465D8E7BC9D768A815FB801930F06F614B03146'

if (not exists(select 1 from [dbo].[BulkSendConfiguration] where interchangeuserid = @interchangeuserid))
begin
INSERT INTO [dbo].[BulkSendConfiguration]
           ([BulkSendConfigurationId]
           ,[InterchangeUserId]
           ,[TenantAccountId]
           ,[MasterDEKey]
           ,[SendLogDEKey]
           ,[TenantBatchIDField]
           ,[TenantBulkSendField]
           ,[FileFormat]
           ,[ImportFileType]
           ,[TenantTrackingField]
           ,[IsSendInvoke]
           ,[IsDynamicDataExtension]
           ,[DataImportType]
           ,[DynamicDataExtensionTemplateName]
           ,[SendableSubscriberField]
           ,[SendableDataExtensionField])
     VALUES
           (@maxBulkSendConfigurationId
           ,@interchangeuserid
		   ,10333954
		   ,'AzureSql_Static_DE'
		   ,'DataExtensionObject[SendLog-BG_23Sub]'
		   ,'BatchGUID'
		   ,'BulkSendGUID'
		   ,'TAB'
		   ,'.zip,.gz,.tsv'
		   ,'BulkSendGUID'
		   ,1
		   ,0
		   ,'Overwrite'
		   ,'TriggeredDataExtension'
		   ,'EmailAddress'
		   ,'EmailAddress'
           )
end

/*
 * Add preferences for new client, 'AzureSql_PROD' in bulk-send classification table
 */
declare @maxBulkSendClassificationId int;
declare @bulkSendConfigurationId int;

select @maxBulkSendClassificationId = max(BulkSendClassificationId)+1 from [dbo].[BulkSendClassification]
select @interchangeuserid = interchangeuserid from [dbo].[InterchangeUser] where [CertificateThumbprint] = 'D465D8E7BC9D768A815FB801930F06F614B03146'
select @bulkSendConfigurationId = bulkSendConfigurationId from [dbo].[BulkSendConfiguration] where interchangeuserid = @interchangeuserid 

if (not exists (select 1 from dbo.bulksendclassification where bulkSendConfigurationId = @bulkSendConfigurationId and EmailType = 'Transactional'))
begin
INSERT INTO [dbo].[BulkSendClassification]
           ([BulkSendClassificationId]
           ,[BulkSendConfigurationId]
           ,[TenantAccountId]
           ,[EmailType]
           ,[SendClassificationKey])
     VALUES
           (@maxBulkSendClassificationId
           ,@bulkSendConfigurationId
           ,'10333954'
           ,'Transactional'
           ,'5397')

end

select @maxBulkSendClassificationId = max(BulkSendClassificationId)+1 from [dbo].[BulkSendClassification]

if (not exists (select 1 from dbo.bulksendclassification where bulkSendConfigurationId = @bulkSendConfigurationId and EmailType = 'Promotional'))
begin

INSERT INTO [dbo].[BulkSendClassification]
           ([BulkSendClassificationId]
           ,[BulkSendConfigurationId]
           ,[TenantAccountId]
           ,[EmailType]
           ,[SendClassificationKey])
     VALUES
           (@maxBulkSendClassificationId
           ,@bulkSendConfigurationId
           ,'10333954'
           ,'Promotional'
           ,'4412')
end