using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Shared
{
    public enum Lang
    {
        en_US,
        cs_CZ
    }

    internal class LangHelper
    {
        private static readonly string currAssembly;
        public static string LangPath => $"avares://{currAssembly}/Assets/Lang/";

        private static readonly Dictionary<Lang, string> langDictionaries = new()
        {
            { Lang.cs_CZ, "cs-CZ" },
            { Lang.en_US, "en-US" },
        };

        static LangHelper()
        {
            currAssembly = Assembly.GetExecutingAssembly().GetName().Name!;
        }

        public static void Translate(Lang targetLang)
        {
            Application? currentApp = Application.Current;

            if (currentApp == null)
                return;

            var mergedDictionaries = currentApp.Resources.MergedDictionaries;

            var translations = mergedDictionaries
                .OfType<ResourceInclude>()
                .FirstOrDefault(x => x.Source?.OriginalString?.Contains("/Lang/") ?? false);

            if (translations != null)
                mergedDictionaries.Remove(translations);

            var loadedRes = (ResourceDictionary)AvaloniaXamlLoader.Load(new Uri($"{LangPath}{langDictionaries[targetLang]}.axaml"));

            mergedDictionaries.Add(loadedRes);

            //CultureInfo.CurrentCulture = new CultureInfo(langDictionaries[targetLang]);
        }
    }
}
