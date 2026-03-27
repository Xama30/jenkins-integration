using Moq;
using ServeurTracker.Api.Models;
using ServeurTracker.Api.Repositories;
using ServeurTracker.Api.Services;

namespace ServeurTracker.Tests
{
    public class DeviceServiceTests
    {
        [Fact] // Indique à xUnit que c'est une méthode de test
        public async Task AddAsync_ShouldForceDeviceToBeOnline()
        {
            // 1. ARRANGE (Préparation)
            // On crée un faux Repository (un Mock)
            var mockRepository = new Mock<IDeviceRepository>();
            
            // On instancie notre vrai Service, mais on lui passe le faux Repository
            var deviceService = new DeviceService(mockRepository.Object);
            
            // On crée un appareil de test avec IsOnline = false
            var testDevice = new Device 
            { 
                Id = 1, 
                Name = "Serveur Test", 
                IpAddress = "127.0.0.1", 
                IsOnline = false // La valeur qu'on veut voir changer !
            };

            // 2. ACT (Exécution)
            // On appelle la méthode de notre service
            await deviceService.AddAsync(testDevice);

            // 3. ASSERT (Vérification)
            // On vérifie que notre logique d'affaires a bien fonctionné
            Assert.True(testDevice.IsOnline);
            
            // On vérifie que le service a bien appelé la méthode AddAsync du (faux) repository exactement une fois
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Device>()), Times.Once);
        }
    }
}