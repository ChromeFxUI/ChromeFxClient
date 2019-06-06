declare module Server {
	interface PluginPageState {
		Engins: Server.Engin[];
		UE4Versions: Server.Version[];
		UnityVersions: Server.Version[];
		FuncModels: Server.FuncModel[];
		Projects: Server.Project[];
		Message: {
			MsgType: any;
			Msg: string;
			Duration: number;
			Showtype: any;
		};
	}
	interface ProjectCreateMoudle {
		Project: Server.Project;
		FuncModels: Server.FuncModel[];
		ProjectName: string;
		Des: string;
	}
	interface Engin {
		Name: string;
		Id: string;
	}
	interface Version {
		Name: string;
		Id: string;
	}
	interface FuncModel {
		Name: string;
		Des: string;
		Version: string;
		Id: string;
		RelativePath: string;
		Versions: Server.Version[];
		Engine: string;
	}
	interface Project {
		Name: string;
		Des: string;
		Version: string;
		Id: string;
		RelativePath: string;
		Versions: Server.Version[];
		Engine: string;
	}
}
