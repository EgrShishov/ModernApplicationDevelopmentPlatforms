using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253505_Shishov.Helpers;

namespace WEB_253505_Shishov.Models
{
    public class IndexViewModel
    {
        public int SelectedId { get; set; }
        public SelectList ListItems { get; set; }
    }
}
