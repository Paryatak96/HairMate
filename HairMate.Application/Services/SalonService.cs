﻿using AutoMapper;
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

        public SalonService(ISalonRepository salonRepository, IMapper mapper)
        {
            _salonRepository = salonRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<Appointment>> GetAppointmentsBySalonIdAsync(int salonId)
        {
            var appointments = await _salonRepository.GetAppointmentsBySalonIdAsync(salonId);
            return appointments.Where(a => a.Status == "Available").ToList();
        }

        public async Task GenerateDailyAppointments(int salonId, DateTime date)
        {
            var existingAppointments = await _salonRepository.GetAppointmentsByDateAsync(salonId, date);
            if (existingAppointments.Any(a => a.Status != "Booked")) return;

            var appointments = new List<Appointment>();
            var startTime = new TimeSpan(9, 0, 0); // Start at 9:00 AM
            var endTime = new TimeSpan(17, 0, 0); // End at 5:00 PM
            var duration = new TimeSpan(1, 0, 0); // 1-hour slots

            for (var time = startTime; time < endTime; time += duration)
            {
                if (time == new TimeSpan(12, 0, 0)) // Skip lunch break from 12:00 PM to 1:00 PM
                {
                    continue;
                }

                appointments.Add(new Appointment
                {
                    SalonId = salonId,
                    Date = date,
                    Time = time,
                    Status = "Available"
                });
            }

            await _salonRepository.AddAppointmentsAsync(appointments);
        }
                public async Task<bool> BookAppointmentAsync(int appointmentId)
        {
            return await _salonRepository.BookAppointmentAsync(appointmentId);
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _salonRepository.GetAppointmentByIdAsync(appointmentId);
        }
    }
}
