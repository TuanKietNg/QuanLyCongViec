namespace HDDT_BE.Models.Core
{
    public class FileModel : Audit, TEntity<string>
    {
        public string FileName { get; set; }
        public string SaveName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public string Ext { get; set; }
    }

    public class FileShort
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        
        public  string Ext { get; set;  }
        public string Path { get; set; }
    }
}