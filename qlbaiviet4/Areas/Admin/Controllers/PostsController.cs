using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Common;
using Models.EF;

namespace qlbaiviet4.Areas.Admin.Controllers
{
    public class PostsController : BaseController
    {
        private QLBaiVietDbContext db = new QLBaiVietDbContext();

        // GET: Admin/Posts
        [HasCredential(RoleId = "VIEW_POST")]
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts.ToList());
        }

        // GET: Admin/Posts/Details/5
        [HasCredential(RoleId = "VIEW_POST")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        [HasCredential(RoleId = "CREATE_POST")]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "CREATE_POST")]
        public ActionResult Create([Bind(Include = "PostId,Title,Pcontent,PostDate,Image,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", post.UserId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        [HasCredential(RoleId = "EDIT_POST")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", post.UserId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "EDIT_POST")]
        public ActionResult Edit([Bind(Include = "PostId,Title,Pcontent,PostDate,Image,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Email", post.UserId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        [HasCredential(RoleId = "DELETE_POST")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "DELETE_POST")]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
