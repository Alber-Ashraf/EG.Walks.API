using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Walks.Domain.DTOs.Requests
{
    public class CreateRegionRequestDto
    {
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
