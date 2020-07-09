using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using Microsoft.AspNetCore.Mvc;
using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers
{
    public class TagController : Controller
    {
        private EventDbContext context;

        public TagController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Tag> tags = context.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if (ModelState.IsValid)
            {
                context.Tags.Add(tag);
                context.SaveChanges();
                return Redirect("/Tag");
            }

            return View("Add", tag);
        }

        [HttpGet]
        public IActionResult AddEvent(int id)
        {
            Event theEvent = context.Events.Find(id);
            List<Tag> possibleTags = context.Tags.ToList();

            AddEventTagViewModel viewModel = new AddEventTagViewModel(theEvent, possibleTags);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddEvent(AddEventTagViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                int eventId = viewModel.EventId;
                int tagId = viewModel.TagId;

                EventTag eventTag = new EventTag
                {
                    EventId = eventId,
                    TagId = tagId
                };

                context.EventTags.Add(eventTag);
                context.SaveChanges();

                return Redirect("/Events/Detail/" + eventId);
            }

            return View(viewModel);
        }
    }
}
