using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using WPFMVVMStarter.Model;

namespace WPFMVVMStarter.ViewModel
{
    /// <summary>
    /// our viewmodel for everything we do in the view.
    /// </summary>
    class TourDeFranceViewModel : Bindable
    {
        /// <summary>
        /// this is our list of cyclists.
        /// we made it a getter and setter so we could use it in our view 
        /// with bindings
        /// </summary>
        private List<Cyclist> _cyclists = new List<Cyclist>();
        public List<Cyclist> Cyclists
        {
            get { return _cyclists; }
            set { _cyclists = value; propertyIsChanged(); }
        }

        public DelegateCommand sortbyName { get; set; }
        public DelegateCommand sortbyCountry { get; set; }
        public DelegateCommand sortbyEndPosition { get; set; }

        public DelegateCommand runParser { get; set; }
        public DelegateCommand validateDTD { get; set; }

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
        
        /// <summary>
        /// This is our constructor it runs the building of the list,
        /// and creates our commands. for the view.
        /// </summary>
        public TourDeFranceViewModel()
        {
            ReadXmlAndBuildList();
            
            //Runs the parser method when the command is called through the delegatecommand which is actived when you click the button.
            runParser = new DelegateCommand(o =>
            {
                Parser();
            });

            //Runs the validator method when the command is called through the delegatecommand which is actived when you click the button.
            validateDTD = new DelegateCommand(o =>
            {
                Validator();
            });

            //Runs the different sorting when the command is called through the delegatecommand which is actived when you click the button.
            sortbyName = new DelegateCommand( o => { Cyclists = Cyclists.OrderBy(x => x.Name).ToList(); });
            sortbyCountry = new DelegateCommand( o => { Cyclists = Cyclists.OrderBy(x => x.CountryOrigin.Length).ThenBy(x => x.CountryOrigin).ToList(); });
            sortbyEndPosition = new DelegateCommand(o => { Cyclists = Cyclists.OrderBy(x => x.EndPosition.Length).ThenBy(x => x.EndPosition).ToList(); });
        }

        /// <summary>
        /// We read the xml file and get all the info we want and put them in the places they need to 
        /// be, so here we populate our list with the cyclist data we need.
        /// </summary>
        private void ReadXmlAndBuildList()
        {

            string fileName = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/Cycling-Tour-De-France.xml";
           
            XElement root = XElement.Load(fileName);

            var cyclists = from e in root.Descendants("event_participant") select e;
            //setting sport String with xml data
            Sport += root.Element("query-response").Element("sport").Attribute("name").Value;
            
            //getting distance and lapName data with a loop. since there is multiple property's
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
            //getting start date of event
            DateOfEvent +=root.Element("query-response").Element("sport").Element("tournament_template").Element("tournament")
                .Element("tournament_stage").Element("event").Attribute("startdate").Value;
            //getting end position and result time for the cyclists. 
            foreach (XElement people in cyclists)
            {
                string endPos2 = " ";
                string resultTime = "1";
                
                var results = people.Elements("results");

                if (people.Element("results") != null)
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
                }
                else { endPos2 = "NC"; resultTime = "NC"; }

                //making the cyclist.
                Cyclists.Add(new Cyclist
                {
                    Name = people.Element("participant").Attribute("name").Value,
                    CountryOrigin = people.Element("participant").Attribute("countryFK").Value,
                    EndPosition = endPos2,
                    Gender = people.Element("participant").Attribute("gender").Value,
                    ResultTime = resultTime
                });
            }
        }

        /// <summary>
        /// A datatable where we add all the data from our list and then we create a xml file out of it through a foreach loop.
        /// </summary>
        public void Parser()
        {
            DataTable dt = new DataTable();

            dt.TableName = "Cyclist";
            dt.Columns.Add("name");
            dt.Columns.Add("gender");
            dt.Columns.Add("countryid");
            dt.Columns.Add("result-time");
            dt.Columns.Add("end-position");

            foreach (var item in Cyclists)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["name"] = item.Name;
                dt.Rows[dt.Rows.Count - 1]["gender"] = item.Gender;
                dt.Rows[dt.Rows.Count - 1]["countryid"] = item.CountryOrigin;
                dt.Rows[dt.Rows.Count - 1]["result-time"] = item.ResultTime;
                dt.Rows[dt.Rows.Count - 1]["end-position"] = item.EndPosition;
            }

            dt.WriteXml(Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/CyclistsNoAttributes.xml");
            MessageBox.Show("The cyclist data is parsed to XML and saved on your desktop!");

        }


        /// <summary>
        /// The validator takes the XML which we gives it. Then it validates with DTD given in the xml file, and if theres an error it will show the error in a messagebox.
        /// If the XML is valid according to the DTD it will show a messagebox the it is valid.
        /// </summary>
        static void Validator()
        {
            string fileName = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/Cycling-Tour-De-France.xml";
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.XmlResolver = new XmlUrlResolver();

            settings.ValidationType = ValidationType.DTD;
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(ValidationCallBack);
            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create(fileName, settings);

            // Parse the file.
            while (reader.Read())
            {
                System.Console.WriteLine("{0}, {1}: {2} ", reader.NodeType, reader.Name, reader.Value);
            }
            MessageBox.Show("Validated the DTD succesfully!");
        }

        private static void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == System.Xml.Schema.XmlSeverityType.Warning)
                MessageBox.Show("Warning: Matching schema not found.  No validation occurred." + e.Message);
            else // Error
                MessageBox.Show("Validation error: " + e.Message);
        }
    }
}
