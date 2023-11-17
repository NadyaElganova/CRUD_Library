namespace CRUD_Library.ViewModels
{
        public class PaginationViewModel
        {
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public int? ReaderId { get; set; }
            public int? CategoryId { get; set; }
            public int LimitItem { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public Dictionary<string, string> RouteParams { get; set; }
        }
}
