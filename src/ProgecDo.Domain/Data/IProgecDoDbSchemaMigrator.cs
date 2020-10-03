using System.Threading.Tasks;

namespace ProgecDo.Data
{
    public interface IProgecDoDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
