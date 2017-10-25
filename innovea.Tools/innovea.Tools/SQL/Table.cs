using System;

namespace innovea.Tools.SQL
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class Table : Attribute
    {
        private string name;

        public Table(string tableName)
        {
            this.name = tableName;
        }

        public virtual string Name
        {
            get { return name; }
        }
    }
}
