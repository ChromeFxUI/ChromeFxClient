using Actions.CreateProjectPageActions;
using Models;
using ReduxCore;
using ResumeHttpClient.Clients.HttpApis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;

namespace Reducers
{
    public class CreateProjectPageReducer : ElementReducer<CreateProjectPageState>
    {
        public CreateProjectPageReducer()
        {
            Process<StartProcess>((state, action) =>
            {
                var curModel = state.Models.FirstOrDefault(p => p.Id == action.ModelProcess.Id);
                curModel.StepName = "下载中...";
                curModel.Percent = 0;
                curModel.IsStart = true;
                
                Task.Factory.StartNew(() =>
                {
                    refreshState(action.RefreshState, curModel);
                    WebClient client = new WebClient();
                    var url = "";
                    if (action.ModelProcess.IsTemplateProject)
                    {
                        url = "http://localhost:5000/Project/" + action.ModelProcess.Id;
                        //HttpApi.Resolve<IFileApi>().GetProjectAsync(action.ModelProcess.Id);
                    }
                    else
                    {
                        //HttpApi.Resolve<IFileApi>().GetPluginAsync(action.ModelProcess.Id);
                        url = "http://localhost:5000/Plugin/" + action.ModelProcess.Id;
                    }

                    FileInfo fileInfo = null;
                    var fileDir = AppDomain.CurrentDomain.BaseDirectory + "DownLoad/";
                    if (!Directory.Exists(fileDir))
                    {
                        Directory.CreateDirectory(fileDir);
                    }
                    var filePath = fileDir + action.ModelProcess.Name;

                    long existLen = 0;

                    if (File.Exists(filePath))
                    {
                        var fInfo = new FileInfo(filePath);
                        existLen = fInfo.Length;
                    }

                    if (File.Exists(filePath))
                    {
                        var finfo = new FileInfo(filePath);

                        if (client.ResponseHeaders != null &&
                            finfo.Length >= Convert.ToInt64(client.ResponseHeaders["Content-Length"]))
                        {

                            File.Delete(filePath);
                        }
                    }

                    DownloadFileWithResume(url, filePath,action.ModelProcess,action.RefreshState,action.ShowMessage);

                });

                return state;
            }).
            Process<RefreshState>((state, action) =>
            {
                state.Models = action.CreateProjectPageState.Models;
                return state;
            }).
            Process<ShowMessage>((state, action) =>
            {
                  state.Message = action.Message;
                  return state; 
            }).
            Process<LoadMoudels>((state, action) =>
            {
                state.Message.Msg = "请点击生成按钮开始生成项目！";
                if(state.Models == null)
                {
                    state.Models = new List<ModelProcess>();
                }
                foreach(var item in state.Models)
                {
                    item.StepName = "";
                    item.Percent = 0;
                }
                 return state;
            }).
            Process<InitProcess>((state, action) =>
            {
                foreach(var item in state.Models)
                {
                    item.OutputPath = action.OutputPath;
                    item.DownloadPath = action.OutputPath + "/temp";
                }

                if(!Directory.Exists(action.OutputPath + "/temp"))
                {
                    Directory.CreateDirectory(action.OutputPath + "/temp");
                }

                return state;
            }); 
        }

        public void refreshState(Action<ModelProcess> refresh, ModelProcess modelProcess)
        {
            if (refresh != null)
            {
                refresh.Invoke(modelProcess);
            }
        }

        /// <summary>
        /// 断点续传下载
        /// </summary>
        /// <param name="sourceUrl"></param>
        /// <param name="destinationPath"></param>
        private void DownloadFileWithResume(string sourceUrl, string destinationPath, ModelProcess curModelProcess, Action<ModelProcess> refresh,Action<Message> showMessage)
        {
            try
            {
                long existLen = 0;
                FileStream saveFileStream;
                if (File.Exists(destinationPath))
                {
                    var fInfo = new FileInfo(destinationPath);
                    existLen = fInfo.Length;
                }
                if (existLen > 0)
                {
                    saveFileStream = new FileStream(destinationPath,
                                                                FileMode.Append, FileAccess.Write,
                                                                FileShare.ReadWrite);
                }
                else
                {
                    saveFileStream = new FileStream(destinationPath,
                                                               FileMode.Create, FileAccess.Write,
                                                               FileShare.ReadWrite);
                }

                var httpWebRequest = (HttpWebRequest)System.Net.HttpWebRequest.Create(sourceUrl);
                httpWebRequest.AddRange(existLen);

                try
                {


                    var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    byte[] buffer = new byte[1024];
                    var totalCount = httpWebResponse.ContentLength;
                    using (var respStream = httpWebResponse.GetResponseStream())
                    {
                        var timout = httpWebRequest.Timeout;
                        //respStream.CopyTo(saveFileStream);

                        int readTotalSize = (int)existLen;

                        //int size = respStream.Read(buffer, 0, buffer.Length);
                        int size = respStream.Read(buffer, 0, buffer.Length);

                        while (size > 0)
                        {
                            //只将读出的字节写入文件
                            saveFileStream.Write(buffer, 0, size);
                            readTotalSize += size;

                            size = respStream.Read(buffer, 0, buffer.Length);

                            if (size == 0)
                            {
                                saveFileStream.Close();
                                saveFileStream.Dispose();

                                curModelProcess.Percent = 100;
                                refreshState(refresh, curModelProcess);
                            }
                            int oldValue = curModelProcess.Percent;
                            var curPer = (int)((readTotalSize / (float)httpWebResponse.ContentLength) * 100);
                            if (curPer > (oldValue + 1))
                            {
                                curModelProcess.Percent = ((int)curPer);
                                refreshState(refresh, curModelProcess);
                            }

                           
                        }


                    }
                }
                catch (Exception ex)
                {
                    //已经下载完成，或者已存在的文件大小大于等于服务端的文件

                    showMessage(new Message() { Duration = 0, MsgType = MessageType.Error, Msg = ex.ToString(), Showtype = Showtype.Message });
                }

            }
            catch (Exception ex)
            {

            }

        }
    }
}
