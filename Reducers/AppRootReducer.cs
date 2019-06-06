using Models;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reducers
{
    public class AppRootReducer:ComposableReducer<Store>
    {
        public AppRootReducer()
        {
            Diverter(state => state.state.PluginPageState, new PluginPageReducer()).
            Diverter(state=> state.state.UploadPageState,new UploadPageReducer()).
            Diverter(state => state.state.SetProjectPathPageState, new SetProjectPathReducer()).
            Diverter(state=> state.state.CreateProjectPageState,new CreateProjectPageReducer());
        }
    }
}
