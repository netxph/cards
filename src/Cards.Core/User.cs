﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class User
    {
        public User()
        {
            IsActive = true;
            CreatedDateUtc = DateProvider.UtcNow();
            ModifiedDateUtc = DateProvider.UtcNow();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedDateUtc { get; set; }
        public DateTime ModifiedDateUtc { get; set; }

        public bool IsActive { get; set; }

        static IDateProvider _dateProvider = null;

        public static IDateProvider DateProvider
        {
            get
            {
                if (_dateProvider == null)
                {
                    _dateProvider = new DateProvider();
                }

                return _dateProvider;
            }
            set
            {
                _dateProvider = value;
            }
        }

        public static User Register(string email, string name)
        {
            var db = DbFactory.Create();

            var user = new User()
            {
                Name  = name,
                Email = email
            };

            user = db.CreateUser(user);

            return user;
        }

        public static User CheckRegistration(string email)
        {
            var db = DbFactory.Create();

            return db.FindUser(email);
        }

        public static User Update(string email, string name)
        {
            var db   = DbFactory.Create();
            var user = db.FindUser(email);

            if (user != null)
            {
                user.Name            = name;
                user.Email           = email;
                user.ModifiedDateUtc = DateProvider.UtcNow();

                db.UpdateUser(user);

                return user;
            }

            return null;
        }

        public static User Delete(string email)
        {
            var db   = DbFactory.Create();
            var user = db.FindUser(email);

            if (user != null)
            {
                user.IsActive        = false;
                user.ModifiedDateUtc = DateProvider.UtcNow();

                db.UpdateUser(user);
                
                return user;
            }

            return null;

        }
    }
}
