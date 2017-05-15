using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Data;
using SampleMvc.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMvc.Identity
{
    public class UserRoleStore : UserStore, IUserRoleStore<User>
    {
        public UserRoleStore(AppDbContext db) : base(db)
        {

        }

        #region IUserRoleStore 멤버

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            var role = _db.Roles.FirstOrDefaultAsync(r => r.NormalizedRoleName == IdentityHelper.GetNormalizedRoleName(roleName)).Result;

            if (role == null)
            {
                throw new ArgumentException("역할을 찾을 수 없습니다.");
            }

            _db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            _db.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>(null);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }


            var roleNames = from userRole in _db.UserRoles
                            join role in _db.Roles
                            on userRole.RoleId equals role.Id
                            where userRole.UserId == user.Id
                            select role.Name;

            return Task.Run<IList<string>>(() => roleNames.ToList(), cancellationToken);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("역할 이름이 입력되지 않았습니다.");
            }

            var normalizedRoleName = IdentityHelper.GetNormalizedRoleName(roleName);

            var users = from userRole in _db.UserRoles
                        join user in _db.Users
                        on userRole.UserId equals user.Id
                        join role in _db.Roles
                        on userRole.RoleId equals role.Id
                        where role.NormalizedRoleName == normalizedRoleName
                        select user;

            return Task.Run<IList<User>>(() => users.ToList(), cancellationToken);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
