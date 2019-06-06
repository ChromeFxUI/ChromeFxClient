using ResumeHttpClient.Clients.HttpApis;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;

namespace ResumeHttpClient
{
    public static class HttpClientContiner
    {
        public static void ConfigureHttpClientContiner(this Object disposable, Uri uri)
        {
            try
            {

                //客户端token请求注册
                HttpApi.Register<IFileApi>().ConfigureHttpApiConfig(option =>
                {
                    option.HttpHost = uri;
                });
                HttpApi.Register<IPluginApi>().ConfigureHttpApiConfig(option =>
                {
                    option.HttpHost = uri;
                });
                HttpApi.Register<IProjectApi>().ConfigureHttpApiConfig(option =>
                {
                    option.HttpHost = uri;
                });
            }
            catch (Exception ex)
            {
                //没有此服务配置，可能不需要要改客户端
            }

        }
    }
}
