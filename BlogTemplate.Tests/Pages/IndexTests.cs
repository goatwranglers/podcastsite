using System;
using System.Collections.Generic;
using System.Linq;
using GW.Site.Models;
using GW.Site.Pages;
using GW.Site.Tests.Fakes;
using Xunit;

namespace GW.Site.Tests.Pages
{
    public class IndexTests
    {
        [Fact]
        public void Index_PostSummary_DoesNotIncludeDeletedComments()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            testDataStore.SavePost(new Post {
                Comments = new List<Comment> {
                    new Comment {
                        Body = "Test comment 1",
                        IsPublic = true,
                    },
                    new Comment {
                        Body = "Deleted comment 1",
                        IsPublic = false,
                    },
                },
                IsPublic = true,
                PubDate = DateTimeOffset.Now,
            });

            var model = new IndexModel(testDataStore);
            model.OnGet();

            Assert.Equal(1, model.PostSummaries.Count());
            Assert.Equal(1, model.PostSummaries.First().CommentCount);
        }
    }
}
