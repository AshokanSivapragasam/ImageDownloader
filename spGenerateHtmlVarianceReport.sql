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
Following is the variance report for Reconciliation process across data systems,<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Genesis<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Geneva<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Exact Target<br/>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Subscription Management<br/>
<br/>

<b><u>COLOR CODES:</u></b><br/>
<table>
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

<b><u>THRESHOLD LEVELS:</u></b><br/>
<table>
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
'<b><u>STATUS OF TASKS:</u></b><br/>
<table>
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
(CASE WHEN (ISNULL([ET_Communication_Total],0)+
ISNULL([ET_Communications_Active],0)+
ISNULL([ET_CommunicationClass_Total],0)+
ISNULL([ET_Subscription_Subscribed],0)+
ISNULL([ET_Subscription_DistinctSubscribed],0)+
ISNULL([ET_Subscription_DistinctSubscribedAndContactable],0)+
ISNULL([ET_Standalone_Subscribed],0)+
ISNULL([ET_Standalone_DistinctSubscribed],0)+
ISNULL([ET_Standalone_DistinctSubscribedAndContactable],0)+
ISNULL([Et_Answer_Total],0)) = 0 
THEN '<td class=''HighDiscrepancyLevelAboveThreshold''>ET_DataPull</td>
<td class=''HighDiscrepancyLevelAboveThreshold''>NotDoneYet</td>'
ELSE '<td>ET_DataPull</td>
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
'<b><u>VARIANCE REPORT (EXACT TARGET VS GENEVA):</u></b>
<br/>
[VarianceReportEtGeneva]
<br/>
<b><u>VARIANCE REPORT (EXACT TARGET VS GENESIS VS SUBSCRIPTION MANAGEMENT):</u></b>
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
						<th>Attribute name</th>
						<th>Exact Target</th>
						<th>Geneva</th>
						<th>Difference (Exact Target - Geneva)</th>
						</tr>' + cast((
				SELECT N'<tr>
									<td>Subscription Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Subscription_Subscribed], 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_Subscribed], 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0)) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Subscription_Subscribed], 0) - ISNULL([Geneva_Subscription_Subscribed], 0))) 
									+ '</td></tr>' 
								+ '<tr><td>Subscription Distinct Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Subscription_DistinctSubscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_DistinctSubscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Subscription_DistinctSubscribed], '') - ISNULL([Geneva_Subscription_DistinctSubscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Subscription Distinct Subscribed And Contactable</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Subscription_DistinctSubscribedAndContactable], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Subscription_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Subscription_DistinctSubscribedAndContactable], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Standalone_Subscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_Subscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Standalone_Subscribed], '') - ISNULL([Geneva_Standalone_Subscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Distinct Subscribed</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Standalone_DistinctSubscribed], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_DistinctSubscribed], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Standalone_DistinctSubscribed], '') - ISNULL([Geneva_Standalone_DistinctSubscribed], ''))) + '</td></tr>' 
								+ '<tr><td>Standalone Distinct Subscribed And Contactable</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Standalone_DistinctSubscribedAndContactable], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Standalone_DistinctSubscribedAndContactable], '') - ISNULL([Geneva_Standalone_DistinctSubscribedAndContactable], ''))) + '</td></tr>' 
								+ '<tr><td>Answer Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Et_Answer_Total], '')) + '</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL([Geneva_Answer_Total], '')) + '</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL([Et_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL([Et_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL([Et_Answer_Total], '') - ISNULL([Geneva_Answer_Total], '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL([Et_Answer_Total], '') - ISNULL([Geneva_Answer_Total], ''))) + '</td></tr>'
						FROM ReconciliationSummary
						WHERE ReportForDate = @ReportForDate
				) AS NVARCHAR(MAX)) + N'</table>';
	SET @tempText = REPLACE(REPLACE(@tempText, '<tdc>', '<td align=''center''>'), '</tdc>', '</td>')
	SET @emailBody = replace(@emailBody, '[VarianceReportEtGeneva]', @tempText);

	SET @tempText = N'<table>
						<tr class=''header''>
						<th>Attribute name</th>
						<th>Exact Target</th>
						<th>Genesis</th>
						<th>Subscription Management</th>
						<th>Difference #1 (ET - Genesis)</th>
						<th>Difference #1 (SM - ET)</th>
						<th>Difference #1 (SM - Genesis)</th>
						</tr>' + cast((
				SELECT N'<tr>
									<td>Communication Total</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Et_Communication_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_Communication_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_Communication_Total, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Et_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Et_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Et_Communication_Total, '') - ISNULL(Genesis_Communication_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Et_Communication_Total, '') - ISNULL(Genesis_Communication_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Et_Communication_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Et_Communication_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_Communication_Total, '') - ISNULL(Et_Communication_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communication_Total, '') - ISNULL(Et_Communication_Total, ''))) + N'</td>
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
									<tdc>' + convert(NVARCHAR(max), ISNULL(Et_Communications_Active, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_Communications_Active, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_Communications_Active, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Et_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Et_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Et_Communications_Active, '') - ISNULL(Genesis_Communications_Active, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Et_Communications_Active, '') - ISNULL(Genesis_Communications_Active, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Et_Communications_Active, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Et_Communications_Active, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_Communications_Active, '') - ISNULL(Et_Communications_Active, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_Communications_Active, '') - ISNULL(Et_Communications_Active, ''))) + N'</td>
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
									<tdc>' + convert(NVARCHAR(max), ISNULL(Et_CommunicationClass_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Genesis_CommunicationClass_Total, 0)) + N'</td>
									<tdc>' + convert(NVARCHAR(max), ISNULL(Sm_CommunicationClass_Total, 0)) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Et_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Et_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Et_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Et_CommunicationClass_Total, '') - ISNULL(Genesis_CommunicationClass_Total, ''))) + N'</td>
									<td align=''center'' class='''
									+ (CASE WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Et_CommunicationClass_Total, '')) = 0
										THEN 'NoDiscrepancy'
										WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Et_CommunicationClass_Total, '')) <= @IgnorableThresholdLevel
										THEN 'LessDiscrepancyLevel'
										WHEN ABS(ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Et_CommunicationClass_Total, '')) <= @ThresholdLevel
										THEN 'MediumDiscrepancyLevelWithinThreshold'
										ELSE 'HighDiscrepancyLevelAboveThreshold' END)
									+ '''>' + convert(NVARCHAR(max), (ISNULL(Sm_CommunicationClass_Total, '') - ISNULL(Et_CommunicationClass_Total, ''))) + N'</td>
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

	SET @emailSubject = convert(varchar, @ReportForDate, 112) + ' | ' + (CASE WHEN @emailBody like '%HighDiscrepancyLevelAboveThreshold%' THEN 'Attention! Code red | High level of discrepancies | '
						WHEN @emailBody like '%MediumDiscrepancyLevelWithinThreshold%' THEN 'Attention! Discrepencies within threshold | '
						WHEN @emailBody like '%LessDiscrepancyLevel%' THEN 'Ignorable level | '
						ELSE 'No action | '
						END)

	SET @emailSubject = @emailSubject + 'Reconciliation variance report'

	SELECT @emailBody, 'INTERNAL', @emailSubject;
END
GO


