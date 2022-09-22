using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.Constant
{
    public class GameObjectTags : Enumeration
    {
        public GameObjectTags(int id, string name) : base(id, name) { }

        public static GameObjectTags Player = new GameObjectTags(1, "player");
    }
}
