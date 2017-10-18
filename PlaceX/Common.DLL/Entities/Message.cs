using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfSending { get; set; }
        public bool IsNew { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
