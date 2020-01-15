using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models
{
    public class Meta
{
    public DateTime LastUpdated { get; set; }
    public List<string> Profile { get; set; }
}

public class Link
{
    public string Relation { get; set; }
    public string Url { get; set; }
}

public class Meta2
{
    public string VersionId { get; set; }
    public DateTime LastUpdated { get; set; }
    public List<string> Profile { get; set; }
}

public class Identifier
{
    public string System { get; set; }
    public string Value { get; set; }
}

public class Telecom
{
    public string System { get; set; }
    public string Value { get; set; }
    public string Use { get; set; }
}

public class Address
{
    public string Use { get; set; }
    public List<string> Line { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostalCode { get; set; }
}

public class Coding
{
    public string System { get; set; }
    public string Code { get; set; }
    public string Display { get; set; }
}

public class ValueCodeableConcept
{
    public List<Coding> Coding { get; set; }
}

public class Extension
{
    public string Url { get; set; }
    public string ValueCode { get; set; }
    public ValueCodeableConcept ValueCodeableConcept { get; set; }
}

public class Schedule
{
    public string Reference { get; set; }
}

public class Actor
{
    public string Reference { get; set; }
}

public class PlanningHorizon
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class Resource
{
    public string ResourceType { get; set; }
    public string Id { get; set; }
    public Meta2 Meta { get; set; }
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

public class Entry
{
    public Resource Resource { get; set; }
}

public class SlotResponse
{
    public string ResourceType { get; set; }
    public string Id { get; set; }
    public Meta Meta { get; set; }
    public string Type { get; set; }
    public List<Link> Link { get; set; }
    public List<Entry> Entry { get; set; }
}
}