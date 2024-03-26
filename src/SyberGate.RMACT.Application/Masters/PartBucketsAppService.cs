

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SyberGate.RMACT.Masters.Exporting;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PartBuckets)]
    public class PartBucketsAppService : RMACTAppServiceBase, IPartBucketsAppService
    {
		 private readonly IRepository<PartBucket> _partBucketRepository;
		 private readonly IPartBucketsExcelExporter _partBucketsExcelExporter;
		 

		  public PartBucketsAppService(IRepository<PartBucket> partBucketRepository, IPartBucketsExcelExporter partBucketsExcelExporter ) 
		  {
			_partBucketRepository = partBucketRepository;
			_partBucketsExcelExporter = partBucketsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPartBucketForViewDto>> GetAll(GetAllPartBucketsInput input)
         {
			
			var filteredPartBuckets = _partBucketRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.RMSpec.Contains(input.Filter) || e.Buckets.Contains(input.Filter) || e.Buyer.Contains(input.Filter) || e.Supplier.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RMSpecFilter),  e => e.RMSpec == input.RMSpecFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BucketsFilter),  e => e.Buckets == input.BucketsFilter)
						.WhereIf(input.MinValueFilter != null, e => e.Value >= input.MinValueFilter)
						.WhereIf(input.MaxValueFilter != null, e => e.Value <= input.MaxValueFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerFilter),  e => e.Buyer == input.BuyerFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierFilter),  e => e.Supplier == input.SupplierFilter);

			var pagedAndFilteredPartBuckets = filteredPartBuckets
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var partBuckets = from o in pagedAndFilteredPartBuckets
                         select new GetPartBucketForViewDto() {
							PartBucket = new PartBucketDto
							{
                                RMSpec = o.RMSpec,
                                Buckets = o.Buckets,
                                Value = o.Value,
                                Buyer = o.Buyer,
                                Supplier = o.Supplier,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPartBuckets.CountAsync();

            return new PagedResultDto<GetPartBucketForViewDto>(
                totalCount,
                await partBuckets.ToListAsync()
            );
         }

		public async Task<List<PartBucketDto>> GetPartBucketForProcess (PartBucketViewModelDto input)
        {
			var buckets = _partBucketRepository.GetAll().Where(w => w.Buyer == input.Buyer && w.Supplier == input.Supplier && w.RMSpec == input.RMSpec);

			var output = buckets.Select(s => new PartBucketDto { Buckets = s.Buckets }).OrderBy(o=> o.Buckets).ToList();
			return output;
		}


		 public async Task<GetPartBucketForProcessViewDto> GetProcessDetails(PartBucketViewModelDto input)
        {
			var bucketdetails = _partBucketRepository.GetAll().Where(w => w.Buyer == input.Buyer && w.Supplier == input.Supplier && w.RMSpec == input.RMSpec).OrderBy(o=>o.Buckets);

			var buckets = bucketdetails.Select(s => new PartBucketDto() { Value = s.Value, Buckets = s.Buckets });
			

			return new GetPartBucketForProcessViewDto()
			{
				BasePrice = input.Price - await buckets.SumAsync(s => s.Value),
				Price = input.Price,
				PartBucketDetails = await buckets.ToListAsync()

			};
		}

		 public async Task<GetPartBucketForViewDto> GetPartBucketForView(int id)
         {
            var partBucket = await _partBucketRepository.GetAsync(id);

            var output = new GetPartBucketForViewDto { PartBucket = ObjectMapper.Map<PartBucketDto>(partBucket) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PartBuckets_Edit)]
		 public async Task<GetPartBucketForEditOutput> GetPartBucketForEdit(EntityDto input)
         {
            var partBucket = await _partBucketRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPartBucketForEditOutput {PartBucket = ObjectMapper.Map<CreateOrEditPartBucketDto>(partBucket)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPartBucketDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PartBuckets_Create)]
		 protected virtual async Task Create(CreateOrEditPartBucketDto input)
         {
            var partBucket = ObjectMapper.Map<PartBucket>(input);

			

            await _partBucketRepository.InsertAsync(partBucket);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PartBuckets_Edit)]
		 protected virtual async Task Update(CreateOrEditPartBucketDto input)
         {
            var partBucket = await _partBucketRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, partBucket);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PartBuckets_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _partBucketRepository.DeleteAsync(input.Id);
         } 

		//public async Task<FileDto> GetPartBucketsToExcel(GetAllPartBucketsForExcelInput input)
  //       {
			
		//	var filteredPartBuckets = _partBucketRepository.GetAll()
		//				.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PartNumber.Contains(input.Filter) || e.Buckets.Contains(input.Filter) || e.Buyer.Contains(input.Filter) || e.Supplier.Contains(input.Filter))
		//				.WhereIf(!string.IsNullOrWhiteSpace(input.PartNumberFilter),  e => e.PartNumber == input.PartNumberFilter)
		//				.WhereIf(!string.IsNullOrWhiteSpace(input.BucketsFilter),  e => e.Buckets == input.BucketsFilter)
		//				.WhereIf(input.MinValueFilter != null, e => e.Value >= input.MinValueFilter)
		//				.WhereIf(input.MaxValueFilter != null, e => e.Value <= input.MaxValueFilter)
		//				.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerFilter),  e => e.Buyer == input.BuyerFilter)
		//				.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierFilter),  e => e.Supplier == input.SupplierFilter);

		//	var query = (from o in filteredPartBuckets
  //                       select new GetPartBucketForViewDto() { 
		//					PartBucket = new PartBucketDto
		//					{
  //                              PartNumber = o.PartNumber,
  //                              Buckets = o.Buckets,
  //                              Value = o.Value,
  //                              Buyer = o.Buyer,
  //                              Supplier = o.Supplier,
  //                              Id = o.Id
		//					}
		//				 });


  //          var partBucketListDtos = await query.ToListAsync();

  //          return _partBucketsExcelExporter.ExportToFile(partBucketListDtos);
  //       }


    }
}