using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SteelStudio.Web.Models;

[assembly: OwinStartupAttribute(typeof(SteelStudio.Web.Startup))]
namespace SteelStudio.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            this.CreateUserAndRoles();
        }
        private void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("administrador"))
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "administrador";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser();
                user.UserName = "oscar@gmail.com";
                user.Email = "oscar@gmail.com";

                string userPWD = "Cienfuegos*19";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role administrador    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "administrador");
                }                
            }
            // creating Diseñador role     
            if (!roleManager.RoleExists("Diseñador"))
            {
                var role = new IdentityRole();
                role.Name = "diseñador";
                roleManager.Create(role);
            }
        }
    }
}
