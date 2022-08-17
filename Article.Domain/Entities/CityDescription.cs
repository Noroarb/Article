using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class CityDescription : IEntityBase
    {

        public int Id { get; set; }
        public int LanguageId { set; get; }
        public int CityId { set; get; }
        public string CityName { set; get; }

        public virtual City City { set; get; }
        public virtual Language Language { set; get; }


    }
}