using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ITravelService
    {
        Task<bool> SaveTravelPurpose(TravelPurpose travelPurpose);
        Task<IEnumerable<TravelPurpose>> GetTravelPurposes();
        Task<TravelPurpose> GetTravelPurposeById(int id);
        Task<bool> DeleteTravelPurposeById(int id);

        Task<bool> SaveVehicleType(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetVehicleType();
        Task<Vehicle> GetVehicleTypeById(int id);
        Task<bool> DeleteVehicleTypeById(int id);
    }
}
