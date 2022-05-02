using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Desktop.Person_Ivanenko.Models;
using Desktop.Person_Ivanenko.Tools;

namespace Desktop.Person_Ivanenko.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = new Person();
        private RelayCommand<object> _proceedCommand;
        private bool _isEnabled;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
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

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChange("IsEnabled");
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

        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ??= new RelayCommand<object>(_ => Commit(), CanExecute);
            }
        }

        private async void Commit()
        {
            try
            {
                _person = await Task.Run(() => new Person(FirstName, LastName, Email, DateOfBirth));
                _person.ValidatePerson();
            } 
            catch (InvalidDateException ex)
            {
                DateOfBirth = _person.DefaultDOB;
            }
            catch ( InvalidEmailException ex)
            {
                Email = "";
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


    }
}
