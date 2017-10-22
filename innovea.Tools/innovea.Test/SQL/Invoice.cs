using innovea.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovea.Test
{
    [Table("INVOICE")]
    class Invoice : IVersionable
    {
        public int? ID { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateTimestamp { get; set; }
        public int ModifiedBy { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("invoice_date")]
        public DateTime Date { get; set; }

        public IList<string> items { get; set; }

        public Invoice() {}
    }
}
