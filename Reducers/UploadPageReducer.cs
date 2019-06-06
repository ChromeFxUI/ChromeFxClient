using Actions.UploadPageActions;
using Models;
using ReduxCore;
using ResumeHttpClient.Clients.HttpApis;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Parameterables;

namespace Reducers
{
    public class UploadPageReducer:ElementReducer<UploadPageState>
    {
        public UploadPageReducer()
        {
            Process<SelectFolder>((state, action) =>
            {
                if(state.Files == null)
                {
                    state.Files = new List<FileState>();
                }
                var folder = SelectPath();
                
                if(state.Files.FirstOrDefault(p=>p.OriginalFileFullPath == folder.Trim()) == null)
                {
                    state.Files.Add(new FileState()
                    {
                        FileName = folder.Substring(folder.LastIndexOf("\\") + 1),
                        OriginalFileFullPath = folder,
                        IsUploading = false,
                        Msg = null,
                        ZipFileFullPath = @"./ZipTemp/"+ folder.Substring(folder.LastIndexOf("\\") + 1)+".zip",
                    });
                }

                if (!Directory.Exists(@"./ZipTemp"))
                {
                    Directory.CreateDirectory(@"./ZipTemp");
                }
                
                return state;
            }).
            Process<SelectProjectFolder>((state, action) =>
            {
                if (state.ProjectFiles == null)
                {
                    state.ProjectFiles = new List<FileState>();
                }
                var folder = SelectPath();

                if (state.ProjectFiles.FirstOrDefault(p => p.OriginalFileFullPath == folder.Trim()) == null)
                {
                    state.ProjectFiles.Add(new FileState()
                    {
                        FileName = folder.Substring(folder.LastIndexOf("\\") + 1),
                        OriginalFileFullPath = folder,
                        IsUploading = false,
                        Msg = null,
                        ZipFileFullPath = @"./ZipTemp/" + folder.Substring(folder.LastIndexOf("\\") + 1) + ".zip",
                    });
                }

                if (!Directory.Exists(@"./ZipTemp"))
                {
                    Directory.CreateDirectory(@"./ZipTemp");
                }

                return state;
            }).
            Process<DoZip>((state,action)=>
            {
                if(!File.Exists(action.File.ZipFileFullPath) && !action.File.IsZiped)
                {
                    ZipFile.CreateFromDirectory(action.File.OriginalFileFullPath, action.File.ZipFileFullPath);
                }

                var file = state.Files.FirstOrDefault(p => p.OriginalFileFullPath == action.File.OriginalFileFullPath);
                if(file != null)
                {
                    file.IsZiped = true;
                }
                
                return state;
            }).
            Process<DoProjectZip>((state, action) =>
            {
                if (!File.Exists(action.File.ZipFileFullPath) && !action.File.IsZiped)
                {
                    ZipFile.CreateFromDirectory(action.File.OriginalFileFullPath, action.File.ZipFileFullPath);
                }

                var file = state.ProjectFiles.FirstOrDefault(p => p.OriginalFileFullPath == action.File.OriginalFileFullPath);
                if (file != null)
                {
                    file.IsZiped = true;
                }

                return state;
            }).
            Process<StartUpload>((state, action) =>
            {
                var file = state.Files.FirstOrDefault(p => p.OriginalFileFullPath == action.File.OriginalFileFullPath);
                if (file != null)
                {
                    file.IsUploading = true;
                    file.IsUploadSuccess = false;
                }
                return state;
            }).
            Process< StartProjectUpload >((state, action) =>
            {
                var file = state.ProjectFiles.FirstOrDefault(p => p.OriginalFileFullPath == action.File.OriginalFileFullPath);
                if (file != null)
                {
                    file.IsUploading = true;
                    file.IsUploadSuccess = false;
                }
                return state;
            }).
            Process<DoUpload>((state,action)=>
            {
                var uplginApiServer = HttpApi.Resolve<IPluginApi>();
               var uploadState = uplginApiServer.AddPluginAsync(action.File.Engine, action.File.FileName, action.File.Des, action.File.Version, new MulitpartFile(action.File.ZipFileFullPath)
               {
                   ContentType = "multipart/form-data",
               }).InvokeAsync().Result ;

                var file = state.Files.FirstOrDefault(p => p.OriginalFileFullPath == action.File.OriginalFileFullPath);
                if (file != null)
                {
                    file.IsUploading = false;
                    file.IsUploadSuccess = uploadState;
                    if (!uploadState)
                    {
                        file.Msg = "上传失败！";
                    }
                    else
                    {
                        file.Msg = "上传成功！";
                    }
                }
                return state;
            }).
            Process<DoProjectUpload>((state, action) =>
            {
                var projectApiServer = HttpApi.Resolve<IProjectApi>();
                var uploadState = projectApiServer.AddProjectAsync(action.File.Engine, action.File.FileName, action.File.Des, action.File.Version, new MulitpartFile(action.File.ZipFileFullPath)
                {
                    ContentType = "multipart/form-data",
                }).InvokeAsync().Result;

                var file = state.ProjectFiles.FirstOrDefault(p => p.OriginalFileFullPath == action.File.OriginalFileFullPath);
                if (file != null)
                {
                    file.IsUploading = false;
                    file.IsUploadSuccess = uploadState;
                    if (!uploadState)
                    {
                        file.Msg = "上传失败！";
                    }
                    else
                    {
                        file.Msg = "上传成功！";
                    }
                }
                return state;
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
