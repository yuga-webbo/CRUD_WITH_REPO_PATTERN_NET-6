using Ecommerce.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetAllUsersBasedOnCount(int count);
    }
}
