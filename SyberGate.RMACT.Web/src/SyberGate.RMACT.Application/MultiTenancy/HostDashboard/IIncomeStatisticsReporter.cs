using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyberGate.RMACT.MultiTenancy.HostDashboard.Dto;

namespace SyberGate.RMACT.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}