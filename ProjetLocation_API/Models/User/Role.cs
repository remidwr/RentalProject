﻿using System.Collections.Generic;

namespace ProjetLocation.API.Models.User
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserInfo> Users { get; set; }
    }
}
