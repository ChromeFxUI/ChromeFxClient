using Actions.UploadPageActions;
using Chromium.WebBrowser;
using Models;
using Newtonsoft.Json;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalExtensions
{
    public static class UploadPageExtensions
    {
        public static void UseUploadPage(this JSObject chromfx, Package<Store> store)
        {
            var uploadPageActions = chromfx.AddObject("uploadPageActions");

            uploadPageActions.AddFunction("uploadFolder").Execute += (func, args) =>
            {
                var fileStr = args.Arguments[0].StringValue;
                var file = JsonConvert.DeserializeObject<FileState>(fileStr);
                var doZipAction = store.asyncAction<FileState, bool>(async (dispatcher, store2, fileState) =>
                {
                    try
                    {
                        dispatcher(new DoZip() { File = fileState });
                        var curFile = store.GetState().UploadPageState.Files.FirstOrDefault(p => p.OriginalFileFullPath == fileState.OriginalFileFullPath && p.IsZiped);
                        return curFile != null;
                    }
                    catch(Exception ex)
                    {
                        return false;
                    }
                    
                });

                var doZipRes = store.Dispatch(doZipAction(file)).Result;

                if (doZipRes)
                {
                    store.Dispatch(new StartUpload() { File = file });
                    store.Dispatch(new DoUpload() { File = file });
                }

            };

            uploadPageActions.AddFunction("uploadProjectFolder").Execute += (func, args) =>
            {
                var fileStr = args.Arguments[0].StringValue;
                var file = JsonConvert.DeserializeObject<FileState>(fileStr);
                var doZipAction = store.asyncAction<FileState, bool>(async (dispatcher, store2, fileState) =>
                {
                    try
                    {
                        dispatcher(new DoProjectZip() { File = fileState });
                        var curFile = store.GetState().UploadPageState.ProjectFiles.FirstOrDefault(p => p.OriginalFileFullPath == fileState.OriginalFileFullPath && p.IsZiped);
                        return curFile != null;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                });

                var doZipRes = store.Dispatch(doZipAction(file)).Result;

                if (doZipRes)
                {
                    store.Dispatch(new StartProjectUpload() { File = file });
                    store.Dispatch(new DoProjectUpload() { File = file });
                }

            };

            uploadPageActions.AddFunction("selectFolder").Execute += (func, args) =>
            {
                store.Dispatch(new SelectFolder());
            };

            uploadPageActions.AddFunction("selectProjectFolder").Execute += (func, args) =>
            {
                store.Dispatch(new SelectProjectFolder());
            };
        }
    }
}
