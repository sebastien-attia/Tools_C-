using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static innovea.Tools.DAOHelper;

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
    }
}
