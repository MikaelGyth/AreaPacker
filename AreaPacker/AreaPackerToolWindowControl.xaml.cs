using System.Collections.Generic;
using AreaPacker.Utils;
using EnvDTE;
using EnvDTE80;

using Microsoft.VisualStudio.Shell;

namespace AreaPacker
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AreaPackerToolWindowControl.
    /// </summary>
    public partial class AreaPackerToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaPackerToolWindowControl"/> class.
        /// </summary>
        public AreaPackerToolWindowControl()
        {
            this.InitializeComponent();
        }

        

        /// <summary>
        /// Generates the package file
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void GeneratePackageButton_OnClick(object sender, RoutedEventArgs e)
        {
            // get project root folder   
            // Check if package exists
            //if it does promt for overwrite/rename
            // create file
            // fille file with data
        }


        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            var treeFactory = new TreeViewFactory();
            treeFactory.PopulateTreeView();
        }
    }
}