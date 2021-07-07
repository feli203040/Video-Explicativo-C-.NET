using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoExplicativo.Models;

namespace VideoExplicativo.Controllers
{
    public class UsuariosController : Controller
    {
        private bdVideoExplicativoEntities db = new bdVideoExplicativoEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.tblUsuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Usuario")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tblUsuario.Add(tblUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblUsuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Usuario")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblUsuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            db.tblUsuario.Remove(tblUsuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
