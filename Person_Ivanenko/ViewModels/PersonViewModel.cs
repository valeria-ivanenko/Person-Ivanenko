using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Desktop.Person_Ivanenko.Models;
using Desktop.Person_Ivanenko.Tools;

namespace Desktop.Person_Ivanenko.ViewModels
{
    class PersonViewModel
    {
        #region Fields
        private Person _person = new Person();
        private RelayCommand<object> _proceedCommand;
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
                _person.FirstName = value;
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
                _person.LastName = value;
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
                _person.Email = value;
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
                _person.DateOfBirth = value;
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

        private void Commit()
        {
            if (_person._isBirthday == true)
            {
                MessageBox.Show("Happy Birthday!");
            }
            if (_person.GetAge() < 0 || _person.GetAge() > 135)
            {
                MessageBox.Show("Incorrect Date!");
            }
            else
            {
                MessageBox.Show(_person.FirstName + " " + _person.LastName + " " + _person.Email + " " + _person.DateOfBirth + " "
                + _person._isAdult + " " + _person._isBirthday + " " + _person._sunSign + " " + _person._chineseSign + " ");
            }
        }

        private bool CanExecute(object obj)
        {
           return true; 
        }


    }
}
