using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Specification
{
    public class ProductsSpecParams
    {
        private const int MaxPageSize = 10;
        public int PageIndex { get; set; } = 1;
        private int Pagesize = 5;

        public int PageSize
        {
            get { return Pagesize; }
            set { Pagesize = value > 10 ? MaxPageSize : value; }
        }

        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string sort { get; set; }
        public string search { get; set; }

    }
}
