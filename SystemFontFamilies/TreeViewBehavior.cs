using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace SystemFontFamilies
{
    class TreeViewBehavior : Behavior<TreeView>
    {
        RoutedEventHandler TreeViewItem_SelectedHandler;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (TreeViewItem_SelectedHandler == null)
                TreeViewItem_SelectedHandler = new RoutedEventHandler(TreeViewItem_Selected);
            AssociatedObject.AddHandler(TreeViewItem.SelectedEvent, TreeViewItem_SelectedHandler);
        }


        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(TreeViewItem.SelectedEvent, TreeViewItem_SelectedHandler);

            base.OnDetaching();
        }


        static TreeView GetTreeView(TreeViewItem tvi)
        {
            TreeView result = null;
            for (DependencyObject dep = tvi; result == null && dep != null; dep = VisualTreeHelper.GetParent(dep))
            {
                if (dep is TreeView tv)
                    result = tv;
            }
            return result;
        }


        void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem tvi)
            {
                for (var parent = VisualTreeHelper.GetParent(tvi); parent != null; parent = VisualTreeHelper.GetParent(parent))
                {
                    if (parent is TreeViewItem parentTVI)
                        parentTVI.IsExpanded = true;
                }

                DependencyObject scope = FocusManager.GetFocusScope(tvi);
                if (FocusManager.GetFocusedElement(scope) is TreeViewItem focused)
                {
                    TreeView curr = GetTreeView(focused);
                    if (curr == null || curr == GetTreeView(tvi))
                    {
                        FocusManager.SetFocusedElement(scope, tvi);
                    }
                }
            }
        }
    }
}
