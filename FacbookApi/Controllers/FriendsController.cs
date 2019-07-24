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

namespace FacbookApi.Controllers
{
    public class FriendsController : ApiController
    {
        private FB db = new FB();

        // GET: api/Friends
        public IQueryable<Friend> GetFriends()
        {
            return db.Friends;
        }

        [HttpPost]
        [Route("ToggleRequest")]
        public IHttpActionResult ToggleRequest(Friend friendRequest)
        {
            var request = db.Friends.SingleOrDefault(r => 
            (r.FriendRecevierID == friendRequest.FriendRecevierID && r.FriendSenderID == friendRequest.FriendSenderID)
            || (r.FriendRecevierID == friendRequest.FriendSenderID && r.FriendSenderID == friendRequest.FriendRecevierID));

            if (request != null)
            {
                request.FriendState = !request.FriendState;

                db.Entry(request).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        return NotFound();
                }
            } else
            {
                db.Friends.Add(friendRequest);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // GET: api/Friends/5
        [ResponseType(typeof(Friend))]
        public IHttpActionResult GetFriend(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                return NotFound();
            }

            return Ok(friend);
        }

        // PUT: api/Friends/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFriend(int id, Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friend.FriendSenderID)
            {
                return BadRequest();
            }

            db.Entry(friend).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // POST: api/Friends
        [ResponseType(typeof(Friend))]
        public IHttpActionResult PostFriend(Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Friends.Add(friend);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FriendExists(friend.FriendSenderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = friend.FriendSenderID }, friend);
        }

        // DELETE: api/Friends/5
        [ResponseType(typeof(Friend))]
        public IHttpActionResult DeleteFriend(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                return NotFound();
            }

            db.Friends.Remove(friend);
            db.SaveChanges();

            return Ok(friend);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendExists(int id)
        {
            return db.Friends.Count(e => e.FriendSenderID == id) > 0;
        }
    }
}