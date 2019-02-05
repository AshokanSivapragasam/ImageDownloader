CREATE TABLE #SmTrackingStatistics
	(ReceivedDateTime date null,	RequestType varchar(50) null,	SfmcAccount	varchar(200) null, RequestStatus varchar(50) null, NoOfRequests bigint null)

CREATE TABLE #ExhaustiveLookupSmTrackingStatistics
	(ReceivedDateTime date null,	RequestType varchar(50) null,	SfmcAccount	varchar(200) null, RequestStatus varchar(50) null, NoOfRequests bigint null)

CREATE TABLE #SmTrackingStatisticsUniqueCategories
	(RequestType varchar(50) null,	SfmcAccount	varchar(200) null, RequestStatus varchar(50) null)

declare @htmlTextBuilder nvarchar(max) = ''
declare @htmlTableHeader nvarchar(max) = ''
declare @htmlTableBody nvarchar(max) = ''
declare @howManyDays int = 14
declare @currentDatePointer date = convert(date, getutcdate())

declare @requestType varchar(100)
declare @printRequestType varchar(100)
declare @noOfRecordsForRequestType int
declare @sfmcAccount varchar(200)
declare @printSfmcAccount varchar(200)
declare @noOfRecordsForSfmcAccount int
declare @RequestStatus varchar(50)
declare @printRequestStatus varchar(50)

/*
 * Get total number of requests for each category on each day
 */
INSERT INTO #SmTrackingStatistics
SELECT
CONVERT(DATE, StartDateTime) AS ReceivedDateTime, RequestType, '<span style="font-size: 1.2em; font-weight: bold;">'+convert(varchar, AccountId) + '</span><br/>(<i>'+ rhs.TenantAccountName +'</i>)' AS SfmcAccount, CASE WHEN [Status] != 'Failure' THEN 'Passed' ELSE 'Failed' END AS RequestStatus, COUNT(1) AS NoOfRequests
FROM dbo.request rq with (nolock)
inner join Rhs rhs with (nolock) on rq.AccountId = rhs.TenantAccountId
WHERE startDateTime >= getutcdate() - @howManyDays
GROUP BY convert(date, StartDateTime), '<span style="font-size: 1.2em; font-weight: bold;">'+convert(varchar, AccountId) + '</span><br/>(<i>'+ rhs.TenantAccountName +'</i>)', RequestType, [Status]
ORDER BY convert(date, StartDateTime) desc

/*
 * Prepare exhastive list of Msi Tenants
 */
INSERT INTO #ExhaustiveLookupSmTrackingStatistics
SELECT
DISTINCT CONVERT(DATE, GETUTCDATE()) AS ReceivedDateTime, RequestType, '<span style="font-size: 1.2em; font-weight: bold;">'+convert(varchar, AccountId) + '</span><br/>(<i>'+ rhs.TenantAccountName +'</i>)' AS SfmcAccount, 'Passed' AS RequestStatus, 0 AS NoOfRequests
FROM dbo.request rq with (nolock)
inner join Rhs rhs with (nolock) on rq.AccountId = rhs.TenantAccountId
WHERE startDateTime >= getutcdate() - (@howManyDays * 4)
GROUP BY convert(date, StartDateTime), '<span style="font-size: 1.2em; font-weight: bold;">'+convert(varchar, AccountId) + '</span><br/>(<i>'+ rhs.TenantAccountName +'</i>)', RequestType, [Status]
UNION ALL
SELECT
DISTINCT CONVERT(DATE, GETUTCDATE()) AS ReceivedDateTime, RequestType, '<span style="font-size: 1.2em; font-weight: bold;">'+convert(varchar, AccountId) + '</span><br/>(<i>'+ rhs.TenantAccountName +'</i>)' AS SfmcAccount, 'Failed' AS RequestStatus, 0 AS NoOfRequests
FROM dbo.request rq with (nolock)
inner join Rhs rhs with (nolock) on rq.AccountId = rhs.TenantAccountId
WHERE startDateTime >= getutcdate() - (@howManyDays * 4)
GROUP BY convert(date, StartDateTime), '<span style="font-size: 1.2em; font-weight: bold;">'+convert(varchar, AccountId) + '</span><br/>(<i>'+ rhs.TenantAccountName +'</i>)', RequestType, [Status]

