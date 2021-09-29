using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVMStarter.Model
{
    class Cyclist
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string countryOrigin;

        public string CountryOrigin
        {
            get { return countryOrigin; }
            set { countryOrigin = value; }
        }

        private string resultTime;

        public string ResultTime
        {
            get { return resultTime; }
            set { resultTime = value; }
        }

        private int endPosition;

        public int EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; }
        }


    }
}
