using CNMSDataAPI.Services;

namespace CNMSDataAPI.Services
{
    public class CNMSService : ICNMSService
    {
        private readonly DatabaseService _databaseService;

        public CNMSService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Dictionary<string, object>>> GetCNMSDataAsync(string bizSrc)
        {
            return await _databaseService.GetCNMSDataAsync(bizSrc);
        }
    }
}