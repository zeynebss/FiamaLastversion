using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public string? CategoryIcon { get; set; }
        public bool IsDeleted { get; set; }
    }
}
