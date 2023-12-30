using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db.DbModels;
using Timetable.Models;

namespace Timetable.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<List<T>> GetItems();
        Task<T> GetItem(int id);
        void Save(T item);
    }
}
