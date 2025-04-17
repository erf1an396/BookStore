using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MvcActionsDiscovery
{
    public class ApiControllerActionDto
    {

        private string _ControllerName;
        public string ControllerName
        {
            get
            {
                return _ControllerName?.ToUpper();
            }
            set
            {
                _ControllerName = value;
            }
        }
        public string ControllerDisplayName { get; set; }


        public string ActionName { get; set; }
        public string ActionDisplayName { get; set; }

        private string _ActionRoute;
        public string ActionRoute
        {
            get
            {
                return _ActionRoute?.ToUpper();
            }
            set
            {
                _ActionRoute = value;
            }
        }
        private string _HttpMethod;
        public string HttpMethod
        {
            get
            {
                return _HttpMethod?.ToUpper();
            }
            set
            {
                _HttpMethod = value;
            }
        }

        public bool IsSecured { get; set; }
        public string Policy { get; set; }
        public string Value => $"{_ControllerName?.ToUpper()}:{ActionName?.ToUpper()}|{Policy?.ToUpper()}";

    }
}
