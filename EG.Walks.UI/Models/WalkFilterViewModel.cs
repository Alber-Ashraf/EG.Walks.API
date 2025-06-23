using EG.Walks.UI.Models.DTOs;

namespace EG.Walks.UI.Models
{
    public class WalkFilterViewModel
    {
        public List<WalkDto> Walks { get; set; } = new List<WalkDto>();

        // Filter/Sort/Paging Parameters
        public string? FilterOn { get; set; }
        public string? FilterQuery { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // For pagination UI
        public int TotalPages { get; set; }
    }

}
