using Datacenter.Models.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Datacenter.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        #region Session
        protected string GetCurrentUserID()
        {
            return ClaimsPrincipal.Current.Claims.Where(it => it.Type == "UserID").Select(it => it.Value).FirstOrDefault();
        }

        protected long GetCurrentUserBranchID()
        {
            string branchID = ClaimsPrincipal.Current.Claims.Where(it => it.Type == "BranchID").Select(it => it.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(branchID))
                return 0;

            return Convert.ToInt64(branchID);
        }

        protected string GetCurrentUserBranchCode()
        {
            return ClaimsPrincipal.Current.Claims.Where(it => it.Type == "BranchCode").Select(it => it.Value).FirstOrDefault();
        }

        protected string GetCurrentUserGroupID()
        {
            return ClaimsPrincipal.Current.Claims.Where(it => it.Type == "GroupID").Select(it => it.Value).FirstOrDefault();
        }

        protected string GetCurrentUsername()
        {
            return ClaimsPrincipal.Current.Claims.Where(it => it.Type == ClaimTypes.Name).Select(it => it.Value).FirstOrDefault();
        }

        protected string GetCurrentUsernameWithDomain()
        {
            return ClaimsPrincipal.Current.Claims.Where(it => it.Type == ClaimTypes.NameIdentifier).Select(it => it.Value).FirstOrDefault();
        }

        protected string GetCurrentUserPassword()
        {
            return ClaimsPrincipal.Current.Claims.Where(it => it.Type == "Password").Select(it => it.Value).FirstOrDefault();
        }

        protected string GetLdapUsernameWithDomain()
        {
            return string.Format(@"{0}\{1}",
                ConfigurationHelper.ReadProperty("LdapName"),
                ConfigurationHelper.ReadProperty("LdapUsername"));
        }

        protected string GetLdapPassword()
        {
            return ConfigurationHelper.ReadProperty("LdapPassword");
        }
        #endregion

        protected IHttpActionResult Success()
        {
            return Json(new ResponseJson()
            {
                Status = true
            });
        }

        protected IHttpActionResult Success<T>(T content)
        {
            return Json(new ResponseJson<T>()
            {
                Status = true,
                Data = content
            });
        }

        protected IHttpActionResult Failed(Exception ex)
        {
            Log.Error(ex, "Error");
            Log.CloseAndFlush();

            return Json(new ResponseJson()
            {
                Status = false,
                Message = ex.Message
            });
        }
    }
}