using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Advertisement : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Company { get; set; }
        public string Sector { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string WorkingTime { get; set; }
        public string AdsDetail { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string RelatedPerson { get; set; }
    }
}