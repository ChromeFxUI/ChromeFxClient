using Models;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.CreateProjectPageActions
{
    public class StartProcess
    {
        public ModelProcess ModelProcess
        {
            get; set;
        }

        public SetProjectPathPageState SetProjectPathPageState
        {
            set; get;
        }

        public Action<ModelProcess> RefreshState;
        public Action<Message> ShowMessage;
    }
    public class LoadMoudels
    {
        
    }
    public class InitProcess
    {
        public string OutputPath
        {
            get;set;
        }
    }

    public class RefreshState
    {
        public CreateProjectPageState CreateProjectPageState
        {
            get; set;
        }
    }

    public class ShowMessage
    {
        public Message Message
        {
            get; set;
        }
    }
}
