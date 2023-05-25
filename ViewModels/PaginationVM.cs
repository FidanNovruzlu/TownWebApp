using TownWebApp.Models;

namespace TownWebApp.ViewModels
{
    public class PaginationVM<T>
    {
        public List<T> Introdactions { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
