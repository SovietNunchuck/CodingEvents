using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = context.Events
                .Include(x => x.Category)
                .ToList();

            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<EventCategory> categories = context.Categories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory theCategory = context.Categories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory,
                    Location = addEventViewModel.Location,
                    NumberOfAttendees = addEventViewModel.NumberOfAttendees,
                    ReservationRequired = addEventViewModel.ReservationRequired
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);

        }

        public IActionResult Delete()
        {
            ViewBag.events = context.Events.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }

            context.SaveChanges();

            return Redirect("/Events");
        }

        [HttpGet("/Events/Edit/{eventId}")]
        public IActionResult Edit(int eventId)
        {
            ViewBag.editingEvent = context.Events.Find(eventId);
            ViewBag.title = $"Edit Event {ViewBag.editingEvent.Name} (id={eventId})";

            return View();
        }

        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description, string contactEmail, string location, int numberOfAttendees)
        {
            Event revisedEvent = context.Events.Find(eventId);
            revisedEvent.Name = name;
            revisedEvent.Description = description;
            revisedEvent.ContactEmail = contactEmail;
            revisedEvent.Location = location;
            revisedEvent.NumberOfAttendees = numberOfAttendees;
            context.Events.Update(revisedEvent);
            context.SaveChanges();

            return Redirect("/Events");
        }

        // responds to route "/Events/Detail/X"
        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events
                .Include(x => x.Category)
                .Single(x => x.Id == id);

            List<EventTag> eventTags = context.EventTags
                .Where(et => et.EventId == id)
                .Include(et => et.Tag)
                .ToList();

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent, eventTags);
            return View(viewModel);
        }
    }
}
