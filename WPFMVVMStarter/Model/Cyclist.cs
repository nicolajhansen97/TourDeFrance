using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVMStarter.Model
{
    class Cyclist : Bindable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; propertyIsChanged(); }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; propertyIsChanged();  }
        }

        private string countryOrigin;

        public string CountryOrigin
        {
            get { return countryOrigin; }
            set { countryOrigin = value; propertyIsChanged(); }
        }

        private string resultTime;

        public string ResultTime
        {
            get { return resultTime; }
            set { resultTime = value; propertyIsChanged(); }
        }

        private int endPosition;

        public int EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; propertyIsChanged(); }
        }


    }
}
