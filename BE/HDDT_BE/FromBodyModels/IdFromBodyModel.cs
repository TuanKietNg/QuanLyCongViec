
namespace CoreApi.FromBodyModels
{
    public class IdFromBodyModel
    {
        public string Id { get; set; }
    }
    
    public class IdFromBodyDaoTaoModel : IdFromBodyModel
    {
        public string Code { get; set; }
    }
    
}