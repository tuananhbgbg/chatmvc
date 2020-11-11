using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMVC
{
    public class ChannelChat
    {
        public List<MsgResponse> Messages { get; set; }
        public Channel Channel { get; set; }
    }
}
