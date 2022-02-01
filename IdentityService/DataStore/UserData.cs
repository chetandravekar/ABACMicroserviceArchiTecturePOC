using IdentityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DataStore
{
    public static class UserData
    {
        public static readonly List<User> users = new List<User>
        {
            new User { Id = Guid.Parse("889cef49-9131-4dc5-a72e-c1bfd8a42d94"), Name = "Swapnil", Address="Nagpur", Age = 24, Designation="Sr. Engineer", Location = "Nagpur", Username = "swapnil", Password = "test@123" },
            new User { Id = Guid.Parse("2abb8b45-9323-4627-9655-f8b3022b34db"), Name = "Chetan", Address="Mumbai", Age = 29, Designation="Sr. Lead", Location = "Mumbai", Username = "chetan", Password = "test@123" },
            new User { Id = Guid.Parse("436bb8bd-5af4-475a-b326-0208a871b59e"), Name = "Rehan", Address="Pune", Age = 23, Designation="Sr. Lead", Location = "Pune", Username = "rehan", Password = "test@123" },
            new User { Id = Guid.Parse("8be78cf9-a8fd-4a39-a179-3f3aafeab203"), Name = "Parul", Address="Hyderabad", Age = 24, Designation="Consultant", Location = "Hyderabad", Username = "parul", Password = "test@123" }
        };

        public static readonly List<Role> roles = new List<Role>
        {
            new Role { Id = Guid.Parse("294f79f1-e462-4475-aaf0-f3a13a210fcf"), },
            new Role { Id = Guid.Parse("0e0a8d07-50d0-42a7-8c6c-1681328ba3ed"), },
            new Role { Id = Guid.Parse("d45ee0aa-0fc8-4ec0-8041-fa66cf8aa271"), }
        };

        public static readonly List<UserRole> userRole = new List<UserRole>
        {
            new UserRole { Id = Guid.NewGuid(), RoleName = "Admin", UserId = Guid.Parse("2abb8b45-9323-4627-9655-f8b3022b34db")},
            new UserRole { Id = Guid.NewGuid(), RoleName = "Admin", UserId = Guid.Parse("436bb8bd-5af4-475a-b326-0208a871b59e")},
            new UserRole { Id = Guid.NewGuid(), RoleName = "Dev", UserId = Guid.Parse("889cef49-9131-4dc5-a72e-c1bfd8a42d94")},
            new UserRole { Id = Guid.NewGuid(), RoleName = "QA", UserId = Guid.Parse("8be78cf9-a8fd-4a39-a179-3f3aafeab203")}
        };
    }
}
