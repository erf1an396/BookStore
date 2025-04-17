using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public List<string> Message { get; set; } = new();

        public bool IsForbiden { get; set; }

        public void Success(string message = null)
        {
            IsSuccess = true;
            Message.Add(message);
        }

        public void Fail(string messsage)
        {
            IsSuccess = false;
            Message.Add(messsage);
        }

        public void Forbiden()
        {
            IsForbiden = true;
        }


    }
    public class ApiResult<T> : ApiResult
    {
        public T Value { get; set; }
    }

    public static class ApiResultStaticMessage
    {
        public static string SavedSuccessfully
        {
            get
            {
                return "اطلاعات با موفقیت ذخیره سازی شد.";
            }
        }
        public static string DeleteSuccessfully
        {
            get
            {
                return "اطلاعات با موفقیت حذف شد.";
            }
        }
        public static string UpdateSuccessfully
        {
            get
            {
                return "اطلاعات با موفقیت به روز رسانی شد.";
            }
        }
        public static string NotFound
        {
            get
            {
                return "آیتم مورد نظر یافت نشد .";
            }
        }
        public static string UnknownExeption
        {
            get
            {
                return "خطای رخ داده است .";
            }
        }
        public static string HasNoAccess
        {
            get
            {
                return "دسترسی این عملیات را ندارید.";
            }
        }
    }

}
