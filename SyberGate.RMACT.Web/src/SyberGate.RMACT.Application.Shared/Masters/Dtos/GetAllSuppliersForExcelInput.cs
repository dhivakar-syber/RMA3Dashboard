﻿using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllSuppliersForExcelInput
    {
		public string Filter { get; set; }

		public string CodeFilter { get; set; }

		public string NameFilter { get; set; }



    }
}