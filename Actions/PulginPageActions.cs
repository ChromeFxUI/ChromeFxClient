using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.PulginPageActions
{
    /// <summary>
    /// 获取引擎
    /// </summary>
    public class LoadEngins { }
    
    /// <summary>
    /// 获取引擎的版本
    /// </summary>
    public class LoadEnginVersions { }
    /// <summary>
    /// 获取功能模块
    /// </summary>
    public class LoadFuncs
    {
        public string FuncName = "";
        public string Engine = "";
        public string Version = "";
    }
    /// <summary>
    /// 获取所有项目
    /// </summary>
    public class LoadProjects { }

    /// <summary>
    /// 创建脚本
    /// </summary>
    public class CreateProjectJson
    {
        public ProjectCreateMoudle ProjectCreateMoudle;
    }

}
