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
    public class BonosController : Controller
    {
        private bdVideoExplicativoEntities db = new bdVideoExplicativoEntities();

        // GET: Bonos
        public ActionResult Index()
        {
            var tblBonos = db.tblBonos.Include(t => t.tblUsuario);
            return View(tblBonos.ToList());
        }

        // GET: Bonos/Details/5
        [HttpPost]
        public ActionResult Index(DateTime desde, DateTime hasta)
        {
            var tblBonos = db.tblBonos.OrderBy(m => m.IdUsuario);
            int totalPagar = 0;
            foreach (var bono in tblBonos)
            {
                if(bono.Fecha > desde && bono.Fecha < hasta)
                {
                    totalPagar = totalPagar + bono.Bono;
                }
            }
            return RedirectToAction("CalculoBono", new { aPagar = totalPagar });
        }

        public ActionResult CalculoBono(int aPagar)
        {
            ViewBag.TotalAPagar = aPagar;
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBonos tblBonos = db.tblBonos.Find(id);
            if (tblBonos == null)
            {
                return HttpNotFound();
            }
            return View(tblBonos);
        }

        // GET: Bonos/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.tblUsuario, "IdUsuario", "Usuario");
            return View();
        }

        // POST: Bonos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBono,Departamento,Fecha,Bono,Motivo,IdUsuario")] tblBonos tblBonos)
        {
            if (ModelState.IsValid)
            {
                db.tblBonos.Add(tblBonos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.tblUsuario, "IdUsuario", "Usuario", tblBonos.IdUsuario);
            return View(tblBonos);
        }

        // GET: Bonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBonos tblBonos = db.tblBonos.Find(id);
            if (tblBonos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.tblUsuario, "IdUsuario", "Usuario", tblBonos.IdUsuario);
            return View(tblBonos);
        }

        // POST: Bonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBono,Departamento,Fecha,Bono,Motivo,IdUsuario")] tblBonos tblBonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.tblUsuario, "IdUsuario", "Usuario", tblBonos.IdUsuario);
            return View(tblBonos);
        }

        // GET: Bonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBonos tblBonos = db.tblBonos.Find(id);
            if (tblBonos == null)
            {
                return HttpNotFound();
            }
            return View(tblBonos);
        }

        // POST: Bonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBonos tblBonos = db.tblBonos.Find(id);
            db.tblBonos.Remove(tblBonos);
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
