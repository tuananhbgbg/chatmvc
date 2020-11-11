using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMVC
{
    public class MsgResponse
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public User Author { get; set; }
    }
}
