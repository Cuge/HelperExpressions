using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace T4HelperMethods.ModelClasses
{
    public class DomainClassInfo
    {
        public DomainClassInfo()
        {

        }
        public IEnumerable<PropertyInfo> NoneCompareProperties { get; set; }
        public IEnumerable<PropertyInfo> Properties { get; set; }
        public string PartialClassTemplate { get; set; }
        public string GeneratedPartialClass()
        {
            Antlr4.StringTemplate.TemplateGroupString template = new Antlr4.StringTemplate.TemplateGroupString(PartialClassTemplate);
            StringBuilder sb = new StringBuilder(PartialClassTemplate);
            return sb.ToString();
        }
    }
}
