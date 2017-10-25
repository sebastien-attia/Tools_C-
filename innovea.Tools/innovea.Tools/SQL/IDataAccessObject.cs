using System;
using System.Collections.Generic;

namespace innovea.Tools.SQL
{
    public  interface IDataAccessObject<T>
    {
        IDictionary<string, Func<object, object>> GettersByColumnName { get; }
        IDictionary<string, Func<object, object, object>> SettersByColumnName { get; }

        T Create(Context ctxt, T obj);
        IList<T> Create(Context ctxt, IList<T> objs);

        T Update(Context ctxt, T obj);
        IList<T> Update(Context ctxt, IList<T> objs);

        T Delete(Context ctxt, T obj);
        IList<T> Delete(Context ctxt, IList<T> objs);

        T findByPK(Context ctxt, object[] pk);
    }
}
