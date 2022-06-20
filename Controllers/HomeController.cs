using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantsShop.Models;
using PlantsShop.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlantsShop.Controllers
{
    public class HomeController : Controller
    {
        Context db;
        IndexViewModel viewModel;
        public HomeController(Context context)
        {
            db = context;
            viewModel = new IndexViewModel();
        }

        public async Task<IActionResult> Index(int? seller, string name, bool isList, int page=1, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Plant> plants = db.Plants;
             
            if (seller != null && seller != 0)
            {
                plants = plants.Where(p => p.SellerId == seller) ;
            }
            if (!String.IsNullOrEmpty(name))
            {
                plants = plants.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    plants = plants.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriceAsc:
                    plants = plants.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    plants = plants.OrderByDescending(s => s.Price);
                    break;
                case SortState.SellerAsc:
                    plants = plants.OrderBy(s => s.Seller.Name);
                    break;
                case SortState.SellerDesc:
                    plants = plants.OrderByDescending(s => s.Seller.Name);
                    break;
                default:
                    plants = plants.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 8;

            var count = await plants.CountAsync();
            var items = await plants.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            viewModel.PageViewModel = new PageViewModel(count, page, pageSize);
            viewModel.SortViewModel = new SortViewModel(sortOrder);
            viewModel.FilterViewModel = new FilterViewModel(db.Sellers.ToList(), seller, name);
            viewModel.Plants = items;
            viewModel.isList = isList;

            return View(viewModel);
        }
    }
}
