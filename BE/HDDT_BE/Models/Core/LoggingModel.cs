using HDDT_BE.Constants;
using HDDT_BE.Interfaces.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core;

public class LoggingModel : Audit, TEntity<string>
{
    
    public string Content { get; set; }
    
    public string Action { get; set; }
    
    public string CaseName { get; set; }
    
    public int? CreatedAtString
    {
        get { return CreatedAt.HasValue ? FormatDate.ConvertDatetimeQG(CreatedAt) : null; }
        set {}
    }

    public LoggingModel(string content, string action , string casename)
    {
        this.Content = content;
        this.Action = action;
        this.CaseName = casename;
    }
}




