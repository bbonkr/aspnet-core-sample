﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Identity.Models
{
    public class User : IdentityUser<string, UserClaim, UserRole, UserLogin>
    {

    }   
}
