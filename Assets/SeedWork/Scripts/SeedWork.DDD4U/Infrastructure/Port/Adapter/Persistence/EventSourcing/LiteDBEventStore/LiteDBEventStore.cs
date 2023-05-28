using System.Collections;
using System.Collections.Generic;
using LiteDB;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public class LiteDBEventStore : IAppendOnlyStore
    {
        LiteDatabase _liteDatabase;

        public LiteDBEventStore(string connectionString)
        {
            _liteDatabase = new LiteDatabase(connectionString);
        }

        public void Append(string Id, IEnumerable<StoredEvent> data, long expectedStreamVersion = -1)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public string NextIdentity()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StoredEvent> ReadRecords(string Id, long afterVersion, int maxCount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StoredEvent> ReadRecords(long afterVersion, int maxCount)
        {
            throw new System.NotImplementedException();
        }
    }
}
