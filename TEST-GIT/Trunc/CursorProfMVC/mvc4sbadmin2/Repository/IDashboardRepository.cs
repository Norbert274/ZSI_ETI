using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using System.Data;

namespace nclprospekt.Repository
{
    public interface IDashboardRepository
    {
        DashboardWO CountStatistics(byte[] sesja);

    }
}