using HDDT_BE.Models.Core;

namespace HDDT_BE.Models.Professional;
public class VanDongVienModel : Audit , TEntity<string>
{
    public string HoVaTen { get; set; }
}