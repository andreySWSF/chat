using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skype.Database;
using Skype.Models;
using Skype.Models.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class ChatRepository : GenericRepository
    {
        SkypeContext context = new SkypeContext();
        private readonly IMapper _mapper;
               
        public ChatRepository(SkypeContext skypeContext, IMapper mapper) : base(skypeContext)
        {
            _mapper = mapper;

            //var chatVM = new ChatVM();
            //var chat = Mapper.Map<Chat>(chatVM);
        }

        //public void Map()
        //{

        //    Mapper.Initialize(u => { u.CreateMap<Chat, ChatVM>();  });
        //    //var g = new GenericService<User>(
        //    //var users = Mapper.Map<IEnumerable<User>, List<UserVM>>(repo.GetAll());
        //}
    }


}

//public async Task<IActionResult> Edit(string id)
//{

//    // Instantiate source object
//    // (Get it from the database or whatever your code calls for)
//    var user = await _context.Users
//        .SingleOrDefaultAsync(u => u.Id == id);

//    // Instantiate the mapped data transfer object
//    // using the mapper you stored in the private field.
//    // The type of the source object is the first type argument
//    // and the type of the destination is the second.
//    // Pass the source object you just instantiated above
//    // as the argument to the _mapper.Map<>() method.
//    var model = _mapper.Map<UserDto>(user);

//    // .... Do whatever you want after that!
//}