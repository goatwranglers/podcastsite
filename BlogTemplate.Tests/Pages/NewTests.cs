using GW.Site.Models;
using GW.Site.Pages;
using GW.Site.Services;
using GW.Site.Tests.Fakes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace GW.Site.Tests.Pages
{
    public class NewTests
    {
        [Fact]
        public void OnPostPublish_NoExcerptIsEntered_AutoGenerateExcerpt()
        {
            IFileSystem fakeFileSystem = new FakeFileSystem();
            var testDataStore = new BlogDataStore(fakeFileSystem);
            var testExcerptGenerator = new ExcerptGenerator(5);
            var testSlugGenerator = new SlugGenerator(testDataStore);

            var model = new NewModel(testDataStore, testSlugGenerator, testExcerptGenerator)
            {
                PageContext = new PageContext(),
                NewPost = new NewModel.NewPostViewModel
                {
                    Title = "Title",
                    Body = "This is the body",
                }
            };

            model.OnPostPublish();

            Assert.Equal("This is the body", model.NewPost.Body);
            Assert.Equal("This ...", model.NewPost.Excerpt);
        }

        [Fact]
        public void OnPostSaveDraft_NoExcerptIsEntered_AutoGenerateExcerpt()
        {
            IFileSystem fakeFileSystem = new FakeFileSystem();
            var testDataStore = new BlogDataStore(fakeFileSystem);
            var testExcerptGenerator = new ExcerptGenerator(5);
            var testSlugGenerator = new SlugGenerator(testDataStore);

            var model = new NewModel(testDataStore, testSlugGenerator, testExcerptGenerator)
            {
                PageContext = new PageContext(),
                NewPost = new NewModel.NewPostViewModel
                {
                    Title = "Title",
                    Body = "This is the body",
                }
            };

            model.OnPostSaveDraft();

            Assert.Equal("This is the body", model.NewPost.Body);
            Assert.Equal("This ...", model.NewPost.Excerpt);
        }
    }
}
