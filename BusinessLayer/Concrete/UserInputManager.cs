using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.UnitOfWork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserInputManager : IUserInputService
    {
        private readonly IUserInputDal _userInputDal;
        private readonly IUowDal _uowDal;

        public UserInputManager(IUserInputDal userInputDal, IUowDal uowDal)
        {
            _userInputDal = userInputDal;
            _uowDal = uowDal;
        }

        public int FindLargestPrimeNumber(List<int> numbers)
        {
            int largestPrime = 0;

            foreach (int number in numbers)
            {
                if (IsPrime(number) && number > largestPrime)
                {
                    largestPrime = number;
                }
            }

            return largestPrime;
        }

        public bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void TDelete(UserInput t)
        {
            _userInputDal.Delete(t);
            _uowDal.Save();
        }

        public UserInput TGetById(int id)
        {
          return  _userInputDal.GetById(id);
        }

        public List<UserInput> TGetList()
        {
           return _userInputDal.GetList();
        }

        public void TInsert(UserInput t)
        {
            _userInputDal.Insert(t);
            _uowDal.Save();
        }

        public void TUpdate(UserInput t)
        {
            _userInputDal.Update(t);
            _uowDal.Save();
        }
    }
}
