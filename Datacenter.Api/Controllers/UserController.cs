using Datacenter.Api.Managers;
using Datacenter.Models.Entities;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Datacenter.Api.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseApiController
    {
        #region Members
        private readonly UserManager userManager;
        #endregion

        #region Constructors
        public UserController()
        {
            userManager = new UserManager();
        }
        #endregion

        #region Methods
        [Route("")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<UserResult>))]
        public IHttpActionResult Get()
        {
            List<UserResult> users = userManager.Get();

            return Ok(users);
        }

        [Route("{userID:long}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserResult))]
        public IHttpActionResult GetByID(long userID)
        {
            UserResult user = userManager.GetByID(userID);

            return Success(user);
        }

        [Route("")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadGateway)]
        public IHttpActionResult Post(User user)
        {
            try
            {
                UserResult userResult = userManager.Save(user);

                return Ok(userResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{userID:long}")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(User))]
        public IHttpActionResult Put(User user, long userId)
        {
            UserResult userResult = userManager.Update(user, userId);

            return Ok(userResult);
        }

        [Route("{userID:long}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadGateway)]
        public IHttpActionResult Delete(long userId)
        {
            userManager.Delete(userId);

            return Ok();
        }
        #endregion

        #region Disposable
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userManager.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}