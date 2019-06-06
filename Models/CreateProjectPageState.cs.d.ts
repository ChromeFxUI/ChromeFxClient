declare module Server {
	interface CreateProjectPageState {
		Models: Server.ModelProcess[];
		Message: {
			MsgType: any;
			Msg: string;
			Duration: number;
			Showtype: any;
		};
	}
	interface ModelProcess {
		IsTemplateProject: boolean;
		Percent: number;
		StepName: string;
		IsStart: boolean;
		IsCompleted: boolean;
		Name: string;
		Version: string;
		Engine: string;
		OutputPath: string;
		DownloadPath: string;
		Id: string;
		Des: string;
	}
}
