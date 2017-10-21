using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL.Entities
{
    
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfComment { get; set; }
        public User CommentAuthor { get; set; }
        public Visibility VisibleFor { get; set; }

        public virtual ICollection<Like> CommentLikes { get; set; }
    }
}
