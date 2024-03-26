using SyberGate.RMACT.Masters.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Buyers
{
    public class CreateOrEditBuyerModalViewModel
    {
       public CreateOrEditBuyerDto Buyer { get; set; }

	   		public string UserName { get; set;}


       public List<BuyerUserLookupTableDto> BuyerUserList { get; set;}


	   public bool IsEditMode => Buyer.Id.HasValue;
    }
}