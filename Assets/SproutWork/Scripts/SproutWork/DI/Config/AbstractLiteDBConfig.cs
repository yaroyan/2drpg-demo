using System.Collections;
using System.Collections.Generic;

using Yaroyan.SproutWork.Infrastructure.DataSource;
public class AbstractLiteDBConfig : ILiteDBConfig
{
    public string ConnectionString { get; init; }

    public AbstractLiteDBConfig(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
