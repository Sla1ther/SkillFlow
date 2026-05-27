using Microsoft.AspNetCore.Mvc;
using SkillFlow.Data;
using SkillFlow.Models;
using SkillFlow.Models.Enums;
using SkillFlow.Services.Interfaces;
using SkillFlow.ViewModels.Skills;

namespace SkillFlow.Controllers
{
    /// <summary>
    /// SkillsController is responsible for handling all HTTP requests related to skill management, including listing, creating,
    /// viewing details, editing, and deleting skills.
    /// </summary>
    public class SkillsController : Controller
    {
        #region Global
        #region Fields
        private readonly ISkillService _skillService;
        private readonly AppDbContext _context;
        #endregion
        /// <summary>
        /// Initializes a new instance of the SkillsController class with the specified skill service and database
        /// context.
        /// </summary>
        /// <param name="skillService">The service used to manage skill-related operations. Cannot be null.</param>
        /// <param name="context">The database context used for data access. Cannot be null.</param>
        public SkillsController(ISkillService skillService, AppDbContext context)
        {
            _skillService = skillService;
            _context = context;
        }

        #region Actions
        /// <summary>
        /// Displays a list of skills based on the provided filter criteria.
        /// </summary>
        /// <param name="filter">The filter criteria for skills.</param>
        /// <returns>A view displaying the filtered list of skills.</returns>
        public IActionResult Index(SkillsFilterViewModel filter)
        {
            var source = (filter.DirectionId.HasValue || filter.Level.HasValue) ? _skillService.GetFilteredSkills(filter) : _skillService.GetAllSkills();

            var skills = source.Select(s => new SkillCardViewModel
            {
                Id = s.Id,
                Title = s.Title,
                DirectionName = s.Direction?.Name ?? string.Empty,
                Level = s.Level,
                ProgressPercent = 0,
                IsCompleted = false
            }).ToList();

            ViewBag.SelectedLevel = filter.Level;
            ViewBag.SelectedDirectionId = filter.DirectionId;
            ViewBag.AvailableLevels = System.Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().ToList();
            ViewBag.Directions = _context.Directions.ToList();

            return View(skills);
        }

        /// <summary>
        /// Displays the details view for a specific skill.
        /// </summary>
        /// <param name="id">The identifier of the skill to display. If null, the method returns a NotFound result.</param>
        /// <returns>An IActionResult that renders the details view for the specified skill. Returns a NotFound result if the
        /// skill is not found or if the identifier is null.</returns>
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = _skillService.GetSkillById(id.Value);
            if (skill?.Id == 0)
            {
                return NotFound();
            }

            var viewModel = new SkillDetailsViewModel
            {
                Id = skill.Id,
                Title = skill.Title,
                Level = skill.Level,
                DirectionName = skill.Direction?.Name ?? string.Empty,
            };

            return View(viewModel);
        }

       /// <summary>
       /// Returns the view for creating a new entity, initializing view data with available directions and skill
       /// levels.
       /// </summary>
       /// <remarks>The view data includes a list of directions and available skill levels for use in the
       /// creation form. This method does not persist any data; it only prepares the form for user input.</remarks>
       /// <returns>A view that displays the creation form with populated direction and skill level options.</returns>
        public IActionResult Create()
        {
            ViewBag.Directions = _context.Directions.ToList();
            ViewBag.AvailableLevels = System.Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().ToList();
            return View();
        }

        
        /// <summary>
        /// Handles HTTP POST requests to create a new skill using the provided view model.
        /// </summary>
        /// <remarks>This action requires a valid anti-forgery token and is intended to be used with form
        /// submissions. If the model state is invalid, the method repopulates necessary view data for redisplaying the
        /// form.</remarks>
        /// <param name="model">The view model containing the data required to create a new skill. Must not be null and must contain valid
        /// values for all required properties.</param>
        /// <returns>A redirect to the Index action if the skill is created successfully; otherwise, the view for creating a
        /// skill with validation errors displayed.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                var skill = new SkillModel
                {
                    Title = model.Title,
                    Level = model.Level,
                    DirectionId = model.DirectionId
                };

                _skillService.CreateSkill(skill);
                return RedirectToAction("Index");
            }

            ViewBag.Directions = _context.Directions.ToList();
            ViewBag.AvailableLevels = System.Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().ToList();
            return View(model);
        }

        
        /// <summary>
        /// Displays the edit form for the specified skill.
        /// </summary>
        /// <remarks>The view includes available directions and skill levels for selection. Returns
        /// NotFound if the skill does not exist.</remarks>
        /// <param name="id">The identifier of the skill to edit. If null, the method returns a NotFound result.</param>
        /// <returns>An IActionResult that renders the edit view for the skill if found; otherwise, a NotFound result.</returns>
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = _skillService.GetSkillById(id.Value);
            if (skill?.Id == 0)
            {
                return NotFound();
            }

            var model = new EditSkillViewModel
            {
                Id = skill.Id,
                Title = skill.Title,
                Level = skill.Level,
                DirectionId = skill.DirectionId
            };

            ViewBag.Directions = _context.Directions.ToList();
            ViewBag.AvailableLevels = System.Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().ToList();
            return View(model);
        }

        /// <summary>
        /// Handles the HTTP POST request to update an existing skill with the specified values.
        /// </summary>
        /// <remarks>This action requires a valid anti-forgery token. If the specified id does not match
        /// the model's Id, a NotFound result is returned. Model validation is performed before updating the
        /// skill.</remarks>
        /// <param name="id">The identifier of the skill to update. Must match the Id property of the provided model.</param>
        /// <param name="model">The view model containing the updated values for the skill. Must not be null.</param>
        /// <returns>A redirect to the details view of the updated skill if the update is successful; otherwise, the edit view
        /// with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditSkillViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var skill = new SkillModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Level = model.Level,
                    DirectionId = model.DirectionId
                };

                _skillService.UpdateSkill(skill);
                return RedirectToAction("Details", new { id = skill.Id });
            }

            ViewBag.Directions = _context.Directions.ToList();
            ViewBag.AvailableLevels = System.Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().ToList();
            return View(model);
        }

        /// <summary>
        /// Displays the confirmation view for deleting a skill with the specified identifier.
        /// </summary>
        /// <remarks>The view includes information about whether the skill is associated with any users.
        /// This method does not perform the actual deletion; it presents a confirmation page.</remarks>
        /// <param name="id">The identifier of the skill to delete. If null, the method returns a NotFound result.</param>
        /// <returns>A view displaying the details of the skill to be deleted, or a NotFound result if the skill does not exist.</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = _skillService.GetSkillById(id.Value);
            if (skill?.Id == 0)
            {
                return NotFound();
            }

            var hasUserSkills = _context.UserSkills.Any(us => us.SkillId == id);

            var viewModel = new SkillDetailsViewModel
            {
                Id = skill.Id,
                Title = skill.Title,
                Level = skill.Level,
                DirectionName = skill.Direction?.Name ?? string.Empty,
            };

            ViewBag.HasUserSkills = hasUserSkills;
            return View(viewModel);
        }

        /// <summary>
        /// Handles the HTTP POST request to delete the specified skill and redirects to the index view.
        /// </summary>
        /// <remarks>This action is invoked as part of the delete confirmation workflow. It requires a
        /// valid anti-forgery token and is typically called from a form submission.</remarks>
        /// <param name="id">The identifier of the skill to delete.</param>
        /// <returns>A redirect to the index action after the skill is deleted.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _skillService.DeleteSkill(id);
            return RedirectToAction("Index");
        }
        #endregion
        #endregion
    }
}
