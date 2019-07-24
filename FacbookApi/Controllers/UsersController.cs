using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FacbookApi.Models;
using System.Web.Helpers;
using static FacbookApi.Models.AccountsViewModels;

namespace FacbookApi.Controllers
{
    public class UsersController : ApiController
    {
        private FB db = new FB();

        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            var users = db.Users.Select(i => new { i.UserID, i.UserFirstName, i.UserLastName, i.UserMail, i.UserGender, i.UserDateOfBirth, i.UserProfilePicture });
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(User))]
        public IHttpActionResult Login(ExternalLoginViewModel model)
        {
            var email = new System.Net.Mail.MailAddress(model.Mail);
            var user = db.Users.Where(s => s.UserMail == email.Address && s.UserPassword == model.Password);
            if (user == null || user.Count() == 0)
                return NotFound();
            return Ok(user.First());
        }

        private User getUserFromRegisterViewModel(RegisterViewModel model)
        {
            return new User()
            {
                UserFirstName = model.UserFirstName,
                UserLastName = model.UserLastName,
                UserMail = model.UserMail,
                UserPassword = model.UserPassword,
                UserPhone = model.UserPhone,
                UserAddress = model.UserAddress,
                UserDateOfBirth = model.UserDateOfBirth,
                UserGender = model.UserGender,
                UserProfilePicture = model.UserProfilePicture
            };
        }

        // POST: api/Users
        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("Register")]
        public IHttpActionResult Registerion(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //User user = getUserFromRegisterViewModel(model);
            Console.WriteLine(user);

            user = db.Users.Add(user);
            db.SaveChanges();

            return Ok(user.UserID);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [ResponseType(typeof(User))]
        [Route("posts")]
        public IHttpActionResult GetPosts(int id)
        {
            var posts = db.Users.Where(i => i.UserID == id).Select(s => s.Posts);
            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [ResponseType(typeof(User))]
        [Route("GetUserByName")]
        public IHttpActionResult GetUserByName(string name)
        {
            var user = db.Users.Where(s => s.UserFirstName == name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        //use it as Change Password
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("EditPassword")]
        public IHttpActionResult EditPassword(EditPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.Find(model.ID);
            user.UserPassword = model.Password;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(model.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}