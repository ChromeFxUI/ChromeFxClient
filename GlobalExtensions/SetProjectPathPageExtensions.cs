using Actions.CreateProjectPageActions;
using Actions.SetProjectPathPageAction;
using Chromium.WebBrowser;
using Models;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalExtensions
{
    public static class SetProjectPathPageExtensions
    {
        public static void UseSetProjectPathPage(this JSObject chromfx, Package<Store> store)
        {
            var uploadPageActions = chromfx.AddObject("setProjectPathActions");

            uploadPageActions.AddFunction("setProjectPath").Execute += (func, args) =>
            {
                store.Dispatch(new SetProjectPath());
            };
        }
    }
}
