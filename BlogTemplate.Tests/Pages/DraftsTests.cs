using System;
using System.Linq;
using GW.Site.Models;
using GW.Site.Pages;
using GW.Site.Tests.Fakes;
using Xunit;

namespace GW.Site.Tests.Pages
{
    public class DraftsTests
    {
        [Fact]
        public void GetDrafts_ShowDraftSummaries()
        {
            IFileSystem fakeFileSystem = new FakeFileSystem();
            var testDataStore = new BlogDataStore(fakeFileSystem);

            var draftPost = new Post
            {
                Title = "Draft Post",
                IsPublic = false,
                PubDate = DateTime.UtcNow,
            };
            var publishedPost = new Post
            {
                Title = "Published Post",
                IsPublic = true,
                PubDate = DateTime.UtcNow,
            };

            testDataStore.SavePost(draftPost);
            testDataStore.SavePost(publishedPost);

            var testDraftsModel = new DraftsModel(testDataStore);
            testDraftsModel.OnGet();
            var testDraftSummaryModel = testDraftsModel.DraftSummaries.First();

            Assert.Equal(1, testDraftsModel.DraftSummaries.Count());
            Assert.Equal(draftPost.Id, testDraftSummaryModel.Id);
            Assert.Equal("Draft Post", testDraftSummaryModel.Title);
            Assert.Equal(draftPost.PubDate, testDraftSummaryModel.PublishTime);
            Assert.Equal(0, testDraftSummaryModel.CommentCount);
        }
    }
}
