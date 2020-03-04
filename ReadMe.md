## SystemFontFamilies
    Show SystemFontFamilies

    ![SystemFontFamilies](https://github.com/snao2020/SystemFontFamilies/blob/master/SystemFontFamilies.jpg)

## Features
* ## Lazy unloading treeview
    When TreeViewItem is collapsed, invisibled item and corresponding container are removed.
    ViewModel uses class CollapsedTree that holds expended/collapsed state.
    TreeView uses class TreeViewBehavior that re-focuses to the closest visible ancestor.
