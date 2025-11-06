using DemoDB2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDB2.Controllers
{
    public class LoginUserController : Controller
    {
        DBUserEntities database = new DBUserEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(AdminUser _user)
        {
            var check = database.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else 
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.NameUser;
                Session["PasswordUser"] = _user.PasswordUser;
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                // Giả sử 'database' là một DbContext (hoặc tương đương) đã được định nghĩa ở đâu đó
                // Cần thay thế 'database' bằng tên DbContext thực tế của bạn
                var check_ID = database.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();

                if (check_ID == null) // chua co ID
                {
                    // Tắt ValidateOnSaveEnabled (có thể không cần thiết trong các phiên bản EF Core mới)
                    // Cần đảm bảo rằng bạn đã import thư viện cho 'database'
                    database.Configuration.ValidateOnSaveEnabled = false;

                    database.AdminUsers.Add(_user);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Lỗi chính tả trong code gốc: "exixts" -> "exists"
                    ViewBag.ErrorRegister = "This ID is exixts";
                    return View(_user); // Trả về View với model để giữ lại dữ liệu đã nhập
                }
            }

            // Nếu ModelState không hợp lệ (ví dụ: lỗi validation), sẽ return View(_user)
            return View(_user);
        }
        public ActionResult Logout()
        {
            Session.Abandon(); // Xóa tất cả dữ liệu trong session
            return RedirectToAction("Index", "LoginUser"); // Chuyển hướng về trang ��ăng nhập
        }
    }
}