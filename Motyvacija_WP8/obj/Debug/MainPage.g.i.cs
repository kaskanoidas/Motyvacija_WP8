﻿#pragma checksum "C:\Users\Rolandas\Desktop\Apps\Motyvacija_WP8\Motyvacija_WP8\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AB768B18E3430D51EBCA533C481681C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Motyvacija_WP8 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot PIVOT;
        
        internal System.Windows.Controls.ListBox Employees;
        
        internal System.Windows.Controls.ListBox Indicators;
        
        internal System.Windows.Controls.ListBox Tasks;
        
        internal System.Windows.Controls.StackPanel MeniuBar;
        
        internal System.Windows.Controls.Button Archive;
        
        internal System.Windows.Controls.Button About;
        
        internal System.Windows.Controls.Button Help;
        
        internal System.Windows.Controls.Button Language;
        
        internal System.Windows.Controls.StackPanel LanguageBar;
        
        internal System.Windows.Controls.Button LT;
        
        internal System.Windows.Controls.Button EN;
        
        internal System.Windows.Controls.Button RU;
        
        internal System.Windows.Controls.Button LanguageBarBack;
        
        internal System.Windows.Controls.Grid HelperGrid;
        
        internal System.Windows.Controls.ScrollViewer AddBarEmployee;
        
        internal System.Windows.Controls.StackPanel AddBarPanell;
        
        internal System.Windows.Controls.TextBox NameBox;
        
        internal System.Windows.Controls.TextBox BABox;
        
        internal System.Windows.Controls.Button CreateNewEmployee;
        
        internal System.Windows.Controls.Button CreateNewEmployeeBack;
        
        internal System.Windows.Controls.Grid SpacingGrid;
        
        internal System.Windows.Controls.ScrollViewer AddBarIndicator;
        
        internal System.Windows.Controls.TextBox PavBoxIND;
        
        internal System.Windows.Controls.TextBox BRBox;
        
        internal System.Windows.Controls.TextBox TRBox;
        
        internal System.Windows.Controls.TextBox FRBox;
        
        internal System.Windows.Controls.TextBox MKDBox;
        
        internal System.Windows.Controls.Button CreateNewIndicator;
        
        internal System.Windows.Controls.Button CreateNewIndicatorBack;
        
        internal System.Windows.Controls.Grid SpacingGridInd;
        
        internal System.Windows.Controls.ScrollViewer AddBarTask;
        
        internal System.Windows.Controls.TextBox PavBoxTSK;
        
        internal System.Windows.Controls.TextBox MAXIVBox;
        
        internal System.Windows.Controls.TextBox IVBox;
        
        internal System.Windows.Controls.Button CreateNewTask;
        
        internal System.Windows.Controls.Button CreateNewTaskBack;
        
        internal System.Windows.Controls.Grid SpacingGridTSK;
        
        internal System.Windows.Controls.Grid EditShowGrid;
        
        internal System.Windows.Controls.Button Edit;
        
        internal System.Windows.Controls.Button Show;
        
        internal System.Windows.Controls.Grid EmployeeDetailPanel;
        
        internal System.Windows.Controls.TextBlock ShowVAR;
        
        internal System.Windows.Controls.TextBlock ShowBA;
        
        internal System.Windows.Controls.TextBlock ShowROD;
        
        internal System.Windows.Controls.TextBlock ShowUZD;
        
        internal System.Windows.Controls.TextBlock ShowViso;
        
        internal System.Windows.Controls.ListBox ShowIndicators;
        
        internal System.Windows.Controls.ListBox ShowTasks;
        
        internal System.Windows.Controls.TextBlock SHowMAxKDP;
        
        internal System.Windows.Controls.Button BackDetaiPanel;
        
        internal System.Windows.Controls.StackPanel MAxKDPST;
        
        internal System.Windows.Controls.TextBox MAXKDPBox;
        
        internal System.Windows.Controls.Button MAXKDPBoxOk;
        
        internal System.Windows.Controls.Button MAXKDPBack;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton Add;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton Meniu;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton Save;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton Calculate;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Motyvacija;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PIVOT = ((Microsoft.Phone.Controls.Pivot)(this.FindName("PIVOT")));
            this.Employees = ((System.Windows.Controls.ListBox)(this.FindName("Employees")));
            this.Indicators = ((System.Windows.Controls.ListBox)(this.FindName("Indicators")));
            this.Tasks = ((System.Windows.Controls.ListBox)(this.FindName("Tasks")));
            this.MeniuBar = ((System.Windows.Controls.StackPanel)(this.FindName("MeniuBar")));
            this.Archive = ((System.Windows.Controls.Button)(this.FindName("Archive")));
            this.About = ((System.Windows.Controls.Button)(this.FindName("About")));
            this.Help = ((System.Windows.Controls.Button)(this.FindName("Help")));
            this.Language = ((System.Windows.Controls.Button)(this.FindName("Language")));
            this.LanguageBar = ((System.Windows.Controls.StackPanel)(this.FindName("LanguageBar")));
            this.LT = ((System.Windows.Controls.Button)(this.FindName("LT")));
            this.EN = ((System.Windows.Controls.Button)(this.FindName("EN")));
            this.RU = ((System.Windows.Controls.Button)(this.FindName("RU")));
            this.LanguageBarBack = ((System.Windows.Controls.Button)(this.FindName("LanguageBarBack")));
            this.HelperGrid = ((System.Windows.Controls.Grid)(this.FindName("HelperGrid")));
            this.AddBarEmployee = ((System.Windows.Controls.ScrollViewer)(this.FindName("AddBarEmployee")));
            this.AddBarPanell = ((System.Windows.Controls.StackPanel)(this.FindName("AddBarPanell")));
            this.NameBox = ((System.Windows.Controls.TextBox)(this.FindName("NameBox")));
            this.BABox = ((System.Windows.Controls.TextBox)(this.FindName("BABox")));
            this.CreateNewEmployee = ((System.Windows.Controls.Button)(this.FindName("CreateNewEmployee")));
            this.CreateNewEmployeeBack = ((System.Windows.Controls.Button)(this.FindName("CreateNewEmployeeBack")));
            this.SpacingGrid = ((System.Windows.Controls.Grid)(this.FindName("SpacingGrid")));
            this.AddBarIndicator = ((System.Windows.Controls.ScrollViewer)(this.FindName("AddBarIndicator")));
            this.PavBoxIND = ((System.Windows.Controls.TextBox)(this.FindName("PavBoxIND")));
            this.BRBox = ((System.Windows.Controls.TextBox)(this.FindName("BRBox")));
            this.TRBox = ((System.Windows.Controls.TextBox)(this.FindName("TRBox")));
            this.FRBox = ((System.Windows.Controls.TextBox)(this.FindName("FRBox")));
            this.MKDBox = ((System.Windows.Controls.TextBox)(this.FindName("MKDBox")));
            this.CreateNewIndicator = ((System.Windows.Controls.Button)(this.FindName("CreateNewIndicator")));
            this.CreateNewIndicatorBack = ((System.Windows.Controls.Button)(this.FindName("CreateNewIndicatorBack")));
            this.SpacingGridInd = ((System.Windows.Controls.Grid)(this.FindName("SpacingGridInd")));
            this.AddBarTask = ((System.Windows.Controls.ScrollViewer)(this.FindName("AddBarTask")));
            this.PavBoxTSK = ((System.Windows.Controls.TextBox)(this.FindName("PavBoxTSK")));
            this.MAXIVBox = ((System.Windows.Controls.TextBox)(this.FindName("MAXIVBox")));
            this.IVBox = ((System.Windows.Controls.TextBox)(this.FindName("IVBox")));
            this.CreateNewTask = ((System.Windows.Controls.Button)(this.FindName("CreateNewTask")));
            this.CreateNewTaskBack = ((System.Windows.Controls.Button)(this.FindName("CreateNewTaskBack")));
            this.SpacingGridTSK = ((System.Windows.Controls.Grid)(this.FindName("SpacingGridTSK")));
            this.EditShowGrid = ((System.Windows.Controls.Grid)(this.FindName("EditShowGrid")));
            this.Edit = ((System.Windows.Controls.Button)(this.FindName("Edit")));
            this.Show = ((System.Windows.Controls.Button)(this.FindName("Show")));
            this.EmployeeDetailPanel = ((System.Windows.Controls.Grid)(this.FindName("EmployeeDetailPanel")));
            this.ShowVAR = ((System.Windows.Controls.TextBlock)(this.FindName("ShowVAR")));
            this.ShowBA = ((System.Windows.Controls.TextBlock)(this.FindName("ShowBA")));
            this.ShowROD = ((System.Windows.Controls.TextBlock)(this.FindName("ShowROD")));
            this.ShowUZD = ((System.Windows.Controls.TextBlock)(this.FindName("ShowUZD")));
            this.ShowViso = ((System.Windows.Controls.TextBlock)(this.FindName("ShowViso")));
            this.ShowIndicators = ((System.Windows.Controls.ListBox)(this.FindName("ShowIndicators")));
            this.ShowTasks = ((System.Windows.Controls.ListBox)(this.FindName("ShowTasks")));
            this.SHowMAxKDP = ((System.Windows.Controls.TextBlock)(this.FindName("SHowMAxKDP")));
            this.BackDetaiPanel = ((System.Windows.Controls.Button)(this.FindName("BackDetaiPanel")));
            this.MAxKDPST = ((System.Windows.Controls.StackPanel)(this.FindName("MAxKDPST")));
            this.MAXKDPBox = ((System.Windows.Controls.TextBox)(this.FindName("MAXKDPBox")));
            this.MAXKDPBoxOk = ((System.Windows.Controls.Button)(this.FindName("MAXKDPBoxOk")));
            this.MAXKDPBack = ((System.Windows.Controls.Button)(this.FindName("MAXKDPBack")));
            this.Add = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("Add")));
            this.Meniu = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("Meniu")));
            this.Save = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("Save")));
            this.Calculate = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("Calculate")));
        }
    }
}

