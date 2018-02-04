using System;
using System.Collections.Generic;
using System.Linq;
using GW.Site.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GW.Site.Pages
{
    public class IndexModel : PageModel
    {
        const string StorageFolder = "BlogFiles";

        private readonly BlogDataStore _dataStore;

        public IEnumerable<PostSummaryModel> PostSummaries { get; private set; }
        public PostSummaryModel CurrentPost { get; private set; }
        public PostSummaryModel NextPost { get; private set; }

        public IndexModel(BlogDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public void OnGet()
        {
            bool PostFilter(Post p) => p.IsPublic;
            bool DeletedPostFilter(Post p) => !p.IsDeleted;
            var postModels = _dataStore.GetAllPosts().Where(PostFilter).Where(DeletedPostFilter);
            var posts = postModels as Post[] ?? postModels.ToArray();
            CurrentPost = posts.Select(p => new PostSummaryModel
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Excerpt = p.Excerpt,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Count(c => c.IsPublic),
            }).Take(1).FirstOrDefault();
            NextPost = posts.Select(p => new PostSummaryModel
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Excerpt = p.Excerpt,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Count(c => c.IsPublic),
            }).Skip(1).Take(1).FirstOrDefault();
            PostSummaries = posts.Select(p => new PostSummaryModel {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Excerpt = p.Excerpt,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Count(c => c.IsPublic),
            }).Skip(2).Take(5);
        }

        public class PostSummaryModel
        {
            public Guid Id { get; set; }
            public string Slug { get; set; }
            public string Title { get; set; }
            public DateTimeOffset PublishTime { get; set; }
            public string Excerpt { get; set; }
            public int CommentCount { get; set; }
       }
    }
}