/*
 * Merge the category of exhaustive lookup to real statistics
 */
insert into #SmTrackingStatistics 
select m2.* from #SmTrackingStatistics m1
full outer join #ExhaustiveLookupSmTrackingStatistics m2 on m1.RequestType = m2.RequestType and m1.RequestStatus = m2.RequestStatus and m1.SfmcAccount = m2.SfmcAccount
where m1.RequestType is null

/*
 * Get unique list of categories
 */
INSERT INTO #SmTrackingStatisticsUniqueCategories
SELECT RequestType, SfmcAccount, RequestStatus
FROM #SmTrackingStatistics
GROUP BY RequestType, SfmcAccount, RequestStatus


SET @htmlTextBuilder = (select requesttype + '</td><tdc>' + [status] + '</td><tdc>' + convert(varchar, count(1)) as tdc from request with (nolock)
where CreatedBy != 'v-assiva@microsoft.com'
and startdatetime > getutcdate() - 180
group by requesttype, [status]
for xml path('tr'), root('tbody'))

SET @htmlTextBuilder =  '<p style=''font-weight: bold''>J F Y I. Pilot view of Sm Tracking Statistics(<span style=''font-weight: normal''><i>~6 months coverage</i></span>)</p>
<table style=''font-family: Segoe UI; margin-left: 50px;''><thead><tr class="header"><td align="center" colspan="3">For Last 6 months</td></tr><tr class="header"><td align="center" rowspan="1">RequestType</td><td align="center" rowspan="1">RequestStatus</td><td align="center" rowspan="1">NoOfRequests</td></thead>' 
	
	+ replace(replace(replace(@htmlTextBuilder, '&lt;', '<'), '&gt;', '>'), 'tdc', 'td align="center"')
	+ '</table>'

/*
 * Generate html report for Msi tracking
 */
SET @htmlTextBuilder = @htmlTextBuilder + '<p style=''font-weight: bold''>J F Y I. Pilot view of Sm Tracking Statistics(<span style=''font-weight: normal''><i>~15 days coverage</i></span>)</p>
<table style=''font-family: Segoe UI; margin-left: 50px;''><thead>{thead}</thead><tbody>{tbody}</tbody></table>'
SET @htmlTableHeader = '<tr class="header"><tdc rowspan="2">RequestType</td><tdc rowspan="2">SfmcAccount</td><tdc rowspan="2">RequestStatus</td>'

while(@currentDatePointer > convert(date, getutcdate() - @howManyDays))
begin
	set @htmlTableHeader = @htmlTableHeader + '<tdc>' + convert(varchar(10), @currentDatePointer) + '</td>'
	set @currentDatePointer = dateadd(day, -1, @currentDatePointer)
end

set @htmlTableHeader = @htmlTableHeader + '</tr><tr class="header">'
set @currentDatePointer = convert(date, getutcdate())

while(@currentDatePointer > convert(date, getutcdate() - @howManyDays))
begin
	set @htmlTableHeader = @htmlTableHeader + '<tdc>' + datename(dw, @currentDatePointer) + '</td>'
	set @currentDatePointer = dateadd(day, -1, @currentDatePointer)
end

set @htmlTableHeader = @htmlTableHeader + '</tr>'

SET @htmlTextBuilder = REPLACE(@htmlTextBuilder, '{thead}', @htmlTableHeader)

print @htmlTextBuilder

declare cursor_RequestType cursor for
select RequestType from #SmTrackingStatistics
group by RequestType
order by RequestType

