using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL.Entities
{
    public enum Interests
    {
        Fashion,
        Restaurant,
        Books,
        SelfDevelopment
    }

    public class UserInterest
    {
        public int Id { get; set; }
        public Interests Interest { get; set; }
    }
}
