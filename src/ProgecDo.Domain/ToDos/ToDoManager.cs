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


        public async Task UpdatToDoItem([NotNull] ToDoItem toDoItem, string description, DateTime? dueDate)
        {
            // Check.NotNull(author, nameof(author));
            // Check.NotNullOrWhiteSpace(description, nameof(description));

            toDoItem.UpdatToDoItem(description, dueDate);
        }
    }
}