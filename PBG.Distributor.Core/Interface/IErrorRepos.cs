using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBG.Distributor.Core.Interface
{
    public interface IErrorRepos
    {
        List<Error> GetAllErrors();

        List<Error> GetAllErrorsBetweenDate(ErrorBetweenDateRequest dateresp);
        void Action(string mesage, ErrorTypeEnum type);
    }
}
