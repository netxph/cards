using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cards.Core;

namespace Cards.Web.Models
{
    public class FacebookUserProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileLink { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }

        private Dictionary<string, string> dictionary;


        public FacebookUserProfile(Dictionary<string, string> dictionary)
        {
            Id = dictionary[SSOConstants.ID_KEY];
            Name = dictionary[SSOConstants.NAME_KEY];
            FirstName = dictionary[SSOConstants.FIRSTNAME_KEY];
            LastName = dictionary[SSOConstants.LASTNAME_KEY];
            ProfileLink = dictionary[SSOConstants.LINK_KEY];
            UserName = dictionary[SSOConstants.USERNAME_KEY];
            Email = dictionary[SSOConstants.USER_DATA_KEY];
            Verified = bool.Parse(dictionary[SSOConstants.VERIFIED_KEY]);
        }

    }
}