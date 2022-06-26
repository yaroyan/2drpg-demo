using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

public class SaveSlot : Entity<SaveSlot>, IAggregateRoot<SaveSlot>
{
    readonly SaveSlotId _saveSlotId;
    public override IEntityId Id => _saveSlotId;
    public bool IsFavorite { get; private set; }
    public SaveSlot(SaveSlotId saveSlotId, bool isFavorite)
    {
        _saveSlotId = saveSlotId;
        IsFavorite = isFavorite;
    }
}
