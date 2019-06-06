using ResumeHttpClient.Clients.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Parameterables;

namespace ResumeHttpClient.Clients.HttpApis
{
    [TraceFilter(OutputTarget = OutputTarget.Console)]
    public interface IProjectApi : IHttpApi
    {
        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns>Success</returns>
        [HttpGet("api/Project")]
        ITask<List<Project>> GetAllProjectAsync();

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="funcName"></param>
        /// <param name="des"></param>
        /// <param name="version"></param>
        /// <param name="fileinput"></param>
        /// <returns>Success</returns>
        [TraceFilter(Enable = false)]
        [HttpPost("api/Project")]
        ITask<bool> AddProjectAsync(string engine, string funcName, string des, string version, MulitpartFile fileinput);

        /// <param name="id"></param>
        /// <returns>Success</returns>
        [HttpDelete("api/Project")]
        ITask<bool> DeleteProjectAsync(string id);

        /// <summary>
        /// 根据项目id获取项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success</returns>
        [HttpGet("api/Project/{id}")]
        ITask<Project> GetProjectByIdAsync([Required] string id);

    }
}
