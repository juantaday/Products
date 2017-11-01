namespace Products.Models
{
    using System;
    public class Response
    {
        public Object Result { get; set; }
        public string Message { get; set; }
        public bool  IsSuccess { get; set; }
    }
}
