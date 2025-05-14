using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum BookLanguageEnum
    {
        [Description("فارسی")]
        فارسی = 1  ,

        [Description("انگلیسی")]
        انگلیسی = 2 ,

        [Description("فرانسوی")]
        فرانسوی = 3
    }
}
