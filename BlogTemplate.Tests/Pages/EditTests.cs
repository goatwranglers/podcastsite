using System;
using GW.Site.Models;
using GW.Site.Pages;
using GW.Site.Services;
using GW.Site.Tests.Fakes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace GW.Site.Tests.Pages
{
    public class EditTests
    {
        [Fact]
        public void UpdatePost_PublishedToPublished_TitleIsUpdated_UpdateSlug()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var slugGenerator = new SlugGenerator(testDataStore);
            var excerptGenerator = new ExcerptGenerator(140);

            var post = new Post {
                Title = "Title",
                Slug = "Title",
                IsPublic = true,
                PubDate = DateTimeOffset.Now,
            };

            testDataStore.SavePost(post);

            var testEditModel = new EditModel(testDataStore, slugGenerator, excerptGenerator);
            testEditModel.PageContext = new PageContext();
            testEditModel.EditedPost = new EditModel.EditedPostModel {
                Title = "Edited Title",
                Excerpt = "Excerpt",
            };

            testEditModel.OnPostPublish(post.Id.ToString("N"), true);

            post = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.Equal("Edited-Title", post.Slug);
        }

        [Fact]
        public void UpdatePost_DraftToPublished_TitleIsUpdated_UpdateSlug()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var slugGenerator = new SlugGenerator(testDataStore);
            var excerptGenerator = new ExcerptGenerator(140);

            var post = new Post {
                Title = "Title",
                IsPublic = false,
            };

            testDataStore.SavePost(post);

            var testEditModel = new EditModel(testDataStore, slugGenerator, excerptGenerator);
            testEditModel.PageContext = new PageContext();
            testEditModel.EditedPost = new EditModel.EditedPostModel {
                Title = "Edited Title",
                Excerpt = "Excerpt",
            };

            testEditModel.OnPostPublish(post.Id.ToString("N"), true);

            post = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.Equal("Edited-Title", post.Slug);
        }

        [Fact]
        public void UpdatePost_PreviouslyPublishedDraftToPublished_TitleIsUpdated_UpdateSlug()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var slugGenerator = new SlugGenerator(testDataStore);
            var excerptGenerator = new ExcerptGenerator(140);

            var post = new Post {
                Title = "Title",
                Slug = "Title",
                IsPublic = false,
            };

            testDataStore.SavePost(post);

            var testEditModel = new EditModel(testDataStore, slugGenerator, excerptGenerator);
            testEditModel.PageContext = new PageContext();
            testEditModel.EditedPost = new EditModel.EditedPostModel {
                Title = "Edited Title",
                Excerpt = "Excerpt",
            };

            testEditModel.OnPostPublish(post.Id.ToString("N"), true);

            post = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.Equal("Edited-Title", post.Slug);
        }

        [Fact]
        public void UpdatePost_PreviouslyPublishedDraftToPublished_DoNotUpdatePubDate()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var slugGenerator = new SlugGenerator(testDataStore);
            var excerptGenerator = new ExcerptGenerator(140);

            var post = new Post {
                Title = "Title",
                Slug = "Title",
                IsPublic = false,
                PubDate = new DateTimeOffset(new DateTime(1997, 7, 3), TimeSpan.Zero),
            };

            testDataStore.SavePost(post);

            var testEditModel = new EditModel(testDataStore, slugGenerator, excerptGenerator);
            testEditModel.PageContext = new PageContext();
            testEditModel.EditedPost = new EditModel.EditedPostModel {
                Title = "Edited Title",
                Excerpt = "Excerpt",
            };

            testEditModel.OnPostPublish(post.Id.ToString("N"), true);

            post = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.Equal(new DateTimeOffset(new DateTime(1997, 7, 3), TimeSpan.Zero), post.PubDate);
        }

        [Fact]
        public void UpdatePost_PublishedToDraft_TitleIsUpdated_DoNotUpdateSlug()
        {
            var testDataStore = new BlogDataStore(new FakeFileSystem());
            var slugGenerator = new SlugGenerator(testDataStore);
            var excerptGenerator = new ExcerptGenerator(140);

            var post = new Post {
                Title = "Title",
                Slug = "Title",
                IsPublic = true,
            };

            testDataStore.SavePost(post);

            var testEditModel = new EditModel(testDataStore, slugGenerator, excerptGenerator);
            testEditModel.PageContext = new PageContext();
            testEditModel.EditedPost = new EditModel.EditedPostModel {
                Title = "Edited Title",
                Excerpt = "Excerpt",
            };

            testEditModel.OnPostSaveDraft(post.Id.ToString("N"));

            post = testDataStore.GetPost(post.Id.ToString("N"));

            Assert.Equal("Title", post.Slug);
        }
    }
}
