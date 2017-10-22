using innovea.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovea.Test
{
    interface IVersionable
    {
        [Column("id", 0)]
        int? ID { get; set; }

        [Column("version_number", 1)]
        int VersionNumber { get; set; }

        [Column("creation_date")]
        DateTime CreationDate { get; set; }

        [Column("last_update_timestamp")]
        DateTime LastUpdateTimestamp { get; set; }

        [Column("modified_by")]
        int ModifiedBy { get; set; }
    }
}
