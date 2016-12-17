using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Tracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string bandName, int id = 0)
    {
      _id = id;
      _name = bandName;
    }
    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        return (idEquality && nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public static List<Band> GetAll()
		{
			List<Band> allBands = new List<Band>{};

			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);

        Band newBand = new Band(bandName, bandId);
        allBands.Add(newband);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allbands;
    }

    public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandsName);", conn);

			SqlParameter nameParameter = new SqlParameter("@BandsName", this.GetName());

			cmd.Parameters.Add(nameParameter);

			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				this._id = rdr.GetInt32(0);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
    }

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandsId;", conn);
      SqlParameter venueIdParameter = new SqlParameter("@BandsId", id.ToString());
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;
      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
      }
      Band foundBand = new Band(foundBandName, foundBandId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundBand;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venues_id, bands_id) VALUES (@VenueId, @BandsId);", conn);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandsId";
      bandIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bandIdParameter);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = newVenue.GetId();
      cmd.Parameters.Add(venueIdParameter);

      cmd.ExecuteNonQuery();

      if(conn!= null)
      {
        conn.Close();
      }
    }

    public List<Venue> GetVenue()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM venues JOIN venues_bands ON (venues_bands.venue_id = venues.id) JOIN bands ON (bands.id = venues_bands.band_id) WHERE band_id = @BandId;", conn);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bandIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> allVenues = new List<Venue> {};
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      return allVenues;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("Delete FROM bands; DELETE FROM venues_bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
