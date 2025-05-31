using Application.Models;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Command.Update
{
    public class UserUpdateCommand : IRequest<ApiResult>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDay_Date { get; set; }

        public GenderEnum Gender { get; set; }

    }

    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand , ApiResult>
    {
        private readonly 
    }
}
