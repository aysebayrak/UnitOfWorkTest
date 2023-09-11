using ApiLayer.Model;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.UnitOfWork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInputController : ControllerBase
    {
        private IUserInputService _userInputService;
        private IUowDal _uowDal;
        private Context _context;

        public UserInputController(IUserInputService userInputService, IUowDal uowDal, Context context)
        {
            _userInputService = userInputService;
            _uowDal = uowDal;
            _context = context;
        }

    

        [IgnoreAntiforgeryToken]
        [HttpPost("addNumbers")]
        public ActionResult AddNumbers([FromBody] List<int> model)
        {
          
            var numbers = model;
            string combinedNumbers = string.Join(" ", numbers);

            int largestPrime = _userInputService.FindLargestPrimeNumber(numbers);

            var num = new UserInput
            {
                CombinedNumbers = combinedNumbers,
                LargestPrimeNumber = largestPrime
            };

            _context.UserInputs.Add(num);
            _uowDal.Save();

            return Ok(largestPrime);
        }

        [HttpGet]
       
        public IActionResult List()
        {
            var values = _userInputService.TGetList();
            return Ok(values);

        }
    }
}



