using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class TownDescription : IEntityBase
    {

        public int Id { get; set; }
        public int LanguageId { set; get; }
        public int TownId { set; get; }
        public string TownName { set; get; }

        public virtual Town Town { set; get; }
        public virtual Language Language { set; get; }


    }
}
