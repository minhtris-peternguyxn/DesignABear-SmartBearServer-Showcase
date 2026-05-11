namespace SmartBearServer.Model
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
