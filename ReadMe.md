## SystemFontFamilies
    Show SystemFontFamilies
    
    ![FontComboBox](https://user-images.githubusercontent.com/58820845/75925401-fd481680-5eab-11ea-808d-fcd3394986d5.png)

## Features
* ## Lazy unloading treeview
    When TreeViewItem is collapsed, invisibled item and corresponding container are removed.
    ViewModel uses class CollapsedTree that holds expended/collapsed state.
    TreeView uses class TreeViewBehavior that re-focuses to the closest visible ancestor.
