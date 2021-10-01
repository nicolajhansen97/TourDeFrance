using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVMStarter.Model;

namespace WPFMVVMStarter.ViewModel
{
    /// <summary>
    /// a baseclass for changing viewmodels, but here it only has one. 
    /// </summary>
    class MainViewModel : Bindable
    {
        /// <summary>
        /// A getter and setter for our viewmodel
        /// </summary>
        public TourDeFranceViewModel TDFVM { get; set; }
        /// <summary>
        /// a private instacnce of our current view
        /// </summary>
        private object _currentView;
        /// <summary>
        /// a complete getter and setter with propyischanged to dectect if we change viewmodel.
        /// </summary>
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; propertyIsChanged(); }
        }
        /// <summary>
        /// our constructor we new our viewmodel and we make our currentview into the viewmodel.
        /// </summary>
        public MainViewModel()
        {
            TDFVM = new TourDeFranceViewModel();
            CurrentView = TDFVM;
        }
    }
}
