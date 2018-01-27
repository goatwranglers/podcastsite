using System.Text.RegularExpressions;
using GW.Site.Models;

namespace GW.Site.Services
{
    public class SlugGenerator
    {
        private BlogDataStore _dataStore;

        public SlugGenerator(BlogDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public string CreateSlug(string title)
        {
            var tempTitle = title;
            tempTitle = tempTitle.Replace(" ", "-");
            var allowList = new Regex("([^A-Za-z0-9-])");
            var slug = allowList.Replace(tempTitle, "");
            return slug;
        }
    }
}

