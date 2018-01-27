using System;
using GW.Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GW.Site.Pages
{
    [Authorize]
    public class ManageCommentModel : PageModel
    {
        private readonly BlogDataStore _dataStore;

        public ManageCommentModel(BlogDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostDeleteComment(Guid commentId, string id)
        {
            var post = _dataStore.GetPost(id);

            var foundComment = _dataStore.FindComment(commentId, post);
            foundComment.IsPublic = false;

            _dataStore.SavePost(post);
            return Redirect($"/Post/{id}/{post.Slug}");
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostUndeleteComment(Guid commentId, string id)
        {
            var post = _dataStore.GetPost(id);

            var foundComment = _dataStore.FindComment(commentId, post);
            foundComment.IsPublic = true;

            _dataStore.SavePost(post);
            return Redirect($"/Post/{id}/{post.Slug}");
        }
    }
}
