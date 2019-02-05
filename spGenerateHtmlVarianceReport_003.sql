USE [SubManDB]
GO

/****** Object:  StoredProcedure [dbo].[spGenerateHtmlVarianceReport]    Script Date: 7/6/2015 4:39:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGenerateHtmlVarianceReport] (@ReportForDate date)
AS
BEGIN
	DECLARE @emailBody NVARCHAR(max);
	DECLARE @emailSubject NVARCHAR(max);
	DECLARE @tempText NVARCHAR(max);
	DECLARE @ThresholdLevel BIGINT;

	SET @ThresholdLevel = 10000000;

	SET @emailBody = '<html>
	<head>
		<style>
			body { font-family: Segoe UI; font-size: 0.8em}
			table { font-family: Segoe UI; border: solid 2px teal; border-collapse: collapse; font-size: 0.83em}
			th { border: 1px solid black; text-align: left}
			td { border: 1px solid black; }
			table tr.header { background: #02add3; color: #FEFAFE; align: left}
			.DiscrepanciesWithinThreshold { background: #9DE69D}
			.DiscrepanciesAboveThreshold { background: #FF8D8D}
		</style>
	</head>
	<body>

Hi All,
<br/>
<br/>
Subscription Management (<i>SM</i>) is the web based solution that allows the users to create/ update communications (<i>like Newsletters, Limited Programs</i>). SM will be spreading the communication related data to other data systems to keep them in sync with each other. Data systems choose different mediums for the data transaction so there are fair chances for data loss.<br/>
<br/>
SFMC is also pulling the subscription related data from Geneva (<i>Data sourced from MS-I</i>) to keep sync with MS-I data systems<br/>
<br/>
The purpose of this email is to get the single version of truth on counts & handshake that data in all of our data systems are in sync with each other.<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Genesis<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Geneva<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Salesforce Marketing Cloud<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Subscription Management<br/>
<br/>

<table>
<tr class=''header''>
<td colspan=''4'' align=''center'' style=''font-size: 1.3em; font-weight: bold''>THRESHOLD LEVELS</td>
</tr>
<tr class=''header''>
	<th>Color code</th>
	<th>Threshold value</th>
	<th>Severity</th>
	<th>Description</th>
</tr>
<tr style=''background: #9DE69D''>
	<td>Code green</td>
	<td>0 - '+convert(varchar(max), @ThresholdLevel)+'</td>
	<td>Low & ignorable</td>
	<td>Discrepancies within this band are ignorable</td>
</tr>
<tr style=''background: #FF8D8D''>
	<td>Code red</td>
	<td> &gt;'+convert(varchar(max), @ThresholdLevel)+'</td>
	<td>High</td>
	<td>Discrepancies in this band are not desirable and needs attention</td>
</tr>
</table>
<br/>

' +
(SELECT TOP 1
'<table>
<tr class=''header''>
<td colspan=''2'' align=''center'' style=''font-size: 1.3em; font-weight: bold''>STATUS OF TASKS</td>
</tr>
<tr class=''header''>
<th>TaskName</th>
<th>ProcessingStatus</th>
</tr>
<tr>'+
(CASE WHEN (ISNULL([Genesis_Communication_Total],0)+
ISNULL([Genesis_Communications_Active],0)+
ISNULL([Genesis_CommunicationClass_Total],0)) = 0 
THEN '<td class=''DiscrepanciesAboveThreshold''>Genesis_DataPull</td>
<td class=''DiscrepanciesAboveThreshold''>NotDoneYet</td>'
ELSE '<td>Genesis_DataPull</td>
<td>Completed</td>'
END)
+
'</tr>
<tr>'+
(CASE WHEN (ISNULL([SFMC_Communication_Total],0)+
ISNULL([SFMC_Communications_Active],0)+
ISNULL([SFMC_CommunicationClass_Total],0)+
ISNULL([SFMC_Subscription_Subscribed],0)+
ISNULL([SFMC_Subscription_DistinctSubscribed],0)+
ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable],0)+
ISNULL([SFMC_Standalone_Subscribed],0)+
ISNULL([SFMC_Standalone_DistinctSubscribed],0)+
ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable],0)+
ISNULL([SFMC_Answer_Total],0)) = 0 
THEN '<td class=''DiscrepanciesAboveThreshold''>SFMC_DataPull</td>
<td class=''DiscrepanciesAboveThreshold''>NotDoneYet</td>'
ELSE '<td>SFMC_DataPull</td>
<td>Completed</td>'
END)
+
'</tr>
<tr>'+
(CASE WHEN (ISNULL([Geneva_Subscription_Subscribed],0)+
ISNULL([Geneva_Subscription_DistinctSubscribed],0)+
ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable],0)+
ISNULL([Geneva_Standalone_Subscribed],0)+
ISNULL([Geneva_Standalone_DistinctSubscribed],0)+
ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable],0)+
ISNULL([Geneva_Answer_Total],0)) = 0 
THEN '<td class=''DiscrepanciesAboveThreshold''>Geneva_DataPull</td>
<td class=''DiscrepanciesAboveThreshold''>NotDoneYet</td>'
ELSE '<td>Geneva_DataPull</td>
<td>Completed</td>'
END)
+
'</tr>
<tr>'+
(CASE WHEN (ISNULL([Sm_Communication_Total],0)+
ISNULL([Sm_Communications_Active],0)+
ISNULL([Sm_CommunicationClass_Total],0)) = 0 
THEN '<td class=''DiscrepanciesAboveThreshold''>SM_DataPull</td>
<td class=''DiscrepanciesAboveThreshold''>NotDoneYet</td>'
ELSE '<td>SM_DataPull</td>
<td>Completed</td>'
END)
+
'</tr>
</table>
<br/>'
from ReconciliationSummary WHERE ReportForDate = @ReportForDate)
+
'[VarianceReportEtGeneva]
<br/>
[VarianceReportGenesisSmExactTarget]
<br/>
Please reach out to Email Interchange Engineering Team (eiengind@microsoft.com)
<br/>
<br/>
Thanks,<br/>
EI Engg Team<br/>
	</body>
</html>'

	SET @tempText = N'<table>
						<tr class=''header''>
							<td colspan=''4'' align=''center'' style=''font-size: 1.3em; font-weight: bold''>VARIANCE REPORT (SALESFORCE MARKETING CLOUD VS GENEVA)</td>
						</tr>
						<tr class=''header''>
						<th>Attribute name</th>
						<th>Salesforce Marketing Cloud</th>
						<th>Geneva</th>
						<th>Difference (Salesforce Marketing Cloud - Geneva)</th>
						</tr>' + cast((
				SELECT N'<tr>
									<td>Subscription Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Subscription_Subscribed], 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_Subscribed], 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0))) 
									+ '</td></tr>' 
								+ '<tr><td>Subscription Distinct Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Subscription_DistinctSubscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_DistinctSubscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Subscription Distinct Subscribed And Contactable</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Standalone_Subscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_Subscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Distinct Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Standalone_DistinctSubscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_DistinctSubscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Distinct Subscribed And Contactable</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], ''))) + '</td></tr>' 
								+ '<tr><td>Answer Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Answer_Total], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Answer_Total], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) <= @ThresholdLevel
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Answer_Total], '') - ISNULL([Geneva_Answer_Total], ''))) + '</td></tr>'
						FROM ReconciliationSummary
						WHERE ReportForDate = @ReportForDate
				) AS NVARCHAR(MAX)) + N'</table>';
	SET @tempText = REPLACE(REPLACE(@tempText, '<tdc>', '<td align=''center''>'), '</tdc>', '</td>')
	SET @emailBody = replace(@emailBody, '[VarianceReportEtGeneva]', @tempText);

	SET @tempText = N'<table>
						<tr class=''header''>
							<td colspan=''7'' align=''center'' style=''font-size: 1.3em; font-weight: bold''>VARIANCE REPORT (SALESFORCE MARKETING CLOUD VS GENESIS VS SUBSCRIPTION MANAGEMENT)</td>
						</tr>
						<tr class=''header''>
						<th>Attribute name</th>
						<th>Salesforce Marketing Cloud</th>
						<th>Genesis</th>
						<th>Subscription Management</th>
						<th>Difference #1 (ET - Genesis)</th>
						<th>Difference #1 (SM - ET)</th>
						<th>Difference #1 (SM - Genesis)</th>
						</tr>' + cast((
				SELECT N'<tr>
									<td>Communication Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(SFMC_Communication_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_Communication_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_Communication_Total, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(SFMC_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(SFMC_Communication_Total, '') - ISNULL(Genesis_Communication_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(SFMC_Communication_Total, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communication_Total, '') - ISNULL(SFMC_Communication_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communication_Total, '') - ISNULL(Genesis_Communication_Total, ''))) + N'</td>
									</tr>
									<tr>
									<td>Communication Active</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(SFMC_Communications_Active, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_Communications_Active, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_Communications_Active, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(SFMC_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(SFMC_Communications_Active, '') - ISNULL(Genesis_Communications_Active, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(SFMC_Communications_Active, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communications_Active, '') - ISNULL(SFMC_Communications_Active, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communications_Active, '') - ISNULL(Genesis_Communications_Active, ''))) + N'</td>
									</tr>
									<tr>
									<td>CommunicationClass Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(SFMC_CommunicationClass_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_CommunicationClass_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_CommunicationClass_Total, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(SFMC_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(SFMC_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(SFMC_CommunicationClass_Total, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(SFMC_CommunicationClass_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) = 0
										THEN 'DiscrepanciesWithinThreshold'
										ELSE 'DiscrepanciesAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, ''))) + N'</td>
									</tr>'
						FROM ReconciliationSummary
						WHERE ReportForDate = @ReportForDate
				) AS NVARCHAR(MAX)) + N'</table>';
	SET @tempText = REPLACE(REPLACE(@tempText, '<tdc>', '<td align=''center''>'), '</tdc>', '</td>')
	SET @emailBody = replace(@emailBody, '[VarianceReportGenesisSmExactTarget]', @tempText);

	SET @emailSubject = convert(varchar, @ReportForDate, 106) + ' | ' + (CASE WHEN @emailBody like '%''DiscrepanciesAboveThreshold''%' THEN 'Attention! Code red | '
						ELSE 'No action | '
						END)

	SET @emailSubject = @emailSubject + 'Reconciliation variance report'

	SELECT @emailBody, @emailSubject;
END

GO

grant execute on spGenerateHtmlVarianceReport to [redmond\a2lsmdev] 
