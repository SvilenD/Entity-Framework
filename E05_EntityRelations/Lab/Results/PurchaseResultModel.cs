using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Results
{
    public class PurchaseResultModel
    {
        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public CarResultModel Car { get; set; }

        public CustomerResultModel Customer { get; set; }
    }
}
