using HR.Models;
using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HR.Reposatory.Evalutions.reposatory
{
    public class Vacations_Setup: IVacations_Setup
    {
        private readonly ApplicationDbContext context;
        public Vacations_Setup(ApplicationDbContext newcontext)
        {
            context = newcontext;
        }
        public Models.Vacations_Setup Find(int ID)
        {
            try
            {
                var model = context.Vacations_Setup.Find(ID);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddOne(Models.Vacations_Setup model)
        {
            try
            {
               
                if (model.LeavesPayItemsCashDays == null)
                {
                    model.LeavesPayItemsCashDays = 0;
                }
                if (model.PRWorkDayPayCode == null)
                {
                    model.PRWorkDayPayCode = 0;
                }
                if (model.EOSCashmandayAmount == null)
                {
                    model.EOSCashmandayAmount = 0;
                }
                context.Vacations_Setup.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {   
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddList(List<Models.Vacations_Setup> model)
        {
            try
            {
                context.Vacations_Setup.AddRange(model);
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Models.Vacations_Setup> GetAll()
        {
            try
            {
              var model=  context.Vacations_Setup.ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var model = context.Vacations_Setup.FirstOrDefault(m => m.ID == id);
                context.Vacations_Setup.Remove(model);
                context.SaveChanges(); return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditOne(Models.Vacations_Setup model)
        {
            try
            {
                var recode = context.Vacations_Setup.FirstOrDefault(m => m.ID == model.ID);
                recode.LeaveTypeCode = model.LeaveTypeCode;
                recode.LeaveTypeNameEnglish = model.LeaveTypeNameEnglish;
                recode.LeaveTypeNameArabic = model.LeaveTypeNameArabic;
                recode.IncludeWeekEnd = model.IncludeWeekEnd;
                recode.FemaleOnly = model.FemaleOnly;
                recode.AcceptNegative = model.AcceptNegative;
                recode.IncludeHoliday = model.IncludeHoliday;
                recode.Show0Balance = model.Show0Balance;
                recode.UnlimitedBalance = model.UnlimitedBalance;
                recode.Proportional = model.Proportional;
                recode.AbleToCash = model.AbleToCash;
                recode.TrackMonthlyAccrualBalance = model.TrackMonthlyAccrualBalance;
                recode.RenewBalance = model.RenewBalance;
                recode.RenewBalanceevery = model.RenewBalanceevery;
                recode.TimesPerLife = model.TimesPerLife;
                recode.MaxCasualDays = model.MaxCasualDays;
                recode.MaxContinousDays = model.MaxContinousDays;
                recode.TotalDaysPerLife = model.TotalDaysPerLife;
                recode.MaxDaysToTransfer = model.MaxDaysToTransfer;
                recode.MaxYearsToTransfer = model.MaxYearsToTransfer;
                recode.MaximumDaysPerMonth = model.MaximumDaysPerMonth;
                recode.MaximumDaysContinous = model.MaximumDaysContinous;
                recode.TakenAfterEmploymentDate = model.TakenAfterEmploymentDate;
                recode.LeavesType = model.LeavesType;
                recode.TestFormula = model.TestFormula;
                recode.LeavesType = model.LeavesType;
                if (model.LeavesPayItemsCashDays == null)
                {
                    model.LeavesPayItemsCashDays = 0;
                }
                if (model.PRWorkDayPayCode == null)
                {
                    model.PRWorkDayPayCode = 0;
                }
                if (model.EOSCashmandayAmount == null)
                {
                    model.EOSCashmandayAmount = 0;
                }
                recode.LeavesPayItemsCashDays = model.LeavesPayItemsCashDays;
                recode.PRWorkDayPayCode = model.PRWorkDayPayCode;
                recode.EOSCashmandayAmount = model.EOSCashmandayAmount;
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}