using System;

namespace ResumeHttpClient
{
    public class HttpClientConfigOptions
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        public string HttpEndPoint { get; set; }

        /// <summary>
        /// 获取提供Token获取的Url节点
        /// </summary>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// client_id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// client_secret
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
