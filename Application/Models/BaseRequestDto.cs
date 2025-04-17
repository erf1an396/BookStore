using Application.Features.MvcActionsDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BaseRequestDto
    {
        protected Guid UserId { get; set; }
        protected ApiControllerActionDto ApiControllerAction { get; set; }

        protected string IpAddress  { get; set; }

        protected int PageNumber { get; set; }

        protected int PageSize { get; set; }


        public void SetUserId(Guid userId)
        {
            this.UserId = userId;   
        }

        public Guid GetUserId()
        { return this.UserId; }


        public void SetApiContorllerAction(ApiControllerActionDto apiControllerActionDto)
        {
            this.ApiControllerAction = apiControllerActionDto;
        }

        public void SetIpAddress(string ipAddress)
        {
            this.IpAddress = ipAddress;
        }

        public string GetIpAddress()
        {
            return this.IpAddress;
        }

        public void SetPageNumber(int PageNumber)
        {
            this.PageNumber = PageNumber;
        }

        public int GetPageNumber()
        {
            return  this.PageNumber;
        }

        public void SetPageSize(int PageSize)
        {
            this.PageSize = PageSize;
        }

        public int GetPageSize()
        {
            return this.PageSize;
        }
    }
}
