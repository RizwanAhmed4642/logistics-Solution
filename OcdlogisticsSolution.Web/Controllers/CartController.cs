using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OcdlogisticsSolution.Common.ViewModels;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Controllers
{
    public class CartController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public CartController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public CartController() { }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        OcdlogisticsEntities ocdlogisticsEntities = new OcdlogisticsEntities();
        private void AddToCartChild(string resourceId, ResourceEnums resourceType)
        {
            if (Session["Carts"] == null)
            {
                List<CartItem> carts = new List<CartItem>();
                CartItem cartItem = new CartItem();
                cartItem.ResourceId = resourceId;
                cartItem.ResourceType = resourceType;
                carts.Add(cartItem);
                Session["Carts"] = carts;
            }
            else
            {
                var carts = (List<CartItem>)Session["Carts"];
                CartItem cartItem = new CartItem();
                cartItem.ResourceId = resourceId;
                cartItem.ResourceType = resourceType;
                carts.Add(cartItem);
                Session["Carts"] = carts;
            }
        }


        public List<CartItem> Cart
        {
            get
            {
                if (Session["Carts"] == null)
                {
                    List<CartItem> items = new List<CartItem>();
                    Session["Carts"] = items;
                    return items;
                }
                else
                {
                    var cart = (List<CartItem>)Session["Carts"];
                    return cart;
                }
            }
            set
            {
                Session["Carts"] = value;
            }
        }

        public ActionResult Index()
        {
            if (Cart.Count <= 0)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(Cart);
        }

        [HttpPost]
        public ActionResult RemoveCartItem(string resourceId,string src)
        {
            CartItem cartItem = null;
            if ((cartItem = Cart.Find(x => x.ResourceId == resourceId)) != null)
            {
                Cart.RemoveAll(x => x.ResourceId == cartItem.ResourceId);
            }
            return Redirect("/" + src + "#showCartModel");
        }

        [HttpPost]
        public ActionResult AddToCart(string resourceId, ResourceEnums resourceType, string src)
        {
            if (!string.IsNullOrWhiteSpace(resourceId))
                AddToCartChild(resourceId, resourceType);
            return Redirect("/" + src + "#showCartModel");
        }

        [HttpPost]
        public ActionResult CartAction(string resourceId, int actionType, string src)
        {
            CartItem cartItem = null;
            if ((cartItem = Cart.Find(x => x.ResourceId == resourceId)) != null)
            {
                if (actionType == 1)
                {
                    Cart.Add(cartItem);
                }
                else
                {
                    Cart.Remove(cartItem);
                }
            }

            return Redirect("/" + src + "#showCartModel");
        }

        public ActionResult CheckOutProcess()
        {
            if (IsAuthenticated)
            {
                return RedirectToAction("CheckOutProcessNext");
            }
            return View();
        }

        public ActionResult CheckOutProcessNext()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("CheckOutProcess");
            }

            ViewBag.Person = UserManager.FindById(userId);
            var cart = (List<CartItem>)Session["Carts"];

            if (cart == null)
                return RedirectToAction("cart");

            return View();
        }

        string userId => User.Identity.GetUserId();

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.User != null &&
                       HttpContext.User.Identity != null &&
                       HttpContext.User.Identity.IsAuthenticated;
            }
        }
    }
}