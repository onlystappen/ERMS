
using System;

namespace ERMS.Domain.Entities
{
    public class RequestComment
    {
        public int RequestCommentId { get; set; }          //PK

        public int RequestId { get; set; }          //FK --> Request
        Request Request { get; set; }     

        public int AuthorId { get; set; }           //FK --> User
        public User Author { get; set; } = null!;

        public string Content { get; set; }          
        public DateTime CreateddAt { get; set; }    





    }
}