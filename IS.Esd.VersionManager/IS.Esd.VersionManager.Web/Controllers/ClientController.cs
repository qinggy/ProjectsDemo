using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IS.Esd.VersionManager.Web.ViewModels;
using IS.Esd.Domain;
using IS.Esd.Business.Interface;
using IS.Esd.Domain.Entities;

namespace IS.Esd.VersionManager.Web.Controllers
{
    public class ClientController : Controller
    {

        private readonly IClientBusiness ClientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            ClientBusiness = clientBusiness;
        }

        //
        // GET: /Client/

        public ActionResult Index()
        {
            List<ClientVM> clientVMs = new List<ClientVM>();
            var clients = ClientBusiness.GetAll().ToList();
            AutoMapper.Mapper.Map(clients, clientVMs);
            return View(clientVMs);
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(Guid id)
        {
            var client = ClientBusiness.SingleOrDefault(id);
            ClientVM clientVM = new ClientVM();
            AutoMapper.Mapper.Map(client, clientVM);

            if (clientVM == null)
            {
                return HttpNotFound();
            }
            return View(clientVM);
        }

        //
        // GET: /Client/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientVM clientvm)
        {
            if (ModelState.IsValid)
            {
                clientvm.Id = Guid.NewGuid();
                Client client = new Client();
                AutoMapper.Mapper.Map(clientvm, client);
                ClientBusiness.CreateClient(client);
                return RedirectToAction("Index");
            }

            return View(clientvm);
        }

        //
        // GET: /Client/Edit/5

        public ActionResult Edit(Guid id)
        {
            ClientVM clientVM = new ClientVM();
            var client = ClientBusiness.SingleOrDefault(id);
            AutoMapper.Mapper.Map(client, clientVM);
            if (clientVM == null)
            {
                return HttpNotFound();
            }
            return View(clientVM);
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientVM clientvm)
        {
            Client client = new Client();
            AutoMapper.Mapper.Map(clientvm, client);
            ClientBusiness.Update(client);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(clientvm);
        }

        //
        // GET: /Client/Delete/5

        public ActionResult Delete(Guid id)
        {
            Client client = ClientBusiness.SingleOrDefault(id);
            ClientVM clientvm = new ClientVM();
            AutoMapper.Mapper.Map(client, clientvm);
            if (clientvm == null)
            {
                return HttpNotFound();
            }
            return View(clientvm);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ClientBusiness.DeleteClient(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}