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
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }
        private void LoadData()
        {
            int i = 0;
            EmployeeClass EC = new EmployeeClass();
            EC.NameLine = "MARKO"; EC.BALine = 1000.00; EC.RODLine = 0; EC.UZDLine = 0; EC.VisoLine = EC.BALine + EC.RODLine + EC.UZDLine; EC.IsChecked = false;
            EC.index = i++;
            Employees.Items.Add(EC);

            EC = new EmployeeClass();
            EC.NameLine = "Employee 2"; EC.BALine = 2000.00; EC.RODLine = 0; EC.UZDLine = 0; EC.VisoLine = EC.BALine + EC.RODLine + EC.UZDLine; EC.IsChecked = false;
            EC.index = i++;
            Employees.Items.Add(EC);

            EC = new EmployeeClass();
            EC.NameLine = "Employee 3"; EC.BALine = 3000.00; EC.RODLine = 0; EC.UZDLine = 0; EC.VisoLine = EC.BALine + EC.RODLine + EC.UZDLine; EC.IsChecked = false;
            EC.index = i++;
            Employees.Items.Add(EC);

            i = 0;
            IndicatorsClass IC = new IndicatorsClass();
            IC.INDPAVLine = "PROFIT"; IC.BRLine = 100000.00; IC.TRLine = 130000.00; IC.FRLine = 121000.00; IC.MKDLine = 1200.00; IC.IsChecked = false;
            IC.index = i++;
            Indicators.Items.Add(IC);

            IC = new IndicatorsClass();
            IC.INDPAVLine = "COSTS"; IC.BRLine = 500000.00; IC.TRLine = 450000.00; IC.FRLine = 480000.00; IC.MKDLine = 800.00; IC.IsChecked = false;
            IC.index = i++;
            Indicators.Items.Add(IC);

            IC = new IndicatorsClass();
            IC.INDPAVLine = "Indicator 3"; IC.BRLine = 2.0; IC.TRLine = 2.4; IC.FRLine = 4.0; IC.MKDLine = 2.0; IC.IsChecked = false;
            IC.index = i++;
            Indicators.Items.Add(IC);

            IC = new IndicatorsClass();
            IC.INDPAVLine = "Indicator 4"; IC.BRLine = 3.0; IC.TRLine = 3.6; IC.FRLine = 6.0; IC.MKDLine = 3.0; IC.IsChecked = false;
            IC.index = i++;
            Indicators.Items.Add(IC);

            i = 0;
            TasksClass TC = new TasksClass();
            TC.UZDPAVLine = "RELATIONSHIP WITH..."; TC.Ivert = 9.0; TC.MaxIvert = 11.0; TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString(); TC.IsChecked = false;
            TC.index = i++;
            Tasks.Items.Add(TC);

            TC = new TasksClass();
            TC.UZDPAVLine = "CUTTING ON THE RET..."; TC.Ivert = 5.0; TC.MaxIvert = 9.0; TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString(); TC.IsChecked = false;
            TC.index = i++;
            Tasks.Items.Add(TC);

            TC = new TasksClass();
            TC.UZDPAVLine = "IMPROVING PROFESS..."; TC.Ivert = 6.0; TC.MaxIvert = 7.0; TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString(); TC.IsChecked = false;
            TC.index = i++;
            Tasks.Items.Add(TC);

            TC = new TasksClass();
            TC.UZDPAVLine = "Task 4"; TC.Ivert = 7.0; TC.MaxIvert = 8.0; TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString(); TC.IsChecked = false;
            TC.index = i++;
            Tasks.Items.Add(TC);

            TC = new TasksClass();
            TC.UZDPAVLine = "Task 5"; TC.Ivert = 8.0; TC.MaxIvert = 9.0; TC.IVERTLine = (TC.Ivert + " / " + TC.MaxIvert).ToString(); TC.IsChecked = false;
            TC.index = i++;
            Tasks.Items.Add(TC);
        }
        private void KillAll()
        {
            MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            LanguageBar.Visibility = System.Windows.Visibility.Collapsed;
            AddBarEmployee.Visibility = System.Windows.Visibility.Collapsed;
            AddBarIndicator.Visibility = System.Windows.Visibility.Collapsed;
            AddBarTask.Visibility = System.Windows.Visibility.Collapsed;
            Edit.Visibility = System.Windows.Visibility.Collapsed;
            MAxKDP.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void ApplicationBarIconButton_Click(object sender, EventArgs e) // Meniu
        {
            if (MeniuBar.Visibility == System.Windows.Visibility.Collapsed)
            {
                LanguageBar.Visibility = System.Windows.Visibility.Collapsed;
                MeniuBar.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e) // Save
        {

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
            if (MAxKDP.Visibility == System.Windows.Visibility.Visible)
            {
                MAxKDP.Visibility = System.Windows.Visibility.Collapsed;
            }
            MAxKDP.Visibility = System.Windows.Visibility.Visible;
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
                MAXKDPBox.Text = "0.0";
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

        }
        private void About_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Language_Click(object sender, RoutedEventArgs e)
        {
            MeniuBar.Visibility = System.Windows.Visibility.Collapsed;
            LanguageBar.Visibility = System.Windows.Visibility.Visible;
        }
        private void LT_Click(object sender, RoutedEventArgs e)
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
        private void EN_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo cult = new CultureInfo("en");
            //
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
            Edit.Visibility = System.Windows.Visibility.Visible;
        }
        private void Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KillAll();
            Edit.Visibility = System.Windows.Visibility.Visible;
        }
        private void Indicators_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KillAll();
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
            KillAll();
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
                        Employees.Items[i] = emp;
                    }
                }
                for (int i = 0; i < Indicators.Items.Count; i++)
                {
                    IndicatorsClass ind = (IndicatorsClass)Indicators.Items[i];
                    ind.IsChecked = false;
                    Indicators.Items[i] = ind;
                }
                for (int i = 0; i < Tasks.Items.Count; i++)
                {
                    TasksClass tsk = (TasksClass)Tasks.Items[i];
                    tsk.IsChecked = false;
                    Tasks.Items[i] = tsk;
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
    }
}