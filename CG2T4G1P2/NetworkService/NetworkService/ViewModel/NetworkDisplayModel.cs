using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayModel : BindableBase
    {
        public UserControl CurrentUserControl { get; set; }

        private static Production selektovano = new Production();
        private Production trenutnaStavka = new Production();
        public MyICommand<ListView> Promena { get; set; }
        public MyICommand PustenMis { get; set; }
        public MyICommand<Canvas> UzmiCanvas { get; set; }
        public MyICommand<Canvas> DropFunkcija { get; set; }
        public MyICommand<Canvas> DragOverFunkcija { get; set; }
        public MyICommand<Canvas> ButtonFunkcija { get; set; }
        public static ObservableCollection<Production> listaStavki { get; set; } = new ObservableCollection<Production>();
        private bool dragging = false;

        public static Dictionary<int, Production> KanvasKomponente { get; set; }

        public List<Canvas> filledCanvases = new List<Canvas>();
        

        private Canvas draggedCanvas;
        private bool fromList = true;

        //teskt
        private string t1;
        private string t2;
        private string t3;
        private string t4;
        private string t5;
        private string t6;
        private string t7;
        private string t8;
        private string t9;
        private string t10;
        private string t11;
        private string t12;

        //slike
        private string s1;
        private string s2;
        private string s3;
        private string s4;
        private string s5;
        private string s6;
        private string s7;
        private string s8;
        private string s9;
        private string s10;
        private string s11;
        private string s12;

        public string Tekst1
        {
            get { return t1; }
            set
            {
                if (t1 != value)
                {
                    t1 = value;
                    OnPropertyChanged("Tekst1");
                }
            }
        }
        public string Tekst2
        {
            get { return t2; }
            set
            {
                if (t2 != value)
                {
                    t2 = value;
                    OnPropertyChanged("Tekst2");
                }
            }
        }
        public string Tekst3
        {
            get { return t3; }
            set
            {
                if (t3 != value)
                {
                    t3 = value;
                    OnPropertyChanged("Tekst3");
                }
            }
        }
        public string Tekst4
        {
            get { return t4; }
            set
            {
                if (t4 != value)
                {
                    t4 = value;
                    OnPropertyChanged("Tekst4");
                }
            }
        }
        public string Tekst5
        {
            get { return t5; }
            set
            {
                if (t5 != value)
                {
                    t5 = value;
                    OnPropertyChanged("Tekst5");
                }
            }
        }
        public string Tekst6
        {
            get { return t6; }
            set
            {
                if (t6 != value)
                {
                    t6 = value;
                    OnPropertyChanged("Tekst6");
                }
            }
        }
        public string Tekst7
        {
            get { return t7; }
            set
            {
                if (t7 != value)
                {
                    t7 = value;
                    OnPropertyChanged("Tekst7");
                }
            }
        }
        public string Tekst8
        {
            get { return t8; }
            set
            {
                if (t8 != value)
                {
                    t8 = value;
                    OnPropertyChanged("Tekst8");
                }
            }
        }
        public string Tekst9
        {
            get { return t9; }
            set
            {
                if (t9 != value)
                {
                    t9 = value;
                    OnPropertyChanged("Tekst9");
                }
            }
        }
        public string Tekst10
        {
            get { return t10; }
            set
            {
                if (t10 != value)
                {
                    t10 = value;
                    OnPropertyChanged("Tekst10");
                }
            }
        }
        public string Tekst11
        {
            get { return t11; }
            set
            {
                if (t11 != value)
                {
                    t11 = value;
                    OnPropertyChanged("Tekst11");
                }
            }
        }
        public string Tekst12
        {
            get { return t12; }
            set
            {
                if (t12 != value)
                {
                    t12 = value;
                    OnPropertyChanged("Tekst12");
                }
            }
        }


        public string Slika1
        {
            get { return s1; }
            set
            {
                if (s1 != value)
                {
                    s1 = value;
                    OnPropertyChanged("Slika1");
                }
            }
        }
        public string Slika2
        {
            get { return s2; }
            set
            {
                if (s2 != value)
                {
                    s2 = value;
                    OnPropertyChanged("Slika2");
                }
            }
        }
        public string Slika3
        {
            get { return s3; }
            set
            {
                if (s3 != value)
                {
                    s3 = value;
                    OnPropertyChanged("Slika3");
                }
            }
        }
        public string Slika4
        {
            get { return s4; }
            set
            {
                if (s4 != value)
                {
                    s4 = value;
                    OnPropertyChanged("Slika4");
                }
            }
        }
        public string Slika5
        {
            get { return s5; }
            set
            {
                if (s5 != value)
                {
                    s5 = value;
                    OnPropertyChanged("Slika5");
                }
            }
        }
        public string Slika6
        {
            get { return s6; }
            set
            {
                if (s6 != value)
                {
                    s6 = value;
                    OnPropertyChanged("Slika6");
                }
            }
        }
        public string Slika7
        {
            get { return s7; }
            set
            {
                if (s7 != value)
                {
                    s7 = value;
                    OnPropertyChanged("Slika7");
                }
            }
        }
        public string Slika8
        {
            get { return s8; }
            set
            {
                if (s8 != value)
                {
                    s8 = value;
                    OnPropertyChanged("Slika8");
                }
            }
        }
        public string Slika9
        {
            get { return s9; }
            set
            {
                if (s9 != value)
                {
                    s9 = value;
                    OnPropertyChanged("Slika9");
                }
            }
        }
        public string Slika10
        {
            get { return s10; }
            set
            {
                if (s10 != value)
                {
                    s10 = value;
                    OnPropertyChanged("Slika10");
                }
            }
        }
        public string Slika11
        {
            get { return s11; }
            set
            {
                if (s11 != value)
                {
                    s11 = value;
                    OnPropertyChanged("Slika11");
                }
            }
        }
        public string Slika12
        {
            get { return s12; }
            set
            {
                if (s12 != value)
                {
                    s12 = value;
                    OnPropertyChanged("Slika12");
                }
            }
        }

        public NetworkDisplayModel(UserControl userControl)
        {
            CurrentUserControl = userControl;

            Promena = new MyICommand<ListView>(SelectionChanged);
            DropFunkcija = new MyICommand<Canvas>(Drop);
            ButtonFunkcija = new MyICommand<Canvas>(ButtonFja);
            DragOverFunkcija = new MyICommand<Canvas>(DragOver);
            PustenMis = new MyICommand(MouseLeftButtonUp);
            UzmiCanvas = new MyICommand<Canvas>(UzimanjeCanvasa);



            checkCanvasTimer.Elapsed += CheckCanvas;
            checkCanvasTimer.AutoReset = true;
            checkCanvasTimer.Enabled = true;

            if (listaStavki == null)
                listaStavki = new ObservableCollection<Production>();
            if (KanvasKomponente == null)
            {
                KanvasKomponente = new Dictionary<int, Production>();
                Tekst1 = "PRAZNO";
                Slika1 = "";

                Tekst2 = "PRAZNO";
                Slika2 = "";

                Tekst3 = "PRAZNO";
                Slika3 = "";

                Tekst4 = "PRAZNO";
                Slika4 = "";

                Tekst5 = "PRAZNO";
                Slika5 = "";

                Tekst6 = "PRAZNO";
                Slika6 = "";

                Tekst7 = "PRAZNO";
                Slika7 = "";

                Tekst8 = "PRAZNO";
                Slika8 = "";

                Tekst9 = "PRAZNO";
                Slika9 = "";

                Tekst10 = "PRAZNO";
                Slika10 = "";

                Tekst11 = "PRAZNO";
                Slika11 = "";

                Tekst12 = "PRAZNO";
                Slika12 = "";


            }
        }

        /// <summary>
        /// Kupi na klik kanvas koji je ispod misa
        /// </summary>
        /// <param name="canvas"></param>

        private void UzimanjeCanvasa(Canvas canvas)
        {
            if(canvas.Resources["taken"] != null)
            {
                string numberPart = canvas.Name.Substring("canvas".Length); 
                int number = Int32.Parse(numberPart);

                selektovano = KanvasKomponente[number];

                if (!dragging && selektovano != null)
                {
                    dragging = true;
                    fromList = false;
                    draggedCanvas = canvas;
                    KanvasKomponente.Remove(selektovano.Id);
                    DragDrop.DoDragDrop(canvas, selektovano, DragDropEffects.Copy | DragDropEffects.Move);

                    
                    canvas.Resources.Remove("taken");
                    
                }
            }
        }

        /// <summary>
        /// Brise kanvas sa kojeg je prevuceno
        /// </summary>

        private void IzbrisiStariCanvas()
        {
            if (draggedCanvas == null)
                return;
            string numberPart = draggedCanvas.Name.Substring("canvas".Length);

            switch (draggedCanvas.Name)
            {
                case "canvas1":
                    Tekst1 = "EMPTY";
                    Slika1 = "";
                    break;
                case "canvas2":
                    Tekst2 = "EMPTY";
                    Slika2 = "";
                    break;
                case "canvas3":
                    Tekst3 = "EMPTY";
                    Slika3 = "";
                    break;
                case "canvas4":
                    Tekst4 = "EMPTY";
                    Slika4 = "";
                    break;
                case "canvas5":
                    Tekst5 = "EMPTY";
                    Slika5 = "";
                    break;
                case "canvas6":
                    Tekst6 = "EMPTY";
                    Slika6 = "";
                    break;
                case "canvas7":
                    Tekst7 = "EMPTY";
                    Slika7 = "";
                    break;
                case "canvas8":
                    Tekst8 = "EMPTY";
                    Slika8 = "";
                    break;
                case "canvas9":
                    Tekst9 = "EMPTY";
                    Slika9 = "";
                    break;
                case "canvas10":
                    Tekst10 = "EMPTY";
                    Slika10 = "";
                    break;
                case "canvas11":
                    Tekst11 = "EMPTY";
                    Slika11 = "";
                    break;
                case "canvas12":
                    Tekst12 = "EMPTY";
                    Slika12 = "";
                    break;

            }

            KanvasKomponente.Remove(Int32.Parse(numberPart));
            filledCanvases.Remove(draggedCanvas);
            draggedCanvas.Resources.Remove("taken");
            draggedCanvas = null;
        }

        /// <summary>
        /// Prevlacenje puta na kanvas
        /// </summary>
        /// <param name="obj"></param>
        private void DragOver(Canvas obj)
        {
            if (obj.Resources["taken"] != null)
            {
                obj.AllowDrop = false;
                
            }
            else
            {
                obj.AllowDrop = true;
            }
        }

        /// <summary>
        /// Pustanje kanvasa
        /// </summary>

        private void MouseLeftButtonUp()
        {
            TrenutnaStavka = null;
            selektovanaStavka = null;
            dragging = false;
        }
        public Production selektovanaStavka
        {
            get
            {
                return selektovano;
            }
            set
            {
                selektovano = value;
                OnPropertyChanged("selektovanaStavka");
            }
        }

        public Production TrenutnaStavka
        {
            get
            {
                return trenutnaStavka;
            }
            set
            {
                trenutnaStavka = value;
                OnPropertyChanged("TrenutnaStavka");
            }
        }

        /// <summary>
        /// Kada se prevlaci sa liste
        /// </summary>
        /// <param name="list"></param>

        public void SelectionChanged(ListView list)
        {
            if (!dragging)
            {
                dragging = true;
                TrenutnaStavka = selektovanaStavka;
                DragDrop.DoDragDrop(list, TrenutnaStavka, DragDropEffects.Copy | DragDropEffects.Move);
                fromList = true;
            }

        }

        /// <summary>
        /// Kada se pusti na kanvas ovo se izvrsava
        /// </summary>
        /// <param name="list"></param>

        public void Drop(Canvas canvas)
        {
            if (selektovano != null)
            {
                if (canvas.Resources["taken"] == null)
                {
                    (canvas).Resources.Add("taken", true);

                    bool isInvalid = (selektovano.Type.Name == "Solar" && selektovano.Value > 5)
                    || (selektovano.Type.Name == "WindTurbine" && selektovano.Value > 5);

                    string numberPart = canvas.Name.Substring("canvas".Length);
                    int number = Int32.Parse(numberPart);

                    switch (number)
                    {
                        case 1:
                            Tekst1 = GetTekstValue(isInvalid, selektovano);
                            Slika1 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(1, selektovano);
                            break;
                        case 2:
                            Tekst2 = GetTekstValue(isInvalid, selektovano);
                            Slika2 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(2, selektovano);
                            break;
                        case 3:
                            Tekst3 = GetTekstValue(isInvalid, selektovano);
                            Slika3 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(3, selektovano);
                            break;
                        case 4:
                            Tekst4 = GetTekstValue(isInvalid, selektovano);
                            Slika4 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(4, selektovano);
                            break;
                        case 5:
                            Tekst5 = GetTekstValue(isInvalid, selektovano);
                            Slika5 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(5, selektovano);
                            break;
                        case 6:
                            Tekst6 = GetTekstValue(isInvalid, selektovano);
                            Slika6 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(6, selektovano);
                            break;
                        case 7:
                            Tekst7 = GetTekstValue(isInvalid, selektovano);
                            Slika7 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(7, selektovano);
                            break;
                        case 8:
                            Tekst8 = GetTekstValue(isInvalid, selektovano);
                            Slika8 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(8, selektovano);
                            break;
                        case 9:
                            Tekst9 = GetTekstValue(isInvalid, selektovano);
                            Slika9 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(9, selektovano);
                            break;
                        case 10:
                            Tekst10 = GetTekstValue(isInvalid, selektovano);
                            Slika10 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(10, selektovano);
                            break;
                        case 11:
                            Tekst11 = GetTekstValue(isInvalid, selektovano);
                            Slika11 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(11, selektovano);
                            break;
                        case 12:
                            Tekst12 = GetTekstValue(isInvalid, selektovano);
                            Slika12 = GetSlikaValue(isInvalid, selektovano, "Slike\\stop.png");
                            KanvasKomponente.Add(12, selektovano);
                            break;
                    }

                    filledCanvases.Add(canvas);

                    if (fromList)
                        listaStavki.Remove(selektovanaStavka);

                    if (!fromList)
                    {
                        IzbrisiStariCanvas();
                    }

                    ConnectCanvasesWithOneLine(filledCanvases);

                }
                selektovanaStavka = null;
                dragging = false;
            }
        }

        string GetTekstValue(bool isInvalid, Production selektovano)
        {
            return isInvalid ? selektovano.ToString() + " INVALID VALUE" : selektovano.ToString();
        }

        string GetSlikaValue(bool isInvalid, Production selektovano, string defaultSlika)
        {
            return isInvalid ? AppDomain.CurrentDomain.BaseDirectory + defaultSlika : selektovano.Type.Img_src;
        }

        /// <summary>
        /// Brise na dugme sa kanvasa
        /// </summary>
        /// <param name="canvas"></param>

        public void ButtonFja(Canvas canvas)
        {
            try
            {
                int id = int.Parse(((TextBlock)canvas.Children[1]).Text.Split(' ')[1].Trim());

                switch (canvas.Name)
                {
                    case "canvas1":
                        Tekst1 = "EMPTY";
                        Slika1 = "";
                        break;
                    case "canvas2":
                        Tekst2 = "EMPTY";
                        Slika2 = "";
                        break;
                    case "canvas3":
                        Tekst3 = "EMPTY";
                        Slika3 = "";
                        break;
                    case "canvas4":
                        Tekst4 = "EMPTY";
                        Slika4 = "";
                        break;
                    case "canvas5":
                        Tekst5 = "EMPTY";
                        Slika5 = "";
                        break;
                    case "canvas6":
                        Tekst6 = "EMPTY";
                        Slika6 = "";
                        break;
                    case "canvas7":
                        Tekst7 = "EMPTY";
                        Slika7 = "";
                        break;
                    case "canvas8":
                        Tekst8 = "EMPTY";
                        Slika8 = "";
                        break;
                    case "canvas9":
                        Tekst9 = "EMPTY";
                        Slika9 = "";
                        break;
                    case "canvas10":
                        Tekst10 = "EMPTY";
                        Slika10 = "";
                        break;
                    case "canvas11":
                        Tekst11 = "EMPTY";
                        Slika11 = "";
                        break;
                    case "canvas12":
                        Tekst12 = "EMPTY";
                        Slika12 = "";
                        break;

                }

                foreach (KeyValuePair<int, Production> ulaz in KanvasKomponente)
                {
                    if (ulaz.Value.Id == id)
                    {
                        listaStavki.Add(ulaz.Value);
                        KanvasKomponente.Remove(ulaz.Key);
                        break;
                    }
                }
                filledCanvases.Remove(canvas);
                canvas.Resources.Remove("taken");
                fromList = true;
                ConnectCanvasesWithOneLine(filledCanvases);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Pokrece se da proverava Da li su vrednosti u validnim opsezima svake 2 sekunde
        /// ako nije onda postavlja sliku na nevalidnu, ako jeste vraca na sliku puta
        /// </summary>

        System.Timers.Timer checkCanvasTimer = new System.Timers.Timer(2000);

        private void CheckCanvas(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Iterate through each canvas and check for invalid values
            foreach (var kvp in KanvasKomponente)
            {
                var canvasNumber = kvp.Key;
                var currentCanvas = kvp.Value;

                // Check if the value is invalid based on the type of selektovano
                bool isInvalid = (currentCanvas.Type.Name == "Solar" && currentCanvas.Value > 5)
                    || (currentCanvas.Type.Name == "WindTurbine" && currentCanvas.Value > 5);

                switch (canvasNumber)
                {
                    case 1:
                        if (isInvalid)
                        {
                            Tekst1 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika1 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst1 = currentCanvas.ToString();
                            Slika1 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 2:
                        if (isInvalid)
                        {
                            Tekst2 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika2 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst2 = currentCanvas.ToString();
                            Slika2 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 3:
                        if (isInvalid)
                        {
                            Tekst3 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika3 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst3 = currentCanvas.ToString();
                            Slika3 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 4:
                        if (isInvalid)
                        {
                            Tekst4 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika4 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst4 = currentCanvas.ToString();
                            Slika4 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 5:
                        if (isInvalid)
                        {
                            Tekst5 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika5 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst5 = currentCanvas.ToString();
                            Slika5 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 6:
                        if (isInvalid)
                        {
                            Tekst6 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika6 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst6 = currentCanvas.ToString();
                            Slika6 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 7:
                        if (isInvalid)
                        {
                            Tekst7 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika7 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst7 = currentCanvas.ToString();
                            Slika7 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 8:
                        if (isInvalid)
                        {
                            Tekst8 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika8 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst8 = currentCanvas.ToString();
                            Slika8 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 9:
                        if (isInvalid)
                        {
                            Tekst9 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika9 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst9 = currentCanvas.ToString();
                            Slika9 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 10:
                        if (isInvalid)
                        {
                            Tekst10 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika10 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst10 = currentCanvas.ToString();
                            Slika10 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 11:
                        if (isInvalid)
                        {
                            Tekst11 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika11 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst11 = currentCanvas.ToString();
                            Slika11 = currentCanvas.Type.Img_src;
                        }
                        break;
                    case 12:
                        if (isInvalid)
                        {
                            Tekst12 = currentCanvas.ToString() + " INVALID VALUE";
                            Slika12 = AppDomain.CurrentDomain.BaseDirectory + "Slike\\stop.png";
                        }
                        else
                        {
                            Tekst12 = currentCanvas.ToString();
                            Slika12 = currentCanvas.Type.Img_src;
                        }
                        break;

                }
            }
        }

        /// <summary>
        /// Racuna i kreira linije kojima se povezuje svaki kanvas
        /// </summary>
        /// <param name="canvases"></param>

        private void ConnectCanvasesWithOneLine(List<Canvas> canvases)
        {

            Canvas canvasLines = (Canvas)CurrentUserControl.FindName("CanvasLines");

            // Remove existing lines
            var existingLines = canvasLines.Children.OfType<Line>().ToList();
            foreach (var line in existingLines)
            {
                canvasLines.Children.Remove(line);
            }
            

            for (int i = 0; i < canvases.Count - 1; i++)
            {
                Canvas canvas1 = canvases[i];
                Canvas canvas2 = canvases[i + 1];


                // Calculate the center points of the canvases
                Point canvas1Center = getPointCanvas(canvas1);
                Point canvas2Center = getPointCanvas(canvas2);
        

                // Create a Line object
                UIElement line = new Line
                {
                    X1 = canvas1Center.X,
                    Y1 = canvas1Center.Y,
                    X2 = canvas2Center.X,
                    Y2 = canvas2Center.Y,
                    Stroke = Brushes.DarkRed,
                    StrokeThickness = 2,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round,
                    IsHitTestVisible = false,
                };

                // Add the Line object to the Grid

                canvasLines.Children.Add(line);
            }
        }

        /// <summary>
        /// Za svaku poziciju kanvasa se uzima njegov centar iz ove funkcije
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>

        private Point getPointCanvas(Canvas canvas)
        {
          
            // Translate center of the canvas to CanvasLines coordinates
            Canvas canvasLines = (Canvas)CurrentUserControl.FindName("CanvasLines");
            Point center = new Point(canvas.Width / 2, canvas.Height / 2);
            Point translated = canvas.TranslatePoint(center, canvasLines);
            return translated;
        }

    }
}
