namespace FileHelper {
	public class PathExplorer {
		private string Path;
		private int FileStart;
		private int FolderStart;
		public PathExplorer(){
			Path = PathGetter.GetDirectoryPath();
		}
		public PathExplorer(string Path){
			this.Path = Path;
		}
		public string GetPath(){
			return Path;
		}
		public void Back(){
			for(int i = 0; i < 2; i++){
				var Index = Path.LastIndexOf('\\');
				Path = Path.Substring(0, Index);
			}
			Path += '\\';
		}
		public string[] GetItemsInDirectory(){
			var Files = System.IO.Directory.GetFiles(Path);
			var Subdirectories = System.IO.Directory.GetDirectories(Path);
			var Combined = new string[Files.Length + Subdirectories.Length];
			FileStart = 0;
			FolderStart = Files.Length;
			for(int i = 0; i < Files.Length; i++)
				Combined[i] = Files[i];
			for(int i = Files.Length; i < Combined.Length; i++)
				Combined[i] = Subdirectories[i - Files.Length];
			return Combined;
		}
		public string[] GetItemsInDirectory(string FileType){
			return System.IO.Directory.GetFiles(Path, FileType);
		}
		public int GetNumberOfItemsInDirectory(){
			return GetItemsInDirectory().Length;
		}
		public int GetNumberOfItemsInDirectory(string FileType){
			return GetItemsInDirectory(FileType).Length;
		}
		public void PrintItemsInDirectory(){
			for(int i = 0; i < GetNumberOfItemsInDirectory(); i++){
				if(i == FileStart)
					System.Console.WriteLine("Files:");
				if(i == FolderStart)
					System.Console.WriteLine("Folders:");
				System.Console.WriteLine(GetItemsInDirectory()[i]);
			}
		}
		public void PrintItemsInDirectory(string FileType){
			System.Console.WriteLine(FileType + " Files:");
			for(int i = 0; i < GetNumberOfItemsInDirectory(FileType); i++)
				System.Console.WriteLine(GetItemsInDirectory(FileType)[i]);
		}
		public bool IsInDirectory(string FileName){
			FileName = Path + FileName;
			for(int i = 0; i < GetNumberOfItemsInDirectory(); i++)
				if(FileName.Equals(GetItemsInDirectory()[i]))
					return true;
			return false;
		}
		public void SwitchDirectory(string Path){
			this.Path = Path;
		}
		public void SwitchToDirectory(string Directory){
			if(IsInDirectory(Directory))
				Path += Directory;
			else
				System.Console.WriteLine(Directory + " not in current directory.");
		}
		public void CreateNewDirectory(string Directory){
			Directory = Path + Directory;
			System.IO.Directory.CreateDirectory(Directory);
		}
		public void Delete(string Location, bool IsDirectory){
			Location = Path + Location;
			if(IsDirectory)
				System.IO.Directory.Delete(Location);
			else
				System.IO.File.Delete(Location);
		}
	}
}
