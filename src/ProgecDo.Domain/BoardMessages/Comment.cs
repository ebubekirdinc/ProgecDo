using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProgecDo.BoardMessages
{
    public class Comment : FullAuditedEntity<Guid>
    {
        public virtual Guid ParentId { get; private set; }

        public virtual string Content { get; private set; }

        public virtual BoardMessage BoardMessage { get; set; }
        // public virtual Todo Todo{ get; set; }

        public Comment()
        {
        }

        internal Comment(Guid parentId, string content)
        {
            ParentId = parentId;
            Content = Check.NotNullOrWhiteSpace(content, nameof(Content), BoardMessageConsts.MaxContentLength, BoardMessageConsts.MinContentLength);
        }

        public void SetContent([NotNull] string content)
        {
            Content = Check.NotNullOrWhiteSpace(content, nameof(Content), maxLength: BoardMessageConsts.MaxContentLength, BoardMessageConsts.MinContentLength); 
        }
    }
}