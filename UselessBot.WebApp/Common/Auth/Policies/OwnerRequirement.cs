using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UselessBot.WebApp.Common.Auth.Policies
{
    public class OwnerRequirement : IAuthorizationRequirement
    {
        public OwnerRequirement()
        {
        }
    }
}
