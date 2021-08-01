using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpecFlowUnitTest.IServices
{
    public interface IParkingFeeService
    {
        int Billing(int inputMinute);
    }
}
