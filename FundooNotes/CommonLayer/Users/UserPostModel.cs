using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Users
{
    public class UserPostModel
    {
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
