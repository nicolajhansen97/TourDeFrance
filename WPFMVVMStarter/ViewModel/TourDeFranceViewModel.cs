using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
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

        public DelegateCommand sortbyName { get; set; }

        private string _sport = "Sport: ";

        public string Sport 
        {
            get { return _sport; } 
            set { _sport = value; propertyIsChanged(); } 
        }
        private string _lapName = "Lap Name: ";

        public string LapName
        {
            get { return _lapName; }
            set { _lapName = value; propertyIsChanged(); }
        }
        private string _distance = "Distance: ";

        public string Distance
        {
            get { return _distance; }
            set { _distance = value; propertyIsChanged(); }
        }
        private string _dateOfEvent = "Date of Event: ";

        public string DateOfEvent
        {
            get { return _dateOfEvent; }
            set { _dateOfEvent = value; propertyIsChanged(); }
        }

        public TourDeFranceViewModel()
        {
            ReadAndBuildXML();
            sortbyName = new DelegateCommand( o => 
            {
                //sortMethod 
            });
        }


        private void ReadAndBuildXML()
        {

            string fileName = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/Cycling-Tour-De-France.xml";
            //string fileName = "Cycling-Tour-De-France.xml"; //path

            XElement root = XElement.Load(fileName);

            // root.Descendants("deepPan").First().Add(newElement); // Wrong attempt... You must fint the place to "hook up the new element" first
            //var Tour = from e in root.Descendants("sport") select e;
            var cyclists = from e in root.Descendants("event_participant") select e;
            var test = from e in root.Descendants("results") select e;

            Sport += root.Element("query-response").Element("sport").Attribute("name").Value;
            
            foreach (var item in root.Element("query-response").Element("sport").Element("tournament_template").Element("tournament")
                .Element("tournament_stage").Element("event").Element("properties").Elements("property"))
            {
                if (item.Attribute("name").Value == "StartName")
                {
                    LapName = item.Attribute("value").Value;
                }
                if (item.Attribute("name").Value == "Kilometers")
                {
                    Distance = item.Attribute("value").Value;
                }
            }
            LapName = "Lap Name: " + LapName;
            Distance = "Distance: " + Distance + "Km";
            //LapName +=;
            //Distance +=;
            DateOfEvent +=root.Element("query-response").Element("sport").Element("tournament_template").Element("tournament")
                .Element("tournament_stage").Element("event").Attribute("startdate").Value;

            foreach (XElement people in cyclists)
            {
                string endPos2 = " ";
                string resultTime = "1";
                
                var results = people.Elements("results");

                if (people.Element("results") != null)//does he have a result.
                {

                    foreach (var item in people.Element("results").Elements("result"))
                    {
                        if (item.Attribute("result_code").Value == "rank")
                        {
                            endPos2 = item.Attribute("value").Value;
                        }
                        
                        if (item.Attribute("result_code").Value == "duration")
                        {
                            resultTime = item.Attribute("value").Value;
                        }
                    }
                    //take last rank
                    /*if (people.Element("results").Element("result").Attribute("result_code").Value == "rank")
                    {
                       // endPos2 = from e in people.Elements("results") select (from d in e.Elements("result") select d.Value);  //where people.Element("reults").Element("result").Attribute("result_code").Value == "rank" select e;
                    }*/
                }
                else { endPos2 = "NC"; resultTime = "NC"; }

                //XElement deepPan = (from e in pizza.Descendants("deepPan") select e).First()
                Cyclists.Add(new Cyclist
                {
                    Name = people.Element("participant").Attribute("name").Value,
                    CountryOrigin = people.Element("participant").Attribute("countryFK").Value,
                    EndPosition = endPos2/*need result value.*/,
                    Gender = people.Element("participant").Attribute("gender").Value,
                    ResultTime = resultTime/*math for endpos and time.*/
                });
            }

            //no attributes.
            root.Save(Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/Cycling-Tour-De-FranceNoAtt.xml");

            Console.WriteLine("Done creating/modifying xml");
        }




    }
}
