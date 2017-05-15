using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Identity
{
    public class IdentityHelper
    {
        /// <summary>
        /// 사용자 이름을 표준화합니다.
        /// <para>대문자로 변환 ==> 빈칸 제거</para>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetNormalizedUserName(string userName)
        {
            var normalizedUserName = userName.ToUpper().Split(' ').Join(String.Empty);

            return normalizedUserName;
        }

        /// <summary>
        /// 역할 이름을 표준화합니다.
        /// <para>대문자로 변환 ==> 빈칸 제거</para>
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static string GetNormalizedRoleName(string roleName)
        {
            var normalizedRoleName = roleName.ToUpper().Split(' ').Join(String.Empty);

            return normalizedRoleName;
        }
    }
}
