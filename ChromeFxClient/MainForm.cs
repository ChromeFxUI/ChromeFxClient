using GlobalExtensions;
using Models;
using ReduxCore;
using ReduxStyleUI;
using System;
using ResumeHttpClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ChromeFxClient
{
    public partial class MainForm : ReduxStyleForm<Store>
    {
        public MainForm(Package<Store> store):
            base(store, "http://10.1.30.241:8080/")
        {
            InitializeComponent();

            this.GlobalObject.UsePluginPage(store);
            this.GlobalObject.UseUploadPage(store);
            this.GlobalObject.UseSetProjectPathPage(store);
            this.GlobalObject.UseCreateProjectPage(store);
            //store.UseNoticeMiddleware();

            this.ConfigureHttpClientContiner(new Uri($"http://{ConfigurationManager.AppSettings.Get("RemoteServerIp")}:{ConfigurationManager.AppSettings.Get("RemoteServerPort")}"));

            LoadHandler.OnLoadEnd += LoadHandler_OnLoadEnd;
        }

        private void LoadHandler_OnLoadEnd(object sender, Chromium.Event.CfxOnLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                Chromium.ShowDevTools();
            }
        }
    }
}
