﻿namespace Book.WebApplication.Models
{
    public class ResponseDto<T>
    {
        public bool  isSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
