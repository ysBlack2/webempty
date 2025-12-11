namespace webempty.Services.Interfaces
{
	public interface IFileService
	{
		public Task<string> Upload(IFormFile file,string location);

		public Boolean DeletephysicalFile(string path);

	}
}
