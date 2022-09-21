using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.SaveData
{
    public class SaveData : Entity<SaveDataId>, IAggregateRoot<SaveDataId>
    {
        // repositoryId
        // branchId
        // commitIds
        static readonly int s_MaxMemoCount = 140;
        public override SaveDataId Id { get; init; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime AccessedAt { get; private set; }
        string _memo;
        public string Memo { get => _memo; private set { _memo = (value?.Length ?? 0) <= s_MaxMemoCount ? value : throw new ArgumentException("Number of Character must be less than equal 140.", nameof(Memo)); } }
        public bool IsFavorite { get; private set; }
        public SaveData(SaveDataId saveDataId, bool isFavorite, DateTime createdAt, DateTime updatedAt, DateTime accessedAt, string memo) : base(Enumerable.Empty<IEvent>())
        {
            Id = saveDataId;
            IsFavorite = isFavorite;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            AccessedAt = accessedAt;
            Memo = memo;
        }

        public SaveData(IEnumerable<IEvent> events) : base(events) { }

        protected override void Mutate(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
