﻿#pragma checksum "..\..\..\Pages\PageAccessCode.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B271042A6F7D758E2EB0CF763F8CE5BDFF810BB60A0240E7BC0877419711B586"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using Views.Pages;
using Views.Properties;


namespace Views.Pages {
    
    
    /// <summary>
    /// PageAccessCode
    /// </summary>
    public partial class PageAccessCode : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame frAccessCode;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbAccessCode;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnJoinGame;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCodeOneNumber;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCodeSecondNumber;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCodeThirdNumber;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCodeFourthNumber;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\PageAccessCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame frMessage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Views;component/pages/pageaccesscode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PageAccessCode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.frAccessCode = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            
            #line 18 "..\..\..\Pages\PageAccessCode.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnClickReturn);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbAccessCode = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.btnJoinGame = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Pages\PageAccessCode.xaml"
            this.btnJoinGame.Click += new System.Windows.RoutedEventHandler(this.BtnClickJoinGame);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtCodeOneNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtCodeSecondNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtCodeThirdNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtCodeFourthNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.frMessage = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

