using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.UnitOfMeasurements
{
    public class CreateOrEditUnitOfMeasurementModalViewModel
    {
       public CreateOrEditUnitOfMeasurementDto UnitOfMeasurement { get; set; }

	   
       
	   public bool IsEditMode => UnitOfMeasurement.Id.HasValue;
    }
}