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
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
            int i = 1;
            int stulpelis = 0; int taskas = 0; int pradinisTaskas = 0;
            Boolean breakCikla = false;
            while (breakCikla == false)
            {
                pradinisTaskas = stulpelis;
                PivotItem pi = new PivotItem();
                pi.Header = Motyvacija_WP8.Resources.AppResources.Page + i.ToString();
                Grid gr = new Grid();
                ScrollViewer sc = new ScrollViewer();
                TextBlock tb = new TextBlock();
                string st = Motyvacija_WP8.Resources.AppResources.AboutString;

                for (int j = 0; j < 15; j++)
                {
                    stulpelis = st.IndexOf('\n', taskas);
                    taskas = stulpelis + 2;
                    if (stulpelis < 0)
                    {
                        j = 15;
                    }
                }
                int ilgis = st.Length;
                if (stulpelis < 0)
                {
                    stulpelis = st.Length;
                    breakCikla = true;
                }
                tb.Text = st.Substring(pradinisTaskas, stulpelis - pradinisTaskas);
                taskas = stulpelis;
                tb.TextWrapping = TextWrapping.Wrap;
                sc.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                sc.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                sc.Content = tb;
                gr.Children.Add(sc);
                pi.Content = gr;
                PIVOT.Items.Add(pi);
                i++;
            } 
        }
    }
}