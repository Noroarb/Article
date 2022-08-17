using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class PropertiesDescriptions : IEntityBase
    {
        public int Id { set; get; }
        public int PropertyId { set; get; }
        public int LanguageId { set; get; }
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// Property unit
        /// </summary>
        public string Unit { get; set; }
        public virtual Language Language { set; get; }
        public virtual Properties Property { set; get; }

    }
}
