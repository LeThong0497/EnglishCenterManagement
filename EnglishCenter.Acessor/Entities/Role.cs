using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

    }
}
