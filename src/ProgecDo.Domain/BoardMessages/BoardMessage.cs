using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProgecDo.BoardMessages
{
    public class BoardMessage : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Title { get; private set; }
        public virtual string Content { get; private set; }
        public virtual Guid ProjectId { get; set; }

        public virtual List<Comment> Comments { get; protected set; }

        // public virtual int CategoryId { get; set; }

        public BoardMessage()
        {
        }

        internal BoardMessage(Guid id, [NotNull] string title, [NotNull] string content, Guid projectId) : base(id)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(Title), BoardMessageConsts.MaxTitleLength, BoardMessageConsts.MinTitleLength);
            Content = Check.NotNullOrWhiteSpace(content, nameof(Content), BoardMessageConsts.MaxContentLength, BoardMessageConsts.MinContentLength);
            ProjectId = projectId;

            Comments = new List<Comment>
            {
                //  new BoardMessageComment(id, content)
            };
        }

        public void AddComment(string content)
        {
            Check.NotNullOrWhiteSpace(content, nameof(Content), BoardMessageConsts.MaxContentLength, BoardMessageConsts.MinContentLength);

            Comments.Add(new Comment(Id, content));
        }

        public void DeleteComment(Guid commentId)
        {
            Comments.Remove(Comments.FirstOrDefault(x => x.Id == commentId));
        }
    }

    // public class MessageCategories  : FullAuditedAggregateRoot<int>
    // {
    //     public virtual string Name { get; set; }
    //     public virtual string Text { get; set; } 
    // }
}