USE [InterchangeDB1]
GO

/****** Object:  StoredProcedure [dbo].[AddRequestReceipt]    Script Date: 4/13/2017 12:34:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[AddXmlSoapRequestResponse] @ApiMethod [nvarchar](50) = NULL,
	@StackMethod [nvarchar](200) = NULL,
	@ApiAction [nvarchar](100) = NULL,
	@ApiOptions [xml] = NULL,
	@ApiObjects [xml] = NULL,
	@ApiResults [xml] = NULL,
	@AdditionalInformation [xml] = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		INSERT INTO XmlSoapRequestResponse (ApiMethod,
			 StackMethod,
			 ApiAction,
			 ApiOptions,
			 ApiObjects,
			 ApiResults,
			 AdditionalInformation,
			 EventDatetime) 
		 values (@ApiMethod,
		         @StackMethod,
				 @ApiAction,
				 @ApiOptions,
				 @ApiObjects,
				 @ApiResults,
				 @AdditionalInformation,
				 GETUTCDATE())

		IF @@TranCount > 0
			COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000)
		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT

		SELECT @ErrorMessage = Error_message()
			,@ErrorSeverity = Error_severity()
			,@ErrorState = Error_state()

		-- Use RAISERROR inside the CATCH block to return error information about the original error that caused            
		-- execution to jump to the CATCH block.              
		IF @@TranCount > 0
			ROLLBACK TRANSACTION

		RAISERROR (
				@ErrorMessage -- Message text.              
				,@ErrorSeverity -- Severity.              
				,@ErrorState -- State.              
				)
	END CATCH
END

GO


