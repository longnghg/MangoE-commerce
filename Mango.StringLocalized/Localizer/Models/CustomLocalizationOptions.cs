using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.StringLocalized.Localizer
{
    public class CustomLocalizationOptions : LocalizationOptions
    {
        /// <summary>
        /// DEFAULT_RESOURCES
        /// </summary>
        private const string DEFAULT_RESOURCES = "JsonResources";

        /// <summary>
        /// DEFAULT_CULTURE
        /// </summary>
        private const string DEFAULT_CULTURE = "en-CA";


        /// <summary>
        /// ResourcesPath
        /// </summary>
        //public new string ResourcesPath { get; set; } = DEFAULT_RESOURCES;

  
        public string FunctionAppDirectory { get; set; }    
        public CultureInfo DefaultCulture
        {
            get { return new CultureInfo(DEFAULT_CULTURE); } // =>  new CultureInfo(DEFAULT_CULTURE);
            set
            {
                if (value != this.DefaultCulture)
                {
                    this.DefaultCulture = value ?? CultureInfo.InvariantCulture;
                }
            }
        }

        public CustomLocalizationOptions()
        {
        }
        public CustomLocalizationOptions(string functionAppDirectory)
        {
            FunctionAppDirectory = functionAppDirectory;
            ResourcesPath = DEFAULT_RESOURCES;
        }

    }
}
