using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models;
using Timetable.Repositories;
using Timetable.Shared.Filters;

namespace Timetable.ViewModels
{
    public partial class PersonsViewModel : CrudViewModelBase<Person, PersonFilter>
    {
        private readonly PersonsRepository personsRepository;
        protected override IRepository<Person, PersonFilter> Repository => personsRepository;

        public PersonsViewModel()
        {
            personsRepository = new PersonsRepository();
        }

        [ObservableProperty]
        private string filterFirstName = string.Empty;

        [ObservableProperty]
        private string filterLastName = string.Empty;


        [RelayCommand()]
        public void ClearFilter()
        {
            FilterFirstName = FilterLastName = string.Empty;
        }


        [RelayCommand()]
        public void Delete()
        {
            //var ids = SelectedItems.Select(p => p.Id).ToArray();
            //_personsRepository.Delete(ids);
            //FilterItems();
        }

        protected override PersonFilter GetFilter() => new()
        {
            FirstName = FilterFirstName,
            LastName = FilterLastName
        };
    }
}
