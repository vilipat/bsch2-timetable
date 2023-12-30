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

namespace Timetable.ViewModels
{
    public partial class PersonsViewModel : CrudViewModelBase<Person>
    {
        private readonly PersonsRepository personsRepository;
        protected override IRepository<Person> Repository => personsRepository;

        public PersonsViewModel()
        {
            personsRepository = new PersonsRepository(dbPerson => 
                    (string.IsNullOrEmpty(filterFirstName) || dbPerson.FirstName.ToLower().Contains(filterFirstName.ToLower())) &&
                    (string.IsNullOrEmpty(filterLastName) || dbPerson.LastName.ToLower().Contains(filterFirstName.ToLower())));
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

    }
}
