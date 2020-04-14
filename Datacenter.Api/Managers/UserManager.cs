using Api.Models.Entities.MySqlDatacenter;
using Api.Models.Persists.MySqlDatacenter;
using Datacenter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Datacenter.Api.Managers
{
    public class UserManager : IDisposable
    {
        #region Members
        private readonly MysqlDatacenterEntities dataCenterEntities;
        private readonly UserPersist userPersist;
        #endregion


        #region Constructors
        public UserManager()
        {
            dataCenterEntities = new MysqlDatacenterEntities();
            userPersist = new UserPersist(dataCenterEntities);
        }

        #endregion

        #region Public Methods
        public List<UserResult> Get()
        {
            List<user> users = userPersist.Get().ToList();

            List<UserResult> userResult = new List<UserResult>();

            foreach (user data in users)
            {
                userResult.Add(new UserResult
                {
                    Age = data.Age,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    UserID = data.UserID,
                    UserName = data.UserName,
                });
            }

            return userResult;
        }

        public UserResult GetByID(long userId)
        {
            user user = userPersist.GetById(userId);

            UserResult userResult = new UserResult
            {
                Age = user.Age,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserID = user.UserID,
                UserName = user.UserName,
            };

            return userResult;
        }

        public UserResult Save(User user)
        {
            user userData = new user
            {
                Age = user.Age,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
            };

            userPersist.Add(userData);
            userPersist.Save();

            return GetByID(userData.UserID);
        }

        public UserResult Update(User user, long userId)
        {
            user customer = userPersist.GetById(userId);

            customer.Age = user.Age;
            customer.Email = user.Email;
            customer.FirstName = user.FirstName;
            customer.LastName = user.LastName;
            customer.UserName = user.UserName;

            userPersist.Edit(userId, customer);
            userPersist.Save();

            return GetByID(userId);
        }

        public void Delete(long userId)
        {
            user customer = userPersist.GetById(userId);

            userPersist.Delete(userId, customer);
            userPersist.Save();
        }
        #endregion

        #region Private Methods

        #endregion

        public void Dispose()
        {
            dataCenterEntities.Dispose();
            userPersist.Dispose();
        }
    }
}