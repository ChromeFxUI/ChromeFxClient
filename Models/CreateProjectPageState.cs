using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CreateProjectPageState : IState, IMessage
    {
        public List<ModelProcess> Models
        {
            get;
            set;
        }
        public Message Message { get; set; } = new Message();
    }

    public class ModelProcess
    {
        public bool IsTemplateProject { get; set; }
        public int Percent
        {
            get;set;
        }

        public string StepName
        {
            get;set;
        }
        public bool IsStart
        {
            get;set;
        }
        public bool IsCompleted
        {
            get;set;
        }

        public string Name
        {
            get;set;
        }

        public string Version
        {
            get;set;
        }

        public string Engine
        {
            set;get;
        }
        
        public string OutputPath
        {
            set;get;
        }

        public string DownloadPath
        {
            set;get;
        }

        public string Id
        {
            set;get;
        }

        public string Des
        {
            get;set;
        }
    }
}
