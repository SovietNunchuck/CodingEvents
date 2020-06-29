using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Event name must be between 3 and 20 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(500, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string ContactEmail { get; set; }

        public EventType Type { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Location { get; set; }

        [Range(0, 100000, ErrorMessage = "Value must be between 0 and 100,000")]
        public int NumberOfAttendees { get; set; }

        public List<SelectListItem> EventTypes { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(EventType.Conference.ToString(), ((int)EventType.Conference).ToString()),
            new SelectListItem(EventType.Meetup.ToString(), ((int)EventType.Meetup).ToString()),
            new SelectListItem(EventType.Workshop.ToString(), ((int)EventType.Workshop).ToString()),
            new SelectListItem(EventType.Social.ToString(), ((int)EventType.Social).ToString())
        };

    }
}
