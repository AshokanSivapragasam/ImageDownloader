using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.IO;

public class SqlClrFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction()]
    public static SqlString getSize(SqlString filePath)
    {
        FileInfo fio = new FileInfo(Convert.ToString(filePath));
        return new SqlString(Convert.ToString(fio.Length));
    }

    [Microsoft.SqlServer.Server.SqlFunction()]
    public static SqlDateTime getLastModifiedTime(SqlString filePath)
    {
        FileInfo fio = new FileInfo(Convert.ToString(filePath));
        return new SqlDateTime(fio.LastWriteTime);
    }
}