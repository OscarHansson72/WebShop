﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUserRepository
    {
        public Task<User> GetByEmail(string email);
    }
}
