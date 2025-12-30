using System.Security.Claims;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Presentation.PresentationConstants;
using IronWorkoutTracker.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IronWorkout.Shared.EnvironmentStateModels;
namespace IronWorkoutTracker.Presentation.Controllers
{
    public class WorkoutProgramController : Controller
    {
        private readonly IWorkoutProgramRepository _repo;
        private readonly CurrentUser _currentUser;
        private readonly IUserProgramRepository _userProgramRepo;
        private readonly IProgramDayRepository _programDayRepo;
        private readonly IWorkoutDayRepository _workoutDayRepo;
        public WorkoutProgramController(IWorkoutProgramRepository repo, IUserProgramRepository userProgramRepo, CurrentUser currentUser, IProgramDayRepository programDayRepo, IWorkoutDayRepository workoutDayRepo)
        {
            _repo = repo;
            _currentUser = currentUser;
            _userProgramRepo = userProgramRepo;
            _programDayRepo = programDayRepo;
            _workoutDayRepo = workoutDayRepo;
        }

        // GET: /WorkoutProgram
        public async Task<IActionResult> Index()
        {
            var programs = await _repo.GetAllAsync();
            return View(programs);
        }

        public async Task<IActionResult> Details(int id)
        {   
            var program = await _repo.GetByIdAsync(id);
            if (program == null)
                return NotFound();

            var days = await _programDayRepo.GetByProgramIdAsync(id);

            UserProgram up = null;

            var userId = _currentUser.UserId;
            int userIdInt = 0;

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out userIdInt))
            {
                up = await _userProgramRepo.GetByUserAndProgramAsync(userIdInt, id);
            }


            var vm = new WorkoutProgramDetailsViewModel
            {
                Program = program,
                Days = days,
                UserProgram = up
            };
            return View(vm);
        }



        // GET: /WorkoutProgram/Create
        public IActionResult Create()
        {
            return View();
        }
    
        // POST: /WorkoutProgram/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutProgram model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int? parsedUserId = null;
            if (!string.IsNullOrEmpty(_currentUser?.UserId) && int.TryParse(_currentUser.UserId, out var userId))
            {   
                parsedUserId = userId;
                model.CreatedById = parsedUserId;
            }
            else
            {
                model.CreatedById = null;
            }

            model.CreatedDate = DateTime.UtcNow;
            await _repo.AddAsync(model);

            // Attach User with Program by creating UserProgram
            if (parsedUserId.HasValue)
            {
                var userProgram = new UserProgram
                {
                    UserId = parsedUserId.Value,
                    WorkoutProgramId = model.WorkoutProgramId   // or WorkoutProgramId if that's the FK name
                };

                await _userProgramRepo.AddAsync(userProgram);
            }

            return RedirectToAction(
                        actionName: "Details",
                        controllerName: "WorkoutProgram",
                        routeValues: new { id = model.WorkoutProgramId });
        }

        // GET: /WorkoutProgram/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var program = await _repo.GetByIdAsync(id);
            if (program == null)
                return NotFound();

            return View(program);
        }

        // POST: /WorkoutProgram/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkoutProgram model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = await _repo.GetByIdAsync(model.WorkoutProgramId);
            if (existing == null)
                return NotFound();

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Visibility = model.Visibility;

            await _repo.UpdateAsync(existing);

            return RedirectToAction(
                actionName: "Details",
                controllerName: "WorkoutProgram",
                routeValues: new { id = model.WorkoutProgramId });
        }

        // POST: /WorkoutProgram/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adopt(int id)
        {
            int userId = 0;
            if (int.TryParse(_currentUser.UserId, out userId))
            {
                var userProgram = new UserProgram
                {
                    UserId = userId,
                    WorkoutProgramId = id
                };

                await _userProgramRepo.AddAsync(userProgram);
            }
            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unadopt(int id)
        {
            int userId;
            if (int.TryParse(_currentUser.UserId, out userId))
            {
                await _userProgramRepo.DeleteByUserAndProgramAsync(userId, id);
            }

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home");
        }

        // Start/Finish UserProgram
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Start(int id) // UserProgramId
        {
            var userId = _currentUser.UserId;
            int userIdInt = 0;
            UserProgram up = null;

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out userIdInt))
            {
                up = await _userProgramRepo.GetByIdAsync(id);
                if (up == null || up.UserId != userIdInt) return Forbid();
            }

            up.Status = ProgramStatus.InProgress;
            await _userProgramRepo.UpdateAsync(up);

            // Copy all ProgramDays to WorkoutDays
            var programDays = await _programDayRepo.GetByProgramIdAsync(up.WorkoutProgramId);

            foreach (var programDay in programDays)
            {
                var workoutDay = new WorkoutDay
                {
                    UserId = userIdInt,
                    ProgramDayId = programDay.ProgramDayId,
                    WorkoutProgramId = up.WorkoutProgramId,
                    Name = programDay.Title,
                    Order = programDay?.Order ?? 0,
                    CreatedDate = DateTime.UtcNow,
                    Exercises = new List<WorkoutDayExercise>()
                };

                // Copy exercises
                foreach (var programDayExercise in programDay.Exercises)
                {
                    var workoutDayExercise = new WorkoutDayExercise
                    {
                        ProgramDayExerciseId = programDayExercise.ProgramDayExerciseId,
                        ExerciseId = programDayExercise.ExerciseId,
                        ExerciseName = programDayExercise.Exercise.Name,
                        Sets = new List<WorkoutDayExerciseSet>(),

                        WorkoutDay = workoutDay
                    };

                    workoutDay.Exercises.Add(workoutDayExercise);
                }

                await _workoutDayRepo.AddAsync(workoutDay);
            }


            return RedirectToAction("Index", "Home", new { filter = "workout" });
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int id) // UserProgramId
        {
            var userId = _currentUser.UserId;
            int userIdInt = 0;
            UserProgram up = null;

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out userIdInt))
            {
                up = await _userProgramRepo.GetByIdAsync(id);
                if (up == null || up.UserId != userIdInt) return Forbid();
            }

            up.Status = ProgramStatus.Finished;
            await _userProgramRepo.UpdateAsync(up);

            return RedirectToAction("Index", "Home", new { filter = "myprograms" });
        }
    }
}
