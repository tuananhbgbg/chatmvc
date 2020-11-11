using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMVC
{
    public class User
    {
        public long UserId { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string ApiKey { get; set; }
        public string AvatarUrl { get; set; }

    }
}
