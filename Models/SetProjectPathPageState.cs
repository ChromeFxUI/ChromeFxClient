using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SetProjectPathPageState: IState, IMessage
    {
        public string OutputPath
        {
            get;set;
        }
        public bool IsLegal
        {
            get;set;
        }
        public Message Message { get; set; } = new Message();
    }
}
