/// <summary>
/// 
/// </summary>
public void IfThenSemantics()
{
	#region IF_CLAUSE
	DataTable dataTable = new DataTable();
	using (SqlConnection sqlConnection = new SqlConnection("Server=tcp:w4k2434z3i.database.windows.net;Database=InterchangeDB1;User ID=bluradar@w4k2434z3i;Password=1qaz!QAZ;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False"))
	{
		sqlConnection.Open();
		using (SqlCommand sqlCommand = new SqlCommand(@"SELECT 'localhost' [ServerIf],'localhost' [ServerThen],'MSIFileProcessorHost' [Service1],'MSIFileProcessorHost' [Service2],'MSIFileProcessorHost' [Service3]", sqlConnection))
		{
			sqlCommand.CommandType = CommandType.Text;
			SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
			da.Fill(dataTable);
		}
	}
	#endregion

	#region THEN_CLAUSE
	string thenClause = @"SC ""\\[ServerThen]"" STOP ""[Service1]"" & SC ""\\[ServerThen]"" STOP ""[Service2]"" & SC ""\\[ServerThen]"" STOP ""[Service3]""  & TIMEOUT /t ""10"" & SC ""\\[ServerThen]"" START ""[Service1]"" & SC ""\\[ServerThen]"" START ""[Service2]"" & SC ""\\[ServerThen]"" START ""[Service3]""";
	foreach (DataRow dataRow in dataTable.Rows)
	{
		var regex = new Regex(@"\[(\w+)\]", RegexOptions.Compiled);
		var matches = regex.Matches(thenClause);
		foreach (System.Text.RegularExpressions.Match match in matches)
			thenClause = thenClause.Replace(match.Groups[0].Value, dataRow[match.Groups[1].Value].ToString());

		#region EXECUTOR
		var processInfo = new ProcessStartInfo("cmd")
		{
			ErrorDialog = false,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			Arguments = "/c " + thenClause
		};

		var proc = Process.Start(processInfo);
		// You can pass any delegate that matches the appropriate 
		// signature to ErrorDataReceived and OutputDataReceived
		proc.ErrorDataReceived += (sender, errorLine) => { if (errorLine.Data != null) Console.WriteLine(errorLine.Data); };
		proc.OutputDataReceived += (sender, outputLine) => { if (outputLine.Data != null) Console.WriteLine(outputLine.Data); };
		proc.BeginErrorReadLine();
		proc.BeginOutputReadLine();
		proc.WaitForExit();
		#endregion
	}
	#endregion
}