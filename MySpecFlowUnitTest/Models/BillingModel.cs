using System;

namespace MySpecFlowUnitTest.Models
{
    public class BillingModel
	{
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// 金額
		/// </summary>
		public int Amount { get; set; }
		/// <summary>
		/// 日停車時間
		/// </summary>
		public int Minutes { get; set; }
	}
}
