using Yaroyan.Game.DDD.SharedKernel;

namespace Com.Github.Yaroyan.Constant
{
    public class GameObjectTags : Enumeration
    {
        public GameObjectTags(int id, string name) : base(id, name) { }

        public static GameObjectTags Player = new GameObjectTags(1, "player");
    }
}
