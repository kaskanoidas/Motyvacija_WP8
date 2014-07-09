using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Motyvacija_WP8
{
    public partial class Help : PhoneApplicationPage
    {
        public Help()
        {
            InitializeComponent();
            string kalba = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            ChangePivotItemText();
            KillAllImages();
            if (kalba == "en-US" || kalba == "en")
            {
                EN1.Visibility = System.Windows.Visibility.Visible;
                EN2.Visibility = System.Windows.Visibility.Visible;
                EN3.Visibility = System.Windows.Visibility.Visible;
                EN4.Visibility = System.Windows.Visibility.Visible;
                EN5.Visibility = System.Windows.Visibility.Visible;
                EN6.Visibility = System.Windows.Visibility.Visible;
            }
            else if (kalba == "lt-LT")
            {
                LT1.Visibility = System.Windows.Visibility.Visible;
                LT2.Visibility = System.Windows.Visibility.Visible;
                LT3.Visibility = System.Windows.Visibility.Visible;
                LT4.Visibility = System.Windows.Visibility.Visible;
                LT5.Visibility = System.Windows.Visibility.Visible;
                LT6.Visibility = System.Windows.Visibility.Visible;
            }
            else if (kalba == "ru-RU")
            {
                RU1.Visibility = System.Windows.Visibility.Visible;
                RU2.Visibility = System.Windows.Visibility.Visible;
                RU3.Visibility = System.Windows.Visibility.Visible;
                RU4.Visibility = System.Windows.Visibility.Visible;
                RU5.Visibility = System.Windows.Visibility.Visible;
                RU6.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void KillAllImages()
        {
            EN1.Visibility = System.Windows.Visibility.Collapsed;
            EN2.Visibility = System.Windows.Visibility.Collapsed;
            EN3.Visibility = System.Windows.Visibility.Collapsed;
            EN4.Visibility = System.Windows.Visibility.Collapsed;
            EN5.Visibility = System.Windows.Visibility.Collapsed;
            EN6.Visibility = System.Windows.Visibility.Collapsed;
            LT1.Visibility = System.Windows.Visibility.Collapsed;
            LT2.Visibility = System.Windows.Visibility.Collapsed;
            LT3.Visibility = System.Windows.Visibility.Collapsed;
            LT4.Visibility = System.Windows.Visibility.Collapsed;
            LT5.Visibility = System.Windows.Visibility.Collapsed;
            LT6.Visibility = System.Windows.Visibility.Collapsed;
            RU1.Visibility = System.Windows.Visibility.Collapsed;
            RU2.Visibility = System.Windows.Visibility.Collapsed;
            RU3.Visibility = System.Windows.Visibility.Collapsed;
            RU4.Visibility = System.Windows.Visibility.Collapsed;
            RU5.Visibility = System.Windows.Visibility.Collapsed;
            RU6.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void ChangePivotItemText()
        {
            for (int i = 0; i < PIVOT.Items.Count; i++)
            {
                PivotItem pvt = (PivotItem)PIVOT.Items[i];
                pvt.Header = Motyvacija_WP8.Resources.AppResources.Page + (i+1).ToString();
            }
        }
    }
}