using Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
    public static class IdentityResultToApiResultExtention
    {
        public static ApiResult ToApiResult(this IdentityResult identityResult)
        {
            ApiResult result = new();
            if (identityResult.Succeeded)
            {
                result.Success("عملیات با موفقیت انجام شد");
                return result;
            }
            if (identityResult.Errors.Where(x => !string.IsNullOrEmpty(x.Description)).Any())
            {
                foreach (var error in identityResult.Errors.Where(x => !string.IsNullOrEmpty(x.Description)))
                {
                    result.Fail(error.Description);
                }
            }
            else
                result.Fail(ApiResultStaticMessage.UnknownExeption);
            return result;
        }
    }
}
