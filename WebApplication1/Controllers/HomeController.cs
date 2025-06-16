using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.service.@abstract;
using WebApplication1.service.concrete;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        IPostService postService;
        public HomeController()
        {
            postService = new PostService();
        }


        [HttpGet]
        public IActionResult Index()
        {
            List <PostResponseModel> responses=postService.getPosts();
            return View(responses);
        }
       
    }
}
