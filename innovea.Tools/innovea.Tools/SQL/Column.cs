using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovea.Tools.SQL
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class Column : Attribute
    {
        private string name;
        private int? pk;

        public Column(string name, int? pkOrder)
        {
            this.name = name;
            this.pk = pkOrder;
        }

        public Column() : this(null, null) { }
        public Column(string name) : this(name, null) { }
        public Column(int pkOrder) : this(null, pkOrder) { }

        public string Name
        {
            get { return name; }
        }

        public int? PK
        {
            get { return pk; }
            set { pk = value; }
        }

    }
}
