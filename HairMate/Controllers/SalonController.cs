using HairMate.Application.Interfaces;
using HairMate.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace HairMate.Controllers
{
    public class SalonController : Controller
    {
        private readonly ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }

        // GET: Salon
        [HttpGet]
        public IActionResult Index()
        {
            var salons = _salonService.GetAllSalonsAsync(5, 1, "");
            return View(salons);
        }

        [HttpPost]
        public async Task <IActionResult> Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _salonService.GetAllSalonsAsync(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        // GET: Salon/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var salon = await _salonService.GetSalonByIdAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // GET: Salon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                await _salonService.CreateSalonAsync(salon);
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salon/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var salon = await _salonService.GetSalonByIdAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // POST: Salon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Salon salon)
        {
            if (id != salon.SalonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _salonService.UpdateSalonAsync(salon);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salon/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var salon = await _salonService.GetSalonByIdAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // POST: Salon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _salonService.DeleteSalonAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
