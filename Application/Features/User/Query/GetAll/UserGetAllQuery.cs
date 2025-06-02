using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Query.GetAll
{
    public  class UserGetAllQuery : IRequest<ApiResult<List<UserDto>>>
    {
    }

    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery , ApiResult<List<UserDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserGetAllQueryHandler(UserManager<ApplicationUser> userManager , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            
        }

         public async Task<ApiResult<List<UserDto>>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            ApiResult<List<UserDto>> result = new();

            List<Domain.Entities.ApplicationUser> users = await _userManager.Users.ToListAsync(cancellationToken);
            result.Value = _mapper.Map<List<UserDto>>(users);
            result.Success();
            return result;

           
        }
    }
}
