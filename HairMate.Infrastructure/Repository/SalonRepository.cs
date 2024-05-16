using HairMate.Domain.Interface;
using HairMate.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Infrastructure.Repository
{
    public class SalonRepository : ISalonRepository
    {
        private readonly Context _context;

        public SalonRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> AddReviewAsync(int salonId, Review review)
        {
            var salon = await _context.Salons.Include(s => s.Reviews).FirstOrDefaultAsync(s => s.SalonId == salonId);
            if (salon == null)
            {
                return false;
            }

            salon.Reviews.Add(review);
            _context.Salons.Update(salon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Salon> CreateSalonAsync(Salon salon)
        {
            _context.Salons.Add(salon);
            await _context.SaveChangesAsync();
            return salon;
        }

        public async Task<bool> DeleteSalonAsync(int salonId)
        {
            var salon = await _context.Salons.FindAsync(salonId);
            if (salon == null)
            {
                return false;
            }

            _context.Salons.Remove(salon);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Salon> GetAllSalons()
        {
            return _context.Salons;
        }

        public async Task<Salon> GetSalonByIdAsync(int salonId)
        {
            return await _context.Salons.FindAsync(salonId);
        }

        public async Task<IEnumerable<Salon>> GetSalonsByProvinceAsync(string province)
        {
            return await _context.Salons.Where(s => s.Province == province).ToListAsync();
        }

        public async Task<IEnumerable<Salon>> GetSalonsByTypeAsync(string type)
        {
            return await _context.Salons.Where(s => s.Type == type).ToListAsync();
        }

        public async Task<bool> ManageEmployeesAsync(int salonId, IEnumerable<Employee> employees)
        {
            var salon = await _context.Salons.Include(s => s.Employees).FirstOrDefaultAsync(s => s.SalonId == salonId);
            if (salon == null)
            {
                return false;
            }

            salon.Employees = employees.ToList();
            _context.Salons.Update(salon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ManageServicesAsync(int salonId, IEnumerable<Service> services)
        {
            var salon = await _context.Salons.Include(s => s.Services).FirstOrDefaultAsync(s => s.SalonId == salonId);
            if (salon == null)
            {
                return false;
            }

            salon.Services = services.ToList();
            _context.Salons.Update(salon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RespondToReviewAsync(int reviewId, string response)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            review.Response = response;
            _context.Reviews.Update(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Salon>> SearchSalonsByNameAsync(string name)
        {
            return await _context.Salons.Where(s => s.Name.Contains(name)).ToListAsync();
        }

        public async Task<bool> UpdateSalonAsync(Salon salon)
        {
            _context.Salons.Update(salon);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
