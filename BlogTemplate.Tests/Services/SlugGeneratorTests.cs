using GW.Site.Models;
using GW.Site.Services;
using GW.Site.Tests.Fakes;
using Xunit;

namespace GW.Site.Tests.Services
{
    public class SlugGeneratorTests
    {
        [Fact]
        public void CreateSlug_ReplacesSpaces()
        {
            IFileSystem testFileSystem = new FakeFileSystem();
            var testDataStore = new BlogDataStore(testFileSystem);
            var testSlugGenerator = new SlugGenerator(testDataStore);
            var test = new Post
            {
                Title = "Test title"
            };
            test.Slug = testSlugGenerator.CreateSlug(test.Title);

            Assert.Equal(test.Slug, "Test-title");
        }

        [Theory]
        [InlineData("test?", "test")]
        [InlineData("test<", "test")]
        [InlineData("test>", "test")]
        [InlineData("test/", "test")]
        [InlineData("test&", "test")]
        [InlineData("test!", "test")]
        [InlineData("test#", "test")]
        [InlineData("test''", "test")]
        [InlineData("test|", "test")]
        [InlineData("test©", "test")]
        [InlineData("test%", "test")]
        public void CreateSlug_TitleContainsInvalidChars_RemoveInvalidCharsInSlug(string input, string expected)
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var testSlugGenerator = new SlugGenerator(testDataStore);

            Assert.Equal(expected, testSlugGenerator.CreateSlug(input));
        }
    }
}
