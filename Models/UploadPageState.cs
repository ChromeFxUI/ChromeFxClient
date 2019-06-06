using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UploadPageState:IState, IMessage
    {
        public List<FileState> Files { get; set; }
        public List<FileState> ProjectFiles { get; set; }
        public Message Message { get; set; } = new Message();
    }

    public class FileState
    {
        public string Engine { get; set; }
        public string Des { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
        public string OriginalFileFullPath { get; set; }
        public string ZipFileFullPath { get; set; }
        public bool IsZiped { get; set; }
        public bool IsUploading { get; set; }
        public bool IsUploadSuccess { get; set; }
        public string Msg { get; set; }
    }
}
