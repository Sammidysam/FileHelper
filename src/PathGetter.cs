namespace FileHelper {
	public class PathGetter {
		public static string GetPathForFile(string File){
			var Path = typeof(PathGetter).Assembly.Location.ToString();
			Path = Path.Substring(0, Path.Length - "FileHelper.dll".Length);
			return System.IO.Path.Combine(Path, File);
		}
		public static string GetDirectoryPath(){
			var Path = typeof(PathGetter).Assembly.Location.ToString();
			return Path.Substring(0, Path.Length - "FileHelper.dll".Length);
		}
	}
}