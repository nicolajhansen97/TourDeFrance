using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMStarter.Model;

namespace WPFMVVMStarter.ViewModel
{
    class MainViewModel : Bindable
    {
        public TourDeFranceViewModel TDFVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; propertyIsChanged(); }
        }

        public MainViewModel()
        {
            TDFVM = new TourDeFranceViewModel();
            CurrentView = TDFVM;
        }
    }
}
