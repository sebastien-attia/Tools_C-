using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static innovea.Tools.SQL.DAOHelper;

namespace innovea.Tools.SQL
{
    public class DefaultDataAccessObject<T> : IDataAccessObject<T>
    {
        private SQLInfo sqlInfo;

        public DefaultDataAccessObject() {}

        public IList<T> Create(Context ctxt, IList<T> objs)
        {
            return HandleSQLOperation(ctxt, SQLOperation.INSERT, objs);
        }

        public T Create(Context ctxt, T obj)
        {
            return Create(ctxt, new List<T>() { obj }).First();
        }

        public IList<T> Update(Context ctxt, IList<T> objs)
        {
            return HandleSQLOperation(ctxt, SQLOperation.UPDATE, objs);
        }

        public T Update(Context ctxt, T obj)
        {
            return Update(ctxt, new List<T>() { obj }).First();
        }

        public IList<T> Delete(Context ctxt, IList<T> objs)
        {
            return HandleSQLOperation(ctxt, SQLOperation.DELETE, objs);
        }

        public T Delete(Context ctxt, T obj)
        {
            return Delete(ctxt, new List<T>() { obj }).First();
        }

        public T findByPK(Context ctxt, object[] pk)
        {
            return FindByPK<T>(ctxt.Transaction, sqlInfo, pk);
        }

        public IList<T> HandleSQLOperation(Context ctxt, SQLOperation sqlOperation, IList<T> objs)
        {
            SqlTransaction Tx = ctxt.Transaction;
            string sql = FormatSQL(sqlOperation, sqlInfo);
            using (SqlCommand sqlCommand = new SqlCommand(sql, Tx.Connection, Tx))
            {
                ExecuteBatch<T>(sqlCommand, sqlInfo, objs);
            }

            return objs;
        }
    }
}
