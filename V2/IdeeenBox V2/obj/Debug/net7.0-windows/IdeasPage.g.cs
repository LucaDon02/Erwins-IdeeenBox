﻿#pragma checksum "..\..\..\IdeasPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7AF6A288BBA6D487FA39834577A49225640B345F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IdeeenBox_V2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace IdeeenBox_V2 {
    
    
    /// <summary>
    /// IdeasPage
    /// </summary>
    public partial class IdeasPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\IdeasPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReturnButton;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\IdeasPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox OwnIdeasList;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\IdeasPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorLabel;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\IdeasPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox SharedIdeasList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/IdeeenBox V2;component/ideaspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\IdeasPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ReturnButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\IdeasPage.xaml"
            this.ReturnButton.Click += new System.Windows.RoutedEventHandler(this.Return);
            
            #line default
            #line hidden
            return;
            case 2:
            this.OwnIdeasList = ((System.Windows.Controls.ListBox)(target));
            
            #line 13 "..\..\..\IdeasPage.xaml"
            this.OwnIdeasList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OwnIdeasList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ErrorLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.SharedIdeasList = ((System.Windows.Controls.ListBox)(target));
            
            #line 15 "..\..\..\IdeasPage.xaml"
            this.SharedIdeasList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SharedIdeasList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

