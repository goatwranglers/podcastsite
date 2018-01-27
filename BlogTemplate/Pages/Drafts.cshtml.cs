using System;
using System.Collections.Generic;
using System.Linq;
using GW.Site.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GW.Site.Pages
{
    public class DraftsModel : PageModel
    {
        const string StorageFolder = "BlogFiles";

        private readonly BlogDataStore _dataStore;

        public IEnumerable<DraftSummaryModel> DraftSummaries { get; private set; }

        public DraftsModel(BlogDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public void OnGet()
        {
            Func<Post, bool> postFilter = p => !p.IsPublic;
            var postModels = _dataStore.GetAllDrafts().Where(postFilter);

            DraftSummaries = postModels.Select(p => new DraftSummaryModel
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Excerpt = p.Excerpt,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Count,
            });
        }
    }

    public class DraftSummaryModel
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public DateTimeOffset PublishTime { get; set; }
        public string Excerpt { get; set; }
        public int CommentCount { get; set; }
    }
}

