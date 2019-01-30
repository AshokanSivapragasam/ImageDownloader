using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VsoReleaseTool.Models
{
    public class ReleaseDefinitionsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public string selfViewLink { get; set; }
        public string webViewLink { get; set; }
        public int noOfGlobalVariables { get; set; }
        public Dictionary<string, int> noOfEnvVariablesInDifferentEnvironments { get; set; }
    }
}