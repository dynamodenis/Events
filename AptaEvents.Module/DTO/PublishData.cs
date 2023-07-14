using AptaEvents.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.DTO
{
    public class PublishData
    {
        public IList<TabDto> Tabs { get; set; }
    }

    public class TabDto
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public List<FieldDto> Fields { get; set; }

    }

    public class FieldDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTime? Expiry { get; set; }
        public int SortOrder { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }

    
}
