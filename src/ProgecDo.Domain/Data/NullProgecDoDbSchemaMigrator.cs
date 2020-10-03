using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ProgecDo.Data
{
    /* This is used if database provider does't define
     * IProgecDoDbSchemaMigrator implementation.
     */
    public class NullProgecDoDbSchemaMigrator : IProgecDoDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}