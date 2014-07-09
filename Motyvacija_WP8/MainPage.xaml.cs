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

namespace Motyvacija_WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        int lastEmployeeChecked;
        double x, x2, y, y2;
        Boolean scrolLock;
        Boolean AddNewItem;
        List<ArchyvedEmployeeClass> AEC;
        Boolean CanExit;
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
            PhoneApplicationService.Current.State["AEC"] = AEC;
            base.OnNavigatedFrom(e);
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {   
            base.OnBackKeyPress(e);
            e.Cancel = true;
            SaveAll();
            SaveKalba(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            Application.Current.Terminate();
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
            while (file.EndOfStream != true)
            {
                string eilute = file.ReadLine();
                if (eilute == "Darbuotoju lentele:")
                {
                    int index = Convert.ToInt32(file.ReadLine());
                    for (int i = 0; i < index; i++)
                    {
                        string[] reiksmes = file.ReadLine().Split(',');
                        EmployeeClass EC = new EmployeeClass();
                        EC.NameLine = reiksmes[0]; EC.BALine = Convert.ToDouble(reiksmes[1]); EC.RODLine = Convert.ToDouble(reiksmes[2]); EC.UZDLine = Convert.ToDouble(reiksmes[3]); EC.VisoLine = Convert.ToDouble(reiksmes[4]); EC.IsChecked = false;
                        EC.index = i;
                        reiksmes = file.ReadLine().Split(',');
                        int rod = Convert.ToInt32(reiksmes[0]);
                        int uzd = Convert.ToInt32(reiksmes[1]);
                        EC.MaxKDP = Convert.ToDouble(reiksmes[2]);
                        EC.RodList = new List<IndicatorsClass>();
                        EC.UzdList = new List<TasksClass>();
                        for (int j = 0; j < rod; j++)
                        {
                            reiksmes = file.ReadLine().Split(',');
                            IndicatorsClass ind = new IndicatorsClass();
                            ind.INDPAVLine = reiksmes[0]; ind.BRLine = Convert.ToDouble(reiksmes[1]); ind.FRLine = Convert.ToDouble(reiksmes[2]); ind.TRLine = Convert.ToDouble(reiksmes[3]); ind.MKDLine = Convert.ToDouble(reiksmes[4]); ind.IsChecked = false;
                            ind.index = j;
                            EC.RodList.Add(ind);
                        }
                        for (int j = 0; j < uzd; j++)
                        {
                            reiksmes = file.ReadLine().Split(',');
                            TasksClass tsk = new TasksClass();
                            tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = Convert.ToDouble(reiksmes[1]); tsk.Ivert = Convert.ToDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
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
                        string[] reiksmes = file.ReadLine().Split(',');
                        IndicatorsClass ind = new IndicatorsClass();
                        ind.INDPAVLine = reiksmes[0]; ind.BRLine = Convert.ToDouble(reiksmes[1]); ind.FRLine = Convert.ToDouble(reiksmes[2]); ind.TRLine = Convert.ToDouble(reiksmes[3]); ind.MKDLine = Convert.ToDouble(reiksmes[4]); ind.IsChecked = false;
                        ind.index = i;
                        Indicators.Items.Add(ind);
                    }
                }
                else if (eilute == "Uzduociu lentele:")
                {
                    int index = Convert.ToInt32(file.ReadLine());
                    for (int i = 0; i < index; i++)
                    {
                        string[] reiksmes = file.ReadLine().Split(',');
                        TasksClass tsk = new TasksClass();
                        tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = Convert.ToDouble(reiksmes[1]); tsk.Ivert = Convert.ToDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
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
                        string[] reiksmes = file.ReadLine().Split(',');
                        ArchyvedEmployeeClass EC = new ArchyvedEmployeeClass();
                        EC.NameLine = reiksmes[0]; EC.BALine = Convert.ToDouble(reiksmes[1]); EC.RODLine = Convert.ToDouble(reiksmes[2]); EC.UZDLine = Convert.ToDouble(reiksmes[3]); EC.VisoLine = Convert.ToDouble(reiksmes[4]); EC.Date = reiksmes[5]; EC.IsChecked = false;
                        EC.index = i;
                        reiksmes = file.ReadLine().Split(',');
                        int rod = Convert.ToInt32(reiksmes[0]);
                        int uzd = Convert.ToInt32(reiksmes[1]);
                        EC.MaxKDP = Convert.ToDouble(reiksmes[2]);
                        EC.RodList = new List<IndicatorsClass>();
                        EC.UzdList = new List<TasksClass>();
                        for (int j = 0; j < rod; j++)
                        {
                            reiksmes = file.ReadLine().Split(',');
                            IndicatorsClass ind = new IndicatorsClass();
                            ind.INDPAVLine = reiksmes[0]; ind.BRLine = Convert.ToDouble(reiksmes[1]); ind.FRLine = Convert.ToDouble(reiksmes[2]); ind.TRLine = Convert.ToDouble(reiksmes[3]); ind.MKDLine = Convert.ToDouble(reiksmes[4]); ind.IsChecked = false;
                            ind.index = j;
                            EC.RodList.Add(ind);
                        }
                        for (int j = 0; j < uzd; j++)
                        {
                            reiksmes = file.ReadLine().Split(',');
                            TasksClass tsk = new TasksClass();
                            tsk.UZDPAVLine = reiksmes[0]; tsk.MaxIvert = Convert.ToDouble(reiksmes[1]); tsk.Ivert = Convert.ToDouble(reiksmes[2]); tsk.IVERTLine = (tsk.Ivert + " / " + tsk.MaxIvert).ToString(); tsk.IsChecked = false;
                            tsk.index = j;
                            EC.UzdList.Add(tsk);
                        }
                        AEC.Add(EC);
                    }
                }
            }
            file.Close();
        }
        private void SaveAll()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(new System.IO.IsolatedStorage.IsolatedStorageFileStream("Duom.txt", System.IO.FileMode.Create, System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication()));
            file.WriteLine("Darbuotoju lentele:");
            file.WriteLine(Employees.Items.Count);
            for (int i = 0; i < Employees.Items.Count; i++)
            {
                EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                file.WriteLine(emp.NameLine + "," + emp.BALine.ToString() + "," + emp.RODLine.ToString() + "," + emp.UZDLine.ToString() + "," + emp.VisoLine.ToString());
                if (emp.RodList == null)
                {
                    emp.RodList = new List<IndicatorsClass>();
                }
                if (emp.UzdList == null)
                {
                    emp.UzdList = new List<TasksClass>();
                }
                file.WriteLine(emp.RodList.Count.ToString() + "," + emp.UzdList.Count.ToString() + "," + emp.MaxKDP.ToString());
                for (int j = 0; j < emp.RodList.Count; j++)
                {
                    file.WriteLine(emp.RodList[j].INDPAVLine + "," + emp.RodList[j].BRLine.ToString() + "," + emp.RodList[j].FRLine.ToString() + "," + emp.RodList[j].TRLine.ToString() + "," + emp.RodList[j].MKDLine.ToString());
                }
                for (int j = 0; j < emp.UzdList.Count; j++)
                {
                    file.WriteLine(emp.UzdList[j].UZDPAVLine + "," + emp.UzdList[j].MaxIvert.ToString() + "," + emp.UzdList[j].Ivert.ToString());
                }
            }

            file.WriteLine("Rodikliu lentele:");
            file.WriteLine(Indicators.Items.Count.ToString());
            for (int i = 0; i < Indicators.Items.Count; i++)
            {
                IndicatorsClass ind = (IndicatorsClass)Indicators.Items[i];
                file.WriteLine(ind.INDPAVLine + "," + ind.BRLine.ToString() + "," + ind.FRLine.ToString() + "," + ind.TRLine.ToString() + "," + ind.MKDLine.ToString());
            }

            file.WriteLine("Uzduociu lentele:");
            file.WriteLine(Tasks.Items.Count.ToString());
            for (int i = 0; i < Tasks.Items.Count; i++)
            {
                TasksClass tsk = (TasksClass)Tasks.Items[i];
                file.WriteLine(tsk.UZDPAVLine + "," + tsk.MaxIvert.ToString() + "," + tsk.Ivert.ToString());
            }
            if (AEC == null)
            {
                AEC = new List<ArchyvedEmployeeClass>();
            }
            file.WriteLine("Archyvo lentele:");
            file.WriteLine(AEC.Count);
            for (int i = 0; i < AEC.Count; i++)
            {
                file.WriteLine(AEC[i].NameLine + "," + AEC[i].BALine.ToString() + "," + AEC[i].RODLine.ToString() + "," + AEC[i].UZDLine.ToString() + "," + AEC[i].VisoLine.ToString() + "," + AEC[i].Date);
                if (AEC[i].RodList == null)
                {
                    AEC[i].RodList = new List<IndicatorsClass>();
                }
                if (AEC[i].UzdList == null)
                {
                    AEC[i].UzdList = new List<TasksClass>();
                }
                file.WriteLine(AEC[i].RodList.Count.ToString() + "," + AEC[i].UzdList.Count.ToString() + "," + AEC[i].MaxKDP.ToString());
                for (int j = 0; j < AEC[i].RodList.Count; j++)
                {
                    file.WriteLine(AEC[i].RodList[j].INDPAVLine + "," + AEC[i].RodList[j].BRLine.ToString() + "," + AEC[i].RodList[j].FRLine.ToString() + "," + AEC[i].RodList[j].TRLine.ToString() + "," + AEC[i].RodList[j].MKDLine.ToString());
                }
                for (int j = 0; j < AEC[i].UzdList.Count; j++)
                {
                    file.WriteLine(AEC[i].UzdList[j].UZDPAVLine + "," + AEC[i].UzdList[j].MaxIvert.ToString() + "," + AEC[i].UzdList[j].Ivert.ToString());
                }
            }
            file.Close();
            CanExit = true;
        }
        private void KillAll()
        {
            MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
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
        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e) // Add
        {
            int index = PIVOT.SelectedIndex;
            AddNewItem = true;

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
                MAxKDPST.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private Boolean TikrintiArPazymeti()
        {
            int kiekRado = 0;

            double rezult;
            try
            {
                rezult = double.Parse(MAXKDPBox.Text);
            }
            catch (Exception)
            {
                MAXKDPBox.Text = "0";
            }

            Boolean rado = false;

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
                }
            }
            if (rado == true)
            {
                kiekRado++;
            }

            if (kiekRado > 0)
            {
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
                //MessageBoxResult msgrez = new MessageBoxResult();
                //if (App.RootFrame.Language.IetfLanguageTag == "lt-lt") // PAKEISTI CUSTUM LENTELE arba DOWN MSGBOX source + change text
                //{
                //    msgrez = MessageBox.Show("Ar tikrai norite ištrinti šį darbuotoją?", "Patvirtinimas", MessageBoxButton.OKCancel);
                //}
                //else if (App.RootFrame.Language.IetfLanguageTag == "en")
                //{
                //    msgrez = MessageBox.Show("Do you want to delete this employee?", "Confirmation", MessageBoxButton.OKCancel);
                //}
                //else if (App.RootFrame.Language.IetfLanguageTag == "ru-rU")
                //{
                //    msgrez = MessageBox.Show("Вы действительно хотите удалить этот сотрудникa?", "Подтверждение", MessageBoxButton.OKCancel);
                //}
                //else
                //{
                //    msgrez = MessageBox.Show("Do you want to delete this employee?", "Confirmation", MessageBoxButton.OKCancel);
                //}
                //if (msgrez == MessageBoxResult.OK)
                //{
                    int index = int.Parse(st.Tag.ToString());
                    for (int i = index + 1; i < Employees.Items.Count; i++)
                    {
                        EmployeeClass emp = (EmployeeClass)Employees.Items[i];
                        emp.index = i - 1;
                        Employees.Items[i] = emp;
                    }
                    Employees.Items.RemoveAt(index);
                //}
                //else
                //{
                //    TranslateTransform tr = new TranslateTransform();
                //    tr.X = 0;
                //    st.RenderTransform = tr;
                //}
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
                int index = int.Parse(st.Tag.ToString());
                for (int i = index + 1; i < Tasks.Items.Count; i++)
                {
                    TasksClass tsk = (TasksClass)Tasks.Items[i];
                    tsk.index = i - 1;
                    Tasks.Items[i] = tsk;
                }
                Tasks.Items.RemoveAt(index);
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
            if (NameBox.Text == "")
            {
                NameBox.Text = "*";
            }
            if (BABox.Text == "")
            {
                BABox.Text = "0";
            }
            double rezult = 0;
            try
            {
                rezult = double.Parse(BABox.Text);
            }
            catch (Exception)
            {
                BABox.Text = "0";
            }
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
        private void CreateNewIndicator_Click(object sender, RoutedEventArgs e)
        {
            if (PavBoxIND.Text == "")
            {
                PavBoxIND.Text = "*";
            }
            if (BRBox.Text == "")
            {
                BRBox.Text = "0";
            }
            double rezult = 0;
            try
            {
                rezult = double.Parse(BRBox.Text);
            }
            catch (Exception)
            {
                BRBox.Text = "0";
            }
            if (TRBox.Text == "")
            {
                TRBox.Text = "0";
            }
            try
            {
                rezult = double.Parse(TRBox.Text);
            }
            catch (Exception)
            {
                TRBox.Text = "0";
            }
            if (FRBox.Text == "")
            {
                FRBox.Text = "0";
            }
            try
            {
                rezult = double.Parse(FRBox.Text);
            }
            catch (Exception)
            {
                FRBox.Text = "0";
            }
            if (MKDBox.Text == "")
            {
                MKDBox.Text = "0";
            }
            try
            {
                rezult = double.Parse(MKDBox.Text);
            }
            catch (Exception)
            {
                MKDBox.Text = "0";
            }
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
        private void CreateNewTask_Click(object sender, RoutedEventArgs e)
        {
            if (PavBoxTSK.Text == "")
            {
                PavBoxTSK.Text = "*";
            }
            if (IVBox.Text == "")
            {
                IVBox.Text = "0";
            }
            double rezult = 0;
            try
            {
                rezult = double.Parse(IVBox.Text);
            }
            catch (Exception)
            {
                IVBox.Text = "0";
            }
            if (MAXIVBox.Text == "")
            {
                MAXIVBox.Text = "0";
            }
            try
            {
                rezult = double.Parse(MAXIVBox.Text);
            }
            catch (Exception)
            {
                MAXIVBox.Text = "0";
            }
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
        private void PIVOT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees.SelectedIndex = -1;
            Indicators.SelectedIndex = -1;
            Tasks.SelectedIndex = -1;
            KillAll();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
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
            if (TikrintiArPazymeti() == true)
            {
                Tuple<int, int> result = calculateSalary();
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
                        emp.MaxKDP = Double.Parse(MAXKDPBox.Text);
                        Employees.Items[i] = emp;
                    }
                }
            }
            KillAll();
            PIVOT.SelectedIndex = 0;
        }
        private Tuple<int, int> calculateSalary()
        {
            int indicatorsSalary = 0;
            int tasksSalary = 0;
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
                                indicatorsSalary += Convert.ToInt32(Math.Ceiling( (ind.FRLine - ind.BRLine) / (ind.TRLine - ind.BRLine) * ind.MKDLine ));
                            }
                            if (ind.FRLine > ind.TRLine)
                            {
                                indicatorsSalary += Convert.ToInt32(Math.Ceiling(ind.MKDLine));
                            }
                        }
                        else if (ind.TRLine < ind.BRLine)
                        {
                            if (ind.TRLine <= ind.FRLine && ind.FRLine <= ind.BRLine)
                            {
                                indicatorsSalary += Convert.ToInt32(Math.Ceiling((ind.FRLine - ind.BRLine) / (ind.TRLine - ind.BRLine) * ind.MKDLine));
                            }
                            if (ind.FRLine < ind.TRLine)
                            {
                                indicatorsSalary += Convert.ToInt32(Math.Ceiling(ind.MKDLine));
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
                tasksSalary = Convert.ToInt32(Math.Ceiling((sfSum / sSum) * Convert.ToDouble(MAXKDPBox.Text)));
            }
            else
            {
                tasksSalary = 0;
            }
            return new Tuple<int, int>(indicatorsSalary, tasksSalary);
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