Pull reports from genesis database,
1. Create stored-procedure, 'GetSummaryOfCommunications (@startDate, @endDate)'
2. Deploy in genesis database

Push reports from genesis database to subscription management,
1. Create windows service
2. Call stored procedure from genesis database; get the report xml document
3. Parse the appropriate values and import them to sm database

To import the data to sm database
1. Driver file with xpaths and database column
2. Method to parse and import them to appropriate 


<Driver>
	<Xpath>
</Driver>
