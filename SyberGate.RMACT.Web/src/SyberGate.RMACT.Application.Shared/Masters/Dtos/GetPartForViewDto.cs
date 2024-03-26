namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetPartForViewDto
    {
		public PartDto Part { get; set; }

		public string SupplierName { get; set;}

		public string SupplierCode { get; set;}

		public string BuyerName { get; set;}

		public string RMGroupName { get; set;}

		public string RMGradeName { get; set; }
    }
}