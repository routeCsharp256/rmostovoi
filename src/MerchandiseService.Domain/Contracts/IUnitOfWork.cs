﻿using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}