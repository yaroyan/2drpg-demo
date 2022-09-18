using Dapper;
using System.Data;
using System;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Dapper.TypeHandler
{
    /// <summary>
    /// See link below.
    /// https://docs.microsoft.com/ja-jp/dotnet/standard/data/sqlite/dapper-limitations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        // Parameters are converted by Microsoft.Data.Sqlite
        public override void SetValue(IDbDataParameter parameter, T value)
            => parameter.Value = value;
    }
    class DateTimeOffsetHandler : SqliteTypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
            => DateTimeOffset.Parse((string)value);
    }

    class GuidHandler : SqliteTypeHandler<Guid>
    {
        public override Guid Parse(object value)
            => Guid.Parse((string)value);
    }

    class TimeSpanHandler : SqliteTypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
            => TimeSpan.Parse((string)value);
    }
}