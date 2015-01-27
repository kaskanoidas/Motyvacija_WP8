using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Motyvacija_WP8.Resources;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Tasks;
using Windows.Storage;
using System.Threading.Tasks;
using System.IO;
using OpenXML.Silverlight.Spreadsheet;
using OpenXML.Silverlight.Spreadsheet.Parts;
using FiftyNine.Ag.OpenXML.Common.Storage;

namespace Motyvacija_WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        int lastEmployeeChecked;
        double x, x2, y, y2;
        Boolean scrolLock;
        Boolean AddNewItem;
        List<ArchyvedEmployeeClass> AEC;
        Boolean TextboxInFocus;
        Boolean DoubleClickOnTextbox;
        TextBox LastSelectedTextbox;
        Boolean CanExit;
        char Skirtukas = '&';
        public MainPage()
        {
            InitializeComponent();
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = AppResources.AddNew;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Text = AppResources.Menu;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).Text = AppResources.SaveTA;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[3]).Text = AppResources.Calculate;
            LoadData();
            lastEmployeeChecked = -1; AddNewItem = false;
            x = 0; x2 = 0; y = 0; y2 = 0; scrolLock = false;
            CanExit = true;
            TextboxInFocus = false;
            DoubleClickOnTextbox = false;
            LastSelectedTextbox = new TextBox();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
            KillAll();
            if (PhoneApplicationService.Current.State.ContainsKey("AEC"))
            {
                AEC = (List<ArchyvedEmployeeClass>)PhoneApplicationService.Current.State["AEC"];
                SaveAll();
            }
            LoadKalba();
            LoadData();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            lastEmployeeChecked = -1;
            SaveAll();
            PhoneApplicationService.Current.State["AEC"] = AEC;
            base.OnNavigatedFrom(e);
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            e.Cancel = true;
            if(MeniuBar.Visibility == System.Windows.Visibility.Visible)
            {
                MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (LanguageBar.Visibility == System.Windows.Visibility.Visible)
            {
                LanguageBar.Visibility = System.Windows.Visibility.Collapsed;
                MeniuBar.Visibility = System.Windows.Visibility.Visible;
            }
            else if (AddBarEmployee.Visibility == System.Windows.Visibility.Visible)
            {
                AddBarEmployee.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (AddBarIndicator.Visibility == System.Windows.Visibility.Visible)
            {
                AddBarIndicator.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (AddBarTask.Visibility == System.Windows.Visibility.Visible)
            {
                AddBarTask.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (MAxKDPST.Visibility == System.Windows.Visibility.Visible)
            {
                MAxKDPST.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (EmployeeDetailPanel.Visibility == System.Windows.Visibility.Visible)
            {
                EmployeeDetailPanel.Visibility = System.Windows.Visibility.Collapsed;
            } 
            else
            {
                SaveAll();
                SaveKalba(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
                Application.Current.Terminate();
            }
        }
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            SaveKalba(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            SaveAll();
        }
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            SaveKalba(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            SaveAll();
        }
        private void LoadKalba()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Kalba.txt", System.IO.FileMode.OpenOrCreate, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            string eilute = file.ReadLine();
            if (eilute != System.Threading.Thread.CurrentThread.CurrentCulture.ToString())
            {
                if (eilute == "en-US" || eilute == "en")
                {
                    CultureInfo cult = new CultureInfo("en");
                    System.Threading.Thread.CurrentThread.CurrentCulture = cult;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
                    Motyvacija_WP8.Resources.AppResources.Culture = cult;
                    App.RootFrame.Language = XmlLanguage.GetLanguage("en");
                    App.Current.RootVisual.UpdateLayout();
                    App.RootFrame.UpdateLayout();
                    var ReloadUri = (App.RootFrame.Content as PhoneApplicationPage).NavigationService.CurrentSource;
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(ReloadUri + "?no-cache=" + Guid.NewGuid(), UriKind.Relative));
                }
                else if (eilute == "lt-LT")
                {
                    CultureInfo cult = new CultureInfo("lt-LT");
                    System.Threading.Thread.CurrentThread.CurrentCulture = cult;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
                    Motyvacija_WP8.Resources.AppResources.Culture = cult;
                    App.RootFrame.Language = XmlLanguage.GetLanguage("lt-LT");
                    App.Current.RootVisual.UpdateLayout();
                    App.RootFrame.UpdateLayout();
                    var ReloadUri = (App.RootFrame.Content as PhoneApplicationPage).NavigationService.CurrentSource;
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(ReloadUri + "?no-cache=" + Guid.NewGuid(), UriKind.Relative));
                }
                else if (eilute == "ru-RU")
                {
                    CultureInfo cult = new CultureInfo("ru-RU");
                    System.Threading.Thread.CurrentThread.CurrentCulture = cult;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
                    Motyvacija_WP8.Resources.AppResources.Culture = cult;
                    App.RootFrame.Language = XmlLanguage.GetLanguage("ru-RU");
                    App.Current.RootVisual.UpdateLayout();
                    App.RootFrame.UpdateLayout();
                    var ReloadUri = (App.RootFrame.Content as PhoneApplicationPage).NavigationService.CurrentSource;
                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(ReloadUri + "?no-cache=" + Guid.NewGuid(), UriKind.Relative));
                }
            }
            file.Close();
        }
        private void SaveKalba(string kalba)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Kalba.txt", System.IO.FileMode.Create, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            file.WriteLine(kalba);
            file.Close();
        }
        private void LoadData()
        {
            System.IO.StreamReader file = new System.IO.StreamReader( new System.IO.IsolatedStorage.IsolatedStorageFileStream("Duom.txt",System.IO.FileMode.OpenOrCreate, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            Employees.Items.Clear();
            Indicators.Items.Clear();
            Tasks.Items.Clear();
            if (AEC == null)
            {
                AEC = new List<ArchyvedEmployeeClass>();
            }
            else
            {
                AEC.Clear();
            }
            Boolean NeraTuscias = false;
            while (file.EndOfStream != true)
            {
                NeraTuscias = true;
                string eilute = file.ReadLine();
                if (eilute == "Darbuotoju lentele:")
                {
                    int index = Convert.ToInt32(file.ReadLine());
                    for (int i = 0; i < index; i++)
                    {
                        string[] reiksmes = file.ReadLine().Split(Skirtukas);
                        EmployeeClass EC = new EmployeeClass();
                        EC.NameLine = reiksmes[0]; EC.BALine = ParseDouble(reiksmes[1]); EC.RODLine = ParseDouble(reiksmes[2]); EC.UZDLine = ParseDouble(reiksmes[3]); EC.VisoLine = ParseDouble(reiksmes[4]); EC.IsChecked = false;
                        EC.index = i;
                        reiksmes = file.ReadLine().Split(Skirtukas);
                        int rod = Convert.ToInt32(reiksmes[0]);
                        int uzd = Convert.ToInt32(reiksmes[1]);
                        EC.MaxKDP = ParseDouble(reiksmes[2]);
                        EC.RodList = new List<IndicatorsClass>();
                        EC.UzdList = new List<TasksClass>();
                        for (int j = 0; j < rod; j++)
                        {
                            reiksmes = file.ReadLine().Split(Skirtukas);
                            IndicatorsClass ind = new IndicatorsClass();
                            ind.INDPAVLine = reiksmes[0]; ind.BRLine = ParseDouble(reiksmes[1]); ind.FRLine = ParseDouble(reiksmes[2]); ind.TRLine = ParseDouble(reiksmes[3]); ind.MKDLine = ParseDouble(reiksmes[4]); ind.IsChecked = false;
                            ind.index = j;
                            EC.RodList.Add(ind);
                        }
                        for (int j = 0; j < uzd; j++)
                        {
                            reiksmes = file.ReadLine().Split(Skirtukas);
                            TasksClass tsk = new TasksClass();
                            tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = ParseDouble(reiksmes[1]); tsk.Ivert = ParseDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
                            tsk.index = j;
                            EC.UzdList.Add(tsk);
                        }
                        Employees.Items.Add(EC);
                    }
                }
                else if (eilute == "Rodikliu lentele:")
                {
                    int index = Convert.ToInt32(file.ReadLine());
                    for (int i = 0; i < index; i++)
                    {
                        string[] reiksmes = file.ReadLine().Split(Skirtukas);
                        IndicatorsClass ind = new IndicatorsClass();
                        ind.INDPAVLine = reiksmes[0]; ind.BRLine = ParseDouble(reiksmes[1]); ind.FRLine = ParseDouble(reiksmes[2]); ind.TRLine = ParseDouble(reiksmes[3]); ind.MKDLine = ParseDouble(reiksmes[4]); ind.IsChecked = false;
                        ind.index = i;
                        Indicators.Items.Add(ind);
                    }
                }
                else if (eilute == "Uzduociu lentele:")
                {
                    int index = Convert.ToInt32(file.ReadLine());
                    for (int i = 0; i < index; i++)
                    {
                        string[] reiksmes = file.ReadLine().Split(Skirtukas);
                        TasksClass tsk = new TasksClass();
                        tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = ParseDouble(reiksmes[1]); tsk.Ivert = ParseDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
                        tsk.index = i;
                        Tasks.Items.Add(tsk);
                    }
                }
                else if (eilute == "Archyvo lentele:")
                {
                    int index = Convert.ToInt32(file.ReadLine());
                    AEC = new List<ArchyvedEmployeeClass>();
                    for (int i = 0; i < index; i++)
                    {
                        string[] reiksmes = file.ReadLine().Split(Skirtukas);
                        ArchyvedEmployeeClass EC = new ArchyvedEmployeeClass();
                        EC.NameLine = reiksmes[0]; EC.BALine = ParseDouble(reiksmes[1]); EC.RODLine = ParseDouble(reiksmes[2]); EC.UZDLine = ParseDouble(reiksmes[3]); EC.VisoLine = ParseDouble(reiksmes[4]); EC.Date = reiksmes[5]; EC.IsChecked = false;
                        EC.index = i;
                        reiksmes = file.ReadLine().Split(Skirtukas);
                        int rod = Convert.ToInt32(reiksmes[0]);
                        int uzd = Convert.ToInt32(reiksmes[1]);
                        EC.MaxKDP = ParseDouble(reiksmes[2]);
                        EC.RodList = new List<IndicatorsClass>();
                        EC.UzdList = new List<TasksClass>();
                        for (int j = 0; j < rod; j++)
                        {
                            reiksmes = file.ReadLine().Split(Skirtukas);
                            IndicatorsClass ind = new IndicatorsClass();
                            ind.INDPAVLine = reiksmes[0]; ind.BRLine = ParseDouble(reiksmes[1]); ind.FRLine = ParseDouble(reiksmes[2]); ind.TRLine = ParseDouble(reiksmes[3]); ind.MKDLine = ParseDouble(reiksmes[4]); ind.IsChecked = false;
                            ind.index = j;
                            EC.RodList.Add(ind);
                        }
                        for (int j = 0; j < uzd; j++)
                        {
                            reiksmes = file.ReadLine().Split(Skirtukas);
                            TasksClass tsk = new TasksClass();
                            tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = ParseDouble(reiksmes[1]); tsk.Ivert = ParseDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
                            tsk.index = j;
                            EC.UzdList.Add(tsk);
                        }
                        AEC.Add(EC);
                    }
                }
            }
            file.Close();
            if (Employees.Items.Count == 0 && Tasks.Items.Count == 0 && Indicators.Items.Count == 0 && AEC.Count == 0 && NeraTuscias == false)
            {
                EmployeeClass EC = new EmployeeClass();
                EC.index = 0; EC.IsChecked = false; EC.NameLine = "Marko"; EC.BALine = 1000.00; EC.RODLine = 0; EC.UZDLine = 0;
                EC.VisoLine = 1000.00; EC.MaxKDP = 0; EC.RodList = new List<IndicatorsClass>() { }; EC.UzdList = new List<TasksClass>() { };
                Employees.Items.Add(EC);

                EC = new EmployeeClass();
                EC.index = 1; EC.IsChecked = false; EC.NameLine = "Employee 2"; EC.BALine = 2000.00; EC.RODLine = 0; EC.UZDLine = 0;
                EC.VisoLine = 2000.00; EC.MaxKDP = 0; EC.RodList = new List<IndicatorsClass>() { }; EC.UzdList = new List<TasksClass>() { };
                Employees.Items.Add(EC);

                EC = new EmployeeClass();
                EC.index = 2; EC.IsChecked = false; EC.NameLine = "Employee 3"; EC.BALine = 3000.00; EC.RODLine = 0; EC.UZDLine = 0;
                EC.VisoLine = 3000.00; EC.MaxKDP = 0; EC.RodList = new List<IndicatorsClass>() { }; EC.UzdList = new List<TasksClass>() { };
                Employees.Items.Add(EC);

                IndicatorsClass ind = new IndicatorsClass();
                ind.INDPAVLine = "PROFIT"; ind.BRLine = 100000.0; ind.FRLine = 121000.0;
                ind.TRLine = 130000.00; ind.MKDLine = 1200.0; ind.IsChecked = false; ind.index = 0;
                Indicators.Items.Add(ind);

                ind = new IndicatorsClass();
                ind.INDPAVLine = "COSTS"; ind.BRLine = 500000.0; ind.FRLine = 480000.0; 
                ind.TRLine = 450000.00; ind.MKDLine = 800.0; ind.IsChecked = false; ind.index = 1;
                Indicators.Items.Add(ind);

                ind = new IndicatorsClass();
                ind.INDPAVLine = "Indicator 3"; ind.BRLine = 2.0; ind.FRLine = 2.4;
                ind.TRLine = 4.0; ind.MKDLine = 2.0; ind.IsChecked = false; ind.index = 2;
                Indicators.Items.Add(ind);

                ind = new IndicatorsClass();
                ind.INDPAVLine = "Indicator 4"; ind.BRLine = 3.00; ind.FRLine = 3.6;
                ind.TRLine = 6.00; ind.MKDLine = 3.00; ind.IsChecked = false; ind.index = 3;
                Indicators.Items.Add(ind);

                TasksClass tsk = new TasksClass();
                tsk.UZDPAVLine = "RELATIONSHIP WITH..."; tsk.MaxIvert = 11.0; tsk.Ivert = 9.0;
                tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false; tsk.index = 0;
                Tasks.Items.Add(tsk);

                tsk = new TasksClass();
                tsk.UZDPAVLine = "CUTTING ON THE RET..."; tsk.MaxIvert = 9.0; tsk.Ivert = 5.0;
                tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false; tsk.index = 1;
                Tasks.Items.Add(tsk);

                tsk = new TasksClass();
                tsk.UZDPAVLine = "IMPROVING PROFESS..."; tsk.MaxIvert = 7.0; tsk.Ivert = 6.0;
                tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false; tsk.index = 2;
                Tasks.Items.Add(tsk);

                tsk = new TasksClass();
                tsk.UZDPAVLine = "TASK 4"; tsk.MaxIvert = 8.0; tsk.Ivert = 7.0;
                tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false; tsk.index = 3;
                Tasks.Items.Add(tsk);

                tsk = new TasksClass();
                tsk.UZDPAVLine = "TASK 5"; tsk.MaxIvert = 9.0; tsk.Ivert = 8.0;
                tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false; tsk.index = 3;
                Tasks.Items.Add(tsk);

                AEC.Clear();
                ArchyvedEmployeeClass ECA = new ArchyvedEmployeeClass();
                ECA.NameLine = "Marko"; ECA.BALine = 1000.00; ECA.RODLine = 0; ECA.UZDLine = 0; ECA.VisoLine = 1000.00; ECA.Date = "2014.06.16";
                ECA.IsChecked = false; ECA.index = 0; ECA.RodList = new List<IndicatorsClass>() { }; ECA.UzdList = new List<TasksClass>() { };
                AEC.Add(ECA);

                ECA = new ArchyvedEmployeeClass();
                ECA.NameLine = "Employee 2"; ECA.BALine = 2000.00; ECA.RODLine = 0; ECA.UZDLine = 0; ECA.VisoLine = 2000.00; ECA.Date = "2014.06.16";
                ECA.IsChecked = false; ECA.index = 1; ECA.RodList = new List<IndicatorsClass>() { }; ECA.UzdList = new List<TasksClass>() { };
                AEC.Add(ECA);

                ECA = new ArchyvedEmployeeClass();
                ECA.NameLine = "Employee 3"; ECA.BALine = 3000.00; ECA.RODLine = 0; ECA.UZDLine = 0; ECA.VisoLine = 3000.00; ECA.Date = "2014.06.16";
                ECA.IsChecked = false; ECA.index = 2; ECA.RodList = new List<IndicatorsClass>() { }; ECA.UzdList = new List<TasksClass>() { };
                AEC.Add(ECA);
            }
        }
        private double ParseDouble(string value)
        {
            double ValueOut = 0.0;
            if(double.TryParse(value,out ValueOut) == false)
            {
                if (value.Contains("."))
                {
                    double.TryParse(value.Replace(".", ","), out ValueOut);
                }
            }
            if (value.Contains(",") && CultureInfo.CurrentCulture.ToString() == "en")
            {
                double.TryParse(value.Replace(",", "."), out ValueOut);
            }
            return ValueOut;
        }
        private void SaveAll()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Duom.txt", System.IO.FileMode.Create, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            file.WriteLine("Darbuotoju lentele:");
            file.WriteLine(Employees.Items.Count);
            for (int i = 0; i < Employees.Items.Count; i++)
            {
                EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                file.WriteLine(emp.NameLine + Skirtukas + emp.BALine.ToString() + Skirtukas + emp.RODLine.ToString() + Skirtukas + emp.UZDLine.ToString() + Skirtukas + emp.VisoLine.ToString());
                if (emp.RodList == null)
                {
                    emp.RodList = new List<IndicatorsClass>();
                }
                if (emp.UzdList == null)
                {
                    emp.UzdList = new List<TasksClass>();
                }
                file.WriteLine(emp.RodList.Count.ToString() + Skirtukas + emp.UzdList.Count.ToString() + Skirtukas + emp.MaxKDP.ToString());
                for (int j = 0; j < emp.RodList.Count; j++)
                {
                    file.WriteLine(emp.RodList[j].INDPAVLine + Skirtukas + emp.RodList[j].BRLine.ToString() + Skirtukas + emp.RodList[j].FRLine.ToString() + Skirtukas + emp.RodList[j].TRLine.ToString() + Skirtukas + emp.RodList[j].MKDLine.ToString());
                }
                for (int j = 0; j < emp.UzdList.Count; j++)
                {
                    file.WriteLine(emp.UzdList[j].UZDPAVLine + Skirtukas + emp.UzdList[j].MaxIvert.ToString() + Skirtukas + emp.UzdList[j].Ivert.ToString());
                }
            }

            file.WriteLine("Rodikliu lentele:");
            file.WriteLine(Indicators.Items.Count.ToString());
            for (int i = 0; i < Indicators.Items.Count; i++)
            {
                IndicatorsClass ind = (IndicatorsClass)Indicators.Items[i];
                file.WriteLine(ind.INDPAVLine + Skirtukas + ind.BRLine.ToString() + Skirtukas + ind.FRLine.ToString() + Skirtukas + ind.TRLine.ToString() + Skirtukas + ind.MKDLine.ToString());
            }

            file.WriteLine("Uzduociu lentele:");
            file.WriteLine(Tasks.Items.Count.ToString());
            for (int i = 0; i < Tasks.Items.Count; i++)
            {
                TasksClass tsk = (TasksClass)Tasks.Items[i];
                file.WriteLine(tsk.UZDPAVLine + Skirtukas + tsk.MaxIvert.ToString() + Skirtukas + tsk.Ivert.ToString());
            }
            if (AEC == null)
            {
                AEC = new List<ArchyvedEmployeeClass>();
            }
            file.WriteLine("Archyvo lentele:");
            file.WriteLine(AEC.Count);
            for (int i = 0; i < AEC.Count; i++)
            {
                file.WriteLine(AEC[i].NameLine + Skirtukas + AEC[i].BALine.ToString() + Skirtukas + AEC[i].RODLine.ToString() + Skirtukas + AEC[i].UZDLine.ToString() + Skirtukas + AEC[i].VisoLine.ToString() + Skirtukas + AEC[i].Date);
                if (AEC[i].RodList == null)
                {
                    AEC[i].RodList = new List<IndicatorsClass>();
                }
                if (AEC[i].UzdList == null)
                {
                    AEC[i].UzdList = new List<TasksClass>();
                }
                file.WriteLine(AEC[i].RodList.Count.ToString() + Skirtukas + AEC[i].UzdList.Count.ToString() + Skirtukas + AEC[i].MaxKDP.ToString());
                for (int j = 0; j < AEC[i].RodList.Count; j++)
                {
                    file.WriteLine(AEC[i].RodList[j].INDPAVLine + Skirtukas + AEC[i].RodList[j].BRLine.ToString() + Skirtukas + AEC[i].RodList[j].FRLine.ToString() + Skirtukas + AEC[i].RodList[j].TRLine.ToString() + Skirtukas + AEC[i].RodList[j].MKDLine.ToString());
                }
                for (int j = 0; j < AEC[i].UzdList.Count; j++)
                {
                    file.WriteLine(AEC[i].UzdList[j].UZDPAVLine + Skirtukas + AEC[i].UzdList[j].MaxIvert.ToString() + Skirtukas + AEC[i].UzdList[j].Ivert.ToString());
                }
            }
            file.Close();
            CanExit = true;
        }
        private void KillAll()
        {
            MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            SaveBar.Visibility = System.Windows.Visibility.Collapsed;
            LanguageBar.Visibility = System.Windows.Visibility.Collapsed;
            AddBarEmployee.Visibility = System.Windows.Visibility.Collapsed;
            AddBarIndicator.Visibility = System.Windows.Visibility.Collapsed;
            AddBarTask.Visibility = System.Windows.Visibility.Collapsed;
            MAxKDPST.Visibility = System.Windows.Visibility.Collapsed;
            EditShowGrid.Visibility = System.Windows.Visibility.Collapsed;
            Edit.Visibility = System.Windows.Visibility.Collapsed;
            Show.Visibility = System.Windows.Visibility.Collapsed;
            EmployeeDetailPanel.Visibility = System.Windows.Visibility.Collapsed;

            NameBox.Text = ""; BABox.Text = "";
            PavBoxIND.Text = ""; BRBox.Text = ""; TRBox.Text = ""; FRBox.Text = ""; MKDBox.Text = "";
            PavBoxTSK.Text = ""; IVBox.Text = ""; MAXIVBox.Text = "";
            MAXKDPBox.Text = "";

            TextboxInFocus = false;
            DoubleClickOnTextbox = false;
            LastSelectedTextbox = new TextBox();
            AddBarEmployee.Height = 280;
            AddBarIndicator.Height = 545;
            AddBarTask.Height = 380;
            AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            AddBarEmployee.ScrollToVerticalOffset(0);
            AddBarIndicator.ScrollToVerticalOffset(0);
            AddBarTask.ScrollToVerticalOffset(0);
            this.Focus();
        }
        private void ApplicationBarIconButton_Click(object sender, EventArgs e) // Meniu
        {
            if (MeniuBar.Visibility == System.Windows.Visibility.Collapsed)
            {
                KillAll();
                MeniuBar.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e) // Save
        {
            if (SaveBar.Visibility == System.Windows.Visibility.Collapsed)
            {
                KillAll();
                SaveBar.Visibility = System.Windows.Visibility.Visible;
                if (CheckForExcel() == false) ExcelClear.Visibility = System.Windows.Visibility.Collapsed;
                else ExcelClear.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                SaveBar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private bool CheckForExcel()
        {
            using (var isoStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStore.FileExists("Excel.txt")) return true;
                else return false;
            }
        }
        private void ExcelClear_Click(object sender, RoutedEventArgs e) // Share reset
        {
            using (var isoStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStore.FileExists("Excel.txt"))
                {
                    MessageBoxResult msgrez = new MessageBoxResult();
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals("lt-LT"))
                    {
                        msgrez = MessageBox.Show("Ar tikrai norite ištrinti Excel failą?", "Patvirtinimas", MessageBoxButton.OKCancel);
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals("ru-RU"))
                    {
                        msgrez = MessageBox.Show("Вы уверены, что хотите удалить Excel файл?", "Подтверждение", MessageBoxButton.OKCancel);
                    }
                    else
                    {
                        msgrez = MessageBox.Show("Do you want to delete the Excel file?", "Confirmation", MessageBoxButton.OKCancel);
                    }
                    if (msgrez == MessageBoxResult.OK) isoStore.DeleteFile("Excel.txt");
                }
                KillAll();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Employees.Items.Count; i++)
            {
                EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                if (emp.IsChecked == true)
                {
                    ArchyvedEmployeeClass A = new ArchyvedEmployeeClass();
                    A.BALine = emp.BALine;
                    DateTime dateTime = DateTime.UtcNow.Date;
                    string data = dateTime.ToString("yyyy.MM.dd");
                    A.Date = data;
                    A.index = AEC.Count;
                    A.IsChecked = false;
                    A.MaxKDP = emp.MaxKDP;
                    A.NameLine = emp.NameLine;
                    A.RODLine = emp.RODLine;
                    A.UZDLine = emp.UZDLine;
                    A.VisoLine = emp.VisoLine;
                    A.RodList = new List<IndicatorsClass>();
                    A.UzdList = new List<TasksClass>();
                    if (emp.RodList == null)
                    {
                        emp.RodList = new List<IndicatorsClass>();
                    }
                    if (emp.UzdList == null)
                    {
                        emp.UzdList = new List<TasksClass>();
                    }
                    for (int j = 0; j < emp.RodList.Count; j++)
                    {
                        IndicatorsClass ind = (IndicatorsClass)emp.RodList[j];
                        IndicatorsClass newind = new IndicatorsClass();
                        newind.FRLine = ind.FRLine; newind.BRLine = ind.BRLine; newind.index = A.RodList.Count; newind.INDPAVLine = ind.INDPAVLine; newind.IsChecked = false; newind.MKDLine = ind.MKDLine; newind.TRLine = ind.TRLine;
                        A.RodList.Add(newind);
                    }
                    for (int j = 0; j < emp.UzdList.Count; j++)
                    {
                        TasksClass tsk = (TasksClass)emp.UzdList[j];
                        TasksClass newtsk = new TasksClass();
                        newtsk.index = A.UzdList.Count; newtsk.IsChecked = false; newtsk.Ivert = tsk.Ivert; newtsk.IVERTLine = tsk.IVERTLine; newtsk.MaxIvert = tsk.MaxIvert; newtsk.UZDPAVLine = tsk.UZDPAVLine;
                        A.UzdList.Add(newtsk);
                    }
                    AEC.Add(A);
                }
            }
            KillAll();
            SaveAll();
            SaveKalba(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
        }
        private async void Share_Click(object sender, RoutedEventArgs e) // Share
        {
            bool ReadOnly = true;
            for (int i = 0; i < Employees.Items.Count; i++)
            {
                EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                List<ArchyvedEmployeeClass> EC = new List<ArchyvedEmployeeClass>(){};
                if (emp.IsChecked == true)
                {
                    ReadOnly = false;
                    SpreadsheetDocument doc = new SpreadsheetDocument();
                    doc.ApplicationName = "Goals_Results_Salary";
                    Create_Header(ref doc);
                    Read_Workers(ref EC, emp);
                    Create_Workers(ref doc, EC);
                    using (var isoStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (isoStore.FileExists("Goals_Results_Salary.xlsx")) isoStore.DeleteFile("Goals_Results_Salary.xlsx");
                        using (var s = isoStore.CreateFile("Goals_Results_Salary.xlsx"))
                        using (IStreamProvider storage = new ZipStreamProvider(s))
                            doc.Save(storage);
                        await Windows.System.Launcher.LaunchFileAsync(await ApplicationData.Current.LocalFolder.GetFileAsync("Goals_Results_Salary.xlsx"));
                    }
                }
            }
            if (ReadOnly == true)
            {
                List<ArchyvedEmployeeClass> EC = new List<ArchyvedEmployeeClass>() { };
                SpreadsheetDocument doc = new SpreadsheetDocument();
                doc.ApplicationName = "Goals_Results_Salary";
                Create_Header(ref doc);
                Read_Workers(ref EC);
                Create_Workers(ref doc, EC);
                using (var isoStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isoStore.FileExists("Goals_Results_Salary.xlsx")) isoStore.DeleteFile("Goals_Results_Salary.xlsx");
                    using (var s = isoStore.CreateFile("Goals_Results_Salary.xlsx"))
                    using (IStreamProvider storage = new ZipStreamProvider(s))
                        doc.Save(storage);
                    await Windows.System.Launcher.LaunchFileAsync(await ApplicationData.Current.LocalFolder.GetFileAsync("Goals_Results_Salary.xlsx"));
                }
            }
            KillAll();
            SaveAll();
            SaveKalba(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
        }
        private void Create_Header(ref SpreadsheetDocument doc)
        {
            List<SharedStringDefinition> str = new List<SharedStringDefinition>() { };
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals("lt-LT"))
            {
                str = new List<SharedStringDefinition>()
                {
                    doc.Workbook.SharedStrings.AddString("Darbuotojas"), doc.Workbook.SharedStrings.AddString(""),
                    doc.Workbook.SharedStrings.AddString("Rodikliai"),
                    doc.Workbook.SharedStrings.AddString("Maksimali kintama\ndalis pagal\nrodiklius"),
                    doc.Workbook.SharedStrings.AddString("Bazinė reikšmė"),
                    doc.Workbook.SharedStrings.AddString("Tikslinė reikšmė"),
                    doc.Workbook.SharedStrings.AddString("Faktinė reikšmė"),
                    doc.Workbook.SharedStrings.AddString("Užduotys"),
                    doc.Workbook.SharedStrings.AddString("Maksimali kintama\ndalis pagal\nužduotis"),
                    doc.Workbook.SharedStrings.AddString("Maksimalus\nįvertinimas"),
                    doc.Workbook.SharedStrings.AddString("Įvertinimas") 
                };
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(0, 0, 23);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(1, 2, 10);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(3, 3, 15);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(4, 6, 13);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(7, 7, 10);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(8, 10, 15);
                doc.Workbook.Sheets[0].Sheet.Rows[0].Height = 40;
            }
            else if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals("ru-RU"))
            {
                str = new List<SharedStringDefinition>()
                {
                    doc.Workbook.SharedStrings.AddString("Сотрудники"), doc.Workbook.SharedStrings.AddString(""),
                    doc.Workbook.SharedStrings.AddString("Показатели"),
                    doc.Workbook.SharedStrings.AddString("Max. Часть\nотносительно\nпоказателей"),
                    doc.Workbook.SharedStrings.AddString("Базовое\nзначение"),
                    doc.Workbook.SharedStrings.AddString("Целевое\nзначение"),
                    doc.Workbook.SharedStrings.AddString("Фактическое\nзначение"),
                    doc.Workbook.SharedStrings.AddString("Задании"),
                    doc.Workbook.SharedStrings.AddString("Max. Часть в\nсоответствии\nс заданиями"),
                    doc.Workbook.SharedStrings.AddString("Mаксимальная\nоценка"),
                    doc.Workbook.SharedStrings.AddString("Оценка")
                };
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(0, 0, 23);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(1, 2, 10);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(3, 3, 15);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(4, 6, 13);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(7, 7, 10);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(8, 10, 15);
                doc.Workbook.Sheets[0].Sheet.Rows[0].Height = 40;
            }
            else
            {
                str = new List<SharedStringDefinition>()
                {
                    doc.Workbook.SharedStrings.AddString("Employee"), doc.Workbook.SharedStrings.AddString(""),
                    doc.Workbook.SharedStrings.AddString("Indicators"),
                    doc.Workbook.SharedStrings.AddString("Max. Indicators\nrelated variable\npart"),
                    doc.Workbook.SharedStrings.AddString("Baseline value"),
                    doc.Workbook.SharedStrings.AddString("Target value"),
                    doc.Workbook.SharedStrings.AddString("Actual outcome"),
                    doc.Workbook.SharedStrings.AddString("Tasks"),
                    doc.Workbook.SharedStrings.AddString("Max. tasks\nrelated variable\npart"),
                    doc.Workbook.SharedStrings.AddString("Maximum\nevaluation"),
                    doc.Workbook.SharedStrings.AddString("Evaluation")
                };
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(0, 0, 23);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(1, 2, 10);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(3, 3, 15);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(4, 6, 13);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(7, 7, 10);
                doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(8, 10, 15);
                doc.Workbook.Sheets[0].Sheet.Rows[0].Height = 40;
            }
            for (int i = 0; i < str.Count; i++) doc.Workbook.Sheets[0].Sheet.Rows[0].Cells[i].SetValue(str[i]);
        }
        private void Read_Workers(ref List<ArchyvedEmployeeClass> ECC, EmployeeClass emp)
        {
            int i = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Excel.txt", System.IO.FileMode.OpenOrCreate, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            while (file.EndOfStream != true)
            {
                i = Convert.ToInt32(file.ReadLine());
                ECC = new List<ArchyvedEmployeeClass>();
                for (int n = 0; n < i; n++)
                {
                    string[] reiksmes = file.ReadLine().Split(Skirtukas);
                    ArchyvedEmployeeClass EC = new ArchyvedEmployeeClass();
                    EC.NameLine = reiksmes[0]; EC.BALine = ParseDouble(reiksmes[1]); EC.RODLine = ParseDouble(reiksmes[2]); EC.UZDLine = ParseDouble(reiksmes[3]); EC.VisoLine = ParseDouble(reiksmes[4]); EC.Date = reiksmes[5]; EC.IsChecked = false;
                    EC.index = n;
                    reiksmes = file.ReadLine().Split(Skirtukas);
                    int rod = Convert.ToInt32(reiksmes[0]);
                    int uzd = Convert.ToInt32(reiksmes[1]);
                    EC.MaxKDP = ParseDouble(reiksmes[2]);
                    EC.RodList = new List<IndicatorsClass>();
                    EC.UzdList = new List<TasksClass>();
                    for (int j = 0; j < rod; j++)
                    {
                        reiksmes = file.ReadLine().Split(Skirtukas);
                        IndicatorsClass ind = new IndicatorsClass();
                        ind.INDPAVLine = reiksmes[0]; ind.BRLine = ParseDouble(reiksmes[1]); ind.FRLine = ParseDouble(reiksmes[2]); ind.TRLine = ParseDouble(reiksmes[3]); ind.MKDLine = ParseDouble(reiksmes[4]); ind.IsChecked = false;
                        ind.index = j;
                        EC.RodList.Add(ind);
                    }
                    for (int j = 0; j < uzd; j++)
                    {
                        reiksmes = file.ReadLine().Split(Skirtukas);
                        TasksClass tsk = new TasksClass();
                        tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = ParseDouble(reiksmes[1]); tsk.Ivert = ParseDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
                        tsk.index = j;
                        EC.UzdList.Add(tsk);
                    }
                    ECC.Add(EC);
                }
            }
            file.Close();

            ArchyvedEmployeeClass empp = new ArchyvedEmployeeClass();
            empp.BALine = emp.BALine;
            empp.Date = DateTime.UtcNow.Date.ToString("yyyy.MM.dd");
            empp.index = i;
            empp.IsChecked = false;
            empp.MaxKDP = emp.MaxKDP;
            empp.NameLine = emp.NameLine;
            empp.RODLine = emp.RODLine;
            empp.RodList = new List<IndicatorsClass>(emp.RodList);
            empp.UZDLine = emp.UZDLine;
            empp.UzdList = new List<TasksClass>(emp.UzdList);
            empp.VisoLine = emp.VisoLine;
            ECC.Add(empp);

            ArchyvedEmployeeClass em = new ArchyvedEmployeeClass();
            em.BALine = emp.BALine;
            em.Date = DateTime.UtcNow.Date.ToString("yyyy.MM.dd");
            em.index = AEC.Count;
            em.IsChecked = false;
            em.MaxKDP = emp.MaxKDP;
            em.NameLine = emp.NameLine;
            em.RODLine = emp.RODLine;
            em.RodList = new List<IndicatorsClass>(emp.RodList);
            em.UZDLine = emp.UZDLine;
            em.UzdList = new List<TasksClass>(emp.UzdList);
            em.VisoLine = emp.VisoLine;
            AEC.Add(em);

            System.IO.StreamWriter fileW = new System.IO.StreamWriter(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Excel.txt", System.IO.FileMode.Create, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            fileW.WriteLine(ECC.Count);
            for (int n = 0; n < ECC.Count; n++)
            {
                fileW.WriteLine(ECC[n].NameLine + Skirtukas + ECC[n].BALine.ToString() + Skirtukas + ECC[n].RODLine.ToString() + Skirtukas + ECC[n].UZDLine.ToString() + Skirtukas + ECC[n].VisoLine.ToString() + Skirtukas + ECC[n].Date);
                if (ECC[n].RodList == null)
                {
                    ECC[n].RodList = new List<IndicatorsClass>();
                }
                if (ECC[n].UzdList == null)
                {
                    ECC[n].UzdList = new List<TasksClass>();
                }
                fileW.WriteLine(ECC[n].RodList.Count.ToString() + Skirtukas + ECC[n].UzdList.Count.ToString() + Skirtukas + ECC[n].MaxKDP.ToString());
                for (int j = 0; j < ECC[n].RodList.Count; j++)
                {
                    fileW.WriteLine(ECC[n].RodList[j].INDPAVLine + Skirtukas + ECC[n].RodList[j].BRLine.ToString() + Skirtukas + ECC[n].RodList[j].FRLine.ToString() + Skirtukas + ECC[n].RodList[j].TRLine.ToString() + Skirtukas + ECC[n].RodList[j].MKDLine.ToString());
                }
                for (int j = 0; j < ECC[n].UzdList.Count; j++)
                {
                    fileW.WriteLine(ECC[n].UzdList[j].UZDPAVLine + Skirtukas + ECC[n].UzdList[j].MaxIvert.ToString() + Skirtukas + ECC[n].UzdList[j].Ivert.ToString());
                }
            }
            fileW.Close();
        }
        private void Read_Workers(ref List<ArchyvedEmployeeClass> ECC)
        {
            int i = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Excel.txt", System.IO.FileMode.OpenOrCreate, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            while (file.EndOfStream != true)
            {
                i = Convert.ToInt32(file.ReadLine());
                ECC = new List<ArchyvedEmployeeClass>();
                for (int n = 0; n < i; n++)
                {
                    string[] reiksmes = file.ReadLine().Split(Skirtukas);
                    ArchyvedEmployeeClass EC = new ArchyvedEmployeeClass();
                    EC.NameLine = reiksmes[0]; EC.BALine = ParseDouble(reiksmes[1]); EC.RODLine = ParseDouble(reiksmes[2]); EC.UZDLine = ParseDouble(reiksmes[3]); EC.VisoLine = ParseDouble(reiksmes[4]); EC.Date = reiksmes[5]; EC.IsChecked = false;
                    EC.index = n;
                    reiksmes = file.ReadLine().Split(Skirtukas);
                    int rod = Convert.ToInt32(reiksmes[0]);
                    int uzd = Convert.ToInt32(reiksmes[1]);
                    EC.MaxKDP = ParseDouble(reiksmes[2]);
                    EC.RodList = new List<IndicatorsClass>();
                    EC.UzdList = new List<TasksClass>();
                    for (int j = 0; j < rod; j++)
                    {
                        reiksmes = file.ReadLine().Split(Skirtukas);
                        IndicatorsClass ind = new IndicatorsClass();
                        ind.INDPAVLine = reiksmes[0]; ind.BRLine = ParseDouble(reiksmes[1]); ind.FRLine = ParseDouble(reiksmes[2]); ind.TRLine = ParseDouble(reiksmes[3]); ind.MKDLine = ParseDouble(reiksmes[4]); ind.IsChecked = false;
                        ind.index = j;
                        EC.RodList.Add(ind);
                    }
                    for (int j = 0; j < uzd; j++)
                    {
                        reiksmes = file.ReadLine().Split(Skirtukas);
                        TasksClass tsk = new TasksClass();
                        tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = ParseDouble(reiksmes[1]); tsk.Ivert = ParseDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
                        tsk.index = j;
                        EC.UzdList.Add(tsk);
                    }
                    ECC.Add(EC);
                }
            }
            file.Close();
        }
        private void Create_Workers(ref SpreadsheetDocument doc, List<ArchyvedEmployeeClass> EC)
        {
            int y = 1;
            for (int n = 0; n < EC.Count; n++)
            {
                ArchyvedEmployeeClass emp = EC[n];
                doc.Workbook.Sheets[0].Sheet.Rows[y].Cells[0].SetValue(emp.NameLine);
                doc.Workbook.Sheets[0].Sheet.Rows[y].Cells[1].SetValue(emp.Date);
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals("lt-LT"))
                {
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 1].Cells[0].SetValue("Bazinis atlyginimas");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 2].Cells[0].SetValue("Kintama dalis pagal rodiklius");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 3].Cells[0].SetValue("Kintama dalis pagal užduotis");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 4].Cells[0].SetValue("Viso");
                }
                else if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals("ru-RU"))
                {
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 1].Cells[0].SetValue("Баз. Зарплата");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 2].Cells[0].SetValue("Перем. Часть-показатели");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 3].Cells[0].SetValue("Перем. Часть-задании");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 4].Cells[0].SetValue("Итого");
                }
                else
                {
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 1].Cells[0].SetValue("Basic salary");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 2].Cells[0].SetValue("Indicators related part");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 3].Cells[0].SetValue("Tasks related part");
                    doc.Workbook.Sheets[0].Sheet.Rows[y + 4].Cells[0].SetValue("Total");
                }
                doc.Workbook.Sheets[0].Sheet.Rows[y + 1].Cells[1].SetValue(emp.BALine.ToString());
                doc.Workbook.Sheets[0].Sheet.Rows[y + 2].Cells[1].SetValue(emp.RODLine.ToString());
                doc.Workbook.Sheets[0].Sheet.Rows[y + 3].Cells[1].SetValue(emp.UZDLine.ToString());
                doc.Workbook.Sheets[0].Sheet.Rows[y + 4].Cells[1].SetValue(emp.VisoLine.ToString());
                if (emp.RodList == null)
                {
                    emp.RodList = new List<IndicatorsClass>();
                }
                if (emp.UzdList == null)
                {
                    emp.UzdList = new List<TasksClass>();
                }
                for (int i = 0; i < emp.RodList.Count; i++)
                {
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[2].SetValue(emp.RodList[i].INDPAVLine);
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[3].SetValue(emp.RodList[i].MKDLine.ToString());
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[4].SetValue(emp.RodList[i].BRLine.ToString());
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[5].SetValue(emp.RodList[i].TRLine.ToString());
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[6].SetValue(emp.RodList[i].FRLine.ToString());
                }
                doc.Workbook.Sheets[0].Sheet.Rows[y].Cells[8].SetValue(emp.MaxKDP.ToString());
                for (int i = 0; i < emp.UzdList.Count; i++)
                {
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[7].SetValue(emp.UzdList[i].UZDPAVLine);
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[9].SetValue(emp.UzdList[i].MaxIvert.ToString());
                    doc.Workbook.Sheets[0].Sheet.Rows[y + i].Cells[10].SetValue(emp.UzdList[i].Ivert.ToString());
                }
                int max = Math.Max(emp.RodList.Count, emp.UzdList.Count); max = Math.Max(max,5);
                //TablePart table = doc.Workbook.Sheets[0].Sheet.AddTable(emp.NameLine + " " + emp.Date.Replace('.', '_'), emp.NameLine + " " + emp.Date.Replace('.', '_'), doc.Workbook.Sheets[0].Sheet.Rows[y].Cells[0], doc.Workbook.Sheets[0].Sheet.Rows[y + max - 2].Cells[9]);
                y += max;
            }
        }
        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e) // Add
        {
            int index = PIVOT.SelectedIndex;
            AddNewItem = true;
            if (index == 0)
            {
                if (AddBarEmployee.Visibility == System.Windows.Visibility.Visible)
                {
                    KillAll();
                }
                else
                {
                    KillAll();
                    AddBarEmployee.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else if (index == 1)
            {
                if (AddBarIndicator.Visibility == System.Windows.Visibility.Visible)
                {
                    KillAll();
                }
                else
                {
                    KillAll();
                    AddBarIndicator.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else if (index == 2) // tasks
            {

                if (AddBarTask.Visibility == System.Windows.Visibility.Visible)
                {
                    KillAll();
                }
                else
                {
                    KillAll();
                    AddBarTask.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e) // Calc
        {
            if (MAxKDPST.Visibility == System.Windows.Visibility.Visible)
            {
                MAxKDPST.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                KillAll();
                Boolean radoIndicator = false; 
                Boolean ArPasirinktasTask = false;

                for (int i = 0; i < Indicators.Items.Count; i++)
                {
                    IndicatorsClass tsk = (IndicatorsClass)Indicators.Items[i];
                    if (tsk.IsChecked == true)
                    {
                        radoIndicator = true;
                    }
                }
                for (int i = 0; i < Tasks.Items.Count; i++)
                {
                    TasksClass tsk = (TasksClass)Tasks.Items[i];
                    if (tsk.IsChecked == true)
                    {
                        ArPasirinktasTask = true;
                    }
                }
                if (radoIndicator == true && ArPasirinktasTask == false)
                {
                    MAXKDPBox.Text = "0";
                    Calc_Method();
                    PIVOT.SelectedIndex = 0;
                }
                else if (ArPasirinktasTask == true)
                {
                    MAxKDPST.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private Boolean TikrintiArPazymeti()
        {
            int kiekRado = 0;
            Boolean rado = false;
            Boolean ArPasirinktasTask = false;
            for (int i = 0; i < Employees.Items.Count; i++)
            {
                EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                if (emp.IsChecked == true)
                {
                    rado = true;
                }
            }
            if (rado == false)
            {
                return false;
            }

            rado = false;
            for (int i = 0; i < Indicators.Items.Count; i++)
            {
                IndicatorsClass ind = (IndicatorsClass)Indicators.Items[i];
                if (ind.IsChecked == true)
                {
                    rado = true;
                }
            }
            if (rado == true)
            {
                kiekRado++;
            }

            rado = false;
            for (int i = 0; i < Tasks.Items.Count; i++)
            {
                TasksClass tsk = (TasksClass)Tasks.Items[i];
                if (tsk.IsChecked == true)
                {
                    rado = true;
                    ArPasirinktasTask = true;
                }
            }
            if (rado == true)
            {
                kiekRado++;
            }

            if (kiekRado > 0)
            {
                if (ArPasirinktasTask == true)
                {
                    if (MAXKDPBox.Text == "")
                    {
                        MessageBox.Show(AppResources.error_maxkdu_empty);
                        return false;
                    }
                    else
                    {
                        double rezult;
                        try
                        {
                            rezult = double.Parse(MAXKDPBox.Text);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(AppResources.error_maxkdu_number);
                            return false;
                        }
                    }
                    if (double.Parse(MAXKDPBox.Text) < 0)
                    {
                        MessageBox.Show(AppResources.error_maxkdu_number);
                        return false;
                    }
                }
                else
                {
                    double rezult;
                    try
                    {
                        rezult = double.Parse(MAXKDPBox.Text);
                    }
                    catch (Exception)
                    {
                        MAXKDPBox.Text = "0.0";
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Archyvas_Click(object sender, RoutedEventArgs e)
        {
            SaveAll();
            NavigationService.Navigate(new Uri("/Archyvas.xaml", UriKind.Relative));
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }
        private void Language_Click(object sender, RoutedEventArgs e)
        {
            MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            LanguageBar.Visibility = System.Windows.Visibility.Visible;
        }
        private void LT_Click(object sender, RoutedEventArgs e)
        {
            SaveKalba("lt-LT");
            SaveAll();
            CultureInfo cult = new CultureInfo("lt-LT");
            System.Threading.Thread.CurrentThread.CurrentCulture = cult;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
            Motyvacija_WP8.Resources.AppResources.Culture = cult;
            App.RootFrame.Language = XmlLanguage.GetLanguage("lt-LT");
            App.Current.RootVisual.UpdateLayout();
            App.RootFrame.UpdateLayout();
            var ReloadUri = (App.RootFrame.Content as PhoneApplicationPage).NavigationService.CurrentSource;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(ReloadUri + "?no-cache=" + Guid.NewGuid(), UriKind.Relative));
        }
        private void EN_Click(object sender, RoutedEventArgs e)
        {
            SaveKalba("en");
            SaveAll();
            CultureInfo cult = new CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentCulture = cult;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
            Motyvacija_WP8.Resources.AppResources.Culture = cult;
            App.RootFrame.Language = XmlLanguage.GetLanguage("en");
            App.Current.RootVisual.UpdateLayout();
            App.RootFrame.UpdateLayout();
            var ReloadUri = (App.RootFrame.Content as PhoneApplicationPage).NavigationService.CurrentSource;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(ReloadUri + "?no-cache=" + Guid.NewGuid(), UriKind.Relative));
        }
        private void RU_Click(object sender, RoutedEventArgs e)
        {
            SaveKalba("ru-RU");
            SaveAll();
            CultureInfo cult = new CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentCulture = cult;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cult;
            Motyvacija_WP8.Resources.AppResources.Culture = cult;
            App.RootFrame.Language = XmlLanguage.GetLanguage("ru-RU");
            App.Current.RootVisual.UpdateLayout();
            App.RootFrame.UpdateLayout();
            var ReloadUri = (App.RootFrame.Content as PhoneApplicationPage).NavigationService.CurrentSource;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(ReloadUri + "?no-cache=" + Guid.NewGuid(), UriKind.Relative));
        }
        private void LanguageBarBack_Click(object sender, RoutedEventArgs e)
        {
            LanguageBar.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KillAll();
            EditShowGrid.Visibility = System.Windows.Visibility.Visible;
            Show.Visibility = System.Windows.Visibility.Visible;
            Edit.Visibility = System.Windows.Visibility.Visible;
        }
        private void Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KillAll();
            EditShowGrid.Visibility = System.Windows.Visibility.Visible;
            Show.Visibility = System.Windows.Visibility.Collapsed;
            Edit.Visibility = System.Windows.Visibility.Visible;
        }
        private void Indicators_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KillAll();
            EditShowGrid.Visibility = System.Windows.Visibility.Visible;
            Show.Visibility = System.Windows.Visibility.Collapsed;
            Edit.Visibility = System.Windows.Visibility.Visible;
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (lastEmployeeChecked != -1)
            {
                EmployeeClass lastemp = (EmployeeClass)Employees.Items[lastEmployeeChecked];
                lastemp.IsChecked = false;
                Employees.Items[lastEmployeeChecked] = lastemp;
            }
            CheckBox source = (CheckBox)e.OriginalSource;
            int index = int.Parse(source.Tag.ToString());
            EmployeeClass emp = (EmployeeClass)Employees.Items[index];
            emp.IsChecked = true;
            lastEmployeeChecked = index;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox source = (CheckBox)e.OriginalSource;
            int index = int.Parse(source.Tag.ToString());
            EmployeeClass emp = (EmployeeClass)Employees.Items[index];
            emp.IsChecked = false;
            lastEmployeeChecked = -1;
        }
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            CheckBox source = (CheckBox)e.OriginalSource;
            int index = int.Parse(source.Tag.ToString());
            IndicatorsClass ind = (IndicatorsClass)Indicators.Items[index];
            ind.IsChecked = true;
        }
        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            CheckBox source = (CheckBox)e.OriginalSource;
            int index = int.Parse(source.Tag.ToString());
            IndicatorsClass ind = (IndicatorsClass)Indicators.Items[index];
            ind.IsChecked = false;
        }
        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            CheckBox source = (CheckBox)e.OriginalSource;
            int index = int.Parse(source.Tag.ToString());
            TasksClass tsk = (TasksClass)Tasks.Items[index];
            tsk.IsChecked = true;
        }
        private void CheckBox_Unchecked_2(object sender, RoutedEventArgs e)
        {
            CheckBox source = (CheckBox)e.OriginalSource;
            int index = int.Parse(source.Tag.ToString());
            TasksClass tsk = (TasksClass)Tasks.Items[index];
            tsk.IsChecked = false;
        }
        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            x = e.ManipulationOrigin.X;
            ContentPresenter cp = (ContentPresenter)VisualTreeHelper.GetParent(Employees);
            cp.ManipulationStarted += gr_ManipulationStarted;
            PIVOT.IsLocked = true;
        }
        void gr_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            y = e.ManipulationOrigin.Y;
        }
        private void StackPanel_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            x2 = e.ManipulationOrigin.X;
            y2 = e.ManipulationOrigin.Y;
            if (Math.Max(y2, y) - Math.Min(y2, y) > ((Math.Max(x2, x) - Math.Min(x2, x)) * 2) && scrolLock == false)
            {
                scrolLock = false;
            }
            if ((x2 - x) > ((Math.Max(y2, y) - Math.Min(y2, y)) * 2) && scrolLock == false)
            {
                scrolLock = true;
            }
            if ((x2 - x) < 0)
            {
                scrolLock = false;
                TranslateTransform tr = new TranslateTransform();
                tr.X = 0;
                StackPanel st = (StackPanel)sender;
                st.RenderTransform = tr;
                e.Complete();
            }
            if (scrolLock == true)
            {
                TranslateTransform tr = new TranslateTransform();
                tr.X = x2 - x;
                StackPanel st = (StackPanel)sender;
                st.RenderTransform = tr;
            }
        }
        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            x2 = e.ManipulationOrigin.X;
            y2 = e.ManipulationOrigin.Y;
            StackPanel st = (StackPanel)sender;
            PIVOT.IsLocked = false;
            if (x < x2 && x2 - x > 200 && scrolLock == true)
            {
                MessageBoxResult msgrez = new MessageBoxResult();
                if (App.RootFrame.Language.IetfLanguageTag == "lt-lt")
                {
                    msgrez = MessageBox.Show("Ar tikrai norite ištrinti šį darbuotoją?", "Patvirtinimas", MessageBoxButton.OKCancel);
                }
                else if (App.RootFrame.Language.IetfLanguageTag == "en")
                {
                    msgrez = MessageBox.Show("Do you want to delete this employee?", "Confirmation", MessageBoxButton.OKCancel);
                }
                else if (App.RootFrame.Language.IetfLanguageTag == "ru-rU")
                {
                    msgrez = MessageBox.Show("Вы действительно хотите удалить этот сотрудникa?", "Подтверждение", MessageBoxButton.OKCancel);
                }
                else
                {
                    msgrez = MessageBox.Show("Do you want to delete this employee?", "Confirmation", MessageBoxButton.OKCancel);
                }
                if (msgrez == MessageBoxResult.OK)
                {
                    int index = int.Parse(st.Tag.ToString());
                    for (int i = index + 1; i < Employees.Items.Count; i++)
                    {
                        EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                        emp.index = i - 1;
                        Employees.Items[i] = emp;
                    }
                    Employees.Items.RemoveAt(index);
                }
                else
                {
                    TranslateTransform tr = new TranslateTransform();
                    tr.X = 0;
                    st.RenderTransform = tr;
                }
            }
            else
            {
                TranslateTransform tr = new TranslateTransform();
                tr.X = 0;
                st.RenderTransform = tr;
            }
        }
        private void StackPanel_ManipulationStarted_1(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            x = e.ManipulationOrigin.X;
            ContentPresenter cp = (ContentPresenter)VisualTreeHelper.GetParent(Indicators);
            cp.ManipulationStarted += gr_ManipulationStarted;
            PIVOT.IsLocked = true;
        }
        private void StackPanel_ManipulationDelta_1(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            x2 = e.ManipulationOrigin.X;
            y2 = e.ManipulationOrigin.Y;
            if (Math.Max(y2, y) - Math.Min(y2, y) > ((Math.Max(x2, x) - Math.Min(x2, x)) * 2) && scrolLock == false)
            {
                scrolLock = false;
            }
            if ((x2 - x) > ((Math.Max(y2, y) - Math.Min(y2, y)) * 2) && scrolLock == false)
            {
                scrolLock = true;
            }
            if ((x2 - x) < 0)
            {
                scrolLock = false;
                TranslateTransform tr = new TranslateTransform();
                tr.X = 0;
                StackPanel st = (StackPanel)sender;
                st.RenderTransform = tr;
            }
            if (scrolLock == true)
            {
                TranslateTransform tr = new TranslateTransform();
                tr.X = x2 - x;
                StackPanel st = (StackPanel)sender;
                st.RenderTransform = tr;
            }
        }
        private void StackPanel_ManipulationCompleted_1(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            x2 = e.ManipulationOrigin.X;
            y2 = e.ManipulationOrigin.Y;
            StackPanel st = (StackPanel)sender;
            PIVOT.IsLocked = false;
            if (x < x2 && x2 - x > 200 && scrolLock == true)
            {
                 MessageBoxResult msgrez = new MessageBoxResult();
                if (App.RootFrame.Language.IetfLanguageTag == "lt-lt")
                {
                    msgrez = MessageBox.Show("Ar tikrai norite ištrinti šį rodiklį?", "Patvirtinimas", MessageBoxButton.OKCancel);
                }
                else if (App.RootFrame.Language.IetfLanguageTag == "en")
                {
                    msgrez = MessageBox.Show("Do you want to delete this indicator?", "Confirmation", MessageBoxButton.OKCancel);
                }
                else if (App.RootFrame.Language.IetfLanguageTag == "ru-rU")
                {
                    msgrez = MessageBox.Show("Вы действительно хотите удалить этот показатель?", "Подтверждение", MessageBoxButton.OKCancel);
                }
                else
                {
                    msgrez = MessageBox.Show("Do you want to delete this indicator?", "Confirmation", MessageBoxButton.OKCancel);
                }
                if (msgrez == MessageBoxResult.OK)
                {
                    int index = int.Parse(st.Tag.ToString());
                    for (int i = index + 1; i < Indicators.Items.Count; i++)
                    {
                        IndicatorsClass ind = (IndicatorsClass)Indicators.Items[i];
                        ind.index = i - 1;
                        Indicators.Items[i] = ind;
                    }
                    Indicators.Items.RemoveAt(index);
                }
                else
                {
                    TranslateTransform tr = new TranslateTransform();
                    tr.X = 0;
                    st.RenderTransform = tr;
                }
            }
            else
            {
                TranslateTransform tr = new TranslateTransform();
                tr.X = 0;
                st.RenderTransform = tr;
            }
        }
        private void StackPanel_ManipulationStarted_2(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            x = e.ManipulationOrigin.X;
            ContentPresenter cp = (ContentPresenter)VisualTreeHelper.GetParent(Tasks);
            cp.ManipulationStarted += gr_ManipulationStarted;
            PIVOT.IsLocked = true;
        }
        private void StackPanel_ManipulationDelta_2(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            x2 = e.ManipulationOrigin.X;
            y2 = e.ManipulationOrigin.Y;
            if (Math.Max(y2, y) - Math.Min(y2, y) > ((Math.Max(x2, x) - Math.Min(x2, x)) * 2) && scrolLock == false)
            {
                scrolLock = false;
            }
            if ((x2 - x) > ((Math.Max(y2, y) - Math.Min(y2, y)) * 2) && scrolLock == false)
            {
                scrolLock = true;
            }
            if ((x2 - x) < 0)
            {
                scrolLock = false;
                TranslateTransform tr = new TranslateTransform();
                tr.X = 0;
                StackPanel st = (StackPanel)sender;
                st.RenderTransform = tr;
            }
            if (scrolLock == true)
            {
                TranslateTransform tr = new TranslateTransform();
                tr.X = x2 - x;
                StackPanel st = (StackPanel)sender;
                st.RenderTransform = tr;
            }
        }
        private void StackPanel_ManipulationCompleted_2(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            x2 = e.ManipulationOrigin.X;
            y2 = e.ManipulationOrigin.Y;
            StackPanel st = (StackPanel)sender;
            PIVOT.IsLocked = false;
            if (x < x2 && x2 - x > 200 && scrolLock == true)
            {
                 MessageBoxResult msgrez = new MessageBoxResult();
                if (App.RootFrame.Language.IetfLanguageTag == "lt-lt") // PAKEISTI CUSTUM LENTELE arba DOWN MSGBOX source + change text
                {
                    msgrez = MessageBox.Show("Ar tikrai norite ištrinti šią užduotį?", "Patvirtinimas", MessageBoxButton.OKCancel);
                }
                else if (App.RootFrame.Language.IetfLanguageTag == "en")
                {
                    msgrez = MessageBox.Show("Do you want to delete this task?", "Confirmation", MessageBoxButton.OKCancel);
                }
                else if (App.RootFrame.Language.IetfLanguageTag == "ru-rU")
                {
                    msgrez = MessageBox.Show("Вы действительно хотите удалить этот задачу?", "Подтверждение", MessageBoxButton.OKCancel);
                }
                else
                {
                    msgrez = MessageBox.Show("Do you want to delete this task?", "Confirmation", MessageBoxButton.OKCancel);
                }
                if (msgrez == MessageBoxResult.OK)
                {
                    int index = int.Parse(st.Tag.ToString());
                    for (int i = index + 1; i < Tasks.Items.Count; i++)
                    {
                        TasksClass tsk = (TasksClass)Tasks.Items[i];
                        tsk.index = i - 1;
                        Tasks.Items[i] = tsk;
                    }
                    Tasks.Items.RemoveAt(index);
                }
            }
            else
            {
                TranslateTransform tr = new TranslateTransform();
                tr.X = 0;
                st.RenderTransform = tr;
            }
        }
        private void CreateNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            Boolean proceed = true;

            this.Focus();
            TextboxInFocus = false;
            LastSelectedTextbox = new TextBox();
            AddBarEmployee.Height = 280;
            AddBarIndicator.Height = 545;
            AddBarTask.Height = 380;
            AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            AddBarEmployee.ScrollToVerticalOffset(0);
            AddBarIndicator.ScrollToVerticalOffset(0);
            AddBarTask.ScrollToVerticalOffset(0);
            DoubleClickOnTextbox = false;

            if (NameBox.Text == "")
            {
                MessageBox.Show(AppResources.error_employee_name_empty);
                proceed = false;
            }
            else if (BABox.Text == "")
            {
                MessageBox.Show(AppResources.error_salary_empty);
                proceed = false;
            }
            else
            {
                double rezult = 0;
                try
                {
                    rezult = double.Parse(BABox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_salary_number);
                    proceed = false;
                }
            }
            if (proceed == true)
            {
                if(double.Parse(BABox.Text) < 0)
                {
                    MessageBox.Show(AppResources.error_salary_number);
                    proceed = false;
                }
            }
            if(proceed == true)
            {
                if (AddNewItem == true)
                {
                    EmployeeClass EC = new EmployeeClass();
                    EC.NameLine = NameBox.Text; EC.BALine = double.Parse(BABox.Text); EC.RODLine = 0; EC.UZDLine = 0; EC.VisoLine = EC.BALine + EC.RODLine + EC.UZDLine; EC.IsChecked = false;
                    EC.index = Employees.Items.Count;
                    Employees.Items.Add(EC);
                }
                else
                {
                    EmployeeClass EC = (EmployeeClass)Employees.Items[Employees.SelectedIndex];
                    EC.NameLine = NameBox.Text; EC.BALine = double.Parse(BABox.Text); EC.VisoLine = EC.BALine + EC.RODLine + EC.UZDLine;
                    Employees.Items[Employees.SelectedIndex] = EC;
                }
                KillAll();
                NameBox.Text = ""; BABox.Text = "";
            } 
        }
        private void CreateNewIndicator_Click(object sender, RoutedEventArgs e)
        {
            double rezult = 0;
            Boolean resume = true;

            this.Focus();
            TextboxInFocus = false;
            LastSelectedTextbox = new TextBox();
            AddBarEmployee.Height = 280;
            AddBarIndicator.Height = 545;
            AddBarTask.Height = 380;
            AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            AddBarEmployee.ScrollToVerticalOffset(0);
            AddBarIndicator.ScrollToVerticalOffset(0);
            AddBarTask.ScrollToVerticalOffset(0);
            DoubleClickOnTextbox = false;

            if (PavBoxIND.Text == "")
            {
                MessageBox.Show(AppResources.error_indicator_title_empty);
                resume = false;
            }
            if (BRBox.Text == "" && resume == true)
            {
                MessageBox.Show(AppResources.error_br_empty);
                resume = false;
            }
            if (BRBox.Text != "" && resume == true)
            {
                try
                {
                    rezult = double.Parse(BRBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_br_number);
                    resume = false;
                }
            }
            if (TRBox.Text == "" && resume == true)
            {
                MessageBox.Show(AppResources.error_tr_empty);
                resume = false;
            }
            if (TRBox.Text != "" && resume == true)
            {
                try
                {
                    rezult = double.Parse(TRBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_tr_number);
                    resume = false;
                }
            }
            if (resume == true)
            {
                if (double.Parse(TRBox.Text) == double.Parse(BRBox.Text))
                {
                    MessageBox.Show(AppResources.error_BR_TR);
                    resume = false;
                }
            }
            if (FRBox.Text == "" && resume == true)
            {
                MessageBox.Show(AppResources.error_fr_empty);
                resume = false;
            }
            if (FRBox.Text != "" && resume == true)
            {
                try
                {
                    rezult = double.Parse(FRBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_fr_number);
                    resume = false;
                }
            }
            if (MKDBox.Text == "" && resume == true)
            {
                MessageBox.Show(AppResources.error_maxKd_empty);
                resume = false;
            }
            if (MKDBox.Text != "" && resume == true)
            {
                try
                {
                    rezult = double.Parse(MKDBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_maxKd_number);
                    resume = false;
                }
            }
            if (resume == true)
            {
                if (double.Parse(MKDBox.Text) < 0)
                {
                    MessageBox.Show(AppResources.error_maxKd_number);
                    resume = false;
                }
            }
            if(resume == true)
            {
                if (AddNewItem == true)
                {
                    IndicatorsClass IC = new IndicatorsClass();
                    IC.INDPAVLine = PavBoxIND.Text; IC.BRLine = double.Parse(BRBox.Text); IC.TRLine = double.Parse(TRBox.Text); IC.FRLine = double.Parse(FRBox.Text); IC.MKDLine = double.Parse(MKDBox.Text); IC.IsChecked = false;
                    IC.index = Indicators.Items.Count;
                    Indicators.Items.Add(IC);
                }
                else
                {
                    IndicatorsClass IC = (IndicatorsClass)Indicators.Items[Indicators.SelectedIndex];
                    IC.INDPAVLine = PavBoxIND.Text; IC.BRLine = double.Parse(BRBox.Text); IC.TRLine = double.Parse(TRBox.Text); IC.FRLine = double.Parse(FRBox.Text); IC.MKDLine = double.Parse(MKDBox.Text);
                    Indicators.Items[Indicators.SelectedIndex] = IC;
                }
                KillAll();
                PavBoxIND.Text = ""; BRBox.Text = ""; TRBox.Text = ""; FRBox.Text = ""; MKDBox.Text = "";
            }  
        }
        private void CreateNewTask_Click(object sender, RoutedEventArgs e)
        {
            double rezult = 0;
            Boolean resume = true;

            this.Focus();
            TextboxInFocus = false;
            LastSelectedTextbox = new TextBox();
            AddBarEmployee.Height = 280;
            AddBarIndicator.Height = 545;
            AddBarTask.Height = 380;
            AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            AddBarEmployee.ScrollToVerticalOffset(0);
            AddBarIndicator.ScrollToVerticalOffset(0);
            AddBarTask.ScrollToVerticalOffset(0);
            DoubleClickOnTextbox = false;

            if (PavBoxTSK.Text == "")
            {
                MessageBox.Show(AppResources.error_task_title_empty);
                resume = false;
            }
            if (MAXIVBox.Text == "" && resume == true)
            {
                MessageBox.Show(AppResources.error_task_value_empty);
                resume = false;
            }
            if (MAXIVBox.Text != "" && resume == true)
            {
                try
                {
                    rezult = double.Parse(MAXIVBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_task_value_number);
                    resume = false;
                }
            }
            if (resume == true)
            {
                if (double.Parse(MAXIVBox.Text) < 0)
                {
                    MessageBox.Show(AppResources.error_task_value_number);
                    resume = false;
                }
            }
            if (IVBox.Text == "" && resume == true)
            {
                MessageBox.Show(AppResources.error_task_result_empty);
                resume = false;
            }
            if (IVBox.Text == "" && resume == true)
            {
                try
                {
                    rezult = double.Parse(IVBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.error_task_result_number);
                    resume = false;
                }
            }
            if (resume == true)
            {
                if (double.Parse(IVBox.Text) < 0)
                {
                    MessageBox.Show(AppResources.error_task_result_number);
                    resume = false;
                }
            }
            if (resume == true)
            {
                if (double.Parse(IVBox.Text) > double.Parse(MAXIVBox.Text))
                {
                    MessageBox.Show(AppResources.error_result_bigger_than_value);
                }
                else
                {
                    if (AddNewItem == true)
                    {
                        TasksClass TC = new TasksClass();
                        TC.UZDPAVLine = PavBoxTSK.Text; TC.Ivert = double.Parse(IVBox.Text); TC.MaxIvert = double.Parse(MAXIVBox.Text); TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString(); TC.IsChecked = false;
                        TC.index = Tasks.Items.Count;
                        Tasks.Items.Add(TC);
                    }
                    else
                    {
                        TasksClass TC = (TasksClass)Tasks.Items[Tasks.SelectedIndex];
                        TC.UZDPAVLine = PavBoxTSK.Text; TC.Ivert = double.Parse(IVBox.Text); TC.MaxIvert = double.Parse(MAXIVBox.Text); TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString();
                        Tasks.Items[Tasks.SelectedIndex] = TC;
                    }
                    KillAll();
                    PavBoxTSK.Text = ""; IVBox.Text = ""; MAXIVBox.Text = "";
                }
            }
        }
        private void PIVOT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees.SelectedIndex = -1;
            Indicators.SelectedIndex = -1;
            Tasks.SelectedIndex = -1;
            KillAll();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddBarEmployee.Height = 545;
            int index = PIVOT.SelectedIndex;
            AddNewItem = false;
            if (index == 0)
            {
                if (AddBarEmployee.Visibility == System.Windows.Visibility.Visible)
                {
                    AddBarEmployee.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    KillAll();
                    AddBarEmployee.Visibility = System.Windows.Visibility.Visible;
                    EmployeeClass emp = (EmployeeClass)Employees.Items[Employees.SelectedIndex];
                    NameBox.Text = emp.NameLine;
                    BABox.Text = emp.BALine.ToString();
                }
            }
            else if (index == 1)
            {
                if (AddBarIndicator.Visibility == System.Windows.Visibility.Visible)
                {
                    AddBarIndicator.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    KillAll();
                    AddBarIndicator.Visibility = System.Windows.Visibility.Visible;
                    IndicatorsClass ind = (IndicatorsClass)Indicators.Items[Indicators.SelectedIndex];
                    PavBoxIND.Text = ind.INDPAVLine;
                    BRBox.Text = ind.BRLine.ToString();
                    FRBox.Text = ind.FRLine.ToString();
                    TRBox.Text = ind.TRLine.ToString();
                    MKDBox.Text = ind.MKDLine.ToString();
                }
            }
            else if (index == 2) // tasks
            {

                if (AddBarTask.Visibility == System.Windows.Visibility.Visible)
                {
                    AddBarTask.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    KillAll();
                    AddBarTask.Visibility = System.Windows.Visibility.Visible;
                    TasksClass tsk = (TasksClass)Tasks.Items[Tasks.SelectedIndex];
                    PavBoxTSK.Text = tsk.UZDPAVLine;
                    MAXIVBox.Text = tsk.MaxIvert.ToString();
                    IVBox.Text = tsk.Ivert.ToString();
                }
            }
        }
        private void CreateNewEmployeeBack_Click(object sender, RoutedEventArgs e)
        {
            int index = PIVOT.SelectedIndex;
            if (index == 0)
            {
                Employees.SelectedIndex = -1;
            }
            else if (index == 1)
            {
                Indicators.SelectedIndex = -1;
            }
            else if (index == 2)
            {
                Tasks.SelectedIndex = -1;
            }
            NameBox.Text = ""; BABox.Text = "";
            PavBoxIND.Text = ""; BRBox.Text = ""; TRBox.Text = ""; FRBox.Text = ""; MKDBox.Text = "";
            PavBoxTSK.Text = ""; IVBox.Text = ""; MAXIVBox.Text = "";
            MAXKDPBox.Text = "";
            KillAll();
        }
        private void MAXKDPBoxOk_Click(object sender, RoutedEventArgs e)
        {
            Boolean resume = true;
            if (resume == true)
            {
                if (TikrintiArPazymeti() == true)
                {
                    Calc_Method();
                }
                KillAll();
                PIVOT.SelectedIndex = 0;
            }
        }
        private void Calc_Method()
        {
            Tuple<double, double> result = calculateSalary();
            if (result.Item1 < 0 || result.Item2 < 0)
            {
                MessageBox.Show(AppResources.error_bad_result_value);
            }
            else
            {
                for (int i = 0; i < Employees.Items.Count; i++)
                {
                    EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                    if (emp.IsChecked == true)
                    {
                        emp.RODLine = Convert.ToDouble(result.Item1);
                        emp.UZDLine = Convert.ToDouble(result.Item2);
                        emp.VisoLine = emp.BALine + Convert.ToDouble(result.Item1 + result.Item2);
                        emp.IsChecked = false;
                        lastEmployeeChecked = -1;
                        emp.RodList = new List<IndicatorsClass>();
                        emp.UzdList = new List<TasksClass>();
                        for (int j = 0; j < Indicators.Items.Count; j++)
                        {
                            IndicatorsClass ind = (IndicatorsClass)Indicators.Items[j];
                            if (ind.IsChecked == true)
                            {
                                IndicatorsClass newind = new IndicatorsClass();
                                newind.FRLine = ind.FRLine; newind.BRLine = ind.BRLine; newind.index = emp.RodList.Count; newind.INDPAVLine = ind.INDPAVLine; newind.IsChecked = false; newind.MKDLine = ind.MKDLine; newind.TRLine = ind.TRLine;
                                emp.RodList.Add(newind);
                                ind.IsChecked = false;
                                Indicators.Items[j] = ind;
                            }
                        }
                        for (int j = 0; j < Tasks.Items.Count; j++)
                        {
                            TasksClass tsk = (TasksClass)Tasks.Items[j];
                            if (tsk.IsChecked == true)
                            {
                                TasksClass newtsk = new TasksClass();
                                newtsk.index = emp.UzdList.Count; newtsk.IsChecked = false; newtsk.Ivert = tsk.Ivert; newtsk.IVERTLine = tsk.IVERTLine; newtsk.MaxIvert = tsk.MaxIvert; newtsk.UZDPAVLine = tsk.UZDPAVLine;
                                emp.UzdList.Add(newtsk);
                                tsk.IsChecked = false;
                                Tasks.Items[j] = tsk;
                            }
                        }
                        if (MAXKDPBox.Text.Equals("")) emp.MaxKDP = 0;
                        else emp.MaxKDP = Double.Parse(MAXKDPBox.Text);
                        Employees.Items[i] = emp;
                    }
                }
            }
        }
        private Tuple<double, double> calculateSalary()
        {
            double indicatorsSalary = 0;
            double tasksSalary = 0;
            if (Indicators.Items.Count != 0)
            {
                for (int i = 0; i < Indicators.Items.Count; i++)
                {
                    IndicatorsClass ind = (IndicatorsClass)Indicators.Items[i];
                    if (ind.IsChecked == true)
                    {
                        if (ind.TRLine > ind.BRLine)
                        {
                            if (ind.BRLine <= ind.FRLine && ind.FRLine <= ind.TRLine)
                            {
                                indicatorsSalary += Math.Truncate(((ind.FRLine - ind.BRLine) / (ind.TRLine - ind.BRLine) * ind.MKDLine)*100)/100;
                            }
                            if (ind.FRLine > ind.TRLine)
                            {
                                indicatorsSalary += Math.Truncate((ind.MKDLine)*100)/100;
                            }
                        }
                        else if (ind.TRLine < ind.BRLine)
                        {
                            if (ind.TRLine <= ind.FRLine && ind.FRLine <= ind.BRLine)
                            {
                                indicatorsSalary += Math.Truncate(((ind.FRLine - ind.BRLine) / (ind.TRLine - ind.BRLine) * ind.MKDLine) * 100) / 100;
                            }
                            if (ind.FRLine < ind.TRLine)
                            {
                                indicatorsSalary += Math.Truncate((ind.MKDLine) * 100) / 100;
                            }
                        }
                    }
                }
            }
            double sfSum = 0;
            double sSum = 0;
            if (Tasks.Items.Count != 0)
            {
                for (int i = 0; i < Tasks.Items.Count; i++)
                {
                    TasksClass tsk = (TasksClass)Tasks.Items[i];
                    if (tsk.IsChecked == true)
                    {
                        sfSum += tsk.Ivert; // ivertinimas
                        sSum += tsk.MaxIvert;   // maksimalus ivertinimas
                    }
                }
            }
            if (sSum != 0)
            {
                tasksSalary = Math.Truncate(((sfSum / sSum) * Convert.ToDouble(MAXKDPBox.Text))*100)/100;
            }
            else
            {
                tasksSalary = 0;
            }
            return new Tuple<double, double>(indicatorsSalary, tasksSalary);
        }
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailPanel.Visibility = System.Windows.Visibility.Visible;
            EmployeeClass emp = (EmployeeClass) Employees.Items[Employees.SelectedIndex];
            ShowVAR.Text = emp.NameLine;
            ShowBA.Text = emp.BALine.ToString();
            ShowROD.Text = emp.RODLine.ToString();
            ShowUZD.Text = emp.UZDLine.ToString();
            ShowViso.Text = emp.VisoLine.ToString();
            SHowMAxKDP.Text = emp.MaxKDP.ToString();
            ShowIndicators.Items.Clear();
            ShowTasks.Items.Clear();
            if (emp.RodList != null)
            {
                for (int i = 0; i < emp.RodList.Count; i++)
                {
                    ShowIndicators.Items.Add(emp.RodList[i]);
                }
            }
            if (emp.UzdList != null)
            {
                for (int i = 0; i < emp.UzdList.Count; i++)
                {
                    ShowTasks.Items.Add(emp.UzdList[i]);
                }
            }
        }

        private void HelperGrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (TextboxInFocus == true && DoubleClickOnTextbox == false)
            {
                this.Focus();
                TextboxInFocus = false;
                AddBarEmployee.Height = 280; 
                AddBarIndicator.Height = 545;
                AddBarTask.Height = 380;
                AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                AddBarEmployee.ScrollToVerticalOffset(0);
                AddBarIndicator.ScrollToVerticalOffset(0);
                AddBarTask.ScrollToVerticalOffset(0);
                DoubleClickOnTextbox = false;
            }
        }
        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextboxInFocus = true;
            App.RootFrame.RenderTransform = new CompositeTransform();
            TextBox textboxSender = (TextBox)sender;
            AddBarEmployee.ScrollToVerticalOffset(Convert.ToDouble(textboxSender.Tag) * 90);
            AddBarIndicator.ScrollToVerticalOffset(Convert.ToDouble(textboxSender.Tag) * 90);
            AddBarTask.ScrollToVerticalOffset(Convert.ToDouble(textboxSender.Tag) * 90);
            AddBarEmployee.Height = AddBarIndicator.Height = AddBarTask.Height = 215; 
            AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            if (DoubleClickOnTextbox == false)
            {
                DoubleClickOnTextbox = true;
                TextBox text = (TextBox)sender;
                //NameBox_GotFocus(sender, e);
                ScrollToOfSet(Convert.ToDouble(textboxSender.Tag) * 90);
            }
            else
            {
                DoubleClickOnTextbox = false;
            }
        }

        public async System.Threading.Tasks.Task ScrollToOfSet(double offset)
        {
            System.Threading.Tasks.Task<int> Delay = DelayOperation();
            int result = await Delay; 
            AddBarEmployee.ScrollToVerticalOffset(offset);
            AddBarIndicator.ScrollToVerticalOffset(offset);
            AddBarTask.ScrollToVerticalOffset(offset);
        }

        public async System.Threading.Tasks.Task<int> DelayOperation()
        {
            await System.Threading.Tasks.Task.Delay(350);
            return 1;
        }

        private void NameBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (LastSelectedTextbox.Equals(text) == false)
            {
                LastSelectedTextbox = text;
                DoubleClickOnTextbox = false;
                TextboxInFocus = false;
                text.Focus();
            }
            else
            {
                DoubleClickOnTextbox = true;
            }
        }
        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextboxInFocus == true)
            {
                AddBarEmployee.Height = 280; //280 545 380;
                AddBarIndicator.Height = 545;
                AddBarTask.Height = 380;
                AddBarEmployee.VerticalAlignment = AddBarIndicator.VerticalAlignment = AddBarTask.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                AddBarEmployee.ScrollToVerticalOffset(0);
                AddBarIndicator.ScrollToVerticalOffset(0);
                AddBarTask.ScrollToVerticalOffset(0);
                DoubleClickOnTextbox = false;
            }
        }
        private void MAXKDPBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Spacing.Height = 330;
        }
        private void MAXKDPBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Spacing.Height = 0;
        }
    }
    public class EmployeeClass
    {
        public string NameLine { get; set; }
        public double BALine { get; set; }
        public double RODLine { get; set; }
        public double UZDLine { get; set; }
        public double VisoLine { get; set; }
        public Boolean IsChecked { get; set; }
        public int index { get; set; }
        public List<IndicatorsClass> RodList { get; set; }
        public List<TasksClass> UzdList { get; set; }
        public double MaxKDP { get; set; }
    }
    public class IndicatorsClass
    {
        public string INDPAVLine { get; set; }
        public double BRLine { get; set; }
        public double FRLine { get; set; }
        public double TRLine { get; set; }
        public double MKDLine { get; set; }
        public Boolean IsChecked { get; set; }
        public int index { get; set; }
    }
    public class TasksClass
    {
        public string UZDPAVLine { get; set; }
        public double MaxIvert { get; set; }
        public double Ivert { get; set; }
        public string IVERTLine { get; set; }
        public Boolean IsChecked { get; set; }
        public int index { get; set; }
    }
    public class ArchyvedEmployeeClass
    {
        public string NameLine { get; set; }
        public double BALine { get; set; }
        public double RODLine { get; set; }
        public double UZDLine { get; set; }
        public double VisoLine { get; set; }
        public Boolean IsChecked { get; set; }
        public string Date { get; set; }
        public int index { get; set; }
        public List<IndicatorsClass> RodList { get; set; }
        public List<TasksClass> UzdList { get; set; }
        public double MaxKDP { get; set; }
    }
}