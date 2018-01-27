using GW.Site.Models;
using GW.Site.Services;
using GW.Site.Tests.Fakes;
using Xunit;

namespace GW.Site.Tests.Services
{
    public class ExcerptGeneratorTests
    {
        [Fact]
        public void CreateExcerpt_BodyLengthExceedsMaxLength_ExcerptIsTruncated()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var testExcerptGenerator = new ExcerptGenerator(5);
            var testExcerpt = testExcerptGenerator.CreateExcerpt("This is the body");
            Assert.Equal("This ...", testExcerpt);
        }
        [Fact]
        public void CreateExcerpt_BodyLengthDoesNotExceedMaxLength_ExcerptEqualsBody()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var testExcerptGenerator = new ExcerptGenerator(50);
            var testExcerpt = testExcerptGenerator.CreateExcerpt("This is the body");
            Assert.Equal("This is the body", testExcerpt);
        }
    }
}
