using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models
{
    public class Resource
    {
        public string ResourceType { get; set; }
        public string Id { get; set; }
        public Meta Meta { get; set; }
        public List<Identifier> Identifier { get; set; }
        public string Name { get; set; }
        public List<Telecom> Telecom { get; set; }
        public List<Address> Address { get; set; }
        public List<Extension> Extension { get; set; }
        public Schedule Schedule { get; set; }
        public string Status { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public List<Actor> Actor { get; set; }
        public PlanningHorizon PlanningHorizon { get; set; }
        public string Comment { get; set; }
    }
}