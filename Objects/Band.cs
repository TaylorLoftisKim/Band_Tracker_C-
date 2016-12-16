using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace tracker
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
        Band newStudent = (Band) otherBand;
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
  }
}
