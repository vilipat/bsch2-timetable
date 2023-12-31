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
    public interface IRepository<TModel, TFilter> where TModel : BaseModel
    {
        Task<List<TModel>> GetItems(TFilter? filterCriteria);
        Task<TModel> GetItem(int id);
        void Save(TModel item);

        void Delete(int id);
    }
}
