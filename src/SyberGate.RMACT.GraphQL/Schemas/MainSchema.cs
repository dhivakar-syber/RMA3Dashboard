using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using SyberGate.RMACT.Queries.Container;

namespace SyberGate.RMACT.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}