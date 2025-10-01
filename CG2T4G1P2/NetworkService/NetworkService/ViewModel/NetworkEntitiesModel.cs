using NetworkService.Model;
using Notification.Wpf;
using Notification.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Input;
using Type = NetworkService.Model.Type;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesModel : BindableBase
    {
        // Terminal komande
        public MyICommand Enter { get; set; }
        public MyICommand Repeat { get; set; }

        // Forma komande
        public MyICommand AddFromForm { get; set; }
        public MyICommand DeleteSelectedCommand { get; set; }
        private List<(string Action, Production Item)> undoHistory = new List<(string, Production)>();
        public MyICommand UndoCommand { get; set; }


        private bool isLoadingFromFile=false;

        // GUI filter komande
        public MyICommand ApplyFilterCommand { get; set; }

        public string textontextbox;
        public string textontextblock;
        public List<string> listaIskucanih = new List<string>();
        public ObservableCollection<Production> Obrisani { get; set; } = new ObservableCollection<Production>();
        public string LastActionExecuted;

        // Form bindable properties
        private string newId;
        private string newName;
        private Type newType;
        public string NewId
        {
            get => newId;
            set { newId = value; OnPropertyChanged(nameof(NewId)); }
        }

        public string NewName
        {
            get => newName;
            set { newName = value; OnPropertyChanged(nameof(NewName)); }
        }

        public Type NewType
        {
            get => newType;
            set { newType = value; OnPropertyChanged(nameof(NewType)); }
        }

        // GUI filter properties
        private Type selectedFilterType;
        public Type SelectedFilterType
        {
            get => selectedFilterType;
            set { selectedFilterType = value; OnPropertyChanged(nameof(SelectedFilterType)); }
        }

        private string filterId;
        public string FilterId
        {
            get => filterId;
            set { filterId = value; OnPropertyChanged(nameof(FilterId)); }
        }

        private bool filterGreater;
        public bool FilterGreater
        {
            get => filterGreater;
            set { filterGreater = value; OnPropertyChanged(nameof(FilterGreater)); }
        }

        private bool filterLess;
        public bool FilterLess
        {
            get => filterLess;
            set { filterLess = value; OnPropertyChanged(nameof(FilterLess)); }
        }

        private bool filterEqual;
        public bool FilterEqual
        {
            get => filterEqual;
            set { filterEqual = value; OnPropertyChanged(nameof(FilterEqual)); }
        }

        // Tipovi
        Type T1 = new Type { Name = "Solar", Img_src = AppDomain.CurrentDomain.BaseDirectory + "Slike\\Solar.jpg" };
        Type T2 = new Type { Name = "WindTurbine", Img_src = AppDomain.CurrentDomain.BaseDirectory + "Slike\\WindTurbine.jpg" };

        // Kolekcije
        public static ObservableCollection<Production> Production { get; private set; } = new ObservableCollection<Production>();
        public static ObservableCollection<Production> Izabrani { get; private set; } = new ObservableCollection<Production>();
        public static ObservableCollection<Type> Tipovi { get; private set; }

        // Fajl za trajno čuvanje
        private readonly string storageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "entiteti.txt");

        public NetworkEntitiesModel()
        {
            Enter = new MyICommand(EnterAction);
            Repeat = new MyICommand(RepeatLastAction);
            AddFromForm = new MyICommand(AddManualAction);
            DeleteSelectedCommand = new MyICommand(DeleteSelectedAction);
            UndoCommand = new MyICommand(Undo);

            ApplyFilterCommand = new MyICommand(ApplyFilterAction);

            Tipovi = new ObservableCollection<Type> { T1, T2 };

            if (Production == null) Production = new ObservableCollection<Production>();
            if (Izabrani == null) Izabrani = new ObservableCollection<Production>();

            // Učitavanje entiteta pri startu
            LoadFromFile();

            // Ovde dodajemo početnu poruku
            TextOnTextBlock = ">>Type 'help' to display valid commands!\n";
        }

        #region Terminal

        public void RepeatLastAction() => TextOnTextBox = LastActionExecuted;

        public string TextOnTextBox
        {
            get => textontextbox;
            set
            {
                if (textontextbox != value)
                {
                    textontextbox = value;
                    OnPropertyChanged(nameof(TextOnTextBox));
                }
            }
        }

        public string TextOnTextBlock
        {
            get => textontextblock;
            set
            {
                if (textontextblock != value)
                {
                    textontextblock = value;
                    OnPropertyChanged(nameof(TextOnTextBlock));
                }
            }
        }

        public void EnterAction()
        {
             string[] razmak = TextOnTextBox.Split(' ');
            if (razmak.Length == 0) return;

            string cmd = razmak[0].ToLower();

            switch (cmd)
            {
                case "add":
                    if (razmak.Length != 4)
                        TextOnTextBlock += ">>Incorrect parameters entered!\n";
                    else
                    {
                        Add(razmak[1], razmak[2].Trim(), razmak[3].Trim());
                        LastActionExecuted = TextOnTextBox;
                    }
                    break;

                case "remove":
                    if (razmak.Length != 2)
                        TextOnTextBlock += ">>Incorrect parameters entered!\n";
                    else
                    {
                        Remove(razmak[1]);
                        LastActionExecuted = TextOnTextBox;
                    }
                    break;

                case "filter":
                    if (razmak.Length < 2 || razmak.Length > 4)
                        TextOnTextBlock += ">>Incorrect parameters entered!\n";
                    else
                    {
                        if (razmak.Length == 2)
                            Filter("-1", razmak[1].Trim(), "");
                        else if (razmak.Length == 3)
                        {
                            if (razmak[2] == ">" || razmak[2] == "<" || razmak[2] == "=")
                                Filter(razmak[1], "", razmak[2].Trim());
                            else
                                Filter(razmak[1], razmak[2].Trim(), "");
                        }
                        else
                            Filter(razmak[1], razmak[2].Trim(), razmak[3].Trim());

                        LastActionExecuted = TextOnTextBox;
                    }
                    break;

                case "repeat":
                    if (!string.IsNullOrEmpty(LastActionExecuted))
                    {
                        TextOnTextBox = LastActionExecuted;
                        EnterAction(); // izvršava poslednju komandu
                    }
                    break;

                case "help":
                    LastActionExecuted = razmak[0];
                    TextOnTextBlock += "COMMANDS!!!\n >>FOR ADD NEW(add id name tip)\nTip:Solar, WindTurbine\n" +
                        ">>FOR DELETE(remove id)\n" +
                        ">>FOR FILTER(filter id type >,<,=)\n" +
                        ">>FOR UNDO(Ctrl+Z)\n"+
                        ">>FOR NAVIGATION (← →)";
                    TextOnTextBox = string.Empty;
                    break;

                case "refresh":
                    Refresh();
                    LastActionExecuted = razmak[0];
                    TextOnTextBlock = string.Empty;
                    TextOnTextBox = string.Empty;
                    break;

                case "undo":
                    Undo();
                    TextOnTextBox = string.Empty;
                    break;

                default:
                    TextOnTextBlock += ">>Type 'help' to display valid commands\"!\n";
                    break;
            }

            if (!string.IsNullOrEmpty(TextOnTextBox))
                listaIskucanih.Add(TextOnTextBox);

            TextOnTextBox = string.Empty;
        }

        public void Undo()
        {
            if (!undoHistory.Any())
            {
                TextOnTextBlock += ">>No steps to undo\n";
                return;
            }

            var last = undoHistory.Last();
            undoHistory.RemoveAt(undoHistory.Count - 1);

            if (last.Action == "add")
            {
                Production.Remove(last.Item);
                Izabrani.Remove(last.Item);
                NetworkDisplayModel.listaStavki.Remove(last.Item);
                MeasurementGraphModel.ProductionChartList.Remove(last.Item);
                TextOnTextBlock += $">>Addition undone: {last.Item.Name}\n";
            }
            else if (last.Action == "remove")
            {
                Production.Add(last.Item);
                Izabrani.Add(last.Item);
                NetworkDisplayModel.listaStavki.Add(last.Item);
                MeasurementGraphModel.ProductionChartList.Add(last.Item);
                TextOnTextBlock += $">>Deletion undone: {last.Item.Name}\n";
            }
            else if (last.Action == "filter")
            {
                Refresh();
                TextOnTextBlock += ">>Filter undone\n";
            }

            SaveToFile();
            OnPropertyChanged("Production");
            OnPropertyChanged("Izabrani");
        }

        #endregion

        #region Production CRUD

        public void Add(string id, string name, string type)
        {
            if (!int.TryParse(id, out int broj))
            {
                TextOnTextBlock += ">>ID must be a number\n";
                return;
            }

            var tip = Tipovi.FirstOrDefault(t => t.Name == type);
            if (tip == null)
            {
                TextOnTextBlock += ">>This type does not exist.\n";
                return;
            }

            if (Production.Any(p => p.Id == broj))
            {
                if (isLoadingFromFile == false)
                {
                    TextOnTextBlock += ">>An item with this ID already exists.\n";
                }
                return;
            }

            if (Production.Any(p => p.Name == name))
            {
                TextOnTextBlock += ">>An item with this name already exists.\n";
                return;
            }

            var novi = new Production { Id = broj, Name = name, Type = tip };
            Production.Add(novi);
            Izabrani.Add(novi);
            
            undoHistory.Add(("add", novi));
            NetworkDisplayModel.listaStavki.Add(novi);
            MeasurementGraphModel.ProductionChartList.Add(novi);

            foreach (var o in Obrisani.ToList())
                if (o.Id == broj) Obrisani.Remove(o);

            SaveToFile();
            OnPropertyChanged("Production");
            OnPropertyChanged("Izabrani");
        }

        private void Remove(string id)
        {
            if (!int.TryParse(id, out int broj))
            {
                TextOnTextBlock += ">>ID must be a number\n";
                return;
            }

            var toRemove = Production.Where(p => p.Id == broj).ToList();
            if (!toRemove.Any())
            {
                TextOnTextBlock += ">>The specified ID does not exist!\n";
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete the selected items?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            foreach (var p in toRemove)
            {
                Production.Remove(p);
                Izabrani.Remove(p);
                Obrisani.Add(p);
                undoHistory.Add(("remove", p));
                MessageBox.Show($"Successfully deleted", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                NetworkDisplayModel.listaStavki.Remove(p);
                MeasurementGraphModel.ProductionChartList.Remove(p);
            }



            SaveToFile();
            OnPropertyChanged("Production");
            OnPropertyChanged("Izabrani");
        }

        public void Refresh()
        {
            Izabrani.Clear();
            foreach (var p in Production)
                Izabrani.Add(p);
            OnPropertyChanged("Izabrani");
        }

        public void Filter(string id, string type, string znak)
        {
            undoHistory.Add(("filter", null));

            if (!int.TryParse(id, out int broj)) broj = -1;

            var filtered = Production.AsEnumerable();

            if (broj != -1 && !string.IsNullOrEmpty(znak))
            {
                switch (znak)
                {
                    case ">": filtered = filtered.Where(p => p.Id > broj); break;
                    case "<": filtered = filtered.Where(p => p.Id < broj); break;
                    case "=": filtered = filtered.Where(p => p.Id == broj); break;
                }
            }
            else if (broj != -1)
                filtered = filtered.Where(p => p.Id == broj);

            if (!string.IsNullOrEmpty(type))
                filtered = filtered.Where(p => p.Type.Name == type);

            Izabrani.Clear();
            foreach (var p in filtered)
                Izabrani.Add(p);

            OnPropertyChanged("Izabrani");
        }

        private void AddManualAction()
        {
            if (string.IsNullOrWhiteSpace(NewId) || string.IsNullOrWhiteSpace(NewName) || NewType == null)
            {
                TextOnTextBlock += ">>Popuni sva polja za dodavanje!\n";
                return;
            }
            // Provera da li ID ili Name već postoji
            if (int.TryParse(NewId, out int number))
            {
                if (Production.Any(p => p.Id == number))
                {
                    MessageBox.Show($"ID {NewId} already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("ID must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Production.Any(p => p.Name == NewName))
            {
                MessageBox.Show($"ID {NewId} already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Add(NewId, NewName, NewType.Name);
            if (isLoadingFromFile == false)
            {
                MessageBox.Show($"Successfully added", "Add", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            NewId = string.Empty;
            NewName = string.Empty;
            NewType = null;
        }

        #endregion

        #region Delete Selected

        private void DeleteSelectedAction()
        {
            var selectedItems = Production.Where(p => p.IsSelected).ToList();

            if (!selectedItems.Any())
            {
                TextOnTextBlock += ">>No items selected for deletion.\n";
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete the selected items?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            foreach (var p in selectedItems)
            {
                Production.Remove(p);
                Izabrani.Remove(p);
                Obrisani.Add(p);
                undoHistory.Add(("remove", p));
                NetworkDisplayModel.listaStavki.Remove(p);
                MeasurementGraphModel.ProductionChartList.Remove(p);
                TextOnTextBlock += $">>Obrisano: {p.Name}\n";
            }


            SaveToFile();
            OnPropertyChanged("Production");
            OnPropertyChanged("Izabrani");
        }

        #endregion

        #region GUI Filter Logic

        private void ApplyFilterAction()
        {
            int idValue = -1;
            if (!string.IsNullOrEmpty(FilterId) && !int.TryParse(FilterId, out idValue))
            {
                TextOnTextBlock += ">>ID mora biti broj!\n";
                return;
            }

            string znak = "";
            if (FilterGreater) znak = ">";
            else if (FilterLess) znak = "<";
            else if (FilterEqual) znak = "=";

            string typeName = SelectedFilterType?.Name ?? "";

            Filter(idValue == -1 ? "-1" : idValue.ToString(), typeName, znak);
        }

        #endregion

        #region File Persistence

        private void SaveToFile()
        {
            var lines = Production.Select(p => $"{p.Id},{p.Name},{p.Type.Name}");
            File.WriteAllLines(storageFile, lines);
        }

        private void LoadFromFile()
        {
            isLoadingFromFile = true;
            if (!File.Exists(storageFile)) return;

            var lines = File.ReadAllLines(storageFile);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                  Add(parts[0], parts[1], parts[2]);
            }
            isLoadingFromFile = false;
        }

        #endregion
    }
}