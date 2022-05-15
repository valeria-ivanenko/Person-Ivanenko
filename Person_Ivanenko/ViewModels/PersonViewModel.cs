using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Desktop.Person_Ivanenko.Models;
using Desktop.Person_Ivanenko.Repositories;
using Desktop.Person_Ivanenko.Tools;

namespace Desktop.Person_Ivanenko.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = new Person();
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _saveCommand;
        private ObservableCollection<Person> _people;
        private static FileRepository Repository = new FileRepository();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        public Person Person
        {
            get
            {
                return _person;
            }
            set
            {
                if (_person != value)
                {
                    _person = value;
                }
                OnPropertyChange("Person");
            }
        }
        public string FirstName
        {
            get
            {
                return _person.FirstName;
            }
            set
            {
                if (_person.FirstName != value)
                {
                    _person.FirstName = value;
                }
                OnPropertyChange("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _person.LastName;
            }
            set
            {
                if (_person.LastName != value)
                {
                    _person.LastName = value;
                }
                OnPropertyChange("LastName");
            }
        }

        public string Email
        {
            get
            {
                return _person.Email;
            }
            set
            {
                if (_person.Email != value)
                {
                    _person.Email = value;
                }
                OnPropertyChange("Email");
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _person.DateOfBirth;
            }
            set
            {
                if (_person.DateOfBirth != value)
                {
                    _person.DateOfBirth = value;
                }
                OnPropertyChange("DateOfBitrh");
            }
        }

        public ObservableCollection<Person> People
        {
            get => _people;
            private set
            {
                _people = value;
                OnPropertyChange("People");
            }
        }

        private void OnPropertyChange(string v)
        {
            if(v != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        #endregion
        public PersonViewModel()
        {
            _people = new ObservableCollection<Person>(RandomPeople());
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand<object>(_ => AddPerson(), CanExecute);
            }
        }

        public RelayCommand<object> DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand<object>(_ => DeletePerson(), CanExecute);
            }
        }

        public RelayCommand<object> SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand<object>(_ => SavePeople(), CanExecute);
            }
        }

        private async void AddPerson()
        {
            try
            {
                _person = await Task.Run(() => new Person(FirstName, LastName, Email, DateOfBirth));
                _person.ValidatePerson();
                People.Add(_person);
            }
            catch (InvalidFirstNameException ex)
            {
                FirstName = "";
                return;
            }
            catch (InvalidLastNameException ex)
            {
                LastName = "";
                return;
            }
            catch (InvalidEmailException ex)
            {
                Email = "";
                return;
            }
            catch (InvalidDateException ex)
            {
                DateOfBirth = _person.DefaultDOB;
                return;
            }
            

            int age = _person.Age;
            if (age < 0 || age > 135)
            {
                MessageBox.Show("Incorrect Date!");
                return;
            }
            if (_person._isBirthday)
            {
                MessageBox.Show("Happy Birthday!");
                ShowInfo();
            }
            if (_person.DateOfBirth.Equals(_person.DefaultDOB))
            {
                MessageBox.Show("First Name: " + _person.FirstName
                        + "\nLast Name: " + _person.LastName
                        + "\nEmail: " + _person.Email
                        + "\nDate Of Birth: None " 
                        + "\nIs Adult: " + _person._isAdult
                        + "\nIs Birthday: " + _person._isBirthday
                        + "\nSun Sign: " + _person._sunSign
                        + "\nChinese SIgn: " + _person._chineseSign);
            }
            else if (!_person._isBirthday)
            {
                ShowInfo();
            }
            
        }
        private void DeletePerson()
        {
            People.Remove(Person);
        }

        private async void SavePeople()
        {
            Repository.CleanRepository();
            foreach (var person in People)
            {
                await Repository.AddOrUpdateAsync(person);
            }
        }

            public void ShowInfo()
        {
            MessageBox.Show("First Name: " + _person.FirstName
                    + "\nLast Name: " + _person.LastName
                    + "\nEmail: " + _person.Email
                    + "\nDate Of Birth: " + _person.DateOfBirth
                    + "\nIs Adult: " + _person._isAdult
                    + "\nIs Birthday: " + _person._isBirthday
                    + "\nSun Sign: " + _person._sunSign
                    + "\nChinese SIgn: " + _person._chineseSign);
        }
        private bool CanExecute(object obj)
        {
            
            return true; 
        }

        public string RandomFirstName()
        {
            Random rnd = new Random();
            string[] firstNameArray = new string[] { "Adam", "Alex", "Aaron", "Ben", "Carl", "Dan", "David",
            "Edward", "Fred", "Frank", "George", "Hal", "Hank", "Ike", "John", "Jack", "Joe", "Larry",
            "Monte", "Matthew", "Mark", "Nathan", "Otto", "Paul", "Peter", "Roger", "Roger", "Steve",
            "Thomas", "Tim", "Ty", "Victor", "Walter" };
            return firstNameArray[rnd.Next(32)];
        }

        public string RandomLastName()
        {
            Random rnd = new Random();
            string[] lastNameArray = new string[] { "Anderson", "Ashwoon", "Aikin", "Bateman", "Bongard", "Bowers",
            "Boyd", "Cannon", "Cast", "Deitz", "Dewalt", "Ebner", "Frick", "Hancock", "Haworth", "Hesch",
            "Hoffman", "Kassing", "Knutson", "Lawless", "Lawicki", "Mccord", "McCormack", "Miller", "Myers",
            "Zandt", "Vanderpoel", "Ventotla", "Vogal", "Wagle", "Wagner", "Wakefield", "Weinstein",
            "McGinnis", "Mills", "Moody", "Moore", "Napier", "Nelson", "Norquist", "Nuttle", "Olson",
            "Ostrander", "Reamer", "Reardon", "Reyes", "Rice", "Ripka", "Roberts", "Rogers"};
            return lastNameArray[rnd.Next(49)];
        }

        public string MakeEmail(string first_name, string last_name)
        {
            return first_name.ToLower() + "." + last_name.ToLower() + "@ukr.net";
        }
        public DateTime RandomDOB()
        {
            Random rnd = new Random();
            DateTime start = new DateTime(1887, 5, 17);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        public ObservableCollection<Person> RandomPeople()
        {
            ObservableCollection<Person> randoms = new ObservableCollection<Person>();
            for (int i = 0; i < 50; ++i)
            {
                string rand_first_name = RandomFirstName();
                string rand_last_name = RandomLastName();
                randoms.Add(new Person(rand_first_name, rand_last_name, MakeEmail(rand_first_name, rand_last_name), RandomDOB()));
            }
            return randoms;
        }


    }
}
