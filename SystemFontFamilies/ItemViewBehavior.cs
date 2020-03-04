using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace SystemFontFamilies
{
    class ItemViewBehavior : Behavior<UserControl>
    {
        public static DependencyProperty ItemContentTableProperty =
            DependencyProperty.RegisterAttached(
                "ItemContentTable", typeof(ItemContentTable), typeof(ItemViewBehavior));

        public static ItemContentTable GetItemContentTable(UIElement ue)
        {
            return (ItemContentTable)ue.GetValue(ItemContentTableProperty);
        }

        public static void SetItemContentTable(UIElement ue, ItemContentTable value)
        {
            ue.SetValue(ItemContentTableProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += Loaded;            
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= Loaded;
            base.OnDetaching();
        }


        private void Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = GetAncestorTreeViewItem(AssociatedObject);
            if (tvi != null && tvi.DataContext is TypeNameValue tnv)
            {
                ItemContentTable ict = ItemContentManager.Select(tnv.Value);
                SetItemContentTable(tvi, ict);

                ItemContentTable parentIct = null;
                TreeViewItem parentTvi = GetAncestorTreeViewItem(tvi);
                if (parentTvi != null)
                    parentIct = GetItemContentTable(parentTvi);
                if (parentIct == null)
                    parentIct = ItemContentManager.Select(null);

                TextBlock tb = AssociatedObject.FindName("TextBlock") as TextBlock;
                ItemContent ic = parentIct.Select(tnv.Name);
                tb.Text = ic.Text(tnv);
                tb.Foreground = ic.Error(tnv) ? Brushes.Red : Brushes.Black;
            }
        }


        static TreeViewItem GetAncestorTreeViewItem(DependencyObject dep)
        {
            TreeViewItem result = null;
            for(dep = VisualTreeHelper.GetParent(dep); 
                dep != null && !(dep is TreeView) && result == null; 
                dep = VisualTreeHelper.GetParent(dep))
            {
                result = dep as TreeViewItem;
            }
            return result;
        }

    }
}
