using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.PartBuckets
{
    public class CreateOrEditPartBucketModalViewModel
    {
       public CreateOrEditPartBucketDto PartBucket { get; set; }

	   
       
	   public bool IsEditMode => PartBucket.Id.HasValue;
    }
}