
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace NetworkService.ViewModel
{
    public class CheckBoxData
    {
        public string Naziv { get; set; }
        public bool IsChecked { get; set; }

        public CheckBoxData(string naziv, bool isChecked)
        {
            Naziv = naziv;
            IsChecked = isChecked;
        }
    }

    public class MeasurementGraphModel : BindableBase
    {
        public MyICommand<CheckBoxData> CheckboxCommand { get; set; }
        public MyICommand OpenedComboBoxCommand { get; set; }
        
        public static ObservableCollection<CheckBoxData> CheckBoxProduction { get; set; } = new ObservableCollection<CheckBoxData>();
        public static ObservableCollection<Production> ProductionChartList { get; set; } = new ObservableCollection<Production>();

        public static ObservableCollection<Production> CheckedChartList { get; set; } = new ObservableCollection<Production>();
        public static GraphValues ElementHeights { get; set; } = new GraphValues();
      

        public BindingList<Production> Roads { get; set; }

        /// <summary>
        /// Prosledjuje vrednosti za svaku tacku
        /// Ako ima oznacenih u CheckedChartListi da samo njima prikazuje promene vrednosti
        /// </summary>
        /// <param name="dot">pozicija od Canvas.Top koliko treba da se pomeri od vrha kanvasa ka dole</param>
        /// <param name="value">vrednost puta</param>
        /// <param name="name">ime puta</param>

        public static void SetFirstDotAndValue(double dot, double value, string name)
        {
            if (CheckedChartList.Count != 0)
            {
                var generator = CheckedChartList.FirstOrDefault(g => g.Name == name);

                if (generator != null)
                {
                    ElementHeights.FirstDot = dot;
                    ElementHeights.FirstValue = value;
                    ElementHeights.FirstTime = DateTime.Now.ToString("HH:mm");
                }
            }
           /* else
            {
                ElementHeights.FirstDot = dot;
                ElementHeights.FirstValue = value;
                ElementHeights.FirstTime = DateTime.Now.ToString("HH:mm");
            }*/

        }

        /// <summary>
        /// Kada se oznaci put u checkboxu da ga doda u listu ili obrise da bi samo njegove 
        /// vrednosti koje pristizu iz Metering simulatora prikazao
        /// </summary>
        /// <param name="checkBoxData"></param>

        private void CheckBoxExecute(CheckBoxData checkBoxData)
        {
            Production forDisplay = new Production();

            if (checkBoxData.IsChecked)
            {
                var generator = ProductionChartList.FirstOrDefault(g => g.Name == checkBoxData.Naziv);
                CheckedChartList.Add(generator);
            }
            else
            {
                var generator = ProductionChartList.FirstOrDefault(g => g.Name == checkBoxData.Naziv);
                CheckedChartList.Remove(generator);
            }

        }

        

        public static double CalculateElementHeight(double value)
        {
            const double inputMin = 0.0;
            const double inputMax = 10.0;
            const double outputMin = 210;
            const double outputMax = 60;

            // ensure the input is within the expected range
            if (value < inputMin) value = inputMin;
            if (value > inputMax) value = inputMax;

            // perform the linear transformation
            double output = outputMin + ((value - inputMin) / (inputMax - inputMin)) * (outputMax - outputMin);

            return output;
        }

        public MeasurementGraphModel()
        {
            CheckboxCommand = new MyICommand<CheckBoxData>(CheckBoxExecute);
            OpenedComboBoxCommand = new MyICommand(OpenedCheckBox);

            ElementHeights.FirstDot = 210;
            ElementHeights.SecondDot = 210;
            ElementHeights.ThirdDot = 210;
            ElementHeights.FourthDot = 210;
            ElementHeights.FifthDot = 210;

        }

        /// <summary>
        /// Biranje puteva
        /// </summary>

        private void OpenedCheckBox()
        {
            CheckBoxProduction.Clear();
            CheckedChartList.Clear();
            foreach (Production ob in ProductionChartList)
            {
                CheckBoxProduction.Add(new CheckBoxData(ob.Name, false));
            }
        }

    }
}



