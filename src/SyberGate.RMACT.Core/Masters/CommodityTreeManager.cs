using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Linq;
using Abp.UI;
using Abp.Zero;

namespace SyberGate.RMACT.Masters
{
    /// <summary>
    /// Performs domain logic for Organization Units.
    /// </summary>
    public class CommodityTreeManager : DomainService
    {
        protected IRepository<CommodityTree, long> CommodityTreeRepository { get; private set; }

        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }

        public CommodityTreeManager(IRepository<CommodityTree, long> commodityTreeRepository)
        {
            CommodityTreeRepository = commodityTreeRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
            AsyncQueryableExecuter = NullAsyncQueryableExecuter.Instance;
        }

        public virtual async Task CreateAsync(CommodityTree commodityTree)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {
                commodityTree.Code = await GetNextChildCodeAsync(commodityTree.ParentId);
                await ValidateCommodityTreeAsync(commodityTree);
                await CommodityTreeRepository.InsertAsync(commodityTree);

                await uow.CompleteAsync();
            }
        }

        public virtual void Create(CommodityTree commodityTree)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {
                commodityTree.Code = GetNextChildCode(commodityTree.ParentId);
                ValidateCommodityTree(commodityTree);
                CommodityTreeRepository.Insert(commodityTree);

                uow.Complete();
            }
        }

        public virtual async Task UpdateAsync(CommodityTree commodityTree)
        {
            await ValidateCommodityTreeAsync(commodityTree);
            await CommodityTreeRepository.UpdateAsync(commodityTree);
        }

        public virtual void Update(CommodityTree commodityTree)
        {
            ValidateCommodityTree(commodityTree);
            CommodityTreeRepository.Update(commodityTree);
        }

        public virtual async Task<string> GetNextChildCodeAsync(long? parentId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId);
            if (lastChild == null)
            {
                var parentCode = parentId != null ? await GetCodeAsync(parentId.Value) : null;
                return CommodityTree.AppendCode(parentCode, CommodityTree.CreateCode(1));
            }

            return CommodityTree.CalculateNextCode(lastChild.Code);
        }

        public virtual string GetNextChildCode(long? parentId)
        {
            var lastChild = GetLastChildOrNull(parentId);
            if (lastChild == null)
            {
                var parentCode = parentId != null ? GetCode(parentId.Value) : null;
                return CommodityTree.AppendCode(parentCode, CommodityTree.CreateCode(1));
            }

            return CommodityTree.CalculateNextCode(lastChild.Code);
        }

        public virtual async Task<CommodityTree> GetLastChildOrNullAsync(long? parentId)
        {
            var query = CommodityTreeRepository.GetAll()
                .Where(ou => ou.ParentId == parentId)
                .OrderByDescending(ou => ou.Code);
            return await AsyncQueryableExecuter.FirstOrDefaultAsync(query);
        }

        public virtual CommodityTree GetLastChildOrNull(long? parentId)
        {
            var query = CommodityTreeRepository.GetAll()
                .Where(ou => ou.ParentId == parentId)
                .OrderByDescending(ou => ou.Code);
            return query.FirstOrDefault();
        }

        public virtual async Task<string> GetCodeAsync(long id)
        {
            return (await CommodityTreeRepository.GetAsync(id)).Code;
        }

        public virtual string GetCode(long id)
        {
            return (CommodityTreeRepository.Get(id)).Code;
        }

        public virtual async Task DeleteAsync(long id)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {
                var children = await FindChildrenAsync(id, true);

                foreach (var child in children)
                {
                    await CommodityTreeRepository.DeleteAsync(child);
                }

                await CommodityTreeRepository.DeleteAsync(id);

                await uow.CompleteAsync();
            }
        }

        public virtual void Delete(long id)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {
                var children = FindChildren(id, true);

                foreach (var child in children)
                {
                    CommodityTreeRepository.Delete(child);
                }

                CommodityTreeRepository.Delete(id);

                uow.Complete();
            }
        }

        public virtual async Task MoveAsync(long id, long? parentId)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {
                var commodityTree = await CommodityTreeRepository.GetAsync(id);
                if (commodityTree.ParentId == parentId)
                {
                    await uow.CompleteAsync();
                    return;
                }

                //Should find children before Code change
                var children = await FindChildrenAsync(id, true);

                //Store old code of OU
                var oldCode = commodityTree.Code;

                //Move OU
                commodityTree.Code = await GetNextChildCodeAsync(parentId);
                commodityTree.ParentId = parentId;

                await ValidateCommodityTreeAsync(commodityTree);

                //Update Children Codes
                foreach (var child in children)
                {
                    child.Code = CommodityTree.AppendCode(commodityTree.Code, CommodityTree.GetRelativeCode(child.Code, oldCode));
                }

                await uow.CompleteAsync();
            }
        }

        public virtual void Move(long id, long? parentId)
        {
            
            var commodityTree = CommodityTreeRepository.Get(id);
            if (commodityTree.ParentId == parentId)
            {
                return;
            }

            //Should find children before Code change
            var children = FindChildren(id, true);

            //Store old code of OU
            var oldCode = commodityTree.Code;

            //Move OU
            commodityTree.Code = GetNextChildCode(parentId);
            commodityTree.ParentId = parentId;

            ValidateCommodityTree(commodityTree);

            //Update Children Codes
            foreach (var child in children)
            {
                child.Code = CommodityTree.AppendCode(commodityTree.Code, CommodityTree.GetRelativeCode(child.Code, oldCode));
            }
        }

        public async Task<List<CommodityTree>> FindChildrenAsync(long? parentId, bool recursive = false)
        {
            if (!recursive)
            {
                return await CommodityTreeRepository.GetAllListAsync(ou => ou.ParentId == parentId);
            }

            if (!parentId.HasValue)
            {
                return await CommodityTreeRepository.GetAllListAsync();
            }

            var code = await GetCodeAsync(parentId.Value);

            return await CommodityTreeRepository.GetAllListAsync(
                ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value
            );
        }

        public List<CommodityTree> FindChildren(long? parentId, bool recursive = false)
        {
            if (!recursive)
            {
                return CommodityTreeRepository.GetAllList(ou => ou.ParentId == parentId);
            }

            if (!parentId.HasValue)
            {
                return CommodityTreeRepository.GetAllList();
            }

            var code = GetCode(parentId.Value);

            return CommodityTreeRepository.GetAllList(
                ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value
            );
        }

        protected virtual async Task ValidateCommodityTreeAsync(CommodityTree commodityTree)
        {
            var siblings = (await FindChildrenAsync(commodityTree.ParentId))
                .Where(ou => ou.Id != commodityTree.Id)
                .ToList();

            if (siblings.Any(ou => ou.DisplayName == commodityTree.DisplayName))
            {
                throw new UserFriendlyException(L("CommodityTreeDuplicateDisplayNameWarning", commodityTree.DisplayName));
            }
        }

        protected virtual void ValidateCommodityTree(CommodityTree commodityTree)
        {
            var siblings = (FindChildren(commodityTree.ParentId))
                .Where(ou => ou.Id != commodityTree.Id)
                .ToList();

            if (siblings.Any(ou => ou.DisplayName == commodityTree.DisplayName))
            {
                throw new UserFriendlyException(L("CommodityTreeDuplicateDisplayNameWarning", commodityTree.DisplayName));
            }
        }
    }
}