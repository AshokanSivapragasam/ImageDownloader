ALTER PROCEDURE [dbo].[spGenerateHtmlVarianceReport] (@ReportForDate date)
AS
BEGIN
	DECLARE @emailBody NVARCHAR(max);
	DECLARE @emailSubject NVARCHAR(max);
	DECLARE @tempText NVARCHAR(max);
	DECLARE @IgnorableThresholdLevel BIGINT;
	DECLARE @ThresholdLevel BIGINT;

	SET @IgnorableThresholdLevel = 400;
	SET @ThresholdLevel = 10000000;

	SET @emailBody = '<html>
	<head>
		<style>
			body { font-family: Segoe UI; font-size: 0.8em}
			table { font-family: Segoe UI; border: solid 2px teal; border-collapse: collapse; font-size: 0.83em}
			th { border: 1px solid black; text-align: left}
			td { border: 1px solid black; }
			table tr.header { background: #02add3; color: #FEFAFE; align: left}
			//tr:nth-child(even) { background: #FFF }
			//tr:nth-child(odd) { background: #E3E3E3 }
			.NoDiscrepancy { background: #87c540}
			.LessDiscrepancyLevel { background: #FFFFB2}
			.MediumDiscrepancyLevelWithinThreshold { background: #FFC4C4}
			.HighDiscrepancyLevelAboveThreshold { background: #FF4D4D}
		</style>
	</head>
	<body>

Hi All,
<br/>
<br/>
<b><u>Geneva:</u></b><br/>
It is a database solution that downloads the useful data from other data systems and organizes/ manages those data in a Data-Mart. For example,<br/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- NotSent<br/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Bounces<br/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Unsub<br/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- SendJobs<br/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- SendLog<br/>
<br/>

<b><u>Salesforce Marketing Cloud (SFMC):</u></b><br/>
It is a web based solution that manages the whole cycle of email and email-associated features like,<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Subscriptions/ Unsubcriptions<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Planned notifications<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Email tracking abilities<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Email customizations<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Automation<br/>
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Segmentation of users by fields<br/>
<br/>

<b><u>Genesis:</u></b><br/>
It is a database solution that exposes the APIs to create any communications (<i>Newsletters, Limited-Programs,...</i>) and is considered to be primary source for all communications.<br/>
<br/>

<b><u>Subscription Management:</u></b><br/>
It is a web based solution that allows end users to create any communications (<i>Newsletters, Limited-Programs,...</i>) via Genesis APIs and the data will be shared across all data systems (<i>Genesis, SFMC</i>) to keep them in sync with each other.<br/>
<br/>
<br/>
The purpose of this email is to confirm & handshake that data in all of our data systems involved are in sync with each other.
<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Genesis<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Geneva<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Salesforce Marketing Cloud<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Subscription Management<br/>
<br/>

<table>
<tr class=''header''>
<td colspan=''2'' align=''center'' style=''font-size: 1.3em; font-weight: bold''>COLOR CODES</td>
</tr>
<tr class=''header''>
	<th>Color code</th>
	<th>Description</th>
</tr>
<tr>
	<td class=''NoDiscrepancy''>Code green</td>
	<td>No action</td>
</tr>
<tr>
	<td class=''LessDiscrepancyLevel''>Code light yellow</td>
	<td>Discrepancies are at ignorable level</td>
</tr>
<tr>
	<td class=''MediumDiscrepancyLevelWithinThreshold''>Code light red</td>
	<td>Discrepancies are considerably high but within threshold</td>
</tr>
<tr>
	<td class=''HighDiscrepancyLevelAboveThreshold''>Code red</td>
	<td>Attention! Discrepancy level exceeded threshold</td>
</tr>
</table>
<br/>

<table>
<tr class=''header''>
<td colspan=''3'' align=''center'' style=''font-size: 1.3em; font-weight: bold''>THRESHOLD LEVELS</td>
</tr>
<tr class=''header''>
	<th>Threshold value</th>
	<th>Severity</th>
	<th>Description</th>
</tr>
<tr class=''LessDiscrepancyLevel''>
	<td>1-'+convert(varchar(max), @IgnorableThresholdLevel)+'</td>
	<td>Low & ignorable</td>
	<td>Discrepancies within this band can be ignorable</td>
</tr>
<tr class=''MediumDiscrepancyLevelWithinThreshold''>
	<td>'+convert(varchar(max), (@IgnorableThresholdLevel+1))+'-'+convert(varchar(max), @ThresholdLevel)+'</td>
	<td>Medium, within threshold</td>
	<td>Discrepancies within this band needs attention; but within threshold level</td>
</tr>
<tr class=''HighDiscrepancyLevelAboveThreshold''>
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
THEN '<td class=''HighDiscrepancyLevelAboveThreshold''>Genesis_DataPull</td>
<td class=''HighDiscrepancyLevelAboveThreshold''>NotDoneYet</td>'
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
THEN '<td class=''HighDiscrepancyLevelAboveThreshold''>SFMC_DataPull</td>
<td class=''HighDiscrepancyLevelAboveThreshold''>NotDoneYet</td>'
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
THEN '<td class=''HighDiscrepancyLevelAboveThreshold''>Geneva_DataPull</td>
<td class=''HighDiscrepancyLevelAboveThreshold''>NotDoneYet</td>'
ELSE '<td>Geneva_DataPull</td>
<td>Completed</td>'
END)
+
'</tr>
<tr>'+
(CASE WHEN (ISNULL([Sm_Communication_Total],0)+
ISNULL([Sm_Communications_Active],0)+
ISNULL([Sm_CommunicationClass_Total],0)) = 0 
THEN '<td class=''HighDiscrepancyLevelAboveThreshold''>SM_DataPull</td>
<td class=''HighDiscrepancyLevelAboveThreshold''>NotDoneYet</td>'
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
									+ (CASE WHEN ABS(ISNULL([SFMC_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0))) 
									+ '</td></tr>' 
								+ '<tr><td>Subscription Distinct Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Subscription_DistinctSubscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_DistinctSubscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Subscription Distinct Subscribed And Contactable</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Standalone_Subscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_Subscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Distinct Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Standalone_DistinctSubscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_DistinctSubscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Distinct Subscribed And Contactable</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([SFMC_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], ''))) + '</td></tr>' 
								+ '<tr><td>Answer Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([SFMC_Answer_Total], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Answer_Total], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([SFMC_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([SFMC_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([SFMC_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
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
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(SFMC_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(SFMC_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(SFMC_Communication_Total, '') - ISNULL(Genesis_Communication_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(SFMC_Communication_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(SFMC_Communication_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(SFMC_Communication_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communication_Total, '') - ISNULL(SFMC_Communication_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communication_Total, '') - ISNULL(Genesis_Communication_Total, ''))) + N'</td>
									</tr>
									<tr>
									<td>Communication Active</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(SFMC_Communications_Active, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_Communications_Active, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_Communications_Active, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(SFMC_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(SFMC_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(SFMC_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(SFMC_Communications_Active, '') - ISNULL(Genesis_Communications_Active, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(SFMC_Communications_Active, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(SFMC_Communications_Active, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(SFMC_Communications_Active, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communications_Active, '') - ISNULL(SFMC_Communications_Active, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communications_Active, '') - ISNULL(Genesis_Communications_Active, ''))) + N'</td>
									</tr>
									<tr>
									<td>CommunicationClass Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(SFMC_CommunicationClass_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_CommunicationClass_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_CommunicationClass_Total, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(SFMC_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(SFMC_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(SFMC_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(SFMC_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(SFMC_CommunicationClass_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(SFMC_CommunicationClass_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(SFMC_CommunicationClass_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(SFMC_CommunicationClass_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, ''))) + N'</td>
									</tr>'
						FROM ReconciliationSummary
						WHERE ReportForDate = @ReportForDate
				) AS NVARCHAR(MAX)) + N'</table>';
	SET @tempText = REPLACE(REPLACE(@tempText, '<tdc>', '<td align=''center''>'), '</tdc>', '</td>')
	SET @emailBody = replace(@emailBody, '[VarianceReportGenesisSmExactTarget]', @tempText);

	SET @emailSubject = convert(varchar, @ReportForDate, 106) + ' | ' + (CASE WHEN @emailBody like '%HighDiscrepancyLevelAboveThreshold%' THEN 'Attention! Code red | High level of discrepancies | '
						WHEN @emailBody like '%MediumDiscrepancyLevelWithinThreshold%' THEN 'Attention! Discrepencies within threshold | '
						WHEN @emailBody like '%LessDiscrepancyLevel%' THEN 'Ignorable level | '
						ELSE 'No action | '
						END)

	SET @emailSubject = @emailSubject + 'Reconciliation variance report'

	SELECT @emailBody, 'INTERNAL', @emailSubject;
END
GO


