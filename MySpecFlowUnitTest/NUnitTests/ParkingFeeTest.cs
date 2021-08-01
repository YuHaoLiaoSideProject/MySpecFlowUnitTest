using MySpecFlowUnitTest.IServices;
using MySpecFlowUnitTest.Services;
using NUnit.Framework;

namespace MySpecFlowUnitTest
{
    // 計算一天的費用計算器
    // []不足十分, 0 元
    // []11~30分, 2元
    // []31~59, 5元
    // []一天最多 30 元
    // []每一小時 5 元
    // []超過 30 分, 收一小時費用
    // []剩餘分鍾 1~ 29, 2 元
    public class ParkingFeeTest
	{
		IParkingFeeService _ParkingFeeService { get; set; }
		public ParkingFeeTest()
        {
			_ParkingFeeService = new ParkingFeeService();

		}
		[TestCase(10)]
		public void GetFee_不足十分鍾_零元(int minutes)
		{
			var fee = _ParkingFeeService.Billing(minutes);
			int expected = 0; // 預期金額
			Assert.AreEqual(expected, fee);
		}

		[TestCase(11)]
		[TestCase(30)]
		public void GetFee_11_30分_2元(int minutes)
		{
			var fee = _ParkingFeeService.Billing(minutes);
			int expected = 2; // 預期金額

			Assert.AreEqual(expected, fee);
		}

		[TestCase(31)]
		[TestCase(59)]
		public void GetFee_31_59分_5元(int minutes)
		{
			var fee = _ParkingFeeService.Billing(minutes);
			int expected = 5; // 預期金額

			Assert.AreEqual(expected, fee);
		}


		[TestCase(1)]
		[TestCase(2)]
		public void GetFee_停N小數_5N元(int hours)
		{
			var fee = _ParkingFeeService.Billing(hours, 0);
			int expected = 5 * hours; // 預期金額

			Assert.AreEqual(expected, fee);
		}

		[TestCase(0, 31, 5)]
		[TestCase(1, 31, 10)]
		public void GetFee_超過三十分視為一小時(int hours, int minutes, int expected)
		{
			var fee = _ParkingFeeService.Billing(hours, minutes);

			Assert.AreEqual(expected, fee);
		}

		[TestCase(1, 30, 7)]
		public void GetFee_剩餘分鐘數不足31分_多收2元(int hours, int minutes, int expected)
		{
			var fee = _ParkingFeeService.Billing(hours, minutes);

			Assert.AreEqual(expected, fee);
		}

		[TestCase(10, 0)]
		public void GetFee_最多30元(int hours, int minutes)
		{
			var fee = _ParkingFeeService.Billing(hours, minutes);

			Assert.AreEqual(30, fee);
		}
	}
}
