using OcdlogisticsSolution.Business;
using OcdlogisticsSolution.Common.ViewModels;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Payment With Paypal Method.
        /// Auther : Rizwan Ahmed
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns>View</returns>
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    if (createdPayment == null)
                    {
                        return RedirectToAction("cart", "Product");
                    }

                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View("FailureView");
            }
            Session["Carts"] = null;
            //on successful payment, show success page to user.  
            return RedirectToAction("Index", "Dashboard");
        }
        private PayPal.Api.Payment payment;
        /// <summary>
        /// Execute Payment Method.
        /// Auther :Rizwan Ahmed.
        /// </summary>
        /// <param name="apiContext"></param>
        /// <param name="payerId"></param>
        /// <param name="paymentId"></param>
        /// <returns>Execute(apiContext, paymentExecution)</returns>
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        /// <summary>
        /// Create Payment Method.
        /// Auther : Rizwan Ahmed.
        /// </summary>
        /// <param name="apiContext"></param>
        /// <param name="redirectUrl"></param>
        /// <returns>Create(apiContext)</returns>

        OcdlogisticsEntities OcdlogisticsEntities = new OcdlogisticsEntities();
        public Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = FilterItems();

            if (listItems == null)
                return null;
            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "2.55",
                shipping = "10.00",
                subtotal = totalCharges.ToString(),
                //shipping_discount = "-1"
            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = (totalCharges + 2.55 + 10.00).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        double totalCharges = 0;
        public ItemList FilterItems()
        {
            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };


            var cart = (List<CartItem>)Session["Carts"];


            List<tbl_Product> tbl_Products = new List<tbl_Product>();
            double subtotal = 0;


            if (cart == null)
                return null;


            var cartDistictItems = cart.Select(x => x.ResourceId).Distinct();

            foreach (var obj in cartDistictItems)
            {
                var item = cart.Find(x => x.ResourceId == obj);
                if (item.ResourceType == ResourceEnums.Product)
                {
                    var product = OcdlogisticsEntities.tbl_Product.Find(item.ResourceId);
                    if (product != null)
                    {
                        subtotal = cart.Where(x => x.ResourceId == product.Id).Count() * (Convert.ToDouble(product.SupplyPrice) * Convert.ToDouble(product.Markup));
                        totalCharges += subtotal;

                        //Adding Item Details like name, currency, price etc
                        itemList.items.Add(new Item()
                        {
                            name = product.Name,
                            currency = "USD",
                            price = (Convert.ToDouble(product.SupplyPrice) * Convert.ToDouble(product.Markup)).ToString(),
                            quantity = cart.Where(x => x.ResourceId == product.Id).Count().ToString(),
                            sku = product.SKU
                        });
                    }
                }
                else if (item.ResourceType == ResourceEnums.DistributionServices)
                {
                    var service_package = OcdlogisticsEntities.tbl_Distribution_Packages.Find(item.ResourceId);
                    if (service_package != null)
                    {
                        subtotal = cart.Where(x => x.ResourceId == service_package.Id).Count() * Convert.ToDouble(service_package.Cost);
                        totalCharges += subtotal;

                        //Adding Item Details like name, currency, price etc
                        itemList.items.Add(new Item()
                        {
                            name = service_package.Name,
                            currency = "USD",
                            price = service_package.Cost,
                            quantity = cart.Where(x => x.ResourceId == service_package.Id).Count().ToString(),
                            sku = "#" + Guid.NewGuid().ToString().Replace("-", string.Empty)
                        });
                    }
                }
            }






            return itemList;
        }

    }
}