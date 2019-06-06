using Actions.PulginPageActions;
using Chromium.WebBrowser;
using Models;
using Newtonsoft.Json;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalExtensions
{
    public static class PluginPageExtensions
    {
        public static void UsePluginPage(this JSObject chromfx, Package<Store> store)
        {
            var pluginPageActions = chromfx.AddObject("pluginPageActions");
            pluginPageActions.AddFunction("createProjectJson").Execute += (func, args) =>
            {
                var str = args.Arguments[0].StringValue;
                var projectCreateMoudle = JsonConvert.DeserializeObject<ProjectCreateMoudle>(str);
                var state = store.GetState();

                state.CreateProjectPageState.Models = new List<ModelProcess>();
                state.CreateProjectPageState.Models.Add(new ModelProcess()
                {
                    Id = projectCreateMoudle.Project.Id,
                    Name = projectCreateMoudle.Project.Name,
                    DownloadPath = "",
                    Engine = projectCreateMoudle.Project.Engine,
                    OutputPath = "",
                    IsTemplateProject = true,
                    Version = projectCreateMoudle.Project.Version,
                    Des = projectCreateMoudle.Project.Des
                });


                foreach (var item in projectCreateMoudle.FuncModels)
                {
                    state.CreateProjectPageState.Models.Add(new ModelProcess()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DownloadPath = "",
                        Engine = item.Engine,
                        OutputPath = "",
                        IsTemplateProject = false,
                        Version = item.Version,
                        Des = item.Des
                    });
                }
            
                store.Dispatch(new CreateProjectJson() { ProjectCreateMoudle = projectCreateMoudle });

            };
            pluginPageActions.AddFunction("loadEngins").Execute += (func, args) =>
            {
                store.Dispatch(new LoadEngins());
            };
            pluginPageActions.AddFunction("loadEnginVersions").Execute += (func, args) =>
            {
                store.Dispatch(new LoadEnginVersions());
            };
            pluginPageActions.AddFunction("loadFuncs").Execute += (func, args) =>
            {
                var engin = args.Arguments[0].StringValue;
                var version = args.Arguments[1].StringValue;
                var queryStr = args.Arguments[2].StringValue;

                store.Dispatch(new LoadFuncs()
                {
                    Engine = engin,
                    Version = version,
                    FuncName = queryStr
                }) ;
            };
            pluginPageActions.AddFunction("loadProjects").Execute += (func, args) =>
            {
                store.Dispatch(new LoadProjects());
            };
        }
    }
}
