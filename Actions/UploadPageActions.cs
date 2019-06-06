using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.UploadPageActions
{
    /// <summary>
    /// 选择文件夹
    /// </summary>
    public class SelectFolder
    {
    }

    public class SelectProjectFolder
    {
    }

    public class DoZip
    {
        public FileState File;
    }

    public class DoProjectZip
    {
        public FileState File;
    }

    public class StartUpload
    {
        public FileState File;
    }
    public class DoUpload
    {
        public FileState File;
    }

    public class StartProjectUpload
    {
        public FileState File;
    }
   
    public class DoProjectUpload
    {
        public FileState File;
    }

    /// <summary>
    /// 更新文件状态
    /// </summary>
    public class RefreshState
    {
        public FileState File;
    }

    /// <summary>
    /// 上传单个文件夹
    /// </summary>
    public class UploadFolder
    {
        public FileState File;
    }

    /// <summary>
    /// 暂停所有上传
    /// </summary>
    public class StopAllUpload
    {
    }

    /// <summary>
    /// 暂停单个上传
    /// </summary>
    public class StopUploadFile
    {
        public FileState File;
    }
}
