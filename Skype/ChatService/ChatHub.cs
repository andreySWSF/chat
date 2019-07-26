using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Skype.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
       // User testUser = new User() { NickName = "dddd", Password = "000" };
        IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task Send(string message, string to)
        {
            var handler = new JwtSecurityTokenHandler();
            string token = Context.GetHttpContext().Request.Query["access_token"];
            var res = token.GetType();
            to = "AndreyN";
            if (token == null)
            {
                throw new Exception();
            }
            else
            {
                var jsonToken = handler.ReadToken(token);
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var claims = tokenS.Claims;
                var name = tokenS.Claims.First(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType).Value;
               // var connectionId = Context.Co;

                if (name != to) // если получатель и текущий пользователь не совпадают
                   // await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, name);
                await Clients.User(to).SendAsync("Send", message, name);
                //await Clients.All.SendAsync("Send", message, name);
            }
            
            
        }

        
    }
}
