﻿#pragma checksum "..\..\..\NewIdeaPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E3ACD83BE1383CDB6DAD93BBC3B0D8CF5FDDCF92"
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
    /// NewIdeaPage
    /// </summary>
    public partial class NewIdeaPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReturnButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Name;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NameLabel;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Description;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DescriptionLabel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorLabel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\NewIdeaPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
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
            System.Uri resourceLocater = new System.Uri("/IdeeenBox V2;V1.0.0.0;component/newideapage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\NewIdeaPage.xaml"
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
            
            #line 12 "..\..\..\NewIdeaPage.xaml"
            this.ReturnButton.Click += new System.Windows.RoutedEventHandler(this.Return);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Name = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.NameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Description = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.DescriptionLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.DescriptionBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.ErrorLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\NewIdeaPage.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

