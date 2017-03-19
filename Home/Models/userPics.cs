using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home.Models
{
    public class userPics
    {
        public int Id { get; set; }
        public byte[] image { get; set; }
        public string Userid { get; set; }
    }
}