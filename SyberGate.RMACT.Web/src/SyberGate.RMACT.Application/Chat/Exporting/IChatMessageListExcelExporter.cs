using System.Collections.Generic;
using Abp;
using SyberGate.RMACT.Chat.Dto;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
