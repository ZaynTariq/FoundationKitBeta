namespace FoundationKit.Authentication.Persistence.DataAccess.Base;

using Microsoft.EntityFrameworkCore;

public interface IRepository
{
    DbContext DbContext { get; set; }
}



