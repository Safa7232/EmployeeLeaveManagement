using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeLeaveManagement.ViewModels;
using EmployeeLeaveManagement.ServiceLayer;
using EmployeeLeaveManagement.CustomFilters;
using System.Net;
using System.Net.Mail;



namespace EmployeeLeaveManagement.Controllers
{
    public class AccountController : Controller
    {
        IEmployeeService employeeService;


        public AccountController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        // GET: Account

        public ActionResult Login()
        {
            employeeService = new EmployeeService();
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                EmployeeViewModel employeeViewModel = this.employeeService.GetEmploeesByEmailAndPassword(lvm.Email, lvm.Password);
                if (employeeViewModel != null)
                {
                    Session["CurrentUserID"] = employeeViewModel.EmployeeID;
                    Session["CurrentUserFirstName"]= employeeViewModel.FirstName + " " + employeeViewModel.LastName;
                    Session["CurrentUserFirstName"] = employeeViewModel.FirstName;
                    Session["CurrentUserLastName"] = employeeViewModel.LastName;
                    Session["CurrentUserEmail"] = employeeViewModel.Email;
                    Session["CurrentUserPassword"] = employeeViewModel.Password;
                    Session["CurrentUserRoleID"] = employeeViewModel.RoleID ;
                    Session["CurrentUserIsSpecialPermission"] = employeeViewModel.IsSpecialPermission;
                    Session["CurrentUserDesignation"] = employeeViewModel.Designation;
                    Session["CurrentUserProfilePhoto"] = employeeViewModel.ImageURL;

                    if (employeeViewModel != null)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }

        [UserAuthorizationFilter]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [UserAuthorizationFilter]
        public ActionResult EditProfile()
        {
            int EmpID = Convert.ToInt32(Session["CurrentUserID"]);
            EmployeeViewModel employeeViewModel = this.employeeService.GetEmployeesByEmployeeID(EmpID);
            EditEmployeeDetailsViewModel EditEmpDetail = new EditEmployeeDetailsViewModel() { FirstName = employeeViewModel.FirstName, LastName = employeeViewModel.LastName, Email = employeeViewModel.Email, ContactNumber = employeeViewModel.ContactNumber, EmployeeID=employeeViewModel.EmployeeID };
            return View(EditEmpDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]

        public ActionResult EditProfile(EditEmployeeDetailsViewModel EditEmpDetail)
        {
            
                var file = Request.Files[0];
                var imagebyte = new byte[file.ContentLength - 1];
                file.InputStream.Read(imagebyte, 0, file.ContentLength - 1);
                var base64string = Convert.ToBase64String(imagebyte, 0, imagebyte.Length);
                byte[] data = Convert.FromBase64String(base64string);
                EditEmpDetail.EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
                string folderPath = Server.MapPath("~/Images/");
                folderPath = folderPath + EditEmpDetail.EmployeeID + ".jpeg";
                string imagepath = EditEmpDetail.ImageURL = "/Images/" + EditEmpDetail.EmployeeID + ".jpeg";
                System.IO.File.WriteAllBytes(folderPath, data);
                Session["CurrentUserProfilePhoto"] = EditEmpDetail.ImageURL;

            

            if (ModelState.IsValid)
            {
                EditEmpDetail.EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
                this.employeeService.UpdateEmployeeDetails(EditEmpDetail);
                Session["CurrentUserFisrtName"] = EditEmpDetail.FirstName;
                Session["CurrentUserLastName"] = EditEmpDetail.LastName;
                Session["CurrentUserContactNumber"] = EditEmpDetail.ContactNumber;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(EditEmpDetail);
            }
        }

        [UserAuthorizationFilter]
        public ActionResult EditPassword()
        {
            int EmpId = Convert.ToInt32(Session["CurrentUserID"]);
            EmployeeViewModel employeeViewModel = this.employeeService.GetEmployeesByEmployeeID(EmpId);
            EditEmployeePasswordViewModel EditPass = new EditEmployeePasswordViewModel() { Email = employeeViewModel.Email, Password = "", ConfirmPassword = "", EmployeeID = employeeViewModel.EmployeeID };
            return View(EditPass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]

        public ActionResult EditPassword(EditEmployeePasswordViewModel EditPass)
        {
            if (ModelState.IsValid)
            {
                EditPass.EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
                this.employeeService.UpdateEmployeePassword(EditPass);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(EditPass);
            }
        }

        [UserAuthorizationFilter]
        public ActionResult MyProfile()
        {
            int EmpID = Convert.ToInt32(Session["CurrentUserID"]);
            EmployeeViewModel employeeViewModel = this.employeeService.GetEmployeesByEmployeeID(EmpID);
            Session["CurrentUserID"] = employeeViewModel.EmployeeID;
            Session["CurrentUserFisrtName"] = employeeViewModel.FirstName;
            Session["CurrentUserLastName"] = employeeViewModel.LastName;
            Session["CurrentUserContactNumber"] = employeeViewModel.ContactNumber;
            Session["CurrentUserDesignation"] = employeeViewModel.Designation;
            Session["CurrentUserEmail"] = employeeViewModel.Email ;
            Session["CurrentUserImage"] = employeeViewModel.ImageURL;
            return View();
        }

        [HRAuthorizationFilter]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [HRAuthorizationFilter]
        public ActionResult AddEmployee(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                int EmployeeID = this.employeeService.InsertEmployee(registerViewModel);
                Session["CurrentUserID"] = EmployeeID;
                Session["CurrentUserFirstName"] = registerViewModel.FirstName;
                Session["CurrentUserLastName"] = registerViewModel.LastName;
                Session["CurrentUserEmail"] = registerViewModel.Email;
                Session["CurrentUserPassword"] = registerViewModel.Password;
                Session["CurrentUserContactNumber"] = registerViewModel.ContactNumber;
                

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }

        public ActionResult Employees()
        {
            List<EmployeeViewModel> employees = this.employeeService.GetEmployee();
            return View(employees);
        }

        [HttpPatch]
        public ActionResult Employees(EmployeeViewModel employeeViewModel)
        {
            List<EmployeeViewModel> employees = this.employeeService.GetEmployee();
            return View(employees);

        }

        [HRAuthorizationFilter]
        public ActionResult ChangeProfile(int id)
        {

            
            EmployeeViewModel employeeViewModel = this.employeeService.GetEmployeesByEmployeeID(id);
            EditEmployeeDetailsViewModel EditEmpDetail = new EditEmployeeDetailsViewModel() { FirstName = employeeViewModel.FirstName, LastName = employeeViewModel.LastName, Email = employeeViewModel.Email, ContactNumber = employeeViewModel.ContactNumber, EmployeeID = employeeViewModel.EmployeeID };
            return View(EditEmpDetail);
        }


        [HttpPost]
        [HRAuthorizationFilter]
        public ActionResult ChangeProfile(EditEmployeeDetailsViewModel EditEmpDetail)
        {

            if (ModelState.IsValid)
            {
                this.employeeService.UpdateEmployeeDetails(EditEmpDetail);
                return RedirectToAction("Employees", "Account"); 
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(EditEmpDetail);
            }
        }

        [HRAuthorizationFilter]
        public ActionResult DeleteEmployee(int id)
        {

            this.employeeService.DeleteUser(id);
            return RedirectToAction("Employees", "Account");
        }

        public ActionResult ViewProfile(int id)
        {
            EmployeeViewModel employeeViewModel = this.employeeService.GetEmployeesByEmployeeID(id);
            return View(employeeViewModel);
        }

        public ActionResult Search(string str, int RoleID)
        {
            if (RoleID == 0)
            {
                List<EmployeeViewModel> employees = this.employeeService.GetEmployee().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower())).ToList();
                ViewBag.str = str;
                return View(employees);
            }
            else if (RoleID == 1)
            {
                List<EmployeeViewModel> employees = this.employeeService.GetEmployee().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower()) && (temp.RoleID == 1)).ToList();
                ViewBag.str = str;
                return View(employees);
            }

            else if (RoleID == 2)
            {
                List<EmployeeViewModel> employees = this.employeeService.GetEmployee().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower()) && (temp.RoleID == 2)).ToList();
                ViewBag.str = str;
                return View(employees);
            }
            else if (RoleID == 3)
            {
                List<EmployeeViewModel> employees = this.employeeService.GetEmployee().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower()) && (temp.RoleID == 3)).ToList();
                ViewBag.str = str;
                return View(employees);
            }

            else
            {
                List<EmployeeViewModel> employees = this.employeeService.GetEmployee().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower()) && (temp.RoleID == 4)).ToList();
                ViewBag.str = str;
                return View(employees);
            }
            
        }
    }
}
    