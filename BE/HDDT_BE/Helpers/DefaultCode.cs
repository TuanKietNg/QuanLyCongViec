namespace HDDT_BE.Helpers;

public static class DefaultCode
{
    public const int SUCCESS = 0; // Thành công 
    
    // TỪ 10 - 19 MÃ CODE THẤT BẠI CỦA THAO TÁC
    public const int CREATE_FAILURE = 10; // THÊM THẤT BẠI
    public const int UPDATE_FAILURE = 11; // CẬP NHẬT THẤP BẠI
    public const int DELETE_FAILURE = 12; // XÓA THẤT BẠI
        
    
    public const int  BEYOND_TIME= 1; // Vượt quá thời gian 
    
    // 20 - 29 MÃ CODE CỦA VỀ DỮ LIỆU
    public const int EXCEPTION = 20; //  lỗi exception 
    public const int DATA_EXISTED = 21; //   DỮ LIỆU ĐÃ TỒN TẠI TRONG HỆ THỐNG
    public const int DATA_NOT_FOUND = 22; //  DỮ LIỆU KHÔNG TỒN TẠI TRONG HỆ THỐNG
    public const int COMMON_NOT_FOUND = 23; // thông tin không đúng về ràng buộc dữ liệu hoặc danh mục!
    public const int ERROR_STRUCTURE = 24; // Cấu trúc gói tin gửi không đúng quy định

}