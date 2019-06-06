using Actions.SetProjectPathPageAction;
using Models;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reducers
{
    public class SetProjectPathReducer : ElementReducer<SetProjectPathPageState>
    {
        public SetProjectPathReducer()
        {
            Process<SetProjectPath>((state, action) =>
            {
                try
                {
                    var folder = SelectPath();
                    state.OutputPath = folder;

                    if (!Directory.Exists(state.OutputPath))
                    {
                        Directory.CreateDirectory(state.OutputPath);
                    }
                    state.IsLegal = true;


                    state.Message.Msg = "选择文件夹成功";
                    state.Message.MsgType = MessageType.Success;
                    state.Message.Showtype = Showtype.Message;

                    return state;
                }
                catch(Exception ex)
                {
                    state.IsLegal = false;
                    return state;
                }
            });
        }

        private string SelectPath()
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            return path;
        }
    }
}
