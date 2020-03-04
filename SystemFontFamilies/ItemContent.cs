using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace SystemFontFamilies
{
    public static class ItemContentManager
    {
        static Dictionary<Type, ItemContentTable> Dictionary;

        static ItemContentManager()
        {
            Dictionary = new Dictionary<Type, ItemContentTable>()
            {
                {typeof(FontFamily), FontFamilyItemContentTable.Create() },
                {typeof(FamilyTypeface), FamilyTypefaceItemContentTable.Create() },
                {typeof(Typeface), TypefaceItemContentTable.Create() },
                {typeof(GlyphTypeface), GlyphTypefaceItemContentTable.Create() },
            };
        }


        public static ItemContentTable Select(object owner)
        {
            if(owner == null || !Dictionary.TryGetValue(owner.GetType(), out ItemContentTable result))
                result = ItemContentTable.DefaultTable;
            return result;
        }
    }


    public class ItemContentTable
    {
        public static ItemContentTable DefaultTable = new ItemContentTable();


        Dictionary<string, ItemContent> Dictionary = new Dictionary<string, ItemContent>();

        public void Add(string name, ItemContent itemContent)
        {
            Dictionary.Add(name, itemContent);
        }


        public ItemContent Select(string name)
        {
            if (name == null || !Dictionary.TryGetValue(name, out ItemContent result))
                result = ItemContent.DefaultItemContent;
            return result;
        }
    }


    public static class FontFamilyItemContentTable
    {
        static ItemContent ItemContent0 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2}",
                tnv.Type.Name, tnv.Name, tnv.Value != null ? tnv.Value.ToString() : "<null>")
            );

        static ItemContent ItemContent1 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                tnv.Type.Name, tnv.Name, ((LanguageSpecificStringDictionary)tnv.Value).Count)
            );

        static ItemContent ItemContent2 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                tnv.Type.Name, tnv.Name, ((FamilyTypefaceCollection)tnv.Value).Count)
            );

        static ItemContent ItemContent3 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                tnv.Type.Name, tnv.Name, ((FontFamilyMapCollection)tnv.Value).Count)
            );

        static ItemContent ItemContent4 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                "ICollection<Typeface>", tnv.Name, ((ICollection<Typeface>)tnv.Value).Count)
            );


        public static ItemContentTable Create()
        {
            var table = new ItemContentTable();

            table.Add("Baseline", ItemContent0);
            table.Add("BaseUri", ItemContent0);
            table.Add("LineSpacing", ItemContent0);
            table.Add("Source", ItemContent0);

            table.Add("FamilyNames", ItemContent1);

            table.Add("FamilyTypefaces", ItemContent2);

            table.Add("FamilyMaps", ItemContent3);

            table.Add("GetTypefaces()", ItemContent4);

            return table;
        }
    }


    public static class FamilyTypefaceItemContentTable
    {
        static ItemContent ItemContent0 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2}",
                tnv.Type.Name, tnv.Name, tnv.Value != null ? tnv.Value.ToString() : "<null>")
            );

        static ItemContent ItemContent1 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2} (usStretchClass={3})",
                tnv.Type.Name, tnv.Name, tnv.Value, ((FontStretch)tnv.Value).ToOpenTypeStretch())
            );

        static ItemContent ItemContent2 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2} (usWeightClass={3})",
                tnv.Type.Name, tnv.Name, tnv.Value, ((FontWeight)tnv.Value).ToOpenTypeWeight())
            );

        static ItemContent ItemContent3 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                "IDictionary<XmlLanguage, String>", tnv.Name, ((IDictionary<XmlLanguage, String>)tnv.Value).Count)
            );

        static ItemContent ItemContent4 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                tnv.Type.Name, tnv.Name, ((CharacterMetricsDictionary)tnv.Value).Count)
            );


        public static ItemContentTable Create()
        {
            var table = new ItemContentTable();

            table.Add("CapsHeight", ItemContent0);
            table.Add("DeviceFontName", ItemContent0);
            table.Add("StrikethroughPosition", ItemContent0);
            table.Add("StrikethroughThickness", ItemContent0);
            table.Add("Style", ItemContent0);
            table.Add("UnderlinePosition", ItemContent0);
            table.Add("UnderlineThickness", ItemContent0);
            table.Add("XHeight", ItemContent0);

            table.Add("Stretch", ItemContent1);

            table.Add("Weight", ItemContent2);

            table.Add("AdjustedFaceNames", ItemContent3);

            table.Add("DeviceFontCharacterMetrics", ItemContent4);

            return table;
        }
    }


    public static class TypefaceItemContentTable
    {
        static ItemContent ItemContent0 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2}",
                tnv.Type.Name, tnv.Name, tnv.Value != null ? tnv.Value.ToString() : "<null>")
            );

        static ItemContent ItemContent1 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2} (usStretchClass={3})",
                tnv.Type.Name, tnv.Name, tnv.Value, ((FontStretch)tnv.Value).ToOpenTypeStretch())
            );

        static ItemContent ItemContent2 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2} (usWeightClass={3})",
                tnv.Type.Name, tnv.Name, tnv.Value, ((FontWeight)tnv.Value).ToOpenTypeWeight())
            );

        static ItemContent ItemContent3 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                tnv.Type.Name, tnv.Name, ((LanguageSpecificStringDictionary)tnv.Value).Count)
            );

        static ItemContent ItemContent4 = new ItemContent(
            (tnv) => string.Format("{0} {1} {2}",
                tnv.Type.Name, tnv.Name, tnv.Value == null ? "=False" : "")
            );


        public static ItemContentTable Create()
        {
            var table = new ItemContentTable();

            table.Add("CapsHeight", ItemContent0);
            table.Add("FontFamily", ItemContent0);
            table.Add("IsBoldSimulated", ItemContent0);
            table.Add("IsObliqueSimulated", ItemContent0);
            table.Add("StrikethroughPosition", ItemContent0);
            table.Add("StrikethroughThickness", ItemContent0);
            table.Add("Style", ItemContent0);
            table.Add("UnderlinePosition", ItemContent0);
            table.Add("UnderlineThickness", ItemContent0);
            table.Add("XHeight", ItemContent0);

            table.Add("Stretch", ItemContent1);

            table.Add("Weight", ItemContent2);

            table.Add("FaceNames", ItemContent3);

            table.Add("TryGetGlyphTypeface()", ItemContent4);

            return table;
        }
    }


    public static class GlyphTypefaceItemContentTable
    {
        static ItemContent ItemContent0 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2}",
                tnv.Type.Name, tnv.Name, tnv.Value != null ? tnv.Value.ToString() : "<null>")
            );

        static ItemContent ItemContent1 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2} (usStretchClass={3})",
                tnv.Type.Name, tnv.Name, tnv.Value, ((FontStretch)tnv.Value).ToOpenTypeStretch())
            );

        static ItemContent ItemContent2 = new ItemContent(
            (tnv) => string.Format("{0} {1}={2} (usWeightClass={3})",
                tnv.Type.Name, tnv.Name, tnv.Value, ((FontWeight)tnv.Value).ToOpenTypeWeight())
            );

        static ItemContent ItemContent3 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                "IDictionary<CultureInfo, String>", tnv.Name, ((IDictionary<CultureInfo, String>)tnv.Value).Count)
            );

        static ItemContent ItemContent4 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                "IDictionary<UInt16, Double>", tnv.Name, ((IDictionary<UInt16, Double>)tnv.Value).Count)
            );

        static ItemContent ItemContent5 = new ItemContent(
            (tnv) => string.Format("{0} {1} (Count={2})",
                "IDictionary<Int32, UInt16>", tnv.Name, ((IDictionary<Int32, UInt16>)tnv.Value).Count)
            );


        public static ItemContentTable Create()
        {
            var table = new ItemContentTable();

            table.Add("Baseline", ItemContent0);
            table.Add("CapsHeight", ItemContent0);
            table.Add("EmbeddingRights", ItemContent0);
            table.Add("FontUri", ItemContent0);
            table.Add("GlyphCount", ItemContent0);
            table.Add("Height", ItemContent0);
            table.Add("StrikethroughPosition", ItemContent0);
            table.Add("StrikethroughThickness", ItemContent0);
            table.Add("Style", ItemContent0);
            table.Add("StyleSimulations", ItemContent0);
            table.Add("Symbol", ItemContent0);
            table.Add("UnderlinePosition", ItemContent0);
            table.Add("UnderlineThickness", ItemContent0);
            table.Add("Version", ItemContent0);
            table.Add("XHeight", ItemContent0);

            table.Add("Stretch", ItemContent1);

            table.Add("Weight", ItemContent2);

            table.Add("Copyrights", ItemContent3);
            table.Add("Descriptions", ItemContent3);
            table.Add("DesignerNames", ItemContent3);
            table.Add("DesignerUrls", ItemContent3);
            table.Add("FaceNames", ItemContent3);
            table.Add("FamilyNames", ItemContent3);
            table.Add("LicenseDescriptions", ItemContent3);
            table.Add("ManufacturerNames", ItemContent3);
            table.Add("SampleTexts", ItemContent3);
            table.Add("Trademarks", ItemContent3);
            table.Add("VendorUrls", ItemContent3);
            table.Add("VersionStrings", ItemContent3);
            table.Add("Win32FaceNames", ItemContent3);
            table.Add("Win32FamilyNames", ItemContent3);

            table.Add("AdvanceHeights", ItemContent4);
            table.Add("AdvanceWidths", ItemContent4);
            table.Add("BottomSideBearings", ItemContent4);
            table.Add("DistancesFromHorizontalBaselineToBlackBoxBottom", ItemContent4);
            table.Add("LeftSideBearings", ItemContent4);
            table.Add("RightSideBearings", ItemContent4);
            table.Add("TopSideBearings", ItemContent4);

            table.Add("CharacterToGlyphMap", ItemContent5);

            return table;
        }
    }


    public class ItemContent
    {
        public static ItemContent DefaultItemContent = new ItemContent(TextFunc);

        public Func<TypeNameValue, string> Text { get; }
        public Func<TypeNameValue, bool> Error { get; }

        public ItemContent(Func<TypeNameValue, string> text, Func<TypeNameValue, bool> error)
        {
            Text = text;
            Error = error;
        }

        public ItemContent(Func<TypeNameValue, string> text)
        {
            Text = text;
            Error = FalseFunc;
        }


        //static Func<TypeNameValue, bool> FalseFunc = (tnv) => { return false; };
        static bool FalseFunc(TypeNameValue tnv)
        {
            return false;
        }


        static string TextFunc(TypeNameValue tnv)
        {
            if (tnv.Type == null)
            {
                if (tnv.Value is null)
                    return string.Format("<no properties>");

                string typeName = tnv.Value.GetType().Name;

                if (tnv.Value is FontFamily ff)
                    return string.Format("{0} (Source={1})",
                                         typeName, ff.Source);
                else if (tnv.Value is DictionaryEntry de)
                    return string.Format("{0} (Key={1})",
                                         typeName, de.Key);
                else if (tnv.Value is FamilyTypeface ft)
                    return string.Format("{0} (Style={1},Weight={2},Stretch={3})",
                                         typeName, ft.Style, ft.Weight, ft.Stretch);
                else if (tnv.Value is FontFamilyMap ffm)
                    return string.Format("{0} (Unicode={1})",
                                         typeName, ffm.Unicode);
                else if (tnv.Value is Typeface t)
                    return string.Format("{0} (Style={1},Weight={2},Stretch={3})",
                                         typeName, t.Style, t.Weight, t.Stretch);
                else if (tnv.Value is KeyValuePair<XmlLanguage, string> kvp)
                    return string.Format("{0} (Key={1})",
                                         "KeyValuePair<XmlLanguage, string>", kvp.Key);
                else if (tnv.Value is KeyValuePair<CultureInfo, String> kvp1)
                    return string.Format("{0} (Key={1})",
                                         "KeyValuePair<CultureInfo, String>", kvp1.Key);
                else if (tnv.Value is KeyValuePair<UInt16, Double> kvp2)
                    return string.Format("{0} (Key={1})",
                                         "KeyValuePair<UInt16, Double>", kvp2.Key);
                else if (tnv.Value is KeyValuePair<Int32, UInt16> kvp3)
                    return string.Format("{0} (Key={1})",
                                         "KeyValuePair<Int32, UInt16>", kvp3.Key);
                else
                    return string.Format("{0} {1}",
                                         typeName, tnv.Value);
            }
            else
            {
                string typeName = tnv.Type.Name;

                if (tnv.Value == null)
                    return string.Format("{0} {1}=<null>", typeName, tnv.Name);

                else if (tnv.Value is IEnumerable && !(tnv.Value is string))
                {
                    PropertyInfo countProp = tnv.Value.GetType().GetProperty("Count");
                    if (countProp != null)
                    {
                        int count = (int)countProp.GetValue(tnv.Value, null);
                        return string.Format("{0} {1} (Count={2})", typeName, tnv.Name, count);
                    }
                    else
                        return string.Format("{0} {1}", typeName, tnv.Name);
                }
                else
                {
                    if (tnv.Value.GetType() != tnv.Type)
                    {
                        string sourceTypeName = tnv.Value.GetType().Name;
                        return string.Format("{0} {1}=(Source Type={2}) {3}", 
                                             typeName, tnv.Name, sourceTypeName, tnv.Value);
                    }
                    else
                        return string.Format("{0} {1}={2}", typeName, tnv.Name, tnv.Value);
                }
            }
        }
    }


}
