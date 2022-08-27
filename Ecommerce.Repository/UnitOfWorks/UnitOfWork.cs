using Ecommerce.Repository;
using Ecommerce.Repository.Interface;
using Ecommerce.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }
        public IUserRepository Users { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
