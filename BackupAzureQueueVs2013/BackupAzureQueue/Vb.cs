//  Microsoft SQL Server Integration Services Script Task
//  Write scripts using Microsoft Visual Basic
//  The ScriptMain class is the entry point of the Script Task.
using System;
using System.Data;
//using System.Math;
using Microsoft.SqlServer.Dts.Runtime;
using System.Collections.Specialized;
using dtrw = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
//<Microsoft(System.CLSCompliantAttribute(false) > Partial);
public class ScriptMain : Microsoft.SqlServer.Dts.Tasks.ScriptTask.VSTARTScriptObjectModelBase
{

    //  The execution engine calls this method when the task executes.
    //  To access the object model, use the Dts object. Connections, variables, events,
    //  and logging features are available as static members of the Dts class.
    //  Before returning from this method, set the value of Dts.TaskResult to indicate success or failure.
    //  
    //  To open Code and Text Editor Help, press F1.
    //  To open Object Browser, press Ctrl+Alt+J.
    public static void Main1(string[] args)
    {
        ScriptMain scriptMain = new ScriptMain();
        
        foreach (PipelineComponentInfo p in new Application().PipelineComponentInfos)
        {
            Console.WriteLine("{0};{1};{2}", p.CreationName, p.Description, p.Name);
        } 
    }
}