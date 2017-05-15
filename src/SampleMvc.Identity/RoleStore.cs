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
    public class RoleStore : IRoleStore<Role>
    {
        private readonly AppDbContext _db;

        public RoleStore(AppDbContext db)
        {
            _db = db;
        }

        #region RoleStore 멤버


        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            try
            {
                if (role == null)
                {
                    throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
                }

                _db.Roles.AddAsync(role, cancellationToken);
                _db.SaveChangesAsync(cancellationToken);

                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError {
                    Code = "ERR201",
                    Description = ex.Message
                }));
            }
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            try
            {
                if (role == null)
                {
                    throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
                }

                _db.Roles.Remove(role);
                _db.SaveChangesAsync(cancellationToken);

                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError
                {
                    Code = "ERR204",
                    Description = ex.Message
                }));
            }
        }

        public void Dispose()
        {
            if(_db != null)
            {
                _db.Dispose();
            }
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentException("역할Id가 입력되지 않았습니다.");
            }

            return _db.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("표준화된 역할이름이 입력되지 않았습니다.");
            }

            return _db.Roles.FirstOrDefaultAsync(r => r.NormalizedRoleName == normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
            }

            return Task.FromResult<string>(role.NormalizedRoleName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
            }

            return Task.FromResult<string>(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
            }

            return Task.FromResult<string>(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
            }

            return Task.FromResult<string>(role.NormalizedRoleName);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
            }

            role.Name = roleName;

            _db.Roles.Update(role);
            _db.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>(null);
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            try
            {
                if (role == null)
                {
                    throw new ArgumentException("역할 정보가 입력되지 않았습니다.");
                }

                _db.Roles.Update(role);
                _db.SaveChangesAsync(cancellationToken);

                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError
                {
                    Code = "ERR203",
                    Description = ex.Message
                }));
            }
        }

        #endregion
    }
}
