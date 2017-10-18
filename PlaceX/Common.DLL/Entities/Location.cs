using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public Position LocationPosition { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Comment> LocationComments { get; set; }
        public virtual ICollection<Like> LocationLikes { get; set; }

        public User Publisher { get; set; }
        public bool HotLocation { get; set; }
    }
}
