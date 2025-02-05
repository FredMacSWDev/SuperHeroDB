﻿using Microsoft.AspNet.Identity;
using SuperFriendsDB.Models.ConnectionModels;
using SuperFriendsDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperFriendsDB.WebMVC.Controllers
{
    [Authorize]
    public class ConnectionController : Controller
    {
        // GET: Connection
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ConnectionService(userId);
            var model = service.GetConnections();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConnectionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateConnectionService();

            if (service.CreateConnection(model))
            {
                TempData["SaveResult"] = "Your connections have been added successfully!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The connections could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateConnectionService();
            var model = service.GetConnectionsById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateConnectionService();
            var detail = service.GetConnectionsById(id);
            var model =
                new ConnectionEdit
                {
                    ConnectionsId = detail.ConnectionsId,
                    CharacterId = detail.CharacterId,
                    GroupAffiliation = detail.GroupAffiliation,
                    Relatives = detail.Relatives
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ConnectionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ConnectionsId != id)
            {
                ModelState.AddModelError("", "ID Mismatch.  Please try again...");
                return View(model);
            }

            var service = CreateConnectionService();

            if (service.UpdateConnections(model))
            {
                TempData["SaveResult"] = "Congratulations!  The connections have been successfully updated!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry...the connections could not be updated");

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateConnectionService();
            var model = service.GetConnectionsById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStats(int id)
        {
            var svc = CreateConnectionService();
            svc.DeleteConnections(id);
            TempData["SaveResult"] = "Powerstats were successfully deleted";
            return RedirectToAction("Index");
        }
        private ConnectionService CreateConnectionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ConnectionService(userId);
            return service;
        }
    }
}