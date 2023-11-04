namespace CRUD_Library.Helpers
{    
        public static class FileUploadHelper
        {
            static public async Task<string> UploadAsync(IFormFile fromFile)
            {
                if (fromFile != null)
                {
                    //Path.GetExtension(Image.FileName);
                    //Console.WriteLine(Image.Length/1024);
                    var filename = $"{Guid.NewGuid()}{Path.GetExtension(fromFile.FileName)}";
                    using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
                    await fromFile.CopyToAsync(fs);
                    return @$"/uploads/{filename}";
                }

                throw new Exception("File was not upload");
            }
        }    
}
