using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovea.Tools
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class Column : Attribute
    {
        private string name;
        private int? pk;

        public Column(string name)
        {
            this.name = name;
            this.pk = null;
        }

        public Column(string name, int pkOrder)
        {
            this.name = name;
            this.pk = pkOrder;
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual int? PK
        {
            get { return pk; }
            set { pk = value; }
        }

    }
}
