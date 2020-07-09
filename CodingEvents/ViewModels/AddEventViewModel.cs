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

        [Required(ErrorMessage = "Required field")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Location { get; set; }

        [Range(0, 100000, ErrorMessage = "Value must be between 0 and 100,000")]
        public int NumberOfAttendees { get; set; }

        public bool IsTrue { get { return true; } }

        [Compare("IsTrue", ErrorMessage = "For the purposes of this exercise, this box must be checked.")]
        public bool ReservationRequired { get; set; }

        public AddEventViewModel(List<EventCategory> categories)
        {
            Categories = new List<SelectListItem>();

            foreach (EventCategory category  in categories)
            {
                Categories.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    }    
                );
            }
        }

        public AddEventViewModel() { }
    }
}
