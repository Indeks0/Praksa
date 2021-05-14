﻿using Models.Common;
using Project.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface ICityRepository
    {
        Task<int> InsertDataAsync(int zipCode, string name);
        Task<ICity> GetDataAsync(int zipCode);
        Task<List<ICity>> GetAllDataAsync(ICustomDBQuery customDBQuery);
        Task<int> UpdateDataAsync(int zipCode, string name);
        Task<int> DeleteDataAsync(int zipCode);
    }
}
