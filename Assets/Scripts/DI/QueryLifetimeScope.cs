using VContainer;
using VContainer.Unity;
using Com.Github.Yaroyan.Rpg.CQRS;

namespace Com.Github.Yaroyan.Rpg.DI
{
    /// <summary>
    /// DI Container for CQRS - Query
    /// </summary>
    public class QueryLifetimeScope : LifetimeScope
    {
        /// <param name="builder"></param>
        protected override void Configure(IContainerBuilder builder)
        {

        }
    }
}
