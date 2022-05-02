using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Person_Ivanenko.Models
{
    class Person
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private int _age;
        private DateTime _defaultDOB = DateTime.Parse("1/1/0001");
        private DateTime _dateOfBirth = DateTime.Parse("1/1/0001");
        public readonly bool _isAdult;
        public readonly string _sunSign;
        public readonly string _chineseSign;
        public readonly bool _isBirthday;
        #endregion

        #region Properties
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
            }
        }

        public DateTime DefaultDOB
        {
            get
            {
                return _defaultDOB;
            }
            set
            {
                _defaultDOB = value;
            }
        }

        #endregion

        #region Constructors
        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            GetAgeAsync();
            _isAdult = IsAdult();
            _sunSign = SetZodiac();
            _chineseSign = SetChineseZodiac();
            _isBirthday = IsBirthday();
            ValidatePerson();
        }

        public Person(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = DateTime.Now;
            GetAgeAsync();
            _isAdult = IsAdult();
            _sunSign = SetZodiac();
            _chineseSign = SetChineseZodiac();
            _isBirthday = IsBirthday();
            ValidatePerson();
        }

        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = null;
            GetAgeAsync();
            _isAdult = IsAdult();
            _sunSign = SetZodiac();
            _chineseSign = SetChineseZodiac();
            _isBirthday = IsBirthday();
            ValidatePerson();
        }

        public Person()
        {
            
        }

        #endregion
        public async void GetAgeAsync()
        {
            Age = await Task.Run(() => GetAge());
        }
        public int GetAge()
        {
            if (DateOfBirth.Equals(DefaultDOB))
            {
                return 0;
            }
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        public bool IsAdult()
        {
            if (DateOfBirth.Equals(DefaultDOB))
            {
                return false;
            }
            if (Age >= 18)
            {
                return true;
            }
            else
            { return false; };
        }

        public string SetZodiac()
        {
            if (DateOfBirth.Equals(DefaultDOB))
            {
                return "No Sign";
            }
            switch (DateOfBirth.Month)
            {
                case 1:
                    if (DateOfBirth.Day <= 20) return  "Capricorn";
                    else
                    { return "Aquarius"; }
                case 2:
                    if (DateOfBirth.Day <= 19)
                    { return "Aquarius"; }
                    else
                    { return "Pisces"; }
                    
                case 3:
                    if (DateOfBirth.Day <= 20)
                    { return "Pisces"; }
                    else
                    { return "Aries"; }
                    
                case 4:
                    if (DateOfBirth.Day <= 20)
                    { return "Aries"; }
                    else
                    { return "Taurus"; }
                    
                case 5:
                    if (DateOfBirth.Day <= 21)
                    { return "Taurus"; }
                    else
                    { return  "Gemini"; }
                    
                case 6:
                    if (DateOfBirth.Day <= 22)
                    { return "Gemini"; }
                    else
                    { return "Cancer"; }
                    
                case 7:
                    if (DateOfBirth.Day <= 22)
                    { return "Cancer"; }
                    else
                    { return "Leo"; }
                    
                case 8:
                    if (DateOfBirth.Day <= 23)
                    { return "Leo"; }
                    else
                    { return "Virgo"; ; }
                    
                case 9:
                    if (DateOfBirth.Day <= 23)
                    { return "Virgo"; }
                    else
                    { return "Libra"; }
                    
                case 10:
                    if (DateOfBirth.Day <= 23)
                    { return "Libra"; }
                    else
                    { return "Scorpio"; ; }
                    
                case 11:
                    if (DateOfBirth.Day <= 22)
                    { return  "Scorpio"; }
                    else
                    { return  "Sagittarius"; }
                    
                case 12:
                    if (DateOfBirth.Day <= 21)
                    { return  "Sagittarius"; }
                    else
                    { return "Capricorn"; }
                    
                default:
                    return "No Sign";
                    
            }
        }
        public string SetChineseZodiac()
        {
            if (DateOfBirth.Equals(DefaultDOB))
            {
                return "No Sign";
            }
            string[] animals = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            string[] elements = { "Wood", "Fire", "Earth", "Metal", "Water" };


            int ei = (int)Math.Floor((DateOfBirth.Year - 4.0) % 10 / 2);
            int ai = (DateOfBirth.Year - 4) % 12;

            return elements[ei] + " " + animals[ai];
        }

        public bool IsBirthday()
        {
            return (DateOfBirth.Day.Equals(DateTime.Today.Day) && DateOfBirth.Month.Equals(DateTime.Today.Month));
        }

        public void ValidatePerson()
        {
            if (Age > 135 || Age < 0)
            {
                throw new InvalidDateException(DateOfBirth);
            }
            if (!Email.Contains('@') || !Email.Contains('.'))
            {
                throw new InvalidEmailException(Email);
            }
        }
    }

    [Serializable]
    class InvalidDateException : Exception
    {
        public InvalidDateException() { }
        public InvalidDateException(DateTime dateOfBirth)
        {
            MessageBox.Show("Exception occured, date " + dateOfBirth + " is incorrect");
        }
            
    }

    [Serializable]
    class InvalidEmailException : Exception
    {
        public InvalidEmailException() { }
        public InvalidEmailException(string email)
        {
            MessageBox.Show("Exception occured, email should contain @ and .");
        }

    }

}
