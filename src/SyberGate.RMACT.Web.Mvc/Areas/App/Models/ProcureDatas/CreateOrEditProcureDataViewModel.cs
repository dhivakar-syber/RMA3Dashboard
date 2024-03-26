using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.ProcureDatas
{
    public class CreateOrEditProcureDataModalViewModel
    {
       public CreateOrEditProcureDataDto ProcureData { get; set; }

	   
       
	   public bool IsEditMode => ProcureData.Id.HasValue;
    }
}