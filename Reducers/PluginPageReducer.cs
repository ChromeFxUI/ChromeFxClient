using Actions.PulginPageActions;
using Models;
using Newtonsoft.Json;
using ReduxCore;
using ResumeHttpClient.Clients.HttpApis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;

namespace Reducers
{
    public class PluginPageReducer: ElementReducer<PluginPageState>
    {
        public PluginPageReducer()
        {
            Process<LoadEngins>((state, action) =>
            {
                state.Engins = new List<Engin>();
                state.Engins.Add(new Engin() { Id = "UE4" ,Name = "UE4" });
                state.Engins.Add(new Engin() { Id = "Unity", Name = "Unity" });

                return state;
            }).
            Process<LoadProjects>((state, action) =>
            {
                state.Projects = new List<Project>();
                var projectApiServer = HttpApi.Resolve<IProjectApi>();
                var result = projectApiServer.GetAllProjectAsync().InvokeAsync().Result;
                if(result != null)
                {
                    foreach(var item in result)
                    {
                        state.Projects.Add(new Project()
                        {
                            Id = item.Id,
                            Des = item.Description,
                            Name = item.ProjectName,
                            RelativePath = item.RelativePath,
                            Version = item.Version,
                            Versions = GetVersions(item.Versions),
                            Engine = item.Engine
                        });
                    }
                }

                return state;
            }).
            Process<LoadEnginVersions>((state,action)=>
            {
                state.UE4Versions = new List<Models.Version>();
                state.UnityVersions = new List<Models.Version>();
                var httpUE4Result = HttpApi.Resolve<IPluginApi>().GetPluginVersionAsync(true).InvokeAsync().Result;
                var httpUnityResult = HttpApi.Resolve<IPluginApi>().GetPluginVersionAsync(false).InvokeAsync().Result;
                
                if(httpUE4Result != null)
                {
                    foreach(var item in httpUE4Result)
                    {
                        state.UE4Versions.Add(new Models.Version() { Id = item, Name = item});
                    }
                }

                if (httpUnityResult != null)
                {
                    foreach (var item in httpUnityResult)
                    {
                        state.UnityVersions.Add(new Models.Version() { Id = item, Name = item });
                    }
                }

                return state;
            }).
            Process<LoadFuncs>((state, action) => {
                state.FuncModels = new List<FuncModel>();

                var httpResult = HttpApi.Resolve<IPluginApi>().GetAllPluginAsync().InvokeAsync().Result;

                if (httpResult != null)
                {
                    foreach(var item in httpResult)
                    {
                        var curFuncModel = new FuncModel()
                        {
                            Id = item.Id,
                            Des = item.Description,
                            Name = item.FuncName,
                            RelativePath = item.RelativePath,
                            Version = item.Version,
                            Versions = GetVersions(item.Versions),
                            Engine = item.Engine,
                        };
                        state.FuncModels.Add(curFuncModel);
                    }
                }

                return state;
            }).
            Process<CreateProjectJson>((state, action) => {
                var dir = @"./ProjectCreateJson";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var fileFullName = dir + "/" + action.ProjectCreateMoudle.ProjectName + ".json";
                if (!File.Exists(fileFullName))
                {
                    File.WriteAllText(fileFullName, JsonConvert.SerializeObject(action.ProjectCreateMoudle));
                }

                state.Message.Msg = "创建json文件成功";
                state.Message.MsgType = MessageType.Success;
                state.Message.Showtype = Showtype.Notification;

                return state;
            });
        }

        private List<Models.Version> GetVersions(List<string> version)
        {
            var list = new List<Models.Version>();
            if(version != null)
            {
                version.ForEach(ele =>
                {
                    list.Add(new Models.Version() { Id = ele, Name = ele });
                });
            }
            
            return list;
        }
    }
}
