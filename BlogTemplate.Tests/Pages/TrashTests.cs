using GW.Site.Models;
using GW.Site.Pages;
using GW.Site.Services;
using GW.Site.Tests.Fakes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace GW.Site.Tests.Pages
{
    public class TrashTests
    {
        [Fact]
        public void DeletePost_MoveToTrash()
        {
            IFileSystem fakeFileSystem = new FakeFileSystem();
            var testDataStore = new BlogDataStore(fakeFileSystem);
            var markdownRenderer = new MarkdownRenderer();
            var testPostModel = new PostModel(testDataStore, markdownRenderer);
            testPostModel.PageContext = new PageContext();

            var post = new Post
            {
                Title = "Title",
                Body = "This is the body of my post",
                IsDeleted = false,
            };
            testDataStore.SavePost(post);

            testPostModel.OnPostDeletePost(post.Id.ToString("N"));
            var result = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.True(result.IsDeleted);
        }

        [Fact]
        public void UnDeletePost_MoveToIndex()
        {
            IFileSystem fakeFileSystem = new FakeFileSystem();
            var testDataStore = new BlogDataStore(fakeFileSystem);
            var markdownRenderer = new MarkdownRenderer();
            var testPostModel = new PostModel(testDataStore, markdownRenderer);
            testPostModel.PageContext = new PageContext();

            var post = new Post
            {
                Title = "Title",
                Body = "This is the body of my post",
                IsDeleted = true,
            };
            testDataStore.SavePost(post);

            testPostModel.OnPostUnDeletePost(post.Id.ToString("N"));
            var result = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.False(result.IsDeleted);
        }
    }
}
