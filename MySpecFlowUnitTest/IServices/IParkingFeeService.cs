using MySpecFlowUnitTest.Models;
using System;
using System.Collections.Generic;

namespace MySpecFlowUnitTest.IServices
{
    public interface IParkingFeeService
    {
        List<BillingModel> Billing(DateTime startTime, DateTime endTime);
        int SingleDayBilling(int inputMinute);
        int SingleDayBilling(int hours, int inputMinute);
    }
}
