SystemFontFamilies
    Show SystemFontFamilies

Features
* Lazy unloading treeview
    When TreeViewItem is collapsed, invisibled item and corresponding container are removed.
    ViewModel uses class CollapsedTree that holds expended/collapsed state.
    TreeView uses class TreeViewBehavior that re-focuses to the closest visible ancestor.
