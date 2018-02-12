#region usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

#endregion

namespace AreaPacker.Utils
{
    internal class TreeViewFactory
    {
        public void PopulateTreeView(Package package, IServiceProvider serviceProvider)
        {
            var window = package.FindToolWindow(typeof(AreaPackerToolWindow), 0, true);
            if (null == window || null == window.Frame) throw new NotSupportedException("Cannot create window.");

            var windowFrame = (IVsWindowFrame) window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());

            // Get the tree view and populate it if there is a project open.  
            var control = (AreaPackerToolWindowControl) window.Content;
            var treeView = control.ProjectTreeView;

            // Reset the TreeView to 0 items.  
            treeView.Items.Clear();

            var dte = (DTE) serviceProvider.GetService(typeof(DTE));
            var projects = dte.Solution.Projects;
            if (projects.Count == 0) // no project is open  
            {
                var item = new TreeViewItem
                {
                    Name = "Projects",
                    ItemsSource = new[] {"no projects are open."},
                    IsExpanded = true
                };
                treeView.Items.Add(item);
                return;
            }

            var project = projects.Item(1);

            var item1 = new TreeViewItem {Header = project.Name};

            treeView.Items.Add(item1);


            var rootfolder = dte.Solution.FullName.TrimSuffix(".sln") + "\\";
            var di = new DirectoryInfo(rootfolder);
            //var dirInfo = di.RootDirectory;

            var items = GetFiles(di);
            items.AddRange(GetDirectories(di));
            item1.ItemsSource = items;
        }

        private static List<TreeViewItem> GetFiles(DirectoryInfo di)
        {
            var fileNames = di.GetFiles("*.*");
            var files = new List<TreeViewItem>();
            foreach (var fi in fileNames)
            {
                var item = new TreeViewItem
                {
                    Header = fi.Name
                };
                files.Add(item);
            }

            return files;
        }

        private static IEnumerable<TreeViewItem> GetDirectories(DirectoryInfo di)
        {
            var dirInfos = di.GetDirectories("*.*");
            var folders = new List<TreeViewItem>();
            foreach (var d in dirInfos)
            {
                var item = new TreeViewItem
                {
                    ItemsSource = new[] {d.Name},
                    IsExpanded = false,
                    Header = d.Name
                };
                item.ItemsSource = GetFiles(d);
                folders.Add(item);
            }

            return folders;
        }
    }
}