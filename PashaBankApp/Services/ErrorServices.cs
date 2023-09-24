using Microsoft.EntityFrameworkCore.Query.Internal;
using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.Services.Interface;

namespace PashaBankApp.Services
{
    public class ErrorServices:IError
    {
        private readonly DbRaisa dbraisa;
        public ErrorServices(DbRaisa dbraisa)
        {
            this.dbraisa = dbraisa;
        }

        #region Action
        public void Action(string mesage,ErrorTypeEnum type)
        {
            if(mesage!=null)
            {
                dbraisa.errors.Add(new Models.Error()
                {
                    ErrorType= type,
                    TimeofOccured=DateTime.Now,
                    Text= mesage
                });
                dbraisa.SaveChanges();
            }
        }
        #endregion

        #region GetAllErrors
        public List<Error> GetAllErrors()
        {
            return dbraisa.errors.ToList();
        }
        #endregion

        #region GetAllErrorsBetweenDate
        public List<Error> GetAllErrorsBetweenDate(DateTime start, DateTime end)
        {
            return dbraisa.errors.Where(i => i.TimeofOccured >= start && i.TimeofOccured <= end).ToList();
        }

        #endregion



    }
}