OPEN cursor_RequestType
FETCH NEXT FROM cursor_RequestType
INTO @requestType
WHILE @@FETCH_STATUS = 0
begin
	set @printRequestType = @requestType

	declare cursor_SfmcAccount cursor for
	select SfmcAccount from #SmTrackingStatistics
	where RequestType = @requestType
	group by SfmcAccount
	order by SfmcAccount

	OPEN cursor_SfmcAccount
	FETCH NEXT FROM cursor_SfmcAccount
	INTO @sfmcAccount
	WHILE @@FETCH_STATUS = 0
	begin
		set @printSfmcAccount = @sfmcAccount

		declare cursor_RequestStatus cursor for
		select RequestStatus from #SmTrackingStatistics
		where RequestType = @requestType and SfmcAccount = @sfmcAccount
		group by RequestStatus
		order by RequestStatus

		OPEN cursor_RequestStatus
		FETCH NEXT FROM cursor_RequestStatus
		INTO @RequestStatus
		WHILE @@FETCH_STATUS = 0
		begin
			set @printRequestStatus = @RequestStatus

			declare @datePointer date = convert(date, getutcdate())
			declare @acmeString varchar(max) = ''

			while(@datePointer > convert(date, getutcdate() - @howManyDays))
			begin
				select @acmeString = @acmeString + '<tdc ' + (case when isnull(sum(NoOfRequests), 0) = 0 then 'class="NoRowsBlueCase"' when @printRequestStatus = 'Passed' then 'class="PassedGreenCase"' else 'class="FailedRedCase"' end) + '>' + convert(varchar(20), isnull(sum(NoOfRequests), 0)) + '</td>' from #SmTrackingStatistics
				where receiveddatetime = @datePointer
				and RequestType	= @requestType
				and SfmcAccount	= @sfmcAccount
				and RequestStatus = @requestStatus

				set @datePointer = dateadd(day, -1, @datePointer)
			end

			select @noOfRecordsForRequestType = count(1) from #SmTrackingStatisticsUniqueCategories where RequestType	= @requestType
			select @noOfRecordsForSfmcAccount = count(1) from #SmTrackingStatisticsUniqueCategories where RequestType	= @requestType and SfmcAccount	= @sfmcAccount

			SET @htmlTableBody = @htmlTableBody + '<tr>' + (case when @printRequestType != '' then '<tdc rowspan="'+ convert(varchar(10), @noOfRecordsForRequestType) +'">' + @printRequestType + '</td>' else '' end) 
			+ (case when @printSfmcAccount != '' then '<tdc rowspan="'+ convert(varchar(10), @noOfRecordsForSfmcAccount) +'">' + @printSfmcAccount + '</td>' else '' end) 
			+ '<tdc ' + (case when @printRequestStatus = 'Passed' then 'class="StatusGreenCase"' else 'class="StatusRedCase"' end) + '>' + @printRequestStatus + '</td>'
			+ @acmeString + '</tr>'

			SET @printRequestType = ''
			SET @printSfmcAccount = ''

			FETCH NEXT FROM cursor_RequestStatus
			INTO @RequestStatus
		end

		CLOSE cursor_RequestStatus
		DEALLOCATE cursor_RequestStatus

		FETCH NEXT FROM cursor_SfmcAccount
		INTO @sfmcAccount
	end

	CLOSE cursor_SfmcAccount
	DEALLOCATE cursor_SfmcAccount

	FETCH NEXT FROM cursor_RequestType
	INTO @requestType
end

CLOSE cursor_RequestType
DEALLOCATE cursor_RequestType

SET @htmlTextBuilder = REPLACE(@htmlTextBuilder, '{tbody}', @htmlTableBody)
SET @htmlTextBuilder = REPLACE(@htmlTextBuilder, '<tdc>0</td>', '<tdc style="background-color: #ffbcc8; color: red;">0</td>')
SET @htmlTextBuilder = REPLACE(@htmlTextBuilder, '<tdc', '<td align="center"')

DROP TABLE #SmTrackingStatistics
DROP TABLE #ExhaustiveLookupSmTrackingStatistics
DROP TABLE #SmTrackingStatisticsUniqueCategories

select len(@htmlTextBuilder)

SELECT convert(nvarchar(max),@htmlTextBuilder) as [EmailMessage]
				, 'Sm Tracking' as [EmailSubject]
				, CASE WHEN @htmlTextBuilder NOT LIKE '%FailedRedCase%' THEN 1 ELSE 0 END as [CodeGreen];
