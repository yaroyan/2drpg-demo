using System.Collections;
using System.Collections.Generic;

namespace Com.Github.Yaroyan.Rpg.Sql
{
    public class SqlCoindition
    {
        public SqlCoindition() { }
        public SqlCoindition(string column, SqlOperator sqlOperator, object value, SqlConnection sqlConnection, SqlOrder sqlOrder, IEnumerable<SqlCoindition> sqlCoinditions)
        {
            this.Column = column;
            this.Operator = sqlOperator;
            this.Value = value;
            this.Connection = sqlConnection;
            this.Order = sqlOrder;
            this.Coinditions = Coinditions;
        }
        public string Column { get; set; }
        public SqlOperator Operator { get; set; }
        public object Value { get; set; }
        public SqlConnection Connection { get; set; }
        public SqlOrder Order { get; set; }
        public IEnumerable<SqlCoindition> Coinditions { get; set; }

        public string ParseSqlCondition()
        {
            return null;
        }
    }
}