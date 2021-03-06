using BackEndCointerest.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;

namespace BackEndCointerest.Models
{
    public class User
    {
        private string username;
        private DateTime birthdate;
        private string image;
        private string password;
        private string bio;

        public User()
        {

        }

        public User(string username, DateTime birthdate, string image, string password,string bio)
        {
            this.username = username;
            this.birthdate = birthdate;
            this.image = image;
            this.password = password;
            this.bio = bio;
        }

        public string Username { get => username; set => username = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public string Image { get => image; set => image = value; }
        public string Password { get => password; set => password = value; }
        public string Bio { get => bio; set => bio = value; }

        public List<User> get_users(string username)
        {
            DBServices ds = new DBServices();
            List<User> users = ds.Get_users(username);
            return users;
        }
        public void Insert()
        {
            DBServices ds = new DBServices();
            ds.Insert(this);
        }

    }
}