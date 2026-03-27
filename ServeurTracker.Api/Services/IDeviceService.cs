using ServeurTracker.Api.Models;

namespace ServeurTracker.Api.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
        Task AddAsync(Device device);
        Task UpdateAsync(int id, Device device);
        Task DeleteAsync(int id);
    }
}