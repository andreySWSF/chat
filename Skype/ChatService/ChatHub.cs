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

    public class ChatHub : Hub
    {
        readonly IMapper _mapper;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        public ChatHub(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            var t = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task Send(string message)
        {

            var chatVM = new Models.VModels.ChatVM
            {
                ChatName = "test"
            };
            var model = _mapper.Map<Models.Chat>(chatVM);
           

            await this.Clients.All.SendAsync("Send", message);
            
        }
    }
}
