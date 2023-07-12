using Microsoft.AspNetCore.Mvc;

namespace HrProject.Controllers
{
    public class VacationsController : Controller
    {
        IVacationsRepository vac;

        public VacationsController(IVacationsRepository vac)
        {
            this.vac = vac;
        }


        public IActionResult Create()
        {
            var model = new VacationsViewModel
            {
                offvac = vac.GetAll()

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(VacationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingVacation = vac.GetAll().FirstOrDefault(v => v.Date == model.Date);

                if (existingVacation != null)
                {

                    ModelState.AddModelError("Date", "this date already exists ");
                }
                else
                {
                    // Create a new vacation
                    var newVacation = new VacationsViewModel
                    {
                        Name = model.Name,
                        Date = model.Date
                    };
                    vac.Create(newVacation);
                    vac.Save();

                    return RedirectToAction("Create");
                }
            }


            model.offvac = vac.GetAll();
            return View("Create", model);
        }
        public IActionResult Delete(int id)
        {
            vac.Delete(id);
            vac.Save();
            return RedirectToAction("Create");
        }

        public IActionResult Edit(int id)
        {
            Vacations editvac = vac.GetById(id);

            var Vacationvm = new VacationsViewModel()
            {
                Date = editvac.Date,
                Name = editvac.Name
            };
            return View(Vacationvm);
        }

        [HttpPost]

        public IActionResult Edit([Bind("Id,Name,Day,Date")] VacationsViewModel evac, int id)
        {

            if (ModelState.IsValid)
            {
                var existingVacation = vac.GetAll().FirstOrDefault(v => v.Date == evac.Date);

                if (existingVacation != null)
                {
                    // Vacation with the same date already exists, return an error message
                    ModelState.AddModelError("Date", "date already exists");
                }
                else
                {
                    vac.update(id, evac);
                    vac.Save();
                    return RedirectToAction("Create");

                }
            }


            return View(evac);

        }

        public IActionResult Details(int id)
        {
            Vacations vo = vac.GetById(id);
            return View(vo);
        }

    }
}
