using Xunit;
using System;
using System.Collections.Generic;

namespace Tracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<Band> allBands = new List<Band>{};
      List<Band> testList = new List<Band>{};
      Band newBand = new Band("ExampleBand");
      testList.Add(newBand);

      newBand.Save();
      allBands = Band.GetAll();

      Assert.Equal(testList, allBands);
    }

    [Fact]
    public void Test_GetVenuesAssociatedWithBand()
    {
      List<Venue> allVenues = new List<Venue>{};
      List<Venue> testVenues = new List<Venue>{};

      Venue newVenue = new Venue("Roseland");
      newVenue.Save();

      Band newBand = new Band("ExampleBand");
      newBand.Save();

      newBand.AddCourse(newVenue);
      allVenues = newBand.GetVenues();
      testVenues.Add(newVenue);

      Assert.Equal(testVenues, allVenues);
    }

    public void Dispose()
    {
      Band.DeleteAll();
    }
  }
}
