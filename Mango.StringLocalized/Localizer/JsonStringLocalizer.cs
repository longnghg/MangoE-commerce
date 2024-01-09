using Mango.Constant;
using Mango.StringLocalized.Localizer;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StringLocalize.Localizer
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private string _searchedLocation;
        protected readonly IOptions<CustomLocalizationOptions> _localizationOptions;
        private IEnumerable<KeyValuePair<string, string>> _localizedData;


        public JsonStringLocalizer(IOptions<CustomLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }
        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (_localizedData == null)
                {
                    _localizedData = ReadJsonResourceFile(_localizationOptions.Value.DefaultCulture);
                }

                var valueResult = _localizedData.FirstOrDefault(s => s.Key == name).Value;

                var localizedString = new LocalizedString(name, valueResult ?? name, resourceNotFound: valueResult == null, searchedLocation: _searchedLocation);
                return localizedString;
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }


        private IEnumerable<KeyValuePair<string, string>> ReadJsonResourceFile(CultureInfo currentCulture)
        {
            string jsonResourcePath = GetJsonResourcesPath(currentCulture);
            IEnumerable<KeyValuePair<string, string>> value = null;
            if (File.Exists(jsonResourcePath))
            {
                var jsonData = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(jsonResourcePath))!;
                value = jsonData[LocalizedConstant.ContentSection]!.ToObject<Dictionary<string, string>>()!;
            }
            return value!;
        }

        // check performance with above function 
        //private IEnumerable<KeyValuePair<string, string>> ReadJsonResourceFile(CultureInfo currentCulture)
        //{
        //    string jsonResourcePath = GetJsonResourcesPath(currentCulture);

        //    if (File.Exists(jsonResourcePath) && new FileInfo(jsonResourcePath).Length > 0)
        //    {
        //        using (FileStream fs = new FileStream(jsonResourcePath, FileMode.Open, FileAccess.Read))
        //        using (JsonDocument doc = JsonDocument.Parse(fs))
        //        {
        //            var root = doc.RootElement;

        //            if (root.TryGetProperty(LocalizedConstant.ContentSection, out var contentSection))
        //            {
        //                return contentSection.EnumerateObject()
        //                    .ToDictionary(property => property.Name, property => property.Value.GetString())
        //                    .ToList();
        //            }
        //        }
        //    }

        //    return Enumerable.Empty<KeyValuePair<string, string>>();
        //}


        /// <summary>
        /// GetJsonResourcesPath
        /// </summary>
        /// <param name="currentCulture"></param>
        /// <returns></returns>
        private string GetJsonResourcesPath(CultureInfo currentCulture)
        {
            // TODO: will remove after supported langauge list is implemented.
            var cultureName = currentCulture?.Name?.Split('-')[0];

            _searchedLocation = Path.Join(_localizationOptions.Value.FunctionAppDirectory ?? AppContext.BaseDirectory, _localizationOptions.Value.ResourcesPath, $"{cultureName}.json");

            return _searchedLocation;
        }
    }
}
