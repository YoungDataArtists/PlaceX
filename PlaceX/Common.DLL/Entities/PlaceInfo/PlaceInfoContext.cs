using Common.DLL.Entities.PlaceInfo;
using System.Data.Entity;

public class PlaceInfoContext : DbContext
{
    public PlaceInfoContext() : base("PlaceInfoDb")
    { }
    public DbSet<Review> Reviews { get; set; }
}