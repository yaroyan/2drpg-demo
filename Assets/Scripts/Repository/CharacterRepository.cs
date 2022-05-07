using System.Collections;
using System.Collections.Generic;

namespace Com.Github.Yaroyan.Rpg.Repository
{
    public class CharacterRepository : AbstractRepository
    {
        [VContainer.Inject]
        public CharacterRepository(ISqliteConfig sqliteConfig) : base(sqliteConfig)
        {
        }
    }
}
