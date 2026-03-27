using ServeurTracker.Api.Models;
using ServeurTracker.Api.Repositories;

namespace ServeurTracker.Api.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;

        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Device device)
        {
            device.IsOnline = true; 
            
            await _repository.AddAsync(device);
        }

        public async Task UpdateAsync(int id, Device device)
        {
            var existingDevice = await _repository.GetByIdAsync(id);
            if (existingDevice == null) throw new Exception("Device not found");

            existingDevice.Name = device.Name;
            existingDevice.IpAddress = device.IpAddress;
            existingDevice.IsOnline = device.IsOnline;

            await _repository.UpdateAsync(existingDevice);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}