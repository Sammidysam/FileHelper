namespace FileHelper {
	public class TextFileReader {
		private string File;
		private string[] InitialContents;
		private int InitialLines;
		private bool HadToCreate = false;
		public TextFileReader(string File){
			this.File = File;
			if(System.IO.File.Exists(this.File)){
				InitialContents = GetContents();
				InitialLines = GetNumberOfLines();
			}
			else {
				Create();
				HadToCreate = true;
			}
		}
		public int GetNumberOfLines(){
			var Lines = 0;
			using (var StreamReader = new System.IO.StreamReader(this.File)) {
				if(StreamReader == null){
					System.Console.WriteLine(StreamReader + " failed to initialize.");
					return 0;
				}
				while(StreamReader.ReadLine() != null)
					Lines++;
			}
			return Lines;
		}
		public string[] GetContents(){
			var Contents = new string[GetNumberOfLines()];
			using (var StreamReader = new System.IO.StreamReader(this.File)) {
				if(StreamReader == null){
					System.Console.WriteLine(StreamReader + " failed to initialize.");
					return null;
				}
				for(int i = 0; i < GetNumberOfLines(); i++)
					Contents[i] = StreamReader.ReadLine();
			}
			return Contents;
		}
		public void PrintContents(){
			for(int i = 0; i < GetNumberOfLines(); i++)
				System.Console.WriteLine(GetContents()[i]);
		}
		public void PrintLocationsOf(string Value){
			for(int i = 0; i < GetNumberOfRecurrences(Value); i++)
				System.Console.WriteLine(ContainsAtWhatLine(Value)[i]);
		}
		public bool Contains(string Value){
			for(int i = 0; i < GetNumberOfLines(); i++)
				if(Value == GetContents()[i])
					return true;
			return false;
		}
		public int[] ContainsAtWhatLine(string Value){
			if(!Contains(Value))
				return null;
			else {
				var Locations = new System.Collections.Generic.List<int>(0);
				for(int i = 0; i < GetNumberOfLines(); i++)
					if(Value == GetContents()[i])
						Locations.Add(i);
				return Locations.ToArray();
			}
		}
		public int GetNumberOfRecurrences(string Value){
			var Recurrences = 0;
			for(int i = 0; i < GetNumberOfLines(); i++)
				if(Value == GetContents()[i])
					Recurrences++;
			return Recurrences;
		}
		public void WriteAtEnd(string Value){
			System.IO.File.AppendAllText(this.File, "\n" + Value);
		}
		public void WriteLinesAtEnd(string[] Lines){
			if(Lines.Length > 0)
				Lines[0] = "\n" + Lines[0];
			System.IO.File.AppendAllLines(this.File, Lines);
		}
		public void WriteAtLocation(string Value, int Location){
			var PreviousContents = GetContents();
			var Lines = GetNumberOfLines();
			System.IO.File.Delete(this.File);
			for(int i = 0; i < Lines; i++){
				if(i == Location)
					WriteAtEnd(Value);
				else
					WriteAtEnd(PreviousContents[i]);
			}
		}
		public void Create(){
			System.IO.File.Create(this.File).Close();
		}
		public void Erase(){
			Create();
		}
		public void AddEmptyLine(){
			WriteAtEnd("");
		}
		public void AddEmptyLine(int Location){
			WriteAtLocation("", Location);
		}
		public void RevertToOriginal(){
			if(!HadToCreate){
				Erase();
				for(int i = 0; i < InitialLines; i++)
					WriteAtEnd(InitialContents[i]);
			}
			if(HadToCreate)
				System.Console.WriteLine("Cannot revert a file created by the system to the original.");
		}
		public void SwitchFile(string File){
			this.File = File;
		}
	}
}
