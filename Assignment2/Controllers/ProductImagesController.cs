using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Assignment2.Models;
using Assignment2.OSDB;

namespace Assignment2.Controllers
{
    public class ProductImagesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: ProductImages
        public ActionResult Index()
        {
            return View(db.ProductImages.ToList());
        }

        // GET: ProductImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImages/Create
        public ActionResult Upload(HttpPostedFileBase file)
        {
            //check the user has entered a file
            if (file != null)
            {
                if (ValidateFile(file))
                {
                    try
                    {
                        SaveFileToDisk(file);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("FileName", "Sorry an error occurred saving the file to disk, please try again");
                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg and less than 2MB in size");
                }
            }
            else
            {
                ModelState.AddModelError("FileName", "Please choose a file");
            }
            if (ModelState.IsValid)
            {
                db.ProductImages.Add(new ProductImage { FileName = file.FileName });
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException.InnerException as SqlException;
                    if (innerException != null && innerException.Number == 2601)
                    {
                        ModelState.AddModelError("FileName", "The file" + file.FileName + " already exists in the system. Please delete it and try again if you wish to re-add it");
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Sorry an error occurred saving the file to disk, please try again");
                    }
                    return View();
                }
                return RedirectToAction("Index");
            }

            return View();
        }


        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {

            /*
             * 
             *      VALIDATION & UPLOADING
             *  
             */

            bool allValid = true;
            string inValidFiles = "";
            db.Database.Log = sql => Trace.WriteLine(sql);

            //no files or too many files?
            if (files[0] == null)
            {
                ModelState.AddModelError("FileName", "Please choose at least one file.");
            }

            else if (files.Length > Constants.MAX_FILE_UPLOAD)
            {
                ModelState.AddModelError("FileName", "You cannot upload more than ten files.");
            }
            else
            {
                //check for invalid files: max 2mb or invalid format
                foreach (var file in files)
                {
                    if (!ValidateFile(file))
                    {
                        allValid = false;
                        inValidFiles += ", " + file.FileName;
                    }
                }

                //are all the files valid? if so, upload
                if (!allValid)
                {
                    ModelState.AddModelError("FifleName", "All files must be gif, png, jpeg, or jpg and less than 2MB in size. The following files " + inValidFiles + " are not valid");
                }
                else
                {
                    foreach (var file in files)
                    {
                        try
                        {
                            SaveFileToDisk(file);
                        }
                        catch
                        {
                            ModelState.AddModelError("FileName", "Sorry, there was an error during uploading the file to the server, please try again.");
                        }
                    }
                }
            }


            /*
             * 
             *      UPDATING THE DATABASE
             *      if & only if there are no modelstate errors
             *
             */
            if (ModelState.IsValid)
            {
                bool duplicates = false;
                bool otherDbError = false;
                string duplicateFiles = "";

                //update database per file to determine if there are duplicates
                foreach (var file in files)
                {
                    var productToAdd = new ProductImage { FileName = file.FileName };
                    try
                    {
                        db.ProductImages.Add(productToAdd);
                        db.SaveChanges();

                    }
                    catch (DbUpdateException ex) //FileName = NO DUPLICATES = db will throw errors on duplicates
                    {
                        SqlException error = ex.InnerException.InnerException as SqlException;
                        if (error != null && error.Number == 2601)
                        {
                            duplicateFiles += ", " + file.FileName;
                            duplicates = true;
                            db.Entry(productToAdd).State = EntityState.Detached;
                        }
                        else
                        {
                            otherDbError = true;
                        }
                    }
                }
                if (duplicates)
                {
                    ModelState.AddModelError("FileName", "All files upload except the files"
                        + duplicateFiles
                        + ", which already exist in the system. Please delete them and try again if you wish to re-add them"
                    );
                    return View();
                }
                else if (otherDbError)
                {
                    ModelState.AddModelError("FileName", "Sorry an error has occured while saving to the database. Please try again.");
                    return View();

                }
                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: ProductImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileName")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //find the image being deleted
            ProductImage productImage = db.ProductImages.Find(id);
            Product product = db.Products.Find(id);
            var mappings = product.ProductImageMappings.Where(pim => pim.PImageID == id);

            foreach (var mapping in mappings)
            {
                //find all mappings for any product containing this image
                var mappingsToUpdate = db.ProductImageMappings.Where(pim => pim.PID ==
                mapping.PID);

                //for each image in each product change its imagenumber to one lower if it is higher
                //than the current image

                foreach (var mappingToUpdate in mappingsToUpdate)
                {
                    if (mappingToUpdate.ImageNumber > mapping.ImageNumber)
                    {
                        mappingToUpdate.ImageNumber--;
                        
                    }
                }

                //remove FK values!
                mapping.PImageID = null;

            }
            System.IO.File.Delete(Request.MapPath(Constants.ProductImagePath + productImage.FileName));
            System.IO.File.Delete(Request.MapPath(Constants.ProductThumbnailPath + productImage.FileName));

            db.ProductImages.Remove(productImage);
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

        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }

        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 190)
            {
                img.Resize(190, img.Height);
            }
            img.Save(Constants.ProductImagePath + file.FileName);
            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }
            img.Save(Constants.ProductThumbnailPath + file.FileName);
        }

    }
}
