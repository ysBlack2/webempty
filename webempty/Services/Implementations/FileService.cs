using webempty.Services.Interfaces;

namespace webempty.Services.Implementations
{
	public class FileService : IFileService
	{
		public readonly IWebHostEnvironment _webHostEnvironment;



		public FileService(IWebHostEnvironment webHostEnvironment) 
		{

			_webHostEnvironment = webHostEnvironment;


		}

		public bool DeletephysicalFile(string path)
		{
			var directoryPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"+path);

			if (File.Exists(directoryPath)) {
				File.Delete(directoryPath);
				return true;
			
			}
			return false;

		}

		public async Task<string> Upload(IFormFile file,string location)
		{
		
				try {
					//READ
					var path = _webHostEnvironment.WebRootPath + location;//wwwroot + image examble:/***/***/*** ...
					var extension = Path.GetExtension(file.FileName);
					var filename =Guid.NewGuid().ToString().Replace("-", string.Empty)+ extension;
					
					//CREATE FILE IF NOT FOUND
					if(!Directory.Exists(path)) 
					{ 
						Directory.CreateDirectory(path);
					
					}
					//SAVE
					using (FileStream fileStream=File.Create(path+filename))
					{
						await file.CopyToAsync(fileStream);

						fileStream.Flush();

						return $"{location}/{filename}";


					}
				}
				catch(Exception ex)
				{
					return ex.Message +"---" +ex.InnerException; 
				}
					

		



		}
	}
}
