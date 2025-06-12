using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.Walks.Domain.Entities
{
    class Difficulty
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
