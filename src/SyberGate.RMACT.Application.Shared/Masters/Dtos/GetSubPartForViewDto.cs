namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetSubPartForViewDto
    {
		public SubPartDto Part { get; set; }

		public string SupplierName { get; set;}

		public string BuyerName { get; set;}

		public string RMGroupName { get; set;}

		public string RMGradeName { get; set; }
    }
}