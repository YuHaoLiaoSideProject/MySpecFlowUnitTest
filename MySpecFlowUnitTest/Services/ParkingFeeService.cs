using MySpecFlowUnitTest.IServices;
using System;

namespace MySpecFlowUnitTest.Services
{
    public class ParkingFeeService : IParkingFeeService
	{
		int freeMinute = 10;
		//計費單位(每60分鐘)
		int unit = 60;
		//每單位費用
		int unitFee = 5;
		//半個單位特殊處裡
		int halfUnit = 30;
		//半個單位費用
		int halfUnitFee = 2;
		//最大費用
		int maxFee = 30;
		public int Billing(int hours, int inputMinute)
        {
			return Billing(hours * 60 + inputMinute);

		}
		public int Billing(int inputMinute)
        {
			int resultFee = 0;
			int dayMinute = 60 * 24;
			while (inputMinute > 0)
			{
				//每天拆開
				int addDayMinute = inputMinute >= dayMinute ? dayMinute : inputMinute;
				inputMinute -= addDayMinute;
				resultFee += SingleDayBilling(addDayMinute);
			}
			return resultFee;
		}
		/// <summary>
		/// 單日計算
		/// </summary>
		/// <param name="inputMinute"></param>
		/// <returns></returns>
		private int SingleDayBilling(int inputMinute)
		{
			//免費分鐘數內不用錢
			if (freeMinute >= inputMinute)
			{
				return 0;
			}
			//不足一小時的時間
			int remainderMinute = inputMinute % unit;
			//幾個小時
			int hour = inputMinute / unit;
			//剩餘時間計費
			int remainderMinuteCost = 0;
			if (remainderMinute > 0)
			{
				remainderMinuteCost = remainderMinute > halfUnit ? unitFee : halfUnitFee;
			}

			int sumFee = hour * unitFee + remainderMinuteCost;

			return Math.Min(sumFee, maxFee);
		}
	}
}
