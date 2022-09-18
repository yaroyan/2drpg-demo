using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Com.Github.Yaroyan.Constant
{
    public class AnimatorParameters : Enumeration
    {
        public AnimatorParameters(int id, string name) : base(id, name) { }
        public static AnimatorParameters AxisY = new AnimatorParameters(1, nameof(AxisY));
        public static AnimatorParameters AxisX = new AnimatorParameters(2, nameof(AxisY));
        public static AnimatorParameters LastAxisX = new AnimatorParameters(3, nameof(LastAxisX));
        public static AnimatorParameters LastAxisY = new AnimatorParameters(4, nameof(LastAxisY));
    }
}
