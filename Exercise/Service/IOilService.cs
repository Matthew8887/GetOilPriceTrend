using Exercise.Model;

namespace Exercise.Service
{
    public interface IOilService
    {
        Task<T> GetDatasource<T>(string url);
        List<OilPrice> GetByDate(DateTime startdate, DateTime enddate);
    }
}
