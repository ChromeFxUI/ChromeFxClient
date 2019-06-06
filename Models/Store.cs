using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 根store
    /// </summary>
    public struct Store: IState
    {
        public PluginPageState PluginPageState;
        public UploadPageState UploadPageState;
        public CreateProjectPageState CreateProjectPageState;
        public SetProjectPathPageState SetProjectPathPageState;
    }
}
