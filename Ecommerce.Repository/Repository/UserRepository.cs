using Ecommerce.Entities.Entity;
using Ecommerce.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.Repository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
        public IEnumerable<User> GetAllUsersBasedOnCount(int count)
        {
            return  _context.Users.OrderByDescending(d => d.Id).Take(count).ToList();
        }
    }
}
