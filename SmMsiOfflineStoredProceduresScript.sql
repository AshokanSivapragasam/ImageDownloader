


USE [SubManDB]
GO
/****** Object:  StoredProcedure [dbo].[spGetAppOfflineHtmContent]    Script Date: 12/5/2017 6:36:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author     :	v-assiva
-- Create date: 24-Nov-2017
-- Description:	Get site offline content
-- =============================================
CREATE PROCEDURE [dbo].[spGetAppOfflineHtmContent]
AS
BEGIN
		SELECT	 '<!DOCTYPE html>
<html>
    <head>
        <title>Site will no longer be available</title>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <style type="text/css">
            body { text-align: center; padding: 10%; font: 20px Helvetica, sans-serif; color: #333; }
            h1 { font-size: 50px; margin: 0; }
            article { display: block; text-align: left; max-width: 650px; margin: 0 auto; }
            a { color: #dc8100; text-decoration: none; }
            a:hover { color: #333; text-decoration: none; }
            @media only screen and (max-width : 480px) {
                h1 { font-size: 40px; }
            }
        </style>
    </head>
    <body>
        <article>
            <h1>Subscription Management site is retired now</h1>
            <p>We apologize for any inconvenience. You can create or manage new communications (aka, Topics) using <a href="#" target="_blank">Contact-Permissions-Master (CPM)</a> tool.</p>
			<p>Please take the <a href="#" target="_blank">trainings</a> now.</p>
            <p id="signature">&mdash; <a href="mailto:SMITET@microsoft.com?cc=eiengind@microsoft.com">Subscription Management Team</a></p>
        </article>
    </body>
</html>' as [AppOfflineHtmContent]
END


GO
/****** Object:  StoredProcedure [dbo].[spGetBreakingChangeNoticeCommunication]    Script Date: 12/5/2017 6:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author     :	v-assiva
-- Create date: 24-Nov-2017
-- Description:	Get Breaking Change Notice Msi
-- =============================================
CREATE PROCEDURE [dbo].[spGetBreakingChangeNoticeCommunication]
@ActionType VARCHAR(50) = 'Create\ Manage'
AS
BEGIN
	IF(@ActionType = 'CreateCommunication')
	BEGIN
		SELECT	 '<div style="border: 2px solid lightblue; padding: 15px; border-radius: 5px;"><h3 style="color: #ff8d8d; font-size: 1.8em">**Breaking change notice**</h3>
			<p>A new communication cannot be created using this application from January, 15 2018. We apologize for any inconvenience. You can manage new communications (<i>aka, topics</i>) using <a href="#" target="_blank">Contact-Permissions-Master (CPM)</a> tool.</p>
			<p>Please take the <a href="#" target="_blank">trainings</a> now.</p>
            <p id="signature">&mdash; <a href="mailto:SMITET@microsoft.com?cc=eiengind@microsoft.com">Subscription Management Team</a></p></div>' as [BreakingChangeNoticeInHtml]
			, 'False' as [CanBreakManageCommunicationPage]
	END
	ELSE
	BEGIN
		SELECT	 '<div style="border: 2px solid lightblue; padding: 15px; border-radius: 5px;"><h3 style="color: #ff8d8d; font-size: 1.8em">**Breaking change notice**</h3>
			<p>' + @ActionType + ' feature will not be available after January, 15 2018. We apologize for any inconvenience. You can manage new subscriptions using <a href="#" target="_blank">Contact-Permissions-Master (CPM)</a> tool.</p>
			<p>Please take the <a href="#" target="_blank">trainings</a> now.</p>
            <p id="signature">&mdash; <a href="mailto:SMITET@microsoft.com?cc=eiengind@microsoft.com">Subscription Management Team</a></p></div>' as [BreakingChangeNoticeInHtml]
			, 'True' as [CanBreakManageCommunicationPage]
	END

END



GO
/****** Object:  StoredProcedure [dbo].[spGetBreakingChangeNoticeMsi]    Script Date: 12/5/2017 6:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author     :	v-assiva
-- Create date: 24-Nov-2017
-- Description:	Get Breaking Change Notice Msi
-- =============================================
CREATE PROCEDURE [dbo].[spGetBreakingChangeNoticeMsi]
@SubscriptionType VARCHAR(50) = 'Subscribe\ Unsubscribe'
AS
BEGIN
	IF(@SubscriptionType = 'Subscribe')
	BEGIN
		SELECT	 '<div style="border: 2px solid red; padding: 15px; border-radius: 5px;"><h3 style="color: #ff8d8d; font-size: 1.8em">**Breaking change notice**</h3>
			<p>Subscribe feature will not be available after January, 15 2018. We apologize for any inconvenience. You can manage new subscriptions using <a href="#" target="_blank">Contact-Permissions-Master (CPM)</a> tool.</p>
			<p>Please take the <a href="#" target="_blank">trainings</a> now.</p>
            <p id="signature">&mdash; <a href="mailto:SMITET@microsoft.com?cc=eiengind@microsoft.com">Subscription Management Team</a></p></div>' as [BreakingChangeNoticeInHtml]
			, 'False' as [CanBreakMsiSubscriptionPage]
	END
	ELSE
	BEGIN
		SELECT	 '<div style="border: 2px solid red; padding: 15px; border-radius: 5px;"><h3 style="color: #ff8d8d; font-size: 1.8em">**Breaking change notice**</h3>
			<p>' + @SubscriptionType + ' feature will not be available after January, 15 2018. We apologize for any inconvenience. You can manage new subscriptions using <a href="#" target="_blank">Contact-Permissions-Master (CPM)</a> tool.</p>
			<p>Please take the <a href="#" target="_blank">trainings</a> now.</p>
            <p id="signature">&mdash; <a href="mailto:SMITET@microsoft.com?cc=eiengind@microsoft.com">Subscription Management Team</a></p></div>' as [BreakingChangeNoticeInHtml]
			, 'False' as [CanBreakMsiSubscriptionPage]
	END

END



GO
