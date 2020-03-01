using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Advertisement : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; }
    }
}