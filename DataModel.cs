using System.Collections.Generic;
using System.Data;

namespace TextToSql2.Models
{
    public class DataModel
    {
        public string UserQuery { get; set; }
        public string SqlQuery { get; set; }
        public DataTable Results { get; set; }
        public DataModel() 
        { 
            UserQuery = string.Empty;
            SqlQuery = string.Empty;
            Results = new DataTable();
        }
    }
}
