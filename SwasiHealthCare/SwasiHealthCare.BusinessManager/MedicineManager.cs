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
    public class MedicineManager : IMedicineManager
    {
        private IRepository<Medicine> MedicineRepository;
        private IRepository<Expense> ExpenseRepository;
        private IRepository<PurchaseMedicine> PurchaseMedicineRepository;
        public MedicineManager()
        {
            this.MedicineRepository = new Repository<Medicine>();
            this.ExpenseRepository = new Repository<Expense>();
            this.PurchaseMedicineRepository = new Repository<PurchaseMedicine>();
        }

        public async Task<ResponseModel> AddNewMedicine(MedicineModel medicineModel)
        {
            try
            {
                if (string.IsNullOrEmpty(medicineModel.MedicineName))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                // Edit Mode - User Id
                if (medicineModel.Mode + "" == "E" && medicineModel.MedicineId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var medicinedata = medicineModel.Mode + "" != "E" ? (await MedicineRepository.GetAll())?.
                        Where(medi => medi.MedicineName.Equals(medicineModel.MedicineName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await MedicineRepository.GetAll())?.Where(medi => medi.MedicineName.Equals(medicineModel.MedicineName, StringComparison.OrdinalIgnoreCase)
                            && medi.MedicineId != medicineModel.MedicineId).FirstOrDefault();

                if (medicinedata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (medicineModel.Mode + "" == "E")
                {
                    var medicine = await MedicineRepository.GetById(medicineModel.MedicineId ?? 0);

                    if (medicine == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    medicine.MedicineDescription = medicineModel.MedicineDescription;
                    medicine.MedicineName = medicineModel.MedicineName;
                    medicine.Date = medicineModel.Date;
                    medicine.MedicineCurrentStack = medicineModel.MedicineCurrentStack;
                    medicine.MedicinePurchaseStack = medicineModel.MedicinePurchaseStack;
                    medicine.MedicinePurchaseRate = medicineModel.MedicinePurchaseRate;
                    medicine.MedicineBalanceStack = medicineModel.MedicineBalanceStack;
                    medicine.MedicineSalesRate = medicineModel.MedicineSalesRate;
                    medicine.MedicineManufacturer = medicineModel.MedicineManufacturer;
                    medicine.MedicineExpiryDate = medicineModel.MedicineExpiryDate;
                    medicine.MedicineModifiedBy = medicineModel.MedicineModifiedBy;
                    medicine.MedicineModifiedDate = DateTime.Now;
                    medicine.HospitalId = medicineModel.HospitalId;
                    await MedicineRepository.Update(medicine);
                }
                else
                {
                    await MedicineRepository.Insert(new Medicine
                    {
                        MedicineDescription = medicineModel.MedicineDescription,
                        MedicineName = medicineModel.MedicineName,
                        Date = DateTime.Now,
                        //MedicineCurrentStack =  medicineModel.MedicineCurrentStack,
                        MedicinePurchaseRate = medicineModel.MedicinePurchaseRate,
                        MedicineSalesRate = medicineModel.MedicineSalesRate,
                        MedicineManufacturer = medicineModel.MedicineManufacturer,
                        MedicineExpiryDate = DateTime.Now,
                        MedicineStatus = true,
                        MedicineCreatedBy = medicineModel.MedicineCreatedBy,
                        MedicineCreatedDate = DateTime.Now,
                        MedicinePurchaseStack = medicineModel.MedicinePurchaseStack,
                        HospitalId = medicineModel.HospitalId
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = medicineModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MedicineModel> GetEditMedicine(long medicineid)
        {
            try
            {
                var medicine = await MedicineRepository.GetById(medicineid);
                var purchasemedicine = (await new Repository<PurchaseMedicine>().GetAll()).
                    Where(x => x.MedicineId == medicineid ).OrderByDescending(x=>x.PurchaseMedicineCreatedDate).FirstOrDefault();
                var currentstack = (await new Repository<OPDPrescription>().GetAll()).Where(x =>
                x.HospitalId == medicine?.HospitalId && x.MedicineId == medicineid).ToList();
                var result = new MedicineModel();
                result.MedicineName = medicine.MedicineName;
                result.MedicineExpiryDate = medicine.MedicineExpiryDate;
                result.MedicineDescription = medicine.MedicineDescription;
                result.MedicineBalanceStack = medicine.MedicineBalanceStack;
                result.MedicinePurchaseRate = medicine.MedicinePurchaseRate;
                result.MedicineStatus = medicine.MedicineStatus;
                result.MedicineManufacturer = medicine.MedicineManufacturer;
                result.MedicineSalesRate = medicine.MedicineSalesRate;
                result.MedicineCurrentStack = medicine.MedicineCurrentStack;
                result.MedicinePurchaseStack = medicine.MedicinePurchaseStack;
                result.MedicineId = medicine.MedicineId;
                //result.CurrentStack = currentstack == null ? 0 : currentstack?.Sum(x => x.MedicineQuantity);
                result.CurrentStack = purchasemedicine?.MedicineCurrentStock ?? 0;
                result.AvailableMedicineCount = purchasemedicine == null ? 0 :
                    purchasemedicine?.AvailableMedicineCount;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteMedicine(long medicineid)
        {
            try
            {
                if (medicineid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await MedicineRepository.GetById(medicineid);

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
                    await MedicineRepository.Delete(medicineid);

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

        public async Task<ResponseModel> UpdateMedicineStatus(long medicineid, bool medicinestatus, long modifiedby)
        {
            try
            {
                if (medicineid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var medicinedata = await MedicineRepository.GetById(medicineid);

                if (medicinedata == null)
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
                    medicinedata.MedicineStatus = medicinestatus;
                    medicinedata.MedicineModifiedBy = modifiedby;
                    medicinedata.MedicineModifiedDate = DateTime.Now;
                    await MedicineRepository.Update(medicinedata);
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

        public async Task<ResponseModel> GetAllMedicine(DateTime? FromDate, DateTime? ToDate,long? hospitalid)
        {
            try
            {
                var medicinelist = new List<Medicine>();
                if (FromDate != null && ToDate != null)
                {
                    medicinelist = (await MedicineRepository.GetAll()).Where(x => x.Date >= FromDate
                    && x.Date <= ToDate && x.HospitalId == hospitalid).ToList();
                }
                else
                {
                    medicinelist = (await MedicineRepository.GetAll()).Where(x => x.HospitalId == hospitalid).ToList();
                }
                return new ResponseModel
                {
                    Status = true,
                    Data = medicinelist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MedicineRevenueReportModel>> GetAllMedicineReport(long hospitalid)
        {
            try
            {
                var result = new List<MedicineRevenueReportModel>();
                var openingballist = (await new Repository<MedicineOpeningBalance>().GetAll()).Where(x => x.HospitalId == hospitalid).FirstOrDefault();
                if (openingballist != null)
                {
                    var data = new MedicineRevenueReportModel
                    {
                        Date = openingballist.Date,
                        Particulars = "Opening Balance",
                        Details = "Opening Balance",
                        OpeningBalance = openingballist.Amount,
                        Income = 0,
                        Profit = 0,
                        Total = 0,
                        HospitalId = openingballist.HospitalId
                    };
                    result.Add(data);
                }

                var t =  (from opdc in (await new Repository<OPDConsoltation>().GetAll())
                          join opdp in (await new Repository<OPDPrescription>().GetAll()) on opdc.OPDConsultationId equals opdp.OPDConsultationId into op
                          from opp in op.DefaultIfEmpty()
                          select new MedicineRevenueReportModel
                          {
                              Details = opp?.MedicineName,
                              Date = opdc == null ? DateTime.Now : opdc.ConsultationDate,
                              Particulars = "Prescription",
                              Income = opp?.MedicineAmount,
                              OpeningBalance = 0,
                              Receipt = 0,
                              Profit = 0,
                              PurchaseMedicine = 0,
                              Total = 0,
                              HospitalId = opp?.HospitalId
                          }).ToList();
                var c = t.Where(x => x.Income != null).Count();
                if (c != 0)
                {
                    result.AddRange(t);
                }
                var purchasemedicinelist = (await new Repository<PurchaseMedicine>().GetAll()).ToList();

                foreach (var item in purchasemedicinelist)
                {
                    var data = new MedicineRevenueReportModel
                    {
                        OpeningBalance = 0,
                        Receipt = 0,
                        PurchaseMedicine = item.PurchaseCost,
                        Date = Convert.ToDateTime(item.PurchaseDate),
                        Particulars = "Purchase Medicine",
                        Total = 0,
                        Income = 0,
                        Details = item?.MedicineName,
                        Profit = 0,
                        HospitalId = item?.HospitalId
                    };
                    result.Add(data);
                }

                
                var receiptlist = (await new Repository<MedicineReceipt>().GetAll()).Where(x => x.ReceiptHospitalId == hospitalid).FirstOrDefault();

                if (receiptlist != null)
                {
                    var data = new MedicineRevenueReportModel
                    {
                        Date = receiptlist.ReceiptDate,
                        Particulars = "Receipt",
                        Details = "Receipt",
                        OpeningBalance = 0,
                        Income = 0,
                        Profit = 0,
                        Total = 0,
                        Receipt = receiptlist.ReceiptAmount,
                        HospitalId = receiptlist.ReceiptHospitalId
                    };
                    result.Add(data);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TreatmentRevenueReportModel>> GetAllTreatmentReport(long hospitalid)
        {
            try
            {
                var result = new List<TreatmentRevenueReportModel>();
                var openingballist = (await new Repository<TreatmentOpeningBalance>().GetAll()).Where(x => x.HospitalId == hospitalid).FirstOrDefault();
                if (openingballist != null)
                {
                    var data = new TreatmentRevenueReportModel
                    {
                        Date = openingballist == null ? DateTime.Now : openingballist.Date,
                        Particulars = "Opening Balance",
                        Details = "Opening Balance",
                        Expense = 0,
                        OpeningBalance = openingballist?.Amount,
                        CompanyExpense = 0,
                        Income = 0,
                        Profit = 0,
                        Total = 0,
                        CompanyId = hospitalid
                    };
                    result.Add(data);
                }
                var resulttmtlist = (from opdc in (await new Repository<OPDConsoltation>().GetAll())
                                     join opds in (await new Repository<OPDServices>().GetAll()) on opdc.OPDConsultationId equals opds.OPDConsoltationId into os
                                     from opds in os.DefaultIfEmpty()
                                     select new TreatmentRevenueReportModel
                                     {
                                         OpeningBalance = 0,
                                         Date = opdc == null ? DateTime.Now : opdc.ConsultationDate,
                                         Particulars = "Service",
                                         Details = opds?.ServiceName,
                                         Income = opds?.ServiceCharge,
                                         Expense = 0,
                                         CompanyExpense = 0,
                                         Profit = 0,
                                         Total = 0,
                                         CompanyId = opds?.HospitalId
                                     }).ToList();
                var t = resulttmtlist.Where(x => x.Income != null).ToList();
                if (t.Count != 0)
                {
                    result.AddRange(resulttmtlist);
                }
                var expenselist = (await new Repository<Expense>().GetAll()).ToList();
                foreach (var item in expenselist)
                {
                    if (item.IsCompanyExpense == true)
                    {
                        var data = new TreatmentRevenueReportModel
                        {
                            Date = item.ExpenseDate,
                            Particulars = "Company Expense",
                            Details = item.StoreName,
                            CompanyExpense = item.BillAmount,
                            IsCompanyExpense = item.IsCompanyExpense,
                            OpeningBalance = 0,
                            Income = 0,
                            Profit = 0,
                            Total = 0,
                            Expense=0,
                            CompanyId = item.HospitalId
                        };
                        result.Add(data);
                    }
                    else
                    {
                        var data = new TreatmentRevenueReportModel
                        {
                            Date = item.ExpenseDate,
                            Particulars = "Expense",
                            Details = item.StoreName,
                            Expense = item.BillAmount,
                            IsCompanyExpense = item.IsCompanyExpense,
                            OpeningBalance = 0,
                            Income = 0,
                            Profit = 0,
                            CompanyExpense = 0,
                            Total = 0,
                            CompanyId = item.HospitalId
                        };
                        result.Add(data);
                    }
                }                
                var receiptlist = (await new Repository<TreatmentReceipt>().GetAll()).Where(x => x.ReceiptHospitalId == hospitalid).FirstOrDefault();
                if (receiptlist != null)
                {
                    var data = new TreatmentRevenueReportModel
                    {
                        Date = receiptlist.ReceiptDate,
                        Particulars = "Receipt",
                        Details = "Receipt",
                        Expense = 0,
                        OpeningBalance = 0,
                        CompanyExpense = 0,
                        Income = 0,
                        Profit = 0,
                        Total = 0,
                        Receipt = receiptlist.ReceiptAmount,
                        CompanyId = receiptlist.ReceiptHospitalId
                    };
                    result.Add(data);
                }                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddNewPurchaseMedicine(PurchaseMedicineModel medicineModel)
        {
            try
            {
                var medicineid = (await MedicineRepository.GetById(medicineModel.MedicineId ?? 0));
                if (string.IsNullOrEmpty(medicineid.MedicineName))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                // Edit Mode - User Id
                if (medicineModel.Mode + "" == "E" && medicineModel.MedicineId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var medicinedata = medicineModel.Mode + "" != "E" ? (await MedicineRepository.GetAll())?.
                        Where(medi => medi.MedicineName.Equals(medicineModel.MedicineName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await MedicineRepository.GetAll())?.Where(medi => medi.MedicineName.Equals(medicineModel.MedicineName, StringComparison.OrdinalIgnoreCase)
                            && medi.MedicineId != medicineModel.MedicineId).FirstOrDefault();

                if (medicinedata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (medicineModel.Mode + "" == "E")
                {
                    var medicine = await PurchaseMedicineRepository.GetById(medicineModel.PurchaseMedicineId);

                    if (medicine == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    medicine.MedicineName = medicineid?.MedicineName;
                    medicine.PurchaseMedicineId = medicineModel.PurchaseMedicineId;
                    medicine.PurchaseDate = medicineModel.PurchaseDate;
                    medicine.MedicineExpiryDate = medicineModel.MedicineExpiryDate;
                    medicine.MedicineCurrentStock = medicineModel.MedicineCurrentStock;
                    medicine.MedicineManufacturer = medicineModel.MedicineManufacturer;                     
                    medicine.PurchaseMedicineModifiedBy = medicineModel.PurchaseMedicineModifiedBy;
                    medicine.PurchaseMedicineModifiedDate = DateTime.Now;
                    medicine.HospitalId = medicineModel.HospitalId;
                    medicine.MedicineCurrentStock = medicineModel.MedicineCurrentStock;
                    medicine.MedicinePurchaseQuanity = medicineModel.MedicinePurchaseQuanity;
                    medicine.AvailableMedicineCount = medicineModel.MedicineCurrentStock;
                    medicine.MedicineId = medicineid.MedicineId;
                    await PurchaseMedicineRepository.Update(medicine);
                }
                else
                {
                    var medicinename = (await new Repository<Medicine>().GetById(medicineModel.MedicineId ?? 0));
                    await PurchaseMedicineRepository.Insert(new PurchaseMedicine
                    {
                        PurchaseMedicineId = medicineModel.PurchaseMedicineId,
                        PurchaseDate = medicineModel.PurchaseDate,
                        MedicineName = medicinename?.MedicineName,
                        MedicineId = medicineModel.MedicineId,
                        MedicineExpiryDate = medicineModel.MedicineExpiryDate,
                        MedicineCurrentStock = medicineModel.MedicineCurrentStock,
                        MedicinePurchaseQuanity = medicineModel.MedicinePurchaseQuanity  ,
                        MedicineManufacturer = medicineModel.MedicineManufacturer,
                        PurchaseCost = medicineModel.PurchaseCost,
                        PurchaseMedicineCreatedBy = medicineModel.PurchaseMedicineCreatedBy,
                        PurchaseMedicineCreatedDate = DateTime.Now,
                        //PurchaseMedicineModifiedBy = x.PurchaseMedicineModifiedBy,
                        //PurchaseMedicineModifiedDate = x.PurchaseMedicineModifiedDate,
                        HospitalId = medicineModel.HospitalId,
                        AvailableMedicineCount = medicineModel?.MedicineCurrentStock
                    });

                    medicinename.MedicineCurrentStack = medicinename.MedicineCurrentStack +
                        medicineModel.MedicinePurchaseQuanity;
                    medicinename.MedicineBalanceStack = medicinename.MedicineBalanceStack +
                        medicineModel.MedicinePurchaseQuanity;
                    medicinename.MedicinePurchaseStack = medicinename.MedicinePurchaseStack +
                         medicineModel.MedicinePurchaseQuanity;
                    medicinename.MedicineModifiedDate = DateTime.Now;
                    await new Repository<Medicine>().Update(medicinename);
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = medicineModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PurchaseMedicineModel> GetEditPurchaseMedicine(long purchsemedicineid)
        {
            try
            {
                var medicine = await PurchaseMedicineRepository.GetById(purchsemedicineid);
                var result = new PurchaseMedicineModel();
                result.PurchaseMedicineId = medicine.PurchaseMedicineId;
                result.PurchaseDate = medicine.PurchaseDate ?? DateTime.Now;
                result.MedicineName = medicine.MedicineName;
                result.MedicineId = medicine.MedicineId;
                result.MedicineExpiryDate = medicine.MedicineExpiryDate;
                result.MedicineCurrentStock = medicine.MedicineCurrentStock;
                result.MedicinePurchaseQuanity = medicine.MedicinePurchaseQuanity;
                result.MedicineManufacturer = medicine.MedicineManufacturer;
                result.PurchaseCost = medicine.PurchaseCost;
                result.PurchaseMedicineCreatedBy = medicine.PurchaseMedicineCreatedBy;
                result.PurchaseMedicineCreatedDate = medicine.PurchaseMedicineCreatedDate;
                result.PurchaseMedicineModifiedBy = medicine.PurchaseMedicineModifiedBy;
                result.PurchaseMedicineModifiedDate = medicine.PurchaseMedicineModifiedDate;
                result.HospitalId = medicine.HospitalId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeletePurchaseMedicine(long purcsemedicineid)
        {
            try
            {
                if (purcsemedicineid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await PurchaseMedicineRepository.GetById(purcsemedicineid);

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
                    await PurchaseMedicineRepository.Delete(purcsemedicineid);

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

        public async Task<ResponseModel> GetAllPurchaseMedicine(DateTime? FromDate, DateTime? ToDate, long? hospitalid)
        {
            try
            {
                if (FromDate != null && ToDate != null)
                {

                     var medicinelist = (from user in (await new Repository<PurchaseMedicine>().GetAll()).Where(x => x.PurchaseMedicineCreatedDate >= FromDate
                      && x.PurchaseMedicineCreatedDate <= ToDate && x.HospitalId == hospitalid).ToList()
                                         join roles in (await new Repository<Medicine>().GetAll()) on user.MedicineId equals roles.MedicineId into rl
                                         from role in rl.DefaultIfEmpty()
                                         select new
                                         {
                                             PurchaseMedicineId = user?.PurchaseMedicineId,
                                             PurchaseDate = user?.PurchaseDate,
                                             MedicineName = user?.MedicineName,
                                             MedicineId = user?.MedicineId,
                                             MedicineExpiryDate = user?.MedicineExpiryDate,
                                             MedicineCurrentStock = user?.MedicineCurrentStock,
                                             MedicinePurchaseQuanity = user?.MedicinePurchaseQuanity,
                                             MedicineManufacturer = user?.MedicineManufacturer,
                                             PurchaseCost = user?.PurchaseCost ?? 0,
                                             NewStock = user?.MedicinePurchaseQuanity,
                                             PurchaseRate = user?.PurchaseCost ?? 0,
                                             PurchaseMedicineCreatedBy = user?.PurchaseMedicineCreatedBy,
                                             PurchaseMedicineCreatedDate = user?.PurchaseMedicineCreatedDate,
                                             PurchaseMedicineModifiedBy = user?.PurchaseMedicineModifiedBy,
                                             PurchaseMedicineModifiedDate = user?.PurchaseMedicineModifiedDate,
                                             HospitalId = user?.HospitalId,
                                             AvailableMedicineCount = user?.AvailableMedicineCount,
                                             MedicineRate = role?.MedicinePurchaseRate ?? 0

                                         }).ToList();
                    return new ResponseModel
                    {
                        Status = true,
                        Data = medicinelist,
                        SuccessMessage = Constants.SuccessMessage
                    };
                }
                else
                {
                    var  medicinelist = (from user in (await new Repository<PurchaseMedicine>().GetAll()).Where(x =>  x.HospitalId == hospitalid).ToList()
                                        join roles in (await new Repository<Medicine>().GetAll()) on user.MedicineId equals roles.MedicineId into rl
                                        from role in rl.DefaultIfEmpty()
                                        select new
                                        {
                                            PurchaseMedicineId = user?.PurchaseMedicineId,
                                            PurchaseDate = user?.PurchaseDate,
                                            MedicineName = user?.MedicineName,
                                            MedicineId = user?.MedicineId,
                                            MedicineExpiryDate = user?.MedicineExpiryDate,
                                            MedicineCurrentStock = user?.MedicineCurrentStock,
                                            MedicinePurchaseQuanity = user?.MedicinePurchaseQuanity,
                                            MedicineManufacturer = user?.MedicineManufacturer,
                                            PurchaseCost = user?.PurchaseCost ?? 0,
                                            NewStock = user.MedicinePurchaseQuanity,
                                            PurchaseRate = user?.PurchaseCost ?? 0,
                                            PurchaseMedicineCreatedBy = user?.PurchaseMedicineCreatedBy,
                                            PurchaseMedicineCreatedDate = user?.PurchaseMedicineCreatedDate,
                                            PurchaseMedicineModifiedBy = user?.PurchaseMedicineModifiedBy,
                                            PurchaseMedicineModifiedDate = user?.PurchaseMedicineModifiedDate,
                                            HospitalId = user?.HospitalId,
                                            AvailableMedicineCount = user?.AvailableMedicineCount,
                                            MedicineRate = role?.MedicinePurchaseRate ?? 0
                                        }).ToList();
                    return new ResponseModel
                    {
                        Status = true,
                        Data = medicinelist,
                        SuccessMessage = Constants.SuccessMessage
                    };
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ResponseModel> AddNewExpense(ExpenseModel expenseModel)
        {
            try
            {
                if (string.IsNullOrEmpty(expenseModel.StoreName))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                // Edit Mode - User Id
                if (expenseModel.Mode + "" == "E" && expenseModel.ExpenseId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                //var expensedata = expenseModel.Mode + "" != "E" ? (await ExpenseRepository.GetAll())?.
                //        Where(medi => medi.StoreName.Equals(expenseModel.StoreName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                //        (await ExpenseRepository.GetAll())?.Where(medi => medi.StoreName.Equals(expenseModel.StoreName, StringComparison.OrdinalIgnoreCase)
                //            && medi.ExpenseId != expenseModel.ExpenseId).FirstOrDefault();

                //if (expensedata != null)
                //{
                //    return new ResponseModel
                //    {
                //        Status = false,
                //        ErrorMessage = Constants.UserDuplicate,
                //        ErrorCode = "409"
                //    };
                //}

                if (expenseModel.Mode + "" == "E")
                {
                    var expense = await ExpenseRepository.GetById(expenseModel.ExpenseId ?? 0);

                    if (expense == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    expense.StoreName = expenseModel.StoreName;
                    expense.Description = expenseModel.Description;
                    expense.BillAmount = expenseModel.BillAmount;
                    expense.ApprovedFrom = expenseModel.ApprovedFrom;
                    expense.BillCopyFileName = expenseModel.BillCopyFileName;
                    expense.ExpenseStatus = expenseModel.ExpenseStatus;
                    expense.ExpenseCreatedBy = expenseModel.ExpenseCreatedBy;
                    expense.ExpenseCreatedDate = expenseModel.ExpenseCreatedDate;
                    expense.ExpenseModifiedBy = expenseModel.ExpenseModifiedBy;
                    expense.ExpenseModifiedDate = DateTime.Now;
                    expense.ExpenseDate = expenseModel.ExpenseDate;
                    expense.IsCompanyExpense = expenseModel.IsCompanyExpense;
                    expense.HospitalId = expenseModel.HospitalId;
                    await ExpenseRepository.Update(expense);
                }
                else
                {
                    await ExpenseRepository.Insert(new Expense
                    {
                        StoreName = expenseModel.StoreName,
                        Description = expenseModel.Description,
                        BillAmount = expenseModel.BillAmount,
                        ApprovedFrom = expenseModel.ApprovedFrom,
                        BillCopyFileName = expenseModel.BillCopyFileName,
                        ExpenseStatus = true,
                        ExpenseCreatedBy = expenseModel.ExpenseCreatedBy,
                        ExpenseCreatedDate = DateTime.Now,
                        ExpenseDate = expenseModel.ExpenseDate,
                        IsCompanyExpense = expenseModel.IsCompanyExpense
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = expenseModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ExpenseModel> GetEditExpense(long expenseid)
        {
            try
            {
                var expense = await ExpenseRepository.GetById(expenseid);
                var result = new ExpenseModel();
                result.StoreName = expense.StoreName;
                result.Description = expense.Description;
                result.BillAmount = expense.BillAmount;
                result.ApprovedFrom = expense.ApprovedFrom;
                result.BillCopyFileName = expense.BillCopyFileName;
                result.ExpenseStatus = expense.ExpenseStatus;
                result.ExpenseCreatedBy = expense.ExpenseCreatedBy;
                result.ExpenseCreatedDate = expense.ExpenseCreatedDate;
                result.ExpenseDate = expense.ExpenseDate;
                result.IsCompanyExpense = expense.IsCompanyExpense ?? false;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteExpense(long expenseid)
        {
            try
            {
                if (expenseid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await ExpenseRepository.GetById(expenseid);

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
                    await ExpenseRepository.Delete(expenseid);

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

        public async Task<ResponseModel> UpdateExpenseStatus(long expenseid, bool expensestatus, long modifiedby)
        {
            try
            {
                if (expenseid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var expensedata = await ExpenseRepository.GetById(expenseid);

                if (expensedata == null)
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
                    expensedata.ExpenseStatus = expensestatus;
                    expensedata.ExpenseModifiedBy = modifiedby;
                    expensedata.ExpenseModifiedDate = DateTime.Now;
                    await ExpenseRepository.Update(expensedata);
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
        
        public async Task<ResponseModel> GetAllExpense()
        {
            try
            {
                var expenselist = await ExpenseRepository.GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = expenselist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SalesReport>> GetAllSalesReport(DateTime? FromDate, DateTime? ToDate,long? hospitalid)
        {
            try
            {
                var result = new List<SalesReport>();
                var data = (await new Repository<OPDPrescription>().GetAll()).Where(x => x.HospitalId == hospitalid).ToList();
                if (data != null)
                {
                    if (FromDate != null && ToDate != null)
                    {
                        result = (from opdcon in (await new Repository<OPDConsoltation>().GetAll()).Where(x => x.HospitalId == hospitalid).ToList()
                                      //join opdser in (await new Repository<OPDServices>().GetAll()) on opdcon.OPDConsultationId equals opdser.OPDConsoltationId into ser
                                      //from opds in ser.DefaultIfEmpty()
                                  join opdpre in (await new Repository<OPDPrescription>().GetAll()) on opdcon.OPDConsultationId equals opdpre.OPDConsultationId into pre
                                  from opdpres in pre.DefaultIfEmpty()
                                  select new SalesReport
                                  {
                                      Date = opdcon == null ? DateTime.Now : opdcon.ConsultationDate,
                                      PatientName = opdcon?.PatientName,
                                      MobileNo = opdcon?.MobileNo,
                                      MedicineName = opdpres?.MedicineName,
                                      Quantity = opdpres?.MedicineQuantity ?? 0,
                                      Rate = opdpres?.MedicineAmount ?? 0,
                                      ModeofPayment = opdcon?.PaymentMode,
                                      OpdNumber = opdcon?.ConsultationIDNumber
                                  }).Where(x => x.Date >= FromDate && x.Date <= ToDate).ToList();
                    }
                    else
                    {
                        result = (from opdcon in (await new Repository<OPDConsoltation>().GetAll()).Where(x => x.HospitalId == hospitalid).ToList()
                                      //join opdser in (await new Repository<OPDServices>().GetAll()) on opdcon.OPDConsultationId equals opdser.OPDConsoltationId into ser
                                      //from opds in ser.DefaultIfEmpty()
                                  join opdpre in (await new Repository<OPDPrescription>().GetAll()) on opdcon.OPDConsultationId equals opdpre.OPDConsultationId into pre
                                  from opdpres in pre.DefaultIfEmpty()
                                  select new SalesReport
                                  {
                                      Date = opdcon == null ? DateTime.Now : opdcon.ConsultationDate,
                                      PatientName = opdcon?.PatientName,
                                      MobileNo = opdcon?.MobileNo,
                                      MedicineName = opdpres?.MedicineName,
                                      Quantity = opdpres?.MedicineQuantity ?? 0,
                                      Rate = opdpres?.MedicineAmount ?? 0,
                                      ModeofPayment = opdcon?.PaymentMode,
                                      OpdNumber = opdcon?.ConsultationIDNumber
                                  }).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
