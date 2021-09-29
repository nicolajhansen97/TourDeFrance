using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMStarter.Model;

namespace WPFMVVMStarter.ViewModel
{
    class TourDeFranceViewModel : Bindable
    {
        private List<Cyclist> _cyclists = new List<Cyclist>();
        public List<Cyclist> Cyclists
        {
            get { return _cyclists; }
            set { _cyclists = value; propertyIsChanged(); }
        }

        public TourDeFranceViewModel()
        {
            Cyclists.Add(new Cyclist { Name = "Bob Test", CountryOrigin = "Germany", EndPosition = 1, Gender = "Man", ResultTime = "Good"});
        }

    }
}
