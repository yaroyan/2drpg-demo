using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository;
using Yaroyan.SproutWork.Infrastructure.DataSource;
using Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository;

public interface IQueryDBUOWProvider : IDisposable
{
    IUnitOfWork Provide();
}

public abstract class AbstractQueryDBUOWProvider : IQueryDBUOWProvider
{
    bool _isDisposed = false;
    public void Dispose() => Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        _isDisposed = !_isDisposed;
    }
    public abstract IUnitOfWork Provide();
}

public class QueryDBUOWProvider : AbstractQueryDBUOWProvider, IDisposable
{
    bool _isDisposed = false;
    readonly IQueryDBUOWProvider _provider;

    public QueryDBUOWProvider(DbConfig config)
    {
        _provider = dispatchProvider(config.QueryDBInstanceType);
    }

    IQueryDBUOWProvider dispatchProvider(QueryDBInstanceType type)
    {
        switch (type)
        {
            case QueryDBInstanceType.SQLite:
                return new SQLiteQueryDBUOWProvider(new SqliteConfig(DbConfig.GetQueryDBPath()));
            case QueryDBInstanceType.InMemorySQLite:
                return new SQLiteQueryDBUOWProvider(new InMemorySqliteConfig());
            case QueryDBInstanceType.Dictionary:
                return new DictionaryQueryDBUOWProvider();
            default:
                throw new InvalidOperationException();
        }
    }
    protected override void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing) _provider.Dispose();
        _isDisposed = !_isDisposed;
        base.Dispose(true);
    }

    public override IUnitOfWork Provide() => _provider.Provide();
}

public class DictionaryQueryDBUOWProvider : AbstractQueryDBUOWProvider
{
    public override IUnitOfWork Provide() => new InMemoryUnitOfWork();
}

public class SQLiteQueryDBUOWProvider : AbstractQueryDBUOWProvider
{
    bool _isDisposed = false;
    readonly ISqliteConfig _config;

    public SQLiteQueryDBUOWProvider(ISqliteConfig config)
    {
        _config = config;
    }

    protected override void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing) _config.Dispose();
        _isDisposed = !_isDisposed;
        base.Dispose(true);
    }

    public override IUnitOfWork Provide() => new SqliteUnitOfWork(_config.getConnectionString());
}