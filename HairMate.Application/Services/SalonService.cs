using AutoMapper;
using AutoMapper.QueryableExtensions;
using HairMate.Application.Interfaces;
using HairMate.Application.ViewModels.Salon;
using HairMate.Domain.Interface;
using HairMate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Application.Services
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;
        private readonly IMapper _mapper;

        public SalonService(ISalonRepository salonRepository)
        {
            _salonRepository = salonRepository;
        }

        public ListSalonForListVm GetAllSalonsAsync(int pageSize, int pageNo, string searchString)
        {
            var salon = _salonRepository.GetAllSalons()
                .ProjectTo<SalonForListVm>(_mapper.ConfigurationProvider).ToList();

            var salonToShow = salon.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            var salonList = new ListSalonForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                SearchString = searchString,
                Salons = salonToShow,
                Count = salon.Count
            };
            return salonList;
        }

        public async Task<Salon> GetSalonByIdAsync(int salonId)
        {
            return await _salonRepository.GetSalonByIdAsync(salonId);
        }

        public async Task<Salon> CreateSalonAsync(Salon salon)
        {
            return await _salonRepository.CreateSalonAsync(salon);
        }

        public async Task<bool> UpdateSalonAsync(Salon salon)
        {
            return await _salonRepository.UpdateSalonAsync(salon);
        }

        public async Task<bool> DeleteSalonAsync(int salonId)
        {
            return await _salonRepository.DeleteSalonAsync(salonId);
        }

        public async Task<IEnumerable<Salon>> GetSalonsByProvinceAsync(string province)
        {
            return await _salonRepository.GetSalonsByProvinceAsync(province);
        }

        public async Task<IEnumerable<Salon>> GetSalonsByTypeAsync(string type)
        {
            return await _salonRepository.GetSalonsByTypeAsync(type);
        }

        public async Task<IEnumerable<Salon>> SearchSalonsByNameAsync(string name)
        {
            return await _salonRepository.SearchSalonsByNameAsync(name);
        }

        public async Task<bool> ManageEmployeesAsync(int salonId, IEnumerable<Employee> employees)
        {
            return await _salonRepository.ManageEmployeesAsync(salonId, employees);
        }

        public async Task<bool> ManageServicesAsync(int salonId, IEnumerable<Service> services)
        {
            return await _salonRepository.ManageServicesAsync(salonId, services);
        }

        public async Task<bool> AddReviewAsync(int salonId, Review review)
        {
            return await _salonRepository.AddReviewAsync(salonId, review);
        }

        public async Task<bool> RespondToReviewAsync(int reviewId, string response)
        {
            return await _salonRepository.RespondToReviewAsync(reviewId, response);
        }
    }
}
