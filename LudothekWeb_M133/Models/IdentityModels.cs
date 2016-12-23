// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LudothekWeb_M133.Models {
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        #region Methodes

        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager) {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        #endregion
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        #region Constructor

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {}

        #endregion

        #region Methodes

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        #endregion
    }
}

#region Helpers

namespace LudothekWeb_M133 {
    public static class IdentityHelper {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public const string ProviderNameKey = "providerName";

        public const string CodeKey = "code";

        public const string UserIdKey = "userId";

        #region Methodes

        public static string GetProviderNameFromRequest(HttpRequest request) {
            return request.QueryString[ProviderNameKey];
        }

        public static string GetCodeFromRequest(HttpRequest request) {
            return request.QueryString[CodeKey];
        }

        public static string GetUserIdFromRequest(HttpRequest request) {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public static string GetResetPasswordRedirectUrl(string code, HttpRequest request) {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public static string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request) {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response) {
            if (!string.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl)) {
                response.Redirect(returnUrl);
            } else {
                response.Redirect("~/");
            }
        }

        private static bool IsLocalUrl(string url) {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        #endregion
    }
}

#endregion