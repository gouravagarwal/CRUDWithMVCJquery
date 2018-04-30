using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();

        // GET: Employee
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            return View(employees);
        }


        //POST: Employee
        [Route("Create")]
        [HttpPost]
        public JsonResult Create(Employee emp)
        {
            if (emp != null && ModelState.IsValid)
            {
                db.Employees.Add(emp);
                db.SaveChanges();

                return Json("true");
            }
            return Json("false");
        }

        //GET: Employee
        [Route("Detail/{id}")]
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            var employee = db.Employees.FirstOrDefault(x => x.ID == id);
            if (employee != null)
            {
                return Json(employee, JsonRequestBehavior.AllowGet);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //DELETE: Employee
        [Route("Delete/{id:int}")]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var emp = db.Employees.FirstOrDefault(x => x.ID == id);
                if (emp != null)
                {
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    return Json("true");
                }
                else
                    return Json("false");
            }
            return Json("false");
        }

        //Update: Employee
        [Route("Update")]
        [HttpPost]
        public JsonResult Update(Employee emp)
        {
            if (emp != null && ModelState.IsValid)
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();

                return Json("true");
            }
            return Json("false");
        }

    }
}