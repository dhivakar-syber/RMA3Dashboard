using System.Collections.Generic;
using SyberGate.RMACT.Auditing.Dto;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
