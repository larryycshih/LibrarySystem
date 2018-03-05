using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Controllers
{
    public class LibrariesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Libraries
        public ActionResult Index()
        {
            return View(db.Libraries.ToList());
        }

        // GET: Libraries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library library = db.Libraries.Find(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // GET: Libraries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Libraries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MediaType,MediaLanguage,MediaCategory,Name,Author,Publisher,DatePurchased,Price,ISBN_ISSN,Comment")] Library library)
        {
            if (ModelState.IsValid)
            {
                String referenceID = "";
                if (library.MediaType == MediaTypes.Book) referenceID += "B";
                else if (library.MediaType == MediaTypes.CD) referenceID += "C";
                else if (library.MediaType == MediaTypes.VCD) referenceID += "D";
                else if (library.MediaType == MediaTypes.DVD) referenceID += "E";
                else if (library.MediaType == MediaTypes.Journals) referenceID += "J";
                else if (library.MediaType == MediaTypes.MP3) referenceID += "M";
                else if (library.MediaType == MediaTypes.SongBooks) referenceID += "S";
                else if (library.MediaType == MediaTypes.Tape) referenceID += "T";
                else if (library.MediaType == MediaTypes.Video) referenceID += "V";
                else /*if (library.MediaType == MediaTypes.Others)*/ referenceID += "X";

                if (library.MediaLanguage == MediaLanguage.Cantonese) referenceID += "C";
                else if (library.MediaLanguage == MediaLanguage.English) referenceID += "E";
                else if (library.MediaLanguage == MediaLanguage.Mandarin) referenceID += "M";
                else if (library.MediaLanguage == MediaLanguage.Taiwaness) referenceID += "T";
                else /*if (library.MediaLanguage == MediaLanguage.Others)*/ referenceID += "X";

                if (library.MediaCategory == MediaCategory.解經) referenceID += "A";
                else if (library.MediaCategory == MediaCategory.靈修) referenceID += "B";
                else if (library.MediaCategory == MediaCategory.神學) referenceID += "C";
                else if (library.MediaCategory == MediaCategory.屬靈) referenceID += "D";
                else if (library.MediaCategory == MediaCategory.禱告) referenceID += "E";
                else if (library.MediaCategory == MediaCategory.心靈) referenceID += "F";
                else if (library.MediaCategory == MediaCategory.生活) referenceID += "G";
                else if (library.MediaCategory == MediaCategory.故事) referenceID += "H";
                else /*if (library.MediaCategory == MediaCategory.解經)*/ referenceID += "X";


                library.ReferenceID = referenceID;

                db.Libraries.Add(library);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(library);
        }

        // GET: Libraries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library library = db.Libraries.Find(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // POST: Libraries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MediaType,MediaLanguage,MediaCategory,Name,Author,Publisher,DatePurchased,Price,ISBN_ISSN,Comment")] Library library)
        {
            if (ModelState.IsValid)
            {
                db.Entry(library).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(library);
        }

        // GET: Libraries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library library = db.Libraries.Find(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Library library = db.Libraries.Find(id);
            db.Libraries.Remove(library);
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

        public ActionResult importItems(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                System.IO.StreamReader input = new System.IO.StreamReader(postedFile.InputStream);
                String result = "";
                input.ReadLine(); //skip the first line
                while (!input.EndOfStream)
                {
                    String[] a = input.ReadLine().Split(',');
                    if (a.Length == 8) // check the corret length
                    {
                        Library entry = new Library();
                        // analysis of the input
                        char c = a[0].ToArray()[0];
                        if (c.Equals('B')) entry.MediaType = MediaTypes.Book;
                        else if (c.Equals('C')) entry.MediaType = MediaTypes.CD;
                        else if (c.Equals('D')) entry.MediaType = MediaTypes.VCD;
                        else if (c.Equals('E')) entry.MediaType = MediaTypes.DVD;
                        else if (c.Equals('J')) entry.MediaType = MediaTypes.Journals;
                        else if (c.Equals('M')) entry.MediaType = MediaTypes.MP3;
                        else if (c.Equals('S')) entry.MediaType = MediaTypes.SongBooks;
                        else if (c.Equals('T')) entry.MediaType = MediaTypes.Tape;
                        else if (c.Equals('V')) entry.MediaType = MediaTypes.Video;
                        else entry.MediaType = MediaTypes.Others;

                        char d = a[0].ToArray()[1];
                        if (d.Equals('C')) entry.MediaLanguage = MediaLanguage.Cantonese;
                        else if (d.Equals('E')) entry.MediaLanguage = MediaLanguage.English;
                        else if (d.Equals('M')) entry.MediaLanguage = MediaLanguage.Mandarin;
                        else if (d.Equals('T')) entry.MediaLanguage = MediaLanguage.Taiwaness;
                        else entry.MediaLanguage = MediaLanguage.Others;

                        char e = a[0].ToArray()[2];
                        if (e.Equals('A')) entry.MediaCategory = MediaCategory.解經;
                        else if (e.Equals('B')) entry.MediaCategory = MediaCategory.靈修;
                        else if (e.Equals('C')) entry.MediaCategory = MediaCategory.神學;
                        else if (e.Equals('D')) entry.MediaCategory = MediaCategory.屬靈;
                        else if (e.Equals('E')) entry.MediaCategory = MediaCategory.禱告;
                        else if (e.Equals('F')) entry.MediaCategory = MediaCategory.心靈;
                        else if (e.Equals('G')) entry.MediaCategory = MediaCategory.生活;
                        else if (e.Equals('H')) entry.MediaCategory = MediaCategory.故事;
                        else entry.MediaCategory = MediaCategory.其他種類;

                        String title = a[1];
                        String author = a[2];
                        String publisher = a[3];

                        DateTime purchaseDate = DateTime.UtcNow;
                        try
                        {
                            purchaseDate = DateTime.Parse(a[4]);
                        }
                        catch (Exception) {
                       }

                        Double price;
                        if (a[5] != "") price = Double.Parse(a[5]);
                        else  price = 0;
                        String ISBN = a[6];
                        String comment =a[7];

                        entry.Name = title;
                        entry.Author = author;
                        entry.Publisher = publisher;
                        entry.DatePurchased = purchaseDate;
                        entry.Price = price;
                        entry.ISBN_ISSN = ISBN;
                        entry.Comment = comment;
                        entry.ReferenceID = a[0];

                        db.Libraries.Add(entry);
                        db.SaveChanges();

                    }


                }
                ViewBag.Result = result;

            }

            return View();
        }

    }
}
