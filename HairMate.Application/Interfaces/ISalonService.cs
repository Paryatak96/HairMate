using HairMate.Application.ViewModels.Salon;
using HairMate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Application.Interfaces
{
    public interface ISalonService
    {
        ListSalonForListVm GetAllSalonsAsync(int pageSize, int pageNo, string searchString);
        Task<Salon> GetSalonByIdAsync(int salonId);
        Task<Salon> CreateSalonAsync(Salon salon);
        Task<bool> UpdateSalonAsync(Salon salon);
        Task<bool> DeleteSalonAsync(int salonId);
        Task<IEnumerable<Salon>> GetSalonsByProvinceAsync(string province);
        Task<IEnumerable<Salon>> GetSalonsByTypeAsync(string type);
        Task<IEnumerable<Salon>> SearchSalonsByNameAsync(string name);
        Task<bool> ManageEmployeesAsync(int salonId, IEnumerable<Employee> employees);
        Task<bool> ManageServicesAsync(int salonId, IEnumerable<Service> services);
        Task<bool> AddReviewAsync(int salonId, Review review);
        Task<bool> RespondToReviewAsync(int reviewId, string response);
    }
}
