using Actions.CreateProjectPageActions;
using ChromFXUI;
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
    public static class CreateProjectPageExtension
    {
        public static void UseCreateProjectPage(this JSObject chromfx, Package<Store> store, ChromFXBaseForm chromFXBaseForm)
        {
            var pluginPageActions = chromfx.AddObject("createProjectActions");
            pluginPageActions.AddFunction("startProcess").Execute += (func, args) =>
            {
                var str = args.Arguments[0].StringValue;
                var modelProcess = JsonConvert.DeserializeObject< List<ModelProcess>>(str);
                var state = store.GetState();

                Parallel.ForEach(modelProcess, ele =>
                {
                    store.Dispatch(new StartProcess()
                    {
                        ModelProcess = ele,
                        SetProjectPathPageState = state.SetProjectPathPageState,
                        RefreshState = (model) =>
                        {
                            var curValue = JsonConvert.SerializeObject(model);
                            string cmd = string.Format("createProjectActions.refreshUpload(\"{0}\",{1})", model.Id,model.Percent);

                            chromFXBaseForm.ExecuteJavascript(cmd);
                        },
                        ShowMessage = (m) =>
                        {
                            store.Dispatch(new ShowMessage() { Message = m });
                        }
                    });
                });
               
            };
            pluginPageActions.AddFunction("loadMoudels").Execute += (func, args) =>
            {
                store.Dispatch(new LoadMoudels());
            };

        }
    }
}
