using HairMate.Application.Interfaces;
using HairMate.Application.ViewModels.Salon;
using HairMate.Domain.Interface;
using HairMate.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace HairMate.Controllers
{
    public class SalonController : Controller
    {
        private readonly ISalonService _salonService;
        private readonly ISalonRepository _salonRepository;

        public SalonController(ISalonService salonService, ISalonRepository salonRepository)
        {
            _salonService = salonService;
            _salonRepository = salonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10, string searchString = "")
        {
            var model = _salonService.GetAllSalonsAsync(pageSize, pageNo, searchString);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new NewSalonVm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewSalonVm model)
        {
            if (true)
            {
                if (model.SalonLogo != null && model.SalonLogo.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/logos", model.SalonLogo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.SalonLogo.CopyToAsync(stream);
                    }
                    model.LogoUrl = $"/logos/{model.SalonLogo.FileName}";
                }

                var salon = new Salon
                {
                    Name = model.Name,
                    LogoUrl = model.LogoUrl,
                    Description = model.Description,
                    Type = model.Type,
                    Province = model.Province,
                    City = model.City,
                    Street = model.Street,
                    PostalCode = model.PostalCode,
                    PaymentType = model.PaymentType,
                    Services = model.Services,
                    Employees = model.Employees,
                    Reviews = model.Reviews
                };

                await _salonService.CreateSalonAsync(salon);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var salon = await _salonService.GetSalonByIdAsync(id);
            if (salon == null)
            {
                return NotFound();
            }

            var today = DateTime.Today;
            await _salonService.GenerateDailyAppointments(id, today);
            var appointments = await _salonService.GetAppointmentsBySalonIdAsync(id);

            var viewModel = new SalonDetailsVm
            {
                Salon = salon,
                Appointments = appointments
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Book(int appointmentId)
        {
            var success = await _salonRepository.BookAppointmentAsync(appointmentId);
            if (!success)
            {
                // Handle the error, e.g., appointment was already booked
                // Można dodać odpowiedni komunikat dla użytkownika
            }

            var appointment = await _salonService.GetAppointmentByIdAsync(appointmentId);
            return RedirectToAction("Details", new { id = appointment.SalonId });
        }
    }
}
