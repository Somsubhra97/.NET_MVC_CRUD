using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_MVC.Service;
using CRUD_MVC.Models;

namespace CRUD_MVC.Controllers
{
   
    public class EmployeeController : Controller
    {
        private EmployeeServices _emp=new EmployeeServices();

        // GET: Employee
        public ActionResult Index()
        {
           var x= _emp.GetEmployeeData();
            return View(x);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var x = _emp.GetEmployeeDataById(id);
             
            return View(x);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.MyList =_emp.GetDesignationData();
           
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public void Create(Employee model)
        {
            _emp.AddEmployeeData(model);           
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var x = _emp.GetEmployeeDataById(id);
            ViewBag.MyList = _emp.GetDesignationData();

            return View(x);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public void Edit( Employee collection)
        {
            _emp.Update(collection);
        }

        // POST: Employee/Delete/5       
        public ActionResult Delete(int id)
        {
            _emp.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
