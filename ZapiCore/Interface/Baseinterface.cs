﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZapiCore.Model;

namespace ZapiCore.Interface
{
    /// <summary>
    /// 公用约束
    /// </summary>
    public interface Baseinterface<T>
    {

        Task<IEnumerable<T>> EnumerableListAsync();

        IQueryable<T> IQueryableListAsync();

        Task<T> GetAsync(string id);


        Task<bool> PutEntityAsync(string id, T entity);

        Task<bool> AddEntityAsync(T entity);

        Task<bool> AddRangeEntityAsync(List<T> listentity);

        Task<bool> DeleteAsync(string id);

        bool IsExists(string id);

    }
}
