using innovea.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace innovea.Tools
{
    /**
     * 
     */
    public class DAOHelper
    {
        public enum SQLCommand
        {
            INSERT,
            UPDATE,
            DELETE,
        }

        public class SQLInfo
        {
            public Type ClassType { get; }
            public string TableName { get; }
            public ISet<string> ColumnNames { get; }
            public IList<string> PKColumnNames { get; }
            public IDictionary<string, Func<object, object>> GettersByColumnName { get; }
            public IDictionary<string, Func<object, object, object>> SettersByColumnName { get; }

            public SQLInfo(Type classType)
            {
                ClassType = classType;
                TableName = GetTableName(classType);

                IDictionary<string, object[]> mappers = GetMappers(classType);

                ColumnNames = new HashSet<string>(mappers.Keys);
                PKColumnNames = mappers.Keys.Where(k => mappers[k][2] != null).OrderBy(k => mappers[k][2]).Select(k => k).ToList<string>();
                GettersByColumnName = ColumnNames.ToDictionary(k => k, k => (Func<object, object>) mappers[k][0]);
                SettersByColumnName = ColumnNames.ToDictionary(k => k, k => (Func<object, object, object>) mappers[k][1]);
            }
        }

        private DAOHelper() {}

        public static string FormatSQL(SQLCommand sqlCommand, SQLInfo sqlInfo)
        {
            StringBuilder builder = new StringBuilder();
            switch(sqlCommand)
            {
                case SQLCommand.INSERT:
                    builder.Append(sqlCommand.ToString()).Append(" INTO ").Append(sqlInfo.TableName);
                    builder.Append(" (").Append(String.Join(",", sqlInfo.ColumnNames)).Append(") ");
                    builder.Append(" VALUES ( @").Append(String.Join(", @", sqlInfo.ColumnNames)).Append(") ");
                    break;
                case SQLCommand.UPDATE:
                    if (sqlInfo.PKColumnNames.Count == 0)
                        throw new Exception("No PK defined - Cannot format the SQL/Update");

                    builder.Append(sqlCommand.ToString()).Append(" ").Append(sqlInfo.TableName).Append(" SET ");
                    builder.Append(String.Join(", ", sqlInfo.ColumnNames.Where(t => !sqlInfo.PKColumnNames.Contains(t)).ToList<string>().Select(t => t + " = @" + t)) );
                    builder.Append(sqlInfo.TableName).Append(" WHERE (");
                    builder.Append(String.Join(" AND ", sqlInfo.PKColumnNames.ToList<string>().Select(t => t + " = @" + t)));
                    builder.Append(")");
                    break;
                case SQLCommand.DELETE:
                    if (sqlInfo.PKColumnNames.Count == 0)
                        throw new Exception("No PK defined - Cannot format the SQL/Delete");

                    builder.Append(sqlCommand.ToString()).Append(" FROM ").Append(sqlInfo.TableName);
                    builder.Append(" WHERE (");
                    builder.Append(String.Join(" AND ", sqlInfo.PKColumnNames.ToList<string>().Select(t => t + " = @" + t)));
                    builder.Append(")");
                    break;
                default:
                    throw new Exception("Does not know how to format this SQL command");
            }
            return builder.ToString();
        }

        public static string GetTableName(Type classType)
        {
            Table tableAttr = (Table) classType.GetCustomAttribute(typeof(Table));
            if (tableAttr == null)
                return null;

            return tableAttr.Name;
        }

        public static IDictionary<string, object[]> GetMappers(Type classType)
        {
            IDictionary<string, object[]> columnNames = new Dictionary<string, object[]>();
            classType.GetInterfaces().ToList().ForEach(i => GetMappers(columnNames, i));
            GetMappers(columnNames, classType);
            return columnNames;
        }

        private static void GetMappers(IDictionary<string, object[]> columnNames, Type classType)
        {
            foreach (var property in classType.GetProperties())
            {
                Column columnAttr = (Column)property.GetCustomAttribute(typeof(Column));
                if (columnAttr == null)
                    continue;

                string columnName = columnAttr.Name;
                int? pkOrder = columnAttr.PK;

                if (columnName == null)
                    columnName = property.Name;

                Func<object, object> getter;
                {
                    MethodInfo refGetter = property.GetMethod;
                    getter = o => refGetter.Invoke(o, null);
                }

                Func<object, object, object> setter;
                {
                    MethodInfo refSetter = property.SetMethod;
                    setter = (o, v) => refSetter.Invoke(o, new object[] { v });
                }

                columnNames.Add(columnName, new object[] { getter, setter, pkOrder });
            }
        }
    }
}
