using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Helper;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.IRepository;
using SwasiHealthCare.Model;
using SwasiHealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SwasiHealthCare.BusinessManager
{
    public class SystemManager : ISystemManager
    {
        private IRepository<SwasiHealthCare.Data.Entities.System> SystemRepository;
        private IRepository<SwasiHealthCare.Data.Entities.Plan> PlanRepository;
        //private IRepository<SwasiHealthCare.Data.Entities.PatientPlanSubscription> PatientPlanSubscriptionRepository;
        public SystemManager()
        {
            this.SystemRepository = new Repository<Data.Entities.System>();
            this.PlanRepository = new Repository<Data.Entities.Plan>();
            //this.PatientPlanSubscriptionRepository = new Repository<Data.Entities.PatientPlanSubscription>();
        }

        public async Task<ResponseModel> AddNewSystem(SystemViewModel systemModel)
        {
            try
            {
                 //ip
                if (string.IsNullOrEmpty(systemModel.SystemIp))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                // Edit Mode - User Id
                if (systemModel.Mode + "" == "E" && systemModel.SystemId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

               
                var systemdata = systemModel.Mode + "" != "E" ? (await SystemRepository.GetAll())?.
                        Where(sys => sys.SystemIp.Equals(systemModel.SystemIp, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await SystemRepository.GetAll())?.Where(sys => sys.SystemIp.Equals(systemModel.SystemIp, StringComparison.OrdinalIgnoreCase)
                            && sys.SystemId != systemModel.SystemId).FirstOrDefault();

                if (systemdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (systemModel.Mode + "" == "E")
                {
                    var system = await SystemRepository.GetById(systemModel.SystemId ?? 0);

                    if (system == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    system.SystemName = systemModel.SystemName;
                    system.SystemModel = systemModel.SystemModel;
                    system.SystemIp = systemModel.SystemIp;
                    system.SystemModifiedBy = systemModel.SystemModifiedBy;
                    system.SystemModifiedDate = DateTime.Now;
                    await SystemRepository.Update(system);
                }
                else
                {
                    await SystemRepository.Insert(new Data.Entities.System
                    {
                        SystemName = systemModel.SystemName,
                        SystemModel = systemModel.SystemModel,
                        SystemIp = systemModel.SystemIp,
                        SystemStatus = true,
                        SystemCreatedBy = systemModel.SystemCreatedBy,
                        SystemCreatedDate = DateTime.Now
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = systemModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SystemViewModel> GetEditSystem(long systemid)
        {
            try
            {
                var system = await SystemRepository.GetById(systemid);
                var result = new SystemViewModel();
                result.SystemId = system.SystemId;
                result.SystemIp = system.SystemIp;
                result.SystemModel = system.SystemModel;
                result.SystemName = system.SystemName;
                result.SystemStatus = system.SystemStatus;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteSystem(long systemid)
        {
            try
            {
                if (systemid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await SystemRepository.GetById(systemid);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    await SystemRepository.Delete(systemid);

                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.DelSuccessMessage,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> UpdateSystemStatus(long systemid, bool systemstatus, long modifiedby)
        {
            try
            {
                if (systemid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var systemdata = await SystemRepository.GetById(systemid);

                if (systemdata == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    systemdata.SystemStatus = systemstatus;
                    systemdata.SystemModifiedBy = modifiedby;
                    systemdata.SystemModifiedDate = DateTime.Now;
                    await SystemRepository.Update(systemdata);
                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.UserStatusChange,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllSystem()
        {
            try
            {
                var systemlist = await SystemRepository.GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = systemlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddPatientPlanSubscription(PatientPlanSubscriptionModel systemModel)
        {
            try
            {
                //ip
                //if (string.IsNullOrEmpty(systemModel.PatientPlanSubscriptionId))
                //{
                //    return new ResponseModel
                //    {
                //        Status = false,
                //        ErrorMessage = Constants.UserNameNotFound,
                //        ErrorCode = "404"
                //    };
                //}


                // Edit Mode - User Id
                if (systemModel.Mode + "" == "E" && systemModel.PatientPlanSubscriptionId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                //var systemdata = systemModel.Mode + "" != "E" ? (await PatientPlanSubscriptionRepository.GetAll())?.
                //        Where(sys => sys.SystemIp.Equals(systemModel.SystemIp, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                //        (await SystemRepository.GetAll())?.Where(sys => sys.SystemIp.Equals(systemModel.SystemIp, StringComparison.OrdinalIgnoreCase)
                //            && sys.SystemId != systemModel.SystemId).FirstOrDefault();

                //if (systemdata != null)
                //{
                //    return new ResponseModel
                //    {
                //        Status = false,
                //        ErrorMessage = Constants.UserDuplicate,
                //        ErrorCode = "409"
                //    };
                //}

                if (systemModel.Mode + "" == "E")
                {
                    var system =  await new Repository<PatientPlanSubscription>().GetById(systemModel.PatientPlanSubscriptionId);
                    if (system == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    //var planname = (await new Repository<Plan>().GetById(systemModel.PlanId));
                    system.PatientPlanSubscriptionDate = systemModel.PatientPlanSubscriptionDate;
                    //system.BalanceServiceAmount = planname.Amount;
                    //system.BalanceServiceCount = long.Parse(planname.NoofTreatment);
                    //system.PatientPlanSubscriptionStatus = true;
                    //system.TotalServiceAmount = planname.Amount;
                    //system.TotalServiceCount =long.Parse( planname.NoofTreatment);
                     
                    system.PlanId = systemModel.PlanId;
                    system.PatientId = systemModel.PatientId;
                    system.PatientPlanSubscriptionModifiedBy = systemModel.PatientPlanSubscriptionModifiedBy;
                    system.PatientPlanSubscriptionModifiedDate = DateTime.Now;
                    system.PatientPlanSubscriptionHospitalId = systemModel.PatientPlanSubscriptionHospitalId;
                    await  new Repository<PatientPlanSubscription>().Update(system);
                }
                else
                {
                    var planname = (await new Repository<Plan>().GetById(systemModel.PlanId));
                    await new Repository<PatientPlanSubscription>().Insert(new Data.Entities.PatientPlanSubscription
                    {
                        PatientPlanSubscriptionDate = systemModel.PatientPlanSubscriptionDate,
                        PlanId = systemModel.PlanId,
                        PatientId = systemModel.PatientId,
                        PatientPlanSubscriptionStatus = true,
                        BalanceServiceAmount = planname.Amount,
                        BalanceServiceCount = long.Parse(planname.NoofTreatment),
                        TotalServiceAmount = planname.Amount,
                        TotalServiceCount = long.Parse(planname.NoofTreatment),
                        PatientPlanSubscriptionCreatedBy = systemModel.PatientPlanSubscriptionCreatedBy,
                        PatientPlanSubscriptionCreatedDate = DateTime.Now,
                        PatientPlanSubscriptionHospitalId = systemModel.PatientPlanSubscriptionHospitalId
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = systemModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PatientPlanSubscriptionModel> GetEditPatientPlanSubscription(long systemid)
        {
            try
            {
                var system = await new Repository<PatientPlanSubscription>().GetById(systemid);
                var result = new PatientPlanSubscriptionModel();
                result.PatientPlanSubscriptionId = system == null ? 0 : system.PatientPlanSubscriptionId;
                result.PatientId = system == null ? 0 : system.PatientId;
                result.PlanId = system == null ? 0 : system.PlanId;
                result.PatientPlanSubscriptionCreatedDate = system == null ? DateTime.Now : system.PatientPlanSubscriptionCreatedDate;
                result.PatientPlanSubscriptionStatus = system == null ? false : system.PatientPlanSubscriptionStatus;
                result.BalanceServiceAmount = system == null ? 0 : system.BalanceServiceAmount;
                result.BalanceServiceCount = system == null ? 0 : system.BalanceServiceCount;
                result.TotalServiceCount = system == null ? 0 : system.TotalServiceCount;
                result.TotalServiceAmount = system == null ? 0 : system.TotalServiceAmount;
                result.PatientPlanSubscriptionDate = system == null ? DateTime.Now : system.PatientPlanSubscriptionDate;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeletePatientPlanSubscription(long systemid)
        {
            try
            {
                if (systemid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await new Repository<PatientPlanSubscription>().GetById(systemid);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    await new Repository<PatientPlanSubscription>().Delete(systemid);

                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.DelSuccessMessage,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> UpdatePatientPlanSubscriptionStatus(long systemid, bool systemstatus, long modifiedby)
        {
            try
            {
                if (systemid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var systemdata = await new Repository<PatientPlanSubscription>().GetById(systemid);

                if (systemdata == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    systemdata.PatientPlanSubscriptionStatus = systemstatus;
                    systemdata.PatientPlanSubscriptionModifiedBy = modifiedby;
                    systemdata.PatientPlanSubscriptionModifiedDate = DateTime.Now;
                    await new Repository<PatientPlanSubscription>().Update(systemdata);
                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.UserStatusChange,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllPatientPlanSubscription()
        {
            try
            {
                var patplansub = await new Repository<PatientPlanSubscription>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = patplansub,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllPatientPlanSubscriptionDropdown()
        {
            try
            {
                var patplansub = (from user in (await new Repository<PatientPlanSubscription>().GetAll()).ToList()
                                  join roles in (await new Repository<Patient>().GetAll()) on user.PatientId equals roles.PatientId into rl
                                  from role in rl.DefaultIfEmpty()
                                  join plans in (await new Repository<Plan>().GetAll()) on user.PlanId equals plans.PlanId into pl
                                  from plan in pl.DefaultIfEmpty()
                                  select new PatientPlanSubscriptionModel
                                  {
                                      PatientPlanSubscriptionId = user?.PatientPlanSubscriptionId ?? 0,
                                      PatientId = user?.PatientId ?? 0,
                                      PatientName = role?.PatientName,
                                      PlanId = user?.PlanId ?? 0,
                                      PlanName = plan?.PackageCode,
                                      TotalServiceCount = user?.TotalServiceCount ?? 0,
                                      TotalServiceAmount = user?.TotalServiceAmount,
                                      PatientPlanSubscriptionStatus = user?.PatientPlanSubscriptionStatus ?? false,
                                      BalanceServiceCount = user?.BalanceServiceCount ?? 0,
                                      BalanceServiceAmount = user?.BalanceServiceAmount ?? 0,
                                      PatientPlanSubscriptionDate = user?.PatientPlanSubscriptionDate ?? DateTime.Now,
                                      PatientPlanSubscriptionCreatedBy = user?.PatientPlanSubscriptionCreatedBy,
                                      PatientPlanSubscriptionCreatedDate = user?.PatientPlanSubscriptionCreatedDate,
                                      PatientPlanSubscriptionModifiedBy = user?.PatientPlanSubscriptionModifiedBy,
                                      PatientPlanSubscriptionModifiedDate = user?.PatientPlanSubscriptionModifiedDate,
                                      PatientPlanSubscriptionHospitalId = user?.PatientPlanSubscriptionHospitalId
                                  }).ToList();
                return new ResponseModel
                {
                    Status = true,
                    Data = patplansub,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllPatientPlanSubscriptionttt()
        {
            try
            {
                //var patplansub = await new Repository<PatientPlanSubscription>().GetAll();
                var patplansub = (from user in (await new Repository<PatientPlanSubscription>().GetAll()).ToList()
                                  join roles in (await new Repository<Patient>().GetAll()) on user.PatientId equals roles.PatientId into rl
                                  from role in rl.DefaultIfEmpty()
                                  join plans in (await new Repository<Plan>().GetAll()) on user.PlanId equals plans.PlanId into pl
                                  from plan in pl.DefaultIfEmpty()
                                  select new PatientPlanSubscriptionModel
                                  {
                                      PatientPlanSubscriptionId = user?.PatientPlanSubscriptionId ?? 0,
                                      PatientId = user?.PatientId ?? 0,
                                      PatientName = role?.PatientName,
                                      PatientIDNumber = role?.PatientIDNumber,
                                      PlanId = user?.PlanId ?? 0,
                                      PlanName = plan?.PackageCode,
                                      TotalServiceCount = user?.TotalServiceCount ?? 0,
                                      TotalServiceAmount = user?.TotalServiceAmount,
                                      PatientPlanSubscriptionStatus = user?.PatientPlanSubscriptionStatus ?? false,
                                      BalanceServiceCount = user?.BalanceServiceCount ?? 0,
                                      BalanceServiceAmount = user?.BalanceServiceAmount ?? 0,
                                      PatientPlanSubscriptionDate = user?.PatientPlanSubscriptionDate ?? DateTime.Now,
                                      PatientPlanSubscriptionCreatedBy = user?.PatientPlanSubscriptionCreatedBy,
                                      PatientPlanSubscriptionCreatedDate = user?.PatientPlanSubscriptionCreatedDate,
                                      PatientPlanSubscriptionModifiedBy = user?.PatientPlanSubscriptionModifiedBy,
                                      PatientPlanSubscriptionModifiedDate = user?.PatientPlanSubscriptionModifiedDate,
                                      PatientPlanSubscriptionHospitalId = user?.PatientPlanSubscriptionHospitalId
                                  }).ToList();
                return new ResponseModel
                {
                    Status = true,
                    Data = patplansub,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllTmtOpeningBalance()
        {
            try
            {
                var systemlist = await new Repository<TreatmentOpeningBalance>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = systemlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllMedOpeningBalance()
        {
            try
            {
                var systemlist = await new Repository<MedicineOpeningBalance>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = systemlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllTmtReceipt()
        {
            try
            {
                var systemlist = await new Repository<TreatmentReceipt>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = systemlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllMedReceipt()
        {
            try
            {
                var systemlist = await new Repository<MedicineReceipt>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = systemlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddNewPlan(PlanModel planModel)
        {
            try
            { 
                // Edit Mode - User Id
                if (planModel.Mode + "" == "E" && planModel.PlanId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var plandata = planModel.Mode + "" != "E" ? (await PlanRepository.GetAll())?.
                        Where(sys => sys.PackageCode.Equals(planModel.PackageCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await PlanRepository.GetAll())?.Where(sys => sys.PackageCode.Equals(planModel.PackageCode, StringComparison.OrdinalIgnoreCase)
                            && sys.PlanId != planModel.PlanId).FirstOrDefault();

                if (plandata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (planModel.Mode + "" == "E")
                {
                    var plan = await PlanRepository.GetById(planModel.PlanId ?? 0);

                    if (plan == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    plan.TreatmentName = planModel.TreatmentName;
                    plan.PackageCode = planModel.PackageCode;
                    plan.NoofTreatment = planModel.NoofTreatment;
                    plan.Offers = planModel.Offers;
                    plan.Amount = planModel.Amount;
                    plan.isfree = planModel.isfree;
                    plan.PlanExpiryDate = planModel.PlanExpiryDate;
                    plan.PlanModifiedDate = DateTime.Now;
                    plan.PlanStatus = planModel.PlanStatus;
                    await PlanRepository.Update(plan);
                }
                else
                {
                    await PlanRepository.Insert(new Data.Entities.Plan
                    {
                        TreatmentName = planModel.TreatmentName,
                        PackageCode = planModel.PackageCode,
                        PlanExpiryDate = planModel.PlanExpiryDate,
                        NoofTreatment = planModel.NoofTreatment,
                        Offers = planModel.Offers,
                        Amount = planModel.Amount,
                        PlanCreatedBy = planModel.PlanCreatedBy,
                        PlanCreatedDate = DateTime.Now,
                        isfree = planModel.isfree,
                        PlanStatus = true
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = planModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PlanModel> GetEditPlan(long planid)
        {
            try
            {
                var plan = await PlanRepository.GetById(planid);
                var result = new PlanModel();
                result.TreatmentName = plan.TreatmentName;
                result.PackageCode = plan.PackageCode;
                result.NoofTreatment = plan.NoofTreatment;
                result.Offers = plan.Offers;
                result.Amount = plan.Amount;
                result.isfree = plan.isfree;
                result.PlanExpiryDate = plan.PlanExpiryDate;
                result.PlanModifiedDate = plan.PlanCreatedDate;
                result.PlanStatus = plan.PlanStatus;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeletePlan(long planid)
        {
            try
            {
                if (planid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await PlanRepository.GetById(planid);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    await PlanRepository.Delete(planid);

                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.DelSuccessMessage,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddTreatmentOpeningBalance(TreatmentOpeningBalanceModel openingbalancemodel)
        {
            try
            {
                if (openingbalancemodel.Mode + "" == "E" && openingbalancemodel.OpeningBalanceId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var obdata = openingbalancemodel.Mode + "" != "E" ? (await new Repository<TreatmentOpeningBalance>().GetAll())?.
                        Where(sys => sys.Date == openingbalancemodel.Date ).FirstOrDefault() :
                        (await new Repository<TreatmentOpeningBalance>().GetAll())?.Where(sys => sys.Date == openingbalancemodel.Date
                            && sys.OpeningBalanceId != openingbalancemodel.OpeningBalanceId).FirstOrDefault();

                if (obdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (openingbalancemodel.Mode + "" == "E")
                {
                    var plan = await new Repository<TreatmentOpeningBalance>().GetById(openingbalancemodel.OpeningBalanceId ?? 0);

                    if (plan == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    plan.HospitalId = openingbalancemodel.HospitalId;
                    plan.Date = openingbalancemodel.Date;
                    plan.CreatedBy = openingbalancemodel.CreatedBy;
                    plan.CreatedDate = DateTime.Now;
                    plan.Amount = openingbalancemodel.Amount;
                    await new Repository<TreatmentOpeningBalance>().Update(plan);
                }
                else
                {
                    await new Repository<TreatmentOpeningBalance>().Insert(new Data.Entities.TreatmentOpeningBalance
                    {
                        HospitalId = openingbalancemodel.HospitalId,
                        Date = openingbalancemodel.Date,
                        CreatedBy = openingbalancemodel.CreatedBy,
                        CreatedDate = DateTime.Now,
                        Amount = openingbalancemodel.Amount
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = openingbalancemodel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddMedicineOpeningBalance(MedicineOpeningBalanceModel openingbalancemodel)
        {
            try
            {
                if (openingbalancemodel.Mode + "" == "E" && openingbalancemodel.OpeningBalanceId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var obdata = openingbalancemodel.Mode + "" != "E" ? (await new Repository<MedicineOpeningBalance>().GetAll())?.
                        Where(sys => sys.Date == openingbalancemodel.Date).FirstOrDefault() :
                        (await new Repository<MedicineOpeningBalance>().GetAll())?.Where(sys => sys.Date == openingbalancemodel.Date
                            && sys.OpeningBalanceId != openingbalancemodel.OpeningBalanceId).FirstOrDefault();

                if (obdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (openingbalancemodel.Mode + "" == "E")
                {
                    var plan = await new Repository<MedicineOpeningBalance>().GetById(openingbalancemodel.OpeningBalanceId ?? 0);

                    if (plan == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    plan.HospitalId = openingbalancemodel.HospitalId;
                    plan.Date = openingbalancemodel.Date;
                    plan.CreatedBy = openingbalancemodel.CreatedBy;
                    plan.CreatedDate = DateTime.Now;
                    plan.Amount = openingbalancemodel.Amount;
                    await new Repository<MedicineOpeningBalance>().Update(plan);
                }
                else
                {
                    await new Repository<MedicineOpeningBalance>().Insert(new Data.Entities.MedicineOpeningBalance
                    {
                        HospitalId = openingbalancemodel.HospitalId,
                        Date = openingbalancemodel.Date,
                        CreatedBy = openingbalancemodel.CreatedBy,
                        CreatedDate = DateTime.Now,
                        Amount = openingbalancemodel.Amount
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = openingbalancemodel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddTreatmentReceipt(TreatmentReceiptModel openingbalancemodel)
        {
            try
            {
                if (openingbalancemodel.Mode + "" == "E" && openingbalancemodel.TreatmentReceiptId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var obdata = openingbalancemodel.Mode + "" != "E" ? (await new Repository<TreatmentReceipt>().GetAll())?.
                        Where(sys => sys.ReceiptDate == openingbalancemodel.ReceiptDate).FirstOrDefault() :
                        (await new Repository<TreatmentReceipt>().GetAll())?.Where(sys => sys.ReceiptDate == openingbalancemodel.ReceiptDate
                            && sys.TreatmentReceiptId != openingbalancemodel.TreatmentReceiptId).FirstOrDefault();

                if (obdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (openingbalancemodel.Mode + "" == "E")
                {
                    var plan = await new Repository<TreatmentReceipt>().GetById(openingbalancemodel.TreatmentReceiptId);

                    if (plan == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    plan.ReceiptHospitalId = openingbalancemodel.ReceiptHospitalId;
                    plan.ReceiptDate = openingbalancemodel.ReceiptDate;
                    plan.ReceiptCreatedBy = openingbalancemodel.ReceiptCreatedBy;
                    plan.ReceiptCreatedDate = DateTime.Now;
                    plan.ReceiptAmount = openingbalancemodel.ReceiptAmount;
                    await new Repository<TreatmentReceipt>().Update(plan);
                }
                else
                {
                    await new Repository<TreatmentReceipt>().Insert(new Data.Entities.TreatmentReceipt
                    {
                        ReceiptHospitalId = openingbalancemodel.ReceiptHospitalId,
                        ReceiptDate = openingbalancemodel.ReceiptDate,
                        ReceiptCreatedBy = openingbalancemodel.ReceiptCreatedBy,
                        ReceiptCreatedDate = DateTime.Now,
                        ReceiptAmount = openingbalancemodel.ReceiptAmount
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = openingbalancemodel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddMedicineReceipt(MedicineReceiptModel openingbalancemodel)
        {
            try
            {
                if (openingbalancemodel.Mode + "" == "E" && openingbalancemodel.MedicineReceiptId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var obdata = openingbalancemodel.Mode + "" != "E" ? (await new Repository<MedicineReceipt>().GetAll())?.
                        Where(sys => sys.ReceiptDate == openingbalancemodel.ReceiptDate).FirstOrDefault() :
                        (await new Repository<MedicineReceipt>().GetAll())?.Where(sys => sys.ReceiptDate == openingbalancemodel.ReceiptDate
                            && sys.MedicineReceiptId != openingbalancemodel.MedicineReceiptId).FirstOrDefault();

                if (obdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (openingbalancemodel.Mode + "" == "E")
                {
                    var plan = await new Repository<MedicineReceipt>().GetById(openingbalancemodel.MedicineReceiptId);

                    if (plan == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    plan.ReceiptHospitalId = openingbalancemodel.ReceiptHospitalId;
                    plan.ReceiptDate = openingbalancemodel.ReceiptDate;
                    plan.ReceiptCreatedBy = openingbalancemodel.ReceiptCreatedBy;
                    plan.ReceiptCreatedDate = DateTime.Now;
                    plan.ReceiptAmount = openingbalancemodel.ReceiptAmount;
                    await new Repository<MedicineReceipt>().Update(plan);
                }
                else
                {
                    await new Repository<MedicineReceipt>().Insert(new Data.Entities.MedicineReceipt
                    {
                        ReceiptHospitalId = openingbalancemodel.ReceiptHospitalId,
                        ReceiptDate = openingbalancemodel.ReceiptDate,
                        ReceiptCreatedBy = openingbalancemodel.ReceiptCreatedBy,
                        ReceiptCreatedDate = DateTime.Now,
                        ReceiptAmount = openingbalancemodel.ReceiptAmount
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = openingbalancemodel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TreatmentOpeningBalanceModel> GetEditTreatmentOpeningBalance(long tmtopeningbalance)
        {
            try
            {
                var openingbalancemodel = await new Repository<TreatmentOpeningBalance>().GetById(tmtopeningbalance);
                var result = new TreatmentOpeningBalanceModel();
                result.HospitalId = openingbalancemodel.HospitalId;
                result.Date = openingbalancemodel.Date;
                result.CreatedBy = openingbalancemodel.CreatedBy;
                result.CreatedDate = DateTime.Now;
                result.Amount = openingbalancemodel.Amount;
                result.OpeningBalanceId = openingbalancemodel.OpeningBalanceId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MedicineOpeningBalanceModel> GetEditMedicineOpeningBalance(long medopeningbalance)
        {
            try
            {
                var openingbalancemodel = await new Repository<MedicineOpeningBalanceModel>().GetById(medopeningbalance);
                var result = new MedicineOpeningBalanceModel();
                result.HospitalId = openingbalancemodel.HospitalId;
                result.Date = openingbalancemodel.Date;
                result.CreatedBy = openingbalancemodel.CreatedBy;
                result.CreatedDate = DateTime.Now;
                result.Amount = openingbalancemodel.Amount;
                result.OpeningBalanceId = openingbalancemodel.OpeningBalanceId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteOpeningBalance(long openingbalance,bool ismed)
        {
            try
            {
                if (openingbalance <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                if (!ismed)
                {
                    var user = await new Repository<TreatmentOpeningBalance>().GetById(openingbalance);

                    if (user == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    else
                    {
                        await new Repository<TreatmentOpeningBalance>().Delete(openingbalance);

                        return new ResponseModel
                        {
                            Status = true,
                            SuccessMessage = Constants.DelSuccessMessage,
                            ErrorMessage = string.Empty,
                            ErrorCode = string.Empty
                        };
                    }
                }
                else
                {
                    var user = await new Repository<MedicineOpeningBalance>().GetById(openingbalance);

                    if (user == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    else
                    {
                        await new Repository<MedicineOpeningBalance>().Delete(openingbalance);

                        return new ResponseModel
                        {
                            Status = true,
                            SuccessMessage = Constants.DelSuccessMessage,
                            ErrorMessage = string.Empty,
                            ErrorCode = string.Empty
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteReceipt(long openingbalance, bool ismed)
        {
            try
            {
                if (openingbalance <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                if (!ismed)
                {
                    var user = await new Repository<TreatmentReceipt>().GetById(openingbalance);

                    if (user == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    else
                    {
                        await new Repository<TreatmentReceipt>().Delete(openingbalance);

                        return new ResponseModel
                        {
                            Status = true,
                            SuccessMessage = Constants.DelSuccessMessage,
                            ErrorMessage = string.Empty,
                            ErrorCode = string.Empty
                        };
                    }
                }
                else
                {
                    var user = await new Repository<MedicineReceipt>().GetById(openingbalance);

                    if (user == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    else
                    {
                        await new Repository<MedicineReceipt>().Delete(openingbalance);

                        return new ResponseModel
                        {
                            Status = true,
                            SuccessMessage = Constants.DelSuccessMessage,
                            ErrorMessage = string.Empty,
                            ErrorCode = string.Empty
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> UpdatePlanStatus(long planid, bool planstatus, long modifiedby)
        {
            try
            {
                if (planid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var plandata = await PlanRepository.GetById(planid);

                if (plandata == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    plandata.PlanStatus = planstatus;
                    plandata.PlanModifiedBy = modifiedby;
                    plandata.PlanModifiedDate = DateTime.Now;
                    await PlanRepository.Update(plandata);
                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.UserStatusChange,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllPlan()
        {
            try
            {
                var planlist = await PlanRepository.GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = planlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
