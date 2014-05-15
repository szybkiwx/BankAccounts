using BankAccounts.Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Test
{
    public class TestHelper
    {
        public static void SetupMockDbSet<T>(Mock<DbSet<T>> dbSet, IEnumerable<T> dataEnumerable) where T: class
        {
            var data = dataEnumerable.AsQueryable();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
