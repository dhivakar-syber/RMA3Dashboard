﻿using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Masters.Importing.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Importing
{
    public interface IInvalidPartModelMatrixExporter
    {
        FileDto ExportToFile(List<ImportPartModelMatrixDto> partModelMatrixlistDtos);
    }
}

