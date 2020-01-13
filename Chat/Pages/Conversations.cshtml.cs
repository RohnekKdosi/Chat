using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chat.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Chat.Data;
using Microsoft.AspNetCore.Identity;

namespace Chat
{
    public class ConversationsModel : PageModel
    {
        DataProvider _dp { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public string UserID { get; set; }
        public ConversationsModel(DataProvider dp, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _dp = dp;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty(SupportsGet = true)]
        public string UserFilter { get; set; }
        public string ConvoFilter { get; set; }
        public List<SelectListItem> UserList
        {
            get
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (var item in _dp.GetUsers())
                {
                    selectListItems.Add(new SelectListItem(item.UserName, item.Id));
                }
                return selectListItems;
            }
        }
        [BindProperty]
        public string Message { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ConvoFilter = UserFilter;
            UserID = "";
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                UserID = user.Id;
                if (UserFilter == null)
                {
                    Messages = null;
                    return Page();
                }
                else
                {
                    if (user.Id != "")
                    {
                        Messages = _dp.GetConversation(UserFilter, user.Id).Messages;
                    }
                    return RedirectToPage("./Conversations");
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            UserID = "";
            var user = await _userManager.GetUserAsync(User);
            if (user != null && Message != "")
            {
                UserID = user.Id;
                _dp.SendMessage(_dp.GetConversation(ConvoFilter, user.Id), user, Message);
            }
            Message = "";
            return Page();
        }
    }
}