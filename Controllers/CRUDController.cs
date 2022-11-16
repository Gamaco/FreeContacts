using FreeContacts.Areas.Identity.Data;
using FreeContacts.Data;
using FreeContacts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Xml.Linq;
using System;
using System.Drawing.Text;
using System.Security.Claims;

// Antes de abrir cualquier pagina, primero hace el request en el controller.

namespace FreeContacts.Controllers
{

    public class CRUDController : Controller
    {
        private readonly UserManager<FreeContactsUser> _userManager;

        public CRUDController(UserManager<FreeContactsUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<CRUDModel> contactInfo = new List<CRUDModel>();

            ContactDAO contactDAO = new ContactDAO();

            contactInfo = contactDAO.FetchAll(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View("Index", contactInfo);
        }

        public IActionResult Details(int id)
        {
            ContactDAO contactDAO = new ContactDAO();

            CRUDModel contact = contactDAO.FetchOne(id);

            return View("Details", contact);
        }

        public IActionResult Create()
        {
            return View("ContactForm");
        }

        public IActionResult Edit(int id)
        {
            ContactDAO contactDAO = new ContactDAO();

            CRUDModel contact = contactDAO.FetchOne(id);

            return View("ContactForm", contact);
        }

        public IActionResult Delete(int id)
        {
            ContactDAO contactDAO = new ContactDAO();

            contactDAO.Delete(id);

            List<CRUDModel> contacts = contactDAO.FetchAll(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View("Index", contacts);
        }

        [HttpPost]
        public IActionResult ProcessCreate(CRUDModel crudModel)
        {
            //Save to the db.
            ContactDAO contactDAO = new ContactDAO();

            crudModel.userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            contactDAO.CreateOrUpdate(crudModel);

            return View("Details", crudModel);
        }
    }
}
