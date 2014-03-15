using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Aspnet5Demo.Models;
using Microsoft.AspNet.Identity;

namespace Aspnet5Demo.Controllers
{

    [Authorize]
    public class EventsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Events
        public async Task<IEnumerable<UserEvent>> GetEvents()
        {
            var userId = User.Identity.GetUserId();
            var events = await db.Events.Include(e => e.Users)
                                .Select(e => new UserEvent
                                {
                                    Id = e.Id,
                                    Topic = e.Topic,
                                    Speaker = e.Speaker,
                                    Date = e.Date,
                                    Registered = e.Users.Any(u => u.Id == userId),
                                    UserCount = e.Users.Count()
                                })
                                .ToListAsync();
            return events;
        }

        // GET api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        [HttpPost]
        [Route("api/events/register")]
        public Task Register([FromBody]int eventId)
        {
            return RegisterHelper(eventId, true);
        }

        [HttpPost]
        [Route("api/events/unregister")]
        public Task Unregister([FromBody]int eventId)
        {
            return RegisterHelper(eventId, false);
        }

        private async Task RegisterHelper(int eventId, bool register)
        {
            var userId = User.Identity.GetUserId();
            var user = await db.Users.Include(u => u.Events).SingleAsync(u => u.Id == userId);
            var @event = db.Events.Find(eventId);

            if (register)
            {
                user.Events.Add(@event);
            }
            else
            {
                user.Events.Remove(@event);
            }


            if (await db.SaveChangesAsync() == 0)
            {
                InternalServerError();
            }
            else
            {
                var newUserCount = await db.Events
                                            .Where(e => e.Id == @event.Id)
                                            .Select(e => e.Users.Count())
                                            .SingleAsync();

                Aspnet5Demo.Hubs.EventsHub.SendEventUpdate(@event.Id, newUserCount);
            }

        }

        [HttpGet]
        [Route("api/events/{id:int}/users")]
        public async Task<IEnumerable<User>> GetUsersByEventId(int id)
        {
            var @event = await db.Events.Include(e => e.Users).SingleAsync(e => e.Id == id);
            return @event.Users.Select(u => new User { Id = u.Id, UserName = u.UserName });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    public class UserEvent
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Speaker { get; set; }
        public DateTime Date { get; set; }
        public bool Registered { get; set; }
        public int UserCount { get; set; }
    }
}