using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skype.ChatService
{
   // [Authorize]
    public class ChatHub : Hub
    {
        readonly IMapper _mapper;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        User testUser = new User() { NickName = "dddd", Password = "000" };
        public ChatHub(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            //var t = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task Send(string message, string to)
        {

            //var chatVM = new Models.VModels.ChatVM
            //{
            //    ChatName = "test"
            //};
            //var model = _mapper.Map<Models.Chat>(chatVM);

           // var user = Context.User;
            //var userName = user.Identity.Name;
            var userName = Context.User.Identity.Name;

            if (Context.UserIdentifier != to) // если получатель и текущий пользователь не совпадают
                await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName);
            await Clients.User(to).SendAsync("Receive", message, userName);
            await Clients.All.SendAsync("Send", message, userName);
            
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"Hi {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }
    }
}
