using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}
