using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PluginPageState : IState, IMessage
    {
        public List<Engin> Engins
        {
            get; set;
        }
        public List<Version> UE4Versions
        {
            get; set;
        }

        public List<Version> UnityVersions
        {
            get; set;
        }
        public List<FuncModel> FuncModels
        {
            get; set;
        }

        public List<Project> Projects
        {
            get;set;
        }
        public Message Message { get; set; } = new Message();
    }

    public class ProjectCreateMoudle
    {
        public Project Project { get; set; }
        public List<FuncModel> FuncModels { get; set; }

        public string ProjectName { get; set; }

        public string Des { get; set; }
    }

    public class Engin
    {
        public string Name
        {
            get; set;
        }
        public string Id
        {
            get; set;
        }
    }

    public class Version
    {
        public string Name
        {
            get; set;
        }
        public string Id
        {
            get; set;
        }
    }
    public class FuncModel
    {
        public string Name
        {
            get; set;
        }
        public string Des
        {
            get; set;
        }
        public string Version
        {
            get; set;
        }
        public string Id
        {
            get; set;
        }
        public string RelativePath
        {
            get; set;
        }
        public List<Version> Versions
        {
            get; set;
        }
        public string Engine
        {
            get;set;
        }
    }

    public class Project
    {
        public string Name
        {
            get; set;
        }
        public string Des
        {
            get; set;
        }
        public string Version
        {
            get; set;
        }
        public string Id
        {
            get; set;
        }
        public string RelativePath
        {
            get; set;
        }
        public List<Version> Versions
        {
            get; set;
        }
        public string Engine
        {
            get; set;
        }
    }
}
