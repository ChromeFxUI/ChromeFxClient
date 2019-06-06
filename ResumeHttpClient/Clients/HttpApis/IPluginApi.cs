using ResumeHttpClient.Clients.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Parameterables;

namespace ResumeHttpClient.Clients.HttpApis
{
    [TraceFilter(OutputTarget = OutputTarget.Console)]
    public interface IPluginApi : IHttpApi
    {
        /// <summary>
        /// 获取所有插件
        /// </summary>
        /// <returns>Success</returns>
        [HttpGet("api/Plugin")]
        ITask<List<Plugin>> GetAllPluginAsync();

        /// <summary>
        /// 新增插件
        /// </summary>
        /// <param name="engine">选项：UE4，Unity</param>
        /// <param name="funcName">插件名称</param>
        /// <param name="des">插件描述</param>
        /// <param name="version">产检版本</param>
        /// <param name="fileinput">插件包，打包后的.zip文件</param>
        /// <returns>Success</returns>
        [TraceFilter(Enable = false)]
        [HttpPost("api/Plugin")]
        ITask<bool> AddPluginAsync(string engine, string funcName, string des, string version, MulitpartFile fileinput);

        /// <summary>
        /// 根据id获取插件
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success</returns>
        [HttpGet("api/Plugin/{id}")]
        ITask<Plugin> GetPluginByIdAsync([Required] string id);

        /// <summary>
        /// 根据id删除插件
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success</returns>
        [HttpDelete("api/Plugin/{id}")]
        ITask<bool> DeleteByIdAsync([Required] string id);

        /// <summary>
        /// 根据引擎类别，插件名称，插件版本获取插件
        /// </summary>
        /// <param name="engine">引擎类别</param>
        /// <param name="funcName">插件名称</param>
        /// <param name="version">插件版本获取插件</param>
        /// <returns>Success</returns>
        [HttpGet("api/Plugin/{engine}/{funcName}/{version}")]
        ITask<Plugin> GetPluginAsync([Required] string engine, [Required] string funcName, [Required] string version);

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="isUE4"></param>
        /// <returns>Success</returns>
        [HttpPost("api/Plugin/GetPluginVersion")]
        ITask<List<string>> GetPluginVersionAsync(bool? isUE4);

    }
}
