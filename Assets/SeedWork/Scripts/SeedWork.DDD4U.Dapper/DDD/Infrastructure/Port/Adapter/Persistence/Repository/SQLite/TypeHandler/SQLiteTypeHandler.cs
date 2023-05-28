using Dapper;
using System.Data;
using System;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.SQLiteRepository.TypeHandler
{
    /// <summary>
    /// See link below.
    /// https://docs.microsoft.com/ja-jp/dotnet/standard/data/sqlite/dapper-limitations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SQLiteTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        // Parameters are converted by Microsoft.Data.Sqlite
        public override void SetValue(IDbDataParameter parameter, T value)
            => parameter.Value = value;
    }
    public class DateTimeOffsetHandler : SQLiteTypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
            => DateTimeOffset.Parse((string)value);
    }

    public class GuidHandler : SQLiteTypeHandler<Guid>
    {
        public override Guid Parse(object value)
            => Guid.Parse((string)value);
    }

    public class TimeSpanHandler : SQLiteTypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
            => TimeSpan.Parse((string)value);
    }
}
