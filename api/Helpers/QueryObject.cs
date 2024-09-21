using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject   //to delay sql query(to list) - defer for filteriing/sorting/limit etc 
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set;} = null;
        
        public string? SortBy { get; set; } = null; // for sorting

        public bool IsDecsending { get; set; } = false; // for sorting 

        public int PageNumber { get; set;} = 1; 
        public int PageSize { get; set;} = 20; 
    }
}