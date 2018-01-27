namespace GW.Site.Services
{
    public class ExcerptGenerator
    {
        private readonly int _maxLength;

        public ExcerptGenerator(int maxLength)
        {
            _maxLength = maxLength;
        }

        public string CreateExcerpt(string body)
        {
            string excerpt;
            if (body.Length > _maxLength)
            {
                excerpt = body.Substring(0, _maxLength) + "...";
            }
            else
            {
                excerpt = body;
            }
            return excerpt;
        }
    }
}
