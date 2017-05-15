using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Data;
using SampleMvc.Identity.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMvc.Identity
{
    public class UserStore : IUserStore<User>
    {
        protected readonly AppDbContext _db;

        public UserStore(AppDbContext db)
        {
            _db = db;
            
        }

        #region IUserStore 멤버

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentException("사용자 정보가 설정되지 않았습니다.");
                }

                _db.Users.AddAsync(user, cancellationToken);
                _db.SaveChangesAsync(cancellationToken);

                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError
                {
                    Code = "ERR101",
                    Description = ex.Message
                }));
            }
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                if(user == null)
                {
                    throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
                }

                _db.Users.Remove(user);
                _db.SaveChangesAsync(cancellationToken);

                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError
                {
                    Code = "ERR104",
                    Description = ex.Message
                }));
            }           
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("사용자 Id 가 입력되지 않았습니다.");
            }

            return _db.Users.FirstOrDefaultAsync(
                predicate: u => u.UserName == userId,
                cancellationToken: cancellationToken);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(normalizedUserName))
            {
                throw new ArgumentException("표준화된 사용자 이름이 입력되지 않았습니다.");
            }

            return _db.Users.FirstOrDefaultAsync(
             predicate: u => u.NormalizedUserName == normalizedUserName,
             cancellationToken: cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            var normalizedUserName = user.NormalizedUserName;

            return Task.Run<string>(() => { return normalizedUserName; }, cancellationToken);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            var id = user.Id;

            return Task.Run<string>(() => { return id; }, cancellationToken);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            var name = user.NormalizedUserName;

            return Task.Run<string>(() => { return name; }, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(normalizedName))
            {
                throw new ArgumentException("표준화된 사용자 이름이 입력되지 않았습니다.");
            }

            user.NormalizedUserName = normalizedName;

            _db.Users.Update(user);
            _db.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("사용자 이름이 입력되지 않았습니다.");
            }

            user.UserName = userName;
            _db.Users.Update(user);
            _db.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>(null);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }
            try
            {
                _db.Users.Update(user);
                _db.SaveChangesAsync(cancellationToken);

                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IdentityResult>(IdentityResult.Failed(new IdentityError
                {
                    Code = "ERR103",
                    Description = ex.Message
                }));
            }
        }

        #endregion
    }


}
