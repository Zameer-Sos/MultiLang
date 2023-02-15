using Microsoft.Extensions.Localization;
using System.Reflection;

namespace MultiLang.Models
{
    public class LangService
    {
      //  private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer _localization;
        public LangService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
        //    var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
          //  _localizer = factory.Create("SharedResource", assemblyName.Name);
            _localization = factory.Create(type);
        }

        public LocalizedString GetVal(string key)
        {
            //var check = _localization[key];
            //var check2 = _localizer[key];
            return _localization[key];
        }
    }
}
