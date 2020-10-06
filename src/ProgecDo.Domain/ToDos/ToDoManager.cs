using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace ProgecDo.ToDos
{
    public class ToDoManager : DomainService
    {
        public async Task<ToDo> CreateAsync([NotNull] string name, string description, Guid projectId)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            return new ToDo(
                GuidGenerator.Create(),
                name,
                description,
                projectId);
        }
    }
}