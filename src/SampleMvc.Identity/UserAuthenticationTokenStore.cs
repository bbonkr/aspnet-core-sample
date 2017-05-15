using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Data;
using SampleMvc.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMvc.Identity
{
    public class UserAuthenticationTokenStore : UserStore, IUserAuthenticationTokenStore<User>
    {
        public UserAuthenticationTokenStore(AppDbContext db) : base(db)
        {

        }

        #region IUserAuthenticationTokenStore 멤버

        public Task<string> GetTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(loginProvider))
            {
                throw new ArgumentException("로그인 제공자가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("토큰 이름이 입력되지 않았습니다.");
            }

            return Task.Run<string>(() =>
            {
                return _db.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id && t.LoginProvider == loginProvider && t.Name == name, cancellationToken).Result?.Value;
            }, cancellationToken);
        }

        public Task RemoveTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(loginProvider))
            {
                throw new ArgumentException("로그인 제공자가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("토큰 이름이 입력되지 않았습니다.");
            }

            var token = _db.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id && t.LoginProvider == loginProvider && t.Name == name, cancellationToken).Result;

            if(token != null)
            {
                _db.UserTokens.Remove(token);
                _db.SaveChangesAsync(cancellationToken);
            }

            return Task.FromResult<object>(null);
        }

        public Task SetTokenAsync(User user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            if(user == null)
            {
                throw new ArgumentException("사용자 정보가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(loginProvider))
            {
                throw new ArgumentException("로그인 제공자가 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("토큰 이름이 입력되지 않았습니다.");
            }

            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("토큰 값이 입력되지 않았습니다.");
            }

            var token = _db.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id && t.LoginProvider == loginProvider && t.Name == name, cancellationToken).Result;

            if (token == null)
            {
                token = new UserToken
                {
                    UserId = user.Id,
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                };
                _db.UserTokens.Add(token);
            }
            else
            {
                token.Value = value;
                _db.UserTokens.Update(token);
            }

            _db.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>(null);
        }

        #endregion
    }
}
