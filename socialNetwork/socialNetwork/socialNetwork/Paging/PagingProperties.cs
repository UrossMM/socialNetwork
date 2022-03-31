using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Paging
{
    public class PagingProperties
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public bool SortOrder { get; set; }
        public string SortCriterium { get; set; }
    }
}
