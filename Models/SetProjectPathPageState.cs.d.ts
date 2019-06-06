declare module Server {
	interface SetProjectPathPageState {
		OutputPath: string;
		IsLegal: boolean;
		Message: {
			MsgType: any;
			Msg: string;
			Duration: number;
			Showtype: any;
		};
	}
}
