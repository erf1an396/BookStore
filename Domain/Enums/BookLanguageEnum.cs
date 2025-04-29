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
        Persian = 0  ,

        [Description("انگلیسی")]
        English = 1 ,

        [Description("فرانسوی")]
        French = 2 
    }
}
