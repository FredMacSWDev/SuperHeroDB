﻿using Microsoft.AspNet.Identity;
using SuperFriendsDB.Models.BiographyModels;
using SuperFriendsDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperFriendsDB.WebMVC.Controllers
{
    [Authorize]
    public class BiographyController : Controller
    {
        // GET: Biography
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BiographyService(userId);
            var model = service.GetBio();

            return View(model);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BiographyCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBioService();

            if (service.CreateBio(model))
            {
                TempData["SaveResult"] = "The biography has been added successfully!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Bio could not be created");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateBioService();
            var model = service.GetBioById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBioService();
            var detail = service.GetBioById(id);
            var model =
                new BiographyEdit
                {
                    BioId = detail.BioId,
                    FullName = detail.FullName,
                    AlterEgos = detail.AlterEgos,
                    PlaceOfBirth = detail.PlaceOfBirth,
                    FirstAppearance = detail.FirstAppearance,
                    Publisher = detail.Publisher,
                    Alignment = detail.Alignment
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BiographyEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BioId != id)
            {
                ModelState.AddModelError("", "ID Mismatch.  Please try again...");
                return View(model);
            }

            var service = CreateBioService();

            if (service.UpdateBio(model))
            {
                TempData["SaveResult"] = "Congratulations!  The biography information has been successfully updated!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The biography information could not be updated");

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateBioService();
            var model = service.GetBioById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBio(int id)
        {
            var svc = CreateBioService();
            svc.DeleteBio(id);
            TempData["SaveResult"] = "The biography was successfully deleted";
            return RedirectToAction("Index");
        }

        private BiographyService CreateBioService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BiographyService(userId);
            return service;
        }
    }
}