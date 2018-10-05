using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class ErrorLog : EntityBase
    {
        [Key]
        public Guid LogId { get; set; }
        [MaxLength(100)]
        public string LogLevel { get; set; }
        [MaxLength(100)]
        public string Line { get; set; }
        [MaxLength(100)]
        public string ExceptionType { get; set; }

        public string Message { get; set; }
        [MaxLength(100)]
        public string Controller { get; set; }
        [MaxLength(100)]
        public string Method { get; set; }
    }
}
