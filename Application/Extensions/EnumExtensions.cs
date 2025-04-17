using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class EnumExtensions
    {
        public class EnumResult
        {
            public string Title { get; set; }
            public List<EnumDetail> EnumDetails { get; set; }
        }
        public class EnumDetail
        {
            public EnumDetail(int Key, string Description)
            {
                this.Key = Key;
                this.Description = Description;
            }
            public int Key { get; set; }
            public string Description { get; set; }

        }
        public static List<EnumDetail> GetDescriptionWithType(this Type type)
        {
            Array values = Enum.GetValues(type);
            List<EnumDetail> valueDescription = new();
            foreach (int val in values)
            {
                MemberInfo[] memInfo = type.GetMember(type.GetEnumName(val));

                DescriptionAttribute descriptionAttribute = memInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null)
                {
                    valueDescription.Add(new EnumDetail(val, descriptionAttribute.Description));
                }
            }

            return valueDescription;
        }
        public static ApiResult<EnumResult> GetDescriptionWithDomainName(this string enumName)
        {
            ApiResult<EnumResult> result = new();
            result.Value = new();
            result.Value.Title = enumName.Split(".").Last();
            result.Value.EnumDetails = new();
            List<EnumDetail> valuePairs = new();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);

                Type type = assembly.GetType(enumName);
                if (type == null)
                {
                    continue;
                }

                if (type.IsEnum)
                {
                    valuePairs = type.GetDescriptionWithType();
                }
            }
            result.Success();
            result.Value.EnumDetails = valuePairs;
            return result;
        }
        public static ApiResult<List<EnumResult>> GetAllEnums()
        {
            ApiResult<List<EnumResult>> result = new();
            result.Value = new();
            List<string> enumNames = new List<string>()
            {
                //"MarriageAndCounselingSystem.Domain.Enums.EducationEnum",

            };

            foreach (var enumName in enumNames)
            {
                EnumResult enum_data = new();
                enum_data.Title = enumName.Split(".").Last();
                enum_data.EnumDetails = new();
                List<EnumDetail> valuePairs = new();
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Console.WriteLine(assembly.FullName);

                    Type type = assembly.GetType(enumName);
                    if (type == null)
                    {
                        continue;
                    }

                    if (type.IsEnum)
                    {
                        valuePairs = type.GetDescriptionWithType();
                    }
                }
                enum_data.EnumDetails = valuePairs;

                result.Value.Add(enum_data);
            }
            result.Success();

            return result;
        }
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

    }
}
