﻿using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class ChatService
    {
        public SkypeContext _skypeContext;

        public ChatService(SkypeContext skypeContext)
        {
            _skypeContext = skypeContext;
        }
    }
}
