using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.IdentityModel.Tokens;
using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services.Interface;
using PashaBankApp.Validation.Regexi;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBankApp.Services
{
    public class DistributorServices:IDistributor
    {
        public readonly DbRaisa dbraisa;
        private readonly ErrorServices error;
        private readonly LogServices log;

        public DistributorServices(DbRaisa db)
        {
           dbraisa= db;
            error = new ErrorServices(db);
            log= new LogServices(db);
        }

        #region InsertDistributor
        public bool InsertDistributor(InsertDistributorRequest req)
        {
            using (var trans = dbraisa.Database.BeginTransaction())
            {
                try
                {
                    if (!RegexForValidate.NameIsMatch(req.distributorName) || !RegexForValidate.SurnameIsMatch(req.LastName) ||
                        !RegexForValidate.AddressIsMatch(req.addressInfo))
                        return false;
                    if((int)req.contactType==2 && !RegexForValidate.PhoneIsMatch(req.contactInformation)) return false;
                    if((int)req.contactType==3 && !RegexForValidate.EmailIsMatch(req.contactInformation)) return false;
                    if (req.dateofIssue > DateTime.Now || req.expireDate < DateTime.Now||req.birthdate>DateTime.Now||req.birthdate>new DateTime(2002,01,01)) return false;
                    

                    int addressID = 0;
                    int contactId = 0;
                    int personalinfoID = 0;
                    int inventerID = 0;

                        var newAddress = new Models.Address { AddressType = req.addressType, AddressInfo = req.addressInfo };
                        dbraisa.addresses.Add(newAddress);
                        dbraisa.SaveChanges();
                        addressID = dbraisa.addresses.Max(i=>i.AdreessID);
                        log.ActionLog($"Successfully Inserted data to address table, id: {addressID}");
                   

                    var existingContact = dbraisa.contactInfos.Where(c => c.ContactInformation == req.contactInformation).FirstOrDefault();
                    if (existingContact != null)
                    {
                        Console.WriteLine("Contact info already exists.");
                        trans.Rollback();
                        error.Action("Contact info already exists", Enums.ErrorTypeEnum.error);
                        return false;
                    }
                    else
                    {
                        var newContact = new Models.ContactInfo { contactType = req.contactType, ContactInformation = req.contactInformation, };
                        dbraisa.contactInfos.Add(newContact);
                        dbraisa.SaveChanges();
                        contactId = dbraisa.contactInfos.Max(i => i.ContactInfoID);
                        log.ActionLog($"Successfully Inserted data to Contact table, id: {contactId}");
                    }
                    var existingPersonalInfo = dbraisa.personalInfos.Where(p => p.PersonalNumber == req.personalNumber).FirstOrDefault();
                    if (existingPersonalInfo != null)
                    {
                        Console.WriteLine("Personal info already exists.");
                        trans.Rollback();
                        error.Action("Personal info already exists", Enums.ErrorTypeEnum.error);
                        return false;
                    }
                    else
                    {
                        var newPersonalInfo = new Models.PersonalInfo
                        {
                            DocumentType = req.documentType,
                            DocumentSeria = req.documentSeria,
                            DocumentNumber = req.documentNumber,
                            DateofIssue = req.dateofIssue,
                            ExpireDate = req.expireDate,
                            PersonalNumber = req.personalNumber,
                            IssuingAuthority = req.issuingAuthority
                            
                            
                        };
                        dbraisa.personalInfos.Add(newPersonalInfo);
                        dbraisa.SaveChanges();
                        personalinfoID = dbraisa.personalInfos.Max(i => i.PersonalInfoID);
                        log.ActionLog($"Successfully Inserted data to Personalinfo table, ID: {personalinfoID}");
                    }

                    if (req.InventerDistributorID == 0)
                    {
                        req.InventerDistributorID = 0;

                    }
                    else
                    {
                        var distrInv = dbraisa.Distributors.Where(a => a.DistributorID == req.InventerDistributorID).FirstOrDefault();
                        if (distrInv.CountOffInvetedDistributor == null)
                        {
                            distrInv.CountOffInvetedDistributor = 0;
                            dbraisa.SaveChanges(true);
                        }

                       
                        if (distrInv != null && distrInv.CountOffInvetedDistributor <= 3)
                        {
                            distrInv.CountOffInvetedDistributor++;
                            dbraisa.SaveChanges(true);
                            inventerID = distrInv.DistributorID;
                        }
                    }

                    dbraisa.SaveChanges();

                    var distributor = new Models.Distributor
                    {
                        DistributorName = req.distributorName,
                        DistributorLastName=req.LastName,
                        BirthDate = req.birthdate,
                        Gender = req.gender,
                        AddressID = addressID,
                        ContactinfoID = contactId,
                        PersonalInfoID = personalinfoID,
                        Recomendedby = req.InventerDistributorID,
                        CountOffInvetedDistributor = 0,
                    };

                    foreach (var item in dbraisa.Distributors.ToList())
                    {
                        CheckLevelForDistributor(item.DistributorID);

                    }

                    dbraisa.Distributors.Add(distributor);
                   dbraisa.SaveChanges(true);
                  
                  // log.ActionLog("Successfully Inserted data to distributor Table");
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                    trans.Rollback();
                    error.Action(ex.Message + " " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                    throw;
                }
            }
        }
        #endregion

        #region funcCheckLevelForDistributor
        private int CheckLevelForDistributor(int id)
        {
           //amowmebs mowveulta xis sighrmes
            var distributor=dbraisa.Distributors.Where(io=>io.DistributorID== id).FirstOrDefault();
            if( distributor != null )
            {
                int indexofcurendistributor = distributor.DistributorID;
                int lv = 0;
                while(lv<5)
                {
                    if(distributor.Level>=5)
                    {
                        return -2;
                    }
                    if(dbraisa.Distributors.Any(io=>io.Recomendedby==indexofcurendistributor))
                    {
                        var suchdistributor = dbraisa.Distributors.Where(io => io.Recomendedby == indexofcurendistributor).FirstOrDefault();
                        if(suchdistributor!=null)
                        {
                            distributor.Level++;
                            indexofcurendistributor = suchdistributor.DistributorID;
                            dbraisa.SaveChanges();

                        }
                    }
                    else
                    {
                        return (int)distributor.Level;
                    }
                    lv++;
                }
                return (int)distributor.Level;
            }
            return 0;
        }
        #endregion

        #region UpdateDistributor
        public bool UpdateDistributor(UpdateDistributorRequest updis)
        {
            try
            {
                var distup = dbraisa.Distributors.Where(a => a.DistributorID == updis.DistributorID).SingleOrDefault();
                var address = dbraisa.Distributors.Where(a => a.DistributorID == updis.DistributorID).Select(i => i.address).FirstOrDefault();
                var contact = dbraisa.Distributors.Where(a => a.DistributorID == updis.DistributorID).Select(i => i.contactinfo).SingleOrDefault();
                var personal = dbraisa.Distributors.Where(a => a.DistributorID == updis.DistributorID).Select(i => i.personalinfo).SingleOrDefault();
                if (distup == null || address == null ||contact==null||personal==null)
                {
                    Console.WriteLine("No such record was found in the Distributor table :))");
                    error.Action("No such record was found in the Distributor table", Enums.ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    distup.DistributorID = updis.DistributorID;
                    distup.DistributorName = distup.DistributorName;
                    distup.BirthDate = distup.BirthDate;
                    distup.Gender = distup.Gender;
                    address.AddressInfo = updis.addressInfo;
                    address.AddressType = updis.addressType;
                    contact.contactType = updis.contactType;
                    contact.ContactInformation = updis.contactInformation;
                    personal.DocumentType = updis.documentType;
                    personal.DocumentSeria = updis.documentSeria;
                    personal.DocumentNumber = updis.documentNumber;
                    personal.DateofIssue = updis.dateofIssue;
                    personal.ExpireDate = updis.expireDate;
                    personal.PersonalNumber = updis.personalNumber;
                    personal.IssuingAuthority = updis.issuingAuthority;
                    dbraisa.SaveChanges();
                    log.ActionLog($"Successfully updated the information in the distributor table, id: {updis.DistributorID}");
                    return true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region DeleteDistributor
        public bool DeleteDistributor(int ditributorID)
        {
            try
            {
                var dist = dbraisa.Distributors.Where(a => a.DistributorID == ditributorID).FirstOrDefault();
                var address = dbraisa.Distributors.Where(a => a.DistributorID == dist.DistributorID).Select(i => i.address).FirstOrDefault();
                var contact = dbraisa.Distributors.Where(a => a.DistributorID == dist.DistributorID).Select(i => i.contactinfo).SingleOrDefault();
                var personal = dbraisa.Distributors.Where(a => a.DistributorID == dist.DistributorID).Select(i => i.personalinfo).SingleOrDefault();
                if(dist==null||address==null||contact==null||personal==null)
                {
                    Console.WriteLine("There is no such record in the table, so we cannot delete it :)");
                    error.Action($"There is no such record in the table, so we cannot delete it, ID {ditributorID}", Enums.ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    dbraisa.Distributors.Remove(dist);
                    dbraisa.personalInfos.Remove(personal);
                    dbraisa.contactInfos.Remove(contact);
                    dbraisa.addresses.Remove(address);
                    dbraisa.SaveChanges();
                    log.ActionLog($"Deleted successfully from Distributor Table, id: {ditributorID}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error.Action(ex.Message + " " + ex.StackTrace, Enums.ErrorTypeEnum.Fatal);
                throw;
            }
        }
        #endregion

        #region SoftDistributorDelete
        public bool SoftDistributorDelete(int distributorID)
        {
            try
            {
                var dist = dbraisa.Distributors.Where(a => a.DistributorID == distributorID).FirstOrDefault();
                if (dist == null)
                {
                    Console.WriteLine("No such record was found");
                    error.Action($"No such record was found, ID {distributorID}", ErrorTypeEnum.error);
                    return false;
                }
                else
                {
                    dist.ExpireOn = "Expired";
                    dist.ExpireDate = DateTime.Now;
                    log.ActionLog($"Distributor {distributorID} -> soft deleted");
                    dbraisa.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                error.Action(ex.StackTrace + " " + ex.Message,Enums.ErrorTypeEnum.Fatal);
                throw;
            }
          
        }

        #endregion

        #region GetDistributor

        public List<GetDistributor> GetDistributor()
        {
            var AllDist = dbraisa.Distributors.Select(a => new GetDistributor
            {
                DistributorID = a.DistributorID,
                DistributorName = a.DistributorName,
                DistributorLastName = a.DistributorLastName,
                BirthDate = a.BirthDate,
                Gender = a.Gender,
                Recomendedby = a.Recomendedby
            }).ToList();
            return AllDist;
        }
        #endregion

    }
}
