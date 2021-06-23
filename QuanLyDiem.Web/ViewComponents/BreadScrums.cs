using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.ViewComponents
{
    public class BreadScrums : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<string> paths = new List<string>();
            if (Request.Path.Value != null)
            {
                var route = Request.Path.Value.ToString();
                paths =  route.Split("/");
            }
            return View(new BreadScrumsViewModel{Paths = paths});
        }
    }
}