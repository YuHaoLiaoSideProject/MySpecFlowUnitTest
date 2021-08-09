using MySpecFlowUnitTest.IServices;
using MySpecFlowUnitTest.Models;
using System;
using System.Collections.Generic;

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
	
		public List<BillingModel> Billing(DateTime startTime, DateTime endTime)
		{
			//如果開始時間小於結束時間，直接調換
			if (endTime < startTime)
            {
				DateTime temp = startTime;
				startTime = endTime;
				endTime = temp;
			}
			//只有一天
			if(startTime.Date == endTime.Date)
            {
                return OnlyOneDay(startTime, endTime);
            }
			//===多天計算===
			List<BillingModel> result = new List<BillingModel>();
			//首日分鐘數
			int minutes = CalculateMinutes(startTime.Date.AddDays(1), startTime);
			//總分鐘數
			int sumMinutes = CalculateMinutes(endTime, startTime);
			//首日計算
			sumMinutes -= minutes;
			result.Add(new BillingModel 
			{
				Amount = SingleDayBilling(minutes),
				Date = startTime.Date,
				Minutes = minutes
			});
			//一天有多少分鐘
			int oneDayMinutes = 60 * 24;
			//其餘日期計算
			while (sumMinutes > 0)
            {
				DateTime date = startTime.Date.AddDays(1);
				//如果大於一天就計算一天的分鐘數，否則就計算剩餘分鐘數
				int calculateMinutes = sumMinutes >= oneDayMinutes ? oneDayMinutes : sumMinutes;
				result.Add(new BillingModel
				{
					Amount = SingleDayBilling(calculateMinutes),
					Date = date,
					Minutes = minutes
				});
				//計算過要扣除
				sumMinutes -= calculateMinutes;
			}
			return result;
		}

        private List<BillingModel> OnlyOneDay(DateTime startTime, DateTime endTime)
        {
			List<BillingModel> result = new List<BillingModel>();

			int minutes = CalculateMinutes(startTime, endTime);
            result.Add(new BillingModel
            {
                Amount = SingleDayBilling(minutes),
                Date = startTime.Date
            });
            return result;
        }

        private int CalculateMinutes(DateTime time1,DateTime time2)
        {
			if(time2 > time1)
            {
				DateTime temp = time1;
				time1 = time2;
				time2 = temp;
			}
			int minutes = Convert.ToInt32((time1 - time2).TotalMinutes);
			return minutes;
		}
		public int SingleDayBilling(int hours,int inputMinute)
		{
			return SingleDayBilling(hours * 60 + inputMinute);

		}
		/// <summary>
		/// 單日計算
		/// </summary>
		/// <param name="inputMinute"></param>
		/// <returns></returns>
		public int SingleDayBilling(int inputMinute)
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
