using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IPartBucketsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPartBucketForViewDto>> GetAll(GetAllPartBucketsInput input);

        Task<GetPartBucketForViewDto> GetPartBucketForView(int id);

        Task<List<PartBucketDto>> GetPartBucketForProcess(PartBucketViewModelDto input);


        Task<GetPartBucketForEditOutput> GetPartBucketForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPartBucketDto input);

		Task Delete(EntityDto input);

		//Task<FileDto> GetPartBucketsToExcel(GetAllPartBucketsForExcelInput input);

		
    }
}