using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ControllerVM
    {
        [JsonIgnore]
        public string AreaName { get; set; }
        [JsonIgnore]
        public IList<Attribute> ControllerAttributes { get; set; }
        public string ControllerDisplayName { get; set; }
        public string ControllerId => $"{AreaName}:{ControllerName}";

        [JsonIgnore]
        public string ControllerName { get; set; }
        public IList<ActionVM> MvcActions { get; set; } = new List<ActionVM>();
    }
}
