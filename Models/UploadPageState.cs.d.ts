declare module Server {
	interface UploadPageState {
		Files: Server.FileState[];
		ProjectFiles: Server.FileState[];
		Message: {
			MsgType: any;
			Msg: string;
			Duration: number;
			Showtype: any;
		};
	}
	interface FileState {
		Engine: string;
		Des: string;
		Version: string;
		FileName: string;
		OriginalFileFullPath: string;
		ZipFileFullPath: string;
		IsZiped: boolean;
		IsUploading: boolean;
		IsUploadSuccess: boolean;
		Msg: string;
	}
}
