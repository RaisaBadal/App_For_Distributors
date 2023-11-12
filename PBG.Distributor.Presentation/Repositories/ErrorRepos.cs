using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PBG.Distributor.Core.Interface;
using PashaBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PashaBankApp.ResponseAndRequest;

namespace PBG.Distributor.Presentation.Repositories
{
    public class ErrorRepos: IErrorRepos
    {
        private readonly DbRaisa dbraisa;
        public ErrorRepos(DbRaisa dbraisa)
        {
            this.dbraisa = dbraisa;
        }

        #region Action
        public void Action(string mesage, ErrorTypeEnum type)
        {
            //funqcia romelic chawers bazashi ra errori moxda
            if (mesage != null)
            {
                dbraisa.errors.Add(new Error()
                {
                    ErrorType = type,
                    TimeofOccured = DateTime.Now,
                    Text = mesage
                });
                dbraisa.SaveChanges();
            }
        }
        #endregion

        #region GetAllErrors
        public List<Error> GetAllErrors()
        {
            //error cxrilshi chawerili yvela chanaweris dabruneba
            return dbraisa.errors.ToList();
        }
        #endregion

        #region GetAllErrorsBetweenDate
        public List<Error> GetAllErrorsBetweenDate(ErrorBetweenDateRequest dateresp)
        {
            //mocemul drois shualedshi momxdari errorebis wamogheba
            return dbraisa.errors.Where(i => i.TimeofOccured >= dateresp.start && i.TimeofOccured <= dateresp.end).ToList();
        }

        #endregion



    }
}
