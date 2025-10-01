using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private int count; // Inicijalna vrednost broja objekata u sistemu
        private NetworkEntitiesModel entitiesViewModel = new NetworkEntitiesModel();
        private NetworkDisplayModel displayViewModel = new NetworkDisplayModel(new Views.NetworkDisplay());
        private MeasurementGraphModel graphViewModel = new MeasurementGraphModel();
        private BindableBase currentViewModel;
        public MyICommand DeleteSelectedCommand { get; set; }
        public MyICommand LeftArrow { get; set; }

      
        public void LeftArrowAction()
        {
            if (CurrentViewModel == entitiesViewModel)
            {
                CurrentViewModel = graphViewModel; // ide unazad od prve na poslednju
            }
            else if (CurrentViewModel == displayViewModel)
            {
                CurrentViewModel = entitiesViewModel;
            }
            else if (CurrentViewModel == graphViewModel)
            {
                CurrentViewModel = displayViewModel;
            }
        }

        public MyICommand Tab { get; set; }
        public MyICommand<string> NavCommand { get; private set; }
        public static string path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

        public MainWindowViewModel()
        {
            count = NetworkEntitiesModel.Production.Count;
            createListener(); //Povezivanje sa serverskom aplikacijom           
            NavCommand = new MyICommand<string>(OnNav);
            LeftArrow = new MyICommand(LeftArrowAction);
            Tab = new MyICommand(TabAction);
            File.WriteAllText(path, "");
            CurrentViewModel = entitiesViewModel;
        }

        /// <summary>
        /// Menja se na naredni view kad se pritisne tab dugme
        /// </summary>

        public void TabAction()
        {
            if (CurrentViewModel == entitiesViewModel)
            {
                CurrentViewModel = displayViewModel;
            }
            else if (CurrentViewModel == displayViewModel)
            {
                CurrentViewModel = graphViewModel;
            }
            else if (CurrentViewModel == graphViewModel)
            {
                CurrentViewModel = entitiesViewModel;
            }
        }


        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        /// <summary>
        /// Kada se pritisne neki od tabova za izmenu view-a onda se ova funkcija izvrsi
        /// </summary>
        /// <param name="destination"></param>

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "entities":
                    CurrentViewModel = entitiesViewModel;
                    break;
                case "display":
                    CurrentViewModel = displayViewModel;
                    break;
                case "graph":
                    CurrentViewModel = graphViewModel;
                    break;
            }
        }

        /// <summary>
        /// Ispisuje u log.txt liniju
        /// </summary>
        /// <param name="txt"></param>

        private void writer(string txt)
        {
            using (StreamWriter outputFile = File.AppendText(path))
            {

                outputFile.WriteLine(txt + ":#" + DateTime.Now + ",");
            }

        }

        static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(NetworkEntitiesModel.Production.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            writer(incomming);
                            AddValueToObject(incomming);
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        /// <summary>
        /// Ovim se procita sta je pristiglo od Metering simulatora i onda se izmeni u listi puteva vrednost
        /// </summary>
        /// <param name="txt"></param>

        private void AddValueToObject(string txt)
        {
            int objectIndex = int.Parse((txt.Split('_')[1]).Split(':')[0]);
            double objectValue = double.Parse((txt.Split('_')[1]).Split(':')[1]);
            try
            {
                NetworkEntitiesModel.Production[objectIndex].Value = objectValue;

                for (int i = 0; i < NetworkEntitiesModel.Production.Count; i++)
                {
                    if (NetworkEntitiesModel.Production[objectIndex].Id == NetworkEntitiesModel.Production[i].Id)
                    {
                        NetworkEntitiesModel.Production[i].Value = objectValue;
                    }
                }
                for (int i = 0; i < NetworkEntitiesModel.Izabrani.Count; i++)
                {
                    if (NetworkEntitiesModel.Production[objectIndex].Id == NetworkEntitiesModel.Izabrani[i].Id)
                    {
                        NetworkEntitiesModel.Izabrani[i].Value = objectValue;
                    }
                }

                for (int i = 0; i < NetworkDisplayModel.listaStavki.Count; i++)
                {
                    if (NetworkEntitiesModel.Production[objectIndex].Id == NetworkDisplayModel.listaStavki[i].Id)
                    {
                        NetworkDisplayModel.listaStavki[i].Value = objectValue;
                    }
                }
                foreach (KeyValuePair<int, Production> entry in NetworkDisplayModel.KanvasKomponente)
                {
                    if (entry.Value.Id == NetworkEntitiesModel.Production[objectIndex].Id)
                    {
                        entry.Value.Value = objectValue;
                        break;
                    }
                }
                foreach (var item in MeasurementGraphModel.ProductionChartList)
                {
                    if(item.Id == NetworkEntitiesModel.Production[objectIndex].Id)
                    {
                        item.Value = objectValue;

                        //Ovde se menjaju vrednosti u listi za chart
                        MeasurementGraphModel.SetFirstDotAndValue(MeasurementGraphModel.CalculateElementHeight(objectValue), objectValue, item.Name);
                        break;
                    }
                }
            }
            catch
            {

            }

            OnPropertyChanged("Production");
            OnPropertyChanged("KanvasKomponente");
            OnPropertyChanged("listaStavki");
            OnPropertyChanged("Izabrani");
        }
     
    }
}
