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
        Persian = 1  ,

        [Description("انگلیسی")]
        English = 2 ,

        [Description("فرانسوی")]
        French = 3
    }
}
