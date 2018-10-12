using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    /// <summary>
    /// Declaration for marker class for all owned entity framework models
    /// </summary>
    public abstract class EntityBase
    {
        [NotMapped]
        public string RefId { get; set; }
        [NotMapped]
        public string RefAction { get; set; }
        [NotMapped]
        public string RefController { get; set; }
    }
}
