using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Haver_Niagara.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Haver_Niagara.ViewModels;
using Microsoft.EntityFrameworkCore;
using Haver_Niagara.Models;
using Haver_Niagara.Utilities;
using Humanizer;
using WebPush;

namespace Haver_Niagara.Controllers
{
    [Authorize] //means they have to be logged in for it to work.
    public class EmployeeAccountController : Controller
    {
        //Controller that allows an AUTHENTICATED user to maintain their account details
        private readonly HaverNiagaraDbContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeAccountController(HaverNiagaraDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: EmployeeAccount
        public IActionResult Index() //dont remove the index action but if they get here, redirect them to details
        {
            return RedirectToAction(nameof(Details));
        }

        // GET: EmployeeAccount/Details/5
        public async Task<IActionResult> Details()
        {
            //Check if we are configured for Push
            if (String.IsNullOrEmpty(_configuration.GetSection("VapidKeys")["PublicKey"]))
            {
                return RedirectToAction("GenerateKeys");
            }

            var employee = await _context.Employees
               .Include(e => e.Subscriptions)
               .Where(e => e.Email == User.Identity.Name)
               .Select(e => new EmployeeVM
               {
                   ID = e.ID,
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   NumberOfPushSubscriptions = e.Subscriptions.Count()
               })
               .FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.PublicKey = _configuration.GetSection("VapidKeys")["PublicKey"];
            return View(employee);
        }

        // GET: EmployeeAccount/Edit/5
        public async Task<IActionResult> Edit()
        {
            var employee = await _context.Employees
                .Where(e => e.Email == User.Identity.Name)
                .Select(e => new EmployeeVM
                {
                    ID = e.ID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    //Phone = e.Phone,
                    NumberOfPushSubscriptions = e.Subscriptions.Count()
                })
                .FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: EmployeeAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeToUpdate = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);

            //Note: Using TryUpdateModel we do not need to invoke the ViewModel
            //Only allow some properties to be updated
            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "",
                c => c.FirstName, c => c.LastName /*,c => c.Phone*/))
            {
                try
                {
                    _context.Update(employeeToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(employeeToUpdate.FullName);
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    //Since we do not allow changing the email, we cannot introduce a duplicate
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            return View(employeeToUpdate);
        }



        // GET: EmployeeAccount/ Create the record of the Push Subscription
        public IActionResult Push(int EmployeeID)
        {
            ViewBag.PublicKey = _configuration.GetSection("VapidKeys")["PublicKey"];
            ViewData["EmployeeID"] = EmployeeID;
            return View();
        }

        // POST: EmployeeAccount/ Create the record of the Push Subscription
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Push([Bind("PushEndpoint,PushP256DH,PushAuth,EmployeeID")] Subscription sub, string btnSubmit)
        {
            if (btnSubmit == "Unsubscribe")//Delete the subscription record
            {
                try
                {
                    var sToRemove = _context.Subscriptions.Where(s => s.PushP256DH == sub.PushP256DH
                        && s.PushEndpoint == sub.PushEndpoint
                        && s.PushAuth == sub.PushAuth).FirstOrDefault();
                    if (sToRemove != null)
                    {
                        _context.Subscriptions.Remove(sToRemove);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error: Could not remove the record of the subscription.");
                }
            }
            else//Create the subscription record
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(sub);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error: Could not create the record of the subscription.");
                }
            }
            ViewData["EmployeeID"] = sub.EmployeeID;
            return View(sub);
        }

        public IActionResult GenerateKeys()
        {
            var keys = VapidHelper.GenerateVapidKeys();
            ViewBag.PublicKey = keys.PublicKey;
            ViewBag.PrivateKey = keys.PrivateKey;
            return View();
        }
        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }

    }
}
