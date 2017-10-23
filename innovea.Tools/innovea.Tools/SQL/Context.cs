using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovea.Tools.SQL
{
    public class Context
    {
        public SqlTransaction Transaction { get; set; }
    }
}
