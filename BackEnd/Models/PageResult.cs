using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class PageResult<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
        public double TotalSalaries { get; set; }
        public double FemaleSalaries { get; set; }
        public double MaleSalaries { get; set; }
    }
}
