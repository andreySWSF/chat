using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ChatService
{
    public class ChatHub : Hub
    {
        readonly IMapper _mapper;
        public ChatHub(IMapper mapper)
        {
            _mapper = mapper;
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
