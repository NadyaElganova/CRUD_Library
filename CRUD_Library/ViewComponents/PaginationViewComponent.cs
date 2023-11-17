using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Library.ViewComponents
{
    public class PaginationViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages, int limit, int? readerId, int? categoryId)
        {
            PaginationViewModel paginationViewModel = new PaginationViewModel()
            {
                TotalPages = totalPages,
                CurrentPage = currentPage,
                LimitItem = limit,
                ReaderId = readerId,
                CategoryId = categoryId

            };
            return View("Pagination", paginationViewModel);
        }
    }
}

