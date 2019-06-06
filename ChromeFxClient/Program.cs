using ChromFXUI;
using Models;
using Reducers;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromeFxClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            var debug = true;
            if (debug)
            {
                if (Bootstrap.Load())
                {
                    Application.Run(new MainForm(new Package<Store>(new AppRootReducer())));
                }
            }
            else
            {
                if (Bootstrap.Load())
                {
                    Bootstrap.RegisterAssemblyResources(System.Reflection.Assembly.GetExecutingAssembly(), "dist");
                    Bootstrap.RegisterFolderResources(Application.StartupPath);

                    Application.Run(new MainForm(new Package<Store>(new AppRootReducer())));
                }
            }
        }
    }
}
