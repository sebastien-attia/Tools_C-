using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static innovea.Tools.DAOHelper;
using System.Data.SqlClient;

namespace innovea.Test
{
    [TestClass]
    public class DAOHelper_UnitTest
    {
        [TestMethod]
        public void TestMethod_GetColumnNames()
        {
            Invoice invoice = new Invoice();
            invoice.CustomerName = "Hello !";

            SQLInfo sqlInfo = new SQLInfo(typeof(Invoice));

            string insertSQL = FormatSQL(SQLOperation.INSERT, sqlInfo);
            string updateSQL = FormatSQL(SQLOperation.UPDATE, sqlInfo);
            string deleteSQL = FormatSQL(SQLOperation.DELETE, sqlInfo);
        }

        /**
         * The pttern is the following:
         * - create a Connection,
         * - create the SQLCommand,
         * - open the Transaction by Connection.BeginTransaction,
         * - enlist the Transaction in the SQLCommand: Command.Transaction = Connection.BeginTransaction,
         * - do the job (Command.ExecuteNonQuery())
         * - if everything is OK, Transaction.Commit() Else Transaction.Rollback()
         */
        [TestMethod]
        public void TestMethod_ExecuteBatch()
        {
            using(SqlConnection conn = new SqlConnection(""))
            {
                SqlTransaction tx = conn.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand("INSERT", conn, tx);
                sqlCommand.CommandType = System.Data.CommandType.Text;

                conn.Open();    
            }     
        }
    }
}
