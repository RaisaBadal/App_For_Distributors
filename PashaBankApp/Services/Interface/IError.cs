using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;

namespace PashaBankApp.Services.Interface
{
    public interface IError
    {
        List<Error> GetAllErrors();

        List<Error> GetAllErrorsBetweenDate(DateTime start, DateTime end);
    }
}
