using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ActionVM
    {
        [JsonIgnore]
        public IList<Attribute> ActionAttributes { get; set; }
        public string ActionDisplayName { get; set; }
        public string ActionId => $"{ControllerId}:{ActionName}";
        public string ControllerId { get; set; }
        public string ActionName { get; set; }
        public bool IsSecuredAction { get; set; }
    }
}
