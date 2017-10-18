using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string CityOfLiving { get; set; }

        public virtual ICollection<User> FriendshipList { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<UserInterest> Interests { get; set; }

        public Like UserLike { get; set; }
    }
}
