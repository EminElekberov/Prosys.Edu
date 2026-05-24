using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Shared.DTOs
{
    public class UsersDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserTypeName { get; set; }
        public Guid? UserTypeId { get; set; }
        public Guid? UserClassId { get; set; }
    }
}
