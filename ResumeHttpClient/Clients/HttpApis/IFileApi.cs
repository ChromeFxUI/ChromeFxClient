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
    public interface IFileApi : IHttpApi
    {
        /// <param name="pluginId"></param>
        /// <returns>Success</returns>
        [HttpGet("Plugin/{pluginId}")]
        ITask<HttpResponseMessage> GetPluginAsync([Required] string pluginId);

        /// <param name="projectId"></param>
        /// <returns>Success</returns>
        [HttpGet("Project/{projectId}")]
        ITask<HttpResponseMessage> GetProjectAsync([Required] string projectId);

        /// <param name="value"></param>
        /// <returns>Success</returns>
        [HttpPost("api/File")]
        ITask<HttpResponseMessage> PostAsync([JsonContent] string value);

        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns>Success</returns>
        [HttpPut("api/File/{id}")]
        ITask<HttpResponseMessage> PutAsync([Required] int id, [JsonContent] string value);

        /// <param name="id"></param>
        /// <returns>Success</returns>
        [HttpDelete("api/File/{id}")]
        ITask<HttpResponseMessage> DeleteAsync([Required] int id);

    }
}
