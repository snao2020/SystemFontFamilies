using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Media;

namespace SystemFontFamilies
{
    public class CollapsedTree
    {
        public bool Collapsed;
        public List<CollapsedTree> Children;
    }


    public class TypeNameValue : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        static readonly List<TypeNameValue> DummyChildren;

        static TypeNameValue()
        {
            DummyChildren = new List<TypeNameValue>()
            {
                new TypeNameValue("Dummy")
            };
        }


        List<TypeNameValue> _Children = null;
        public List<TypeNameValue> Children
        {
            get
            {
                if (_Children == null)
                {
                    if (IsLeaf())
                        return null;
                    if (!IsExpanded)
                        return DummyChildren;

                    object value = (Value is TypeNameValue tnv) ? tnv.Value : Value;

                    var list = new List<TypeNameValue>();

                    if (value is IEnumerable e)
                    {
                        foreach (object o in e)
                            list.Add(new TypeNameValue(o));
                    }
                    else
                    {
                        PropertyInfo[] properties = value.GetType().GetProperties(
                                    BindingFlags.Public | BindingFlags.Instance);

                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            Type type = propertyInfo.PropertyType;
                            string childName = propertyInfo.Name;
                            object childValue = propertyInfo.GetValue(value);
                            list.Add(new TypeNameValue(type, childName, childValue));
                        }
                        if (list.Count == 0)
                            list.Add(new TypeNameValue(null));

                        if (value is FontFamily ff)
                        {
                            string childName = "GetTypefaces()";
                            list.Add(new TypeNameValue(typeof(ICollection<Typeface>), childName, ff.GetTypefaces()));
                        }
                        else if (value is Typeface t)
                        {
                            string childName = "TryGetGlyphTypeface()";
                            if (t.TryGetGlyphTypeface(out GlyphTypeface gt))
                                list.Add(new TypeNameValue(typeof(GlyphTypeface), childName, gt));
                            else
                                list.Add(new TypeNameValue(typeof(GlyphTypeface), childName, null));
                        }
                    }
                    _Children = list;

                }
                return _Children;
            }

            set
            {
                _Children = value;
                RaisePropertyChanged("Children");
            }
        }


        List<CollapsedTree> CollapsedTree;
        public bool IsExpanded
        {
            get { return CollapsedTree == null; }
            set
            {
                if (IsLeaf())
                    return;
                if (value != (CollapsedTree == null))
                {
                    if (value)
                    {
                        LoadFromCollapsedTree();
                    }
                    else
                    {
                        SaveToCollapsedTree();
                    }
                    RaisePropertyChanged("IsExpanded");
                }
            }
        }

        public Type Type;
        public string Name;
        public object Value;

        public TypeNameValue(Type type, string name, object value)
        {
            Type = type;
            Name = name;
            Value = value;

            if (!IsLeaf())
                CollapsedTree = new List<CollapsedTree>();
        }


        public TypeNameValue(object value)
        {
            Type = null;
            Name = null;
            Value = value;
        
            if (!IsLeaf())
                CollapsedTree = new List<CollapsedTree>();
        }

        bool IsLeaf()
        {
            bool result = false;
            if (Value == null || Value is string || Value.GetType().IsPrimitive)
                result = true;
            else if (Value is IEnumerable e)
            {
                IEnumerator ie = e.GetEnumerator();
                result = ie.MoveNext() == false;
            }
            return result;
        }


        void SaveToCollapsedTree()
        {
            if (Children != null)
            {
                var collapsedTree = new List<CollapsedTree>();

                foreach (TypeNameValue tnv in Children)
                {
                    bool collapsed = !tnv.IsExpanded;
                    if (!collapsed)
                        tnv.SaveToCollapsedTree();

                    var item = new CollapsedTree()
                    {
                        Collapsed = collapsed,
                        Children = tnv.CollapsedTree
                    };
                    collapsedTree.Add(item);
                }

                CollapsedTree = collapsedTree;
                Children = null; // reload Children
            }
        }


        void LoadFromCollapsedTree()
        {
            var collapsedTree = CollapsedTree;
            CollapsedTree = null;
            Children = null; // reload Children

            if(collapsedTree != null && collapsedTree.Count > 0)
            {
                var children = Children;
                for(int i = 0; i < children.Count; i++)
                {
                    var ct = collapsedTree[i];
                    children[i].CollapsedTree = ct.Children;
                    if (!ct.Collapsed)
                        children[i].IsExpanded = true;
                }
            }
        }
    }


    public class ViewModel
    {
        TypeNameValue _Root;
        public TypeNameValue Root
        {
            get
            {
                if (_Root == null)
                {
                    _Root = new TypeNameValue(Fonts.SystemFontFamilies)
                    {
                        IsExpanded = true
                    };
                }
                return _Root;
            }
        }
    }
}
