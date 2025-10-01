using NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Production : INotifyPropertyChanged
    {

        private int id;
        private string name;
        private Type type;
        private double value;      
        private bool isSelected;

        public ObservableCollection<double> Values { get; set; } = new ObservableCollection<double>();


        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        public int Id
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public Type Type
        {
            get { return this.type; }
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    RaisePropertyChanged("Type");
                }
            }
        }

        public double Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;    
                    RaisePropertyChanged("Value");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public override string ToString()
        {
            return $"Object {Id} : {Name}"; ;
        }

        public Production(int id, string name, Type type, double value)
        {
            Id = id;
            Name = name;
            Type = type;
            Value = value;
        }

        public Production()
        {
        }
    }
}
