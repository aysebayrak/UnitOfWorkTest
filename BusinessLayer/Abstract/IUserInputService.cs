using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserInputService :IGenericService<UserInput>
    {
        bool IsPrime(int number);
        int FindLargestPrimeNumber(List<int> numbers);
    }
}
