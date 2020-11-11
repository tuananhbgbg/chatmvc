using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMVC
{
    public class Channel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Admin { get; set; }
        public string Secret { get; set; }

    }
}
