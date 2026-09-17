using HrmsCoreMvc.Repositories.Terminations;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Termination
{
    public class TerminationController : Controller
    {
        private readonly ITerminationRepository _terminationRepository;
        public TerminationController(ITerminationRepository terminationRepository)
        {
            _terminationRepository = terminationRepository;
        }
        public async Task<IActionResult> Index()
        {
            var termination = await _terminationRepository.GetTerminationsAsync();
            return View(termination);
        }
    }
}
