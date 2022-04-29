using System.Collections;
using System.Collections.Generic;

namespace Com.Github.Yaroyan.Rpg.Sql
{
    public class SqlOrder
    {
        public SqlOrder(string column, SqlOrderOperator sqlOrderOperator)
        {
            this.Column = column;
            this.Operator = sqlOrderOperator;
        }
        public SqlOrderOperator Operator { get; private set; }
        public string Column { get; private set; }

        public override string ToString()
        {
            return "ORDER BY " + Column + " " + Operator;
        }
    }
}