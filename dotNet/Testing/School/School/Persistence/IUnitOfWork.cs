﻿using System.Threading;
using System.Threading.Tasks;

namespace School.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}