using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public User Publisher { get; set; }
        public virtual ICollection<Comment> PhotoComments { get; set; }
        public virtual ICollection<Like> PhotoLikes { get; set; }
    }
}
