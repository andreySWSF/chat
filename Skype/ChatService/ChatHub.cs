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
        static HashSet<string> CurrentConnections = new HashSet<string>();
        // User testUser = new User() { NickName = "dddd", Password = "000" };
        IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            CurrentConnections.Add(id);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
            var connection = CurrentConnections.FirstOrDefault(x => x == Context.ConnectionId);

            if (connection != null)
            {
                CurrentConnections.Remove(connection);
            }

            return base.OnDisconnectedAsync(ex);
        }


        //return list of all active connections
        public List<string> GetAllActiveConnections()
        {
            return CurrentConnections.ToList();
        }
        //public void SendChatMessage(string who, string message)
        //{
        //    string name = Context.User.Identity.Name;

        //    foreach (var connectionId in _connections.GetConnections(who))
        //    {
        //        Clients.Client(connectionId).addChatMessage(name + ": " + message);
        //    }
        //}

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
                var connections = CurrentConnections.ToList();

                if (name != to) // если получатель и текущий пользователь не совпадают
                   // await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, name);
                await Clients.User(to).SendAsync("Send", message, name);
                //await Clients.All.SendAsync("Send", message, name);
            }
            
            
        }

        
    }
}
