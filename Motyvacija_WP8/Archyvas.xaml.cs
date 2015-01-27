using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Globalization;
using System.Windows.Media;
namespace Motyvacija_WP8
{
    public partial class Archyvas : PhoneApplicationPage
    {
        List<ArchyvedEmployeeClass> AEC;
        double x, y, x2, y2;
        Boolean scrolLock;
        public Archyvas()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (PhoneApplicationService.Current.State.ContainsKey("AEC"))
            {
                AEC = (List<ArchyvedEmployeeClass>)PhoneApplicationService.Current.State["AEC"];
                PopList();
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            AEC.Clear();
            for (int i = 0; i < ArchivedEmployees.Items.Count; i++)
            {
                AEC.Add((ArchyvedEmployeeClass)ArchivedEmployees.Items[i]);
            }
            PhoneApplicationService.Current.State["AEC"] = AEC;
            base.OnNavigatedFrom(e);
        }
        private void PopList()
        {
            ArchivedEmployees.Items.Clear();
            for (int i = 0; i < AEC.Count; i++)
            {
                
                ArchyvedEmployeeClass EC = new ArchyvedEmployeeClass();
                EC.NameLine = AEC[i].NameLine; EC.BALine = AEC[i].BALine; EC.RODLine = AEC[i].RODLine; EC.UZDLine = AEC[i].UZDLine; EC.VisoLine = AEC[i].VisoLine; EC.Date = AEC[i].Date; EC.IsChecked = false;
                EC.MaxKDP = AEC[i].MaxKDP;
                EC.index = i;
                EC.RodList = new List<IndicatorsClass>();
                EC.UzdList = new List<TasksClass>();
                if (AEC[i].RodList == null)
                {
                    AEC[i].RodList = new List<IndicatorsClass>();
                }
                if (AEC[i].UzdList == null)
                {
                    AEC[i].UzdList = new List<TasksClass>();
                }
                for (int j = 0; j < AEC[i].RodList.Count; j++)
                {
                    IndicatorsClass ind = new IndicatorsClass();
                    ind.INDPAVLine = AEC[i].RodList[j].INDPAVLine; ind.BRLine = AEC[i].RodList[j].BRLine; ind.FRLine = AEC[i].RodList[j].FRLine; ind.TRLine = AEC[i].RodList[j].TRLine; ind.MKDLine = AEC[i].RodList[j].MKDLine; ind.IsChecked = false;
                    ind.index = j;
                    EC.RodList.Add(ind);
                }
                for (int j = 0; j < AEC[i].UzdList.Count; j++)
                {
                    TasksClass tsk = new TasksClass();
                    tsk.UZDPAVLine = AEC[i].UzdList[j].UZDPAVLine; tsk.MaxIvert = AEC[i].UzdList[j].MaxIvert; tsk.Ivert = AEC[i].UzdList[j].Ivert; tsk.IVERTLine = AEC[i].UzdList[j].IVERTLine; tsk.IsChecked = false;
                    tsk.index = j;
                    EC.UzdList.Add(tsk);
                }
                ArchivedEmployees.Items.Add(EC);
            }   
        }
        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            x = e.ManipulationOrigin.X;
            Grid cp = (Grid)VisualTreeHelper.GetParent(ArchivedEmployees);
            cp.ManipulationStarted += gr_ManipulationStarted;
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
            if (x < x2 && x2 - x > 200 && scrolLock == true)
            {
                MessageBoxResult msgrez = new MessageBoxResult();
                if (App.RootFrame.Language.IetfLanguageTag == "lt-lt") // PAKEISTI CUSTUM LENTELE arba DOWN MSGBOX source + change text
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
                for (int i = index + 1; i < ArchivedEmployees.Items.Count; i++)
                {
                    ArchyvedEmployeeClass emp = (ArchyvedEmployeeClass)ArchivedEmployees.Items[i];
                    emp.index = i - 1;
                    ArchivedEmployees.Items[i] = emp;
                }
                ArchivedEmployees.Items.RemoveAt(index);
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
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailPanel.Visibility = System.Windows.Visibility.Visible;
            Show.Visibility = System.Windows.Visibility.Collapsed;
            EditShowGrid.Visibility = System.Windows.Visibility.Collapsed;
            ArchyvedEmployeeClass emp = (ArchyvedEmployeeClass)ArchivedEmployees.Items[ArchivedEmployees.SelectedIndex];
            ShowVAR.Text = emp.NameLine;
            ShowBA.Text = emp.BALine.ToString();
            ShowROD.Text = emp.RODLine.ToString();
            ShowUZD.Text = emp.UZDLine.ToString();
            ShowViso.Text = emp.VisoLine.ToString();
            SHowMAxKDP.Text = emp.MaxKDP.ToString();
            ShowDate.Text = emp.Date;
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
        private void BackDetaiPanel_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailPanel.Visibility = System.Windows.Visibility.Collapsed;
            EditShowGrid.Visibility = System.Windows.Visibility.Collapsed;
            Show.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void ArchivedEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditShowGrid.Visibility = System.Windows.Visibility.Visible;
            Show.Visibility = System.Windows.Visibility.Visible;
        }
    }
    //public class ArchyvedEmployeeClass
    //{
    //    public string NameLine { get; set; }
    //    public double BALine { get; set; }
    //    public double RODLine { get; set; }
    //    public double UZDLine { get; set; }
    //    public double VisoLine { get; set; }
    //    public Boolean IsChecked { get; set; }
    //    public int index { get; set; }
    //    public List<IndicatorsClass> RodList { get; set; }
    //    public List<TasksClass> UzdList { get; set; }
    //}
}