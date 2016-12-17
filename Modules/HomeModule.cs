using Nancy;
using System;
using System.Collections.Generic;

namespace Tracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View["index.cshtml"];
      };
      Get["/add-new-band"] = _ =>
      {
        return View["add-new-band.cshtml"];
      };
      Post["/added-band"] = _ =>
      {
        string bandName = Request.Form["band-name"];
        Band newBand = new Band(bandName);
        newBand.Save();
        return View["added-band.cshtml", bandName];
      };
      Get["/add-new-venue"] = _ =>
      {
        return View["add-new-venue.cshtml"];
      };
      Post["/added-venue"] = _ =>
      {
        string venueName = Request.Form["venue-name"];
        Venue newVenue = new Venue(venueName);
        newVenue.Save();
        return View["added-venue.cshtml", newVenue];
      };
      Get["/view-all-venues"] = _ =>
      {
        List<Venue> allVenues = new List<Venue>{};
        allVenues = Venue.GetAll();
        return View["view-all-venues.cshtml", allVenues];
      };
      Get["/view-all-bands"] = _ =>
      {
        List<Band> allBands = new List<Band>{};
        allBands = Band.GetAll();
        return View["view-all-bands.cshtml", allBands];
      };
      Get["/venue/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> VenueBands = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("venue", selectedVenue);
        model.Add("VenueBands", VenueBands);
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Get["/band/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> VenueBands = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("VenueBands", VenueBands);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };
      Post["/band/add_venue"] = _ =>
      {
        Band band = Band.Find(Request.Form["band-id"]);
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        band.AddVenue(venue);
        List<Band> allBands = new List<Band>{};
        allBands = Band.GetAll();
        return View["view-all-bands.cshtml", allBands];
      };
      Post["/venue/add_band"] = _ =>
      {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        venue.AddBand(band);
        List<Venue> allVenues = new List<Venue>{};
        allVenues = Venue.GetAll();
        return View["view-all-venues.cshtml", allVenues];
      };
    }
  }
}
