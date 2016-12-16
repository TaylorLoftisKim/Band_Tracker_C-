using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Tracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string venueName,int id = 0)
    {
      _id = id;
      _name = venueName;
    }
    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (idEquality && nameEquality);
      }
    }
    public int GetId()
    {
      return _id
    }
    public string GetName()
    {
      return _name;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenue = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);

        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if(rdr.Close != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName)", conn);

      SqlParameter nameParameter = new SqlParameter("@VenueName", this.GetName());

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
//Change this below//
    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter courseIdParameter = new SqlParameter("@VenueId", id.ToString());
      cmd.Parameters.Add(courseIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundCourseName = null;
      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundCourseId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }
  }
}
