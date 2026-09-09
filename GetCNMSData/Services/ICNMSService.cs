namespace CNMSDataAPI.Services
{
    public interface ICNMSService
    {
        Task<List<Dictionary<string, object>>> GetCNMSDataAsync(string bizSrc);
    }
}