using MobilePlan.Classes;
using PMSRedirect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePlan.Models
{
    public class Contact
    {
        dbcontrol s = new dbcontrol();
        UserSessions session = new UserSessions();

        public DateTime DatePrinted { get; set; }
        public string user { get; set; }

        [Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public Int32 ID { get; set; }

        [Display(Name = "Employee")]
        //[Required]
        public Int32 EID { get; set; }

        [Display(Name = "Account No")]
        public string AccountNo { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        //[RegularExpression(@"^(0|([0-9]{1,3}))([0-9]){10}",
        [RegularExpression(@"(((\+?[0-9]{2,3})|(0))?([0-9]{3})(\-)?([0-9]{3})(\-)?([0-9]{4})(\-)?)", ErrorMessage = "Please enter a valid phone number \nEx: 09123456789")]
        [Display(Name = "Mobile No")]
        [Required]
        //[MaxLength(13)]
        public String MobileNo { get; set; }

        [Display(Name = "Network")]
        [Required]
        public Int32 NetworkTypeID { get; set; }

        [Display(Name = "BINCARD")]
        [Required]
        public String BINCARD { get; set; }

        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        [Display(Name = "ContractStart")]
        [DateLimit("ContractEnd", false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? ContractStart { get; set; }

        [Display(Name = "ContractEnd")]
        [DateLimit("ContractStart", true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? ContractEnd { get; set; }

        [Display(Name = "Status")]
        public String Status { get; set; }

        [Display(Name = "End User")]
        public Int32 EndUser { get; set; }

        [Display(Name = "Overdue Balance")]
        public String OverdueBalance { get; set; }

        [Display(Name = "Early Renewal")]
        public String EarlyRenewal { get; set; }

        [Display(Name = "Existing Plan")]
        public String ExistingPlan { get; set; }

        [Display(Name = "Inclusion")]
        public String Inclusion { get; set; }

        [Display(Name = "Handset Model")]
        public String HandsetModel { get; set; }

        [Display(Name = "Existing Duo No")]
        public String ExistingDuoNo { get; set; }

        [Display(Name = "Credit Limit")]
        public String CreditLimit { get; set; }

        [Display(Name = "Spending Limit")]
        public String SpendingLimit { get; set; }

        [Display(Name = "Billing Cycle")]
        public String BillingCycle { get; set; }

        [Display(Name = "Modified By")]
        [ScaffoldColumn(false)]
        public Int32 ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Encoded By")]
        [ScaffoldColumn(false)]
        public Int32 encBy { get; set; }

        [Display(Name = "Encoded Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy hh:mm tt}")]
        [DataType(DataType.Date)]
        public DateTime? encDate { get; set; }

        public string NetworkTypeName { get; set; }

        public string Employee { get; set; }

        [Display(Name = "End-User")]
        public string EndUserName { get; set; }

        public string ModifiedByName { get; set; }

        public string encByName { get; set; }

        public Contact()
        {
            
        }
        public List<Contact> List(string Search = "")
        {

            return s.Query<Contact>("tbl_Contact_Proc", p => { p.Add("@Type", "Search"); p.Add("@Search", $"%{Search}%"); }, CommandType.StoredProcedure)
            .Select(r =>
            {
                return r;
            }).ToList();
        }

        public List<Contact> List(int ID = 0)
        {
            return s.Query<Contact>("tbl_Contact_Proc", p => { p.Add("@Type", "FindNetwork"); p.Add("@ID", $"{ID}"); }, CommandType.StoredProcedure)
            .Select(r =>
            {
                return r;
            }).ToList();
        }

        public List<Contact> List(DateTime? StartDate, DateTime? EndDate, int NetworkID = 0, string Status = "All")
        {
            DatePrinted = DateTime.Now;
            user = session.User.User;

            var list = s.Query<Contact>("tbl_Contact_Proc", p => { p.Add("@Type", "Filter"); p.Add("@ContractStart", StartDate); p.Add("@ContractEnd", EndDate); p.Add("@ID", $"{NetworkID}"); p.Add("@Status", $"{Status}"); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).ToList();

            return list;
        }

        public Contact Find(int ID)
        {

            return s.Query<Contact>("tbl_Contact_Proc", p => { p.Add("@Type", "Find"); p.Add("@ID", ID); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public void Create(Contact obj)
        {
            s.Query("tbl_Contact_Proc", p =>
            {
                p.Add("@Type", "Create");
                p.Add("@EID", obj.EID);
                p.Add("@AccountNo", obj.AccountNo);
                p.Add("@AccountName", obj.AccountName);
                p.Add("@MobileNo", obj.MobileNo);
                p.Add("@NetworkTypeID", obj.NetworkTypeID);
                p.Add("@BINCARD", obj.BINCARD);
                p.Add("@Remarks", obj.Remarks);
                p.Add("@ContractStart", obj.ContractStart);
                p.Add("@ContractEnd", obj.ContractEnd);
                p.Add("@Status", obj.Status);
                p.Add("@EndUser", obj.EndUser);
                p.Add("@EndUserName", obj.EndUserName);
                p.Add("@OverdueBalance", obj.OverdueBalance);
                p.Add("@EarlyRenewal", obj.EarlyRenewal);
                p.Add("@ExistingPlan", obj.ExistingPlan);
                p.Add("@Inclusion", obj.Inclusion);
                p.Add("@HandsetModel", obj.HandsetModel);
                p.Add("@ExistingDuoNo", obj.ExistingDuoNo);
                p.Add("@CreditLimit", obj.CreditLimit);
                p.Add("@SpendingLimit", obj.SpendingLimit);
                p.Add("@BillingCycle", obj.BillingCycle);
                p.Add("@ModifiedBy", session.User.ID);
                p.Add("@encBy", session.User.ID);

            }, CommandType.StoredProcedure);
        }

        public void Update(Contact obj)
        {
            s.Query("tbl_Contact_Proc", p =>
            {
                p.Add("@Type", "Update");
                p.Add("@ID", obj.ID);
                p.Add("@EID", obj.EID);
                p.Add("@AccountNo", obj.AccountNo);
                p.Add("@AccountName", obj.AccountName);
                p.Add("@MobileNo", obj.MobileNo);
                p.Add("@NetworkTypeID", obj.NetworkTypeID);
                p.Add("@BINCARD", obj.BINCARD);
                p.Add("@Remarks", obj.Remarks);
                p.Add("@ContractStart", obj.ContractStart);
                p.Add("@ContractEnd", obj.ContractEnd);
                p.Add("@Status", obj.Status);
                p.Add("@EndUser", obj.EndUser);
                p.Add("@EndUserName", obj.EndUserName);
                p.Add("@OverdueBalance", obj.OverdueBalance);
                p.Add("@EarlyRenewal", obj.EarlyRenewal);
                p.Add("@ExistingPlan", obj.ExistingPlan);
                p.Add("@Inclusion", obj.Inclusion);
                p.Add("@HandsetModel", obj.HandsetModel);
                p.Add("@ExistingDuoNo", obj.ExistingDuoNo);
                p.Add("@CreditLimit", obj.CreditLimit);
                p.Add("@SpendingLimit", obj.SpendingLimit);
                p.Add("@BillingCycle", obj.BillingCycle);
                p.Add("@ModifiedBy", session.User.ID);
            }, CommandType.StoredProcedure);
        }
        public void Delete(Contact obj)
        {
            s.Query("DELETE FROM [tbl_Contact] WHERE ID = @ID", p =>
            {
                p.Add("@ID", obj.ID);
            });
        }

        public SelectList EmployeeList()
        {
            var emps = new List<object>();
            //var emps = new List<KeyValuePair<int, string>>();
            var users = new UserSessions().User.List();
            
            users.ForEach(r =>
            {
                if (!string.IsNullOrEmpty(r.Info?.Fullnamev2))
                {
                    var emp = new
                    {
                        ID = r.ID,
                        Fullname = r.Info?.Fullnamev2.Trim()
                    };
                    //var emp = new KeyValuePair<int, string>(r.ID, r.Info?.Fullnamev2);
                    emps.Add(emp);
                }
            });

            return new SelectList(emps.OrderBy(emp => emp.GetType().GetProperty("Fullname")?.GetValue(emp)), "ID", "Fullname");
        }



        public SelectList StatusSelectList()
        {
            var emps = new List<object>();
            //var emps = new List<KeyValuePair<int, string>>();
            var users = new UserSessions().User.List();

            var statusList = s.Query<Contact>("tbl_Contact_Proc", p => { p.Add("@Type", "SelectAllStatus"); }, CommandType.StoredProcedure)
            .Select(r =>
            {
                return r;
            }).ToList();

            users.ForEach(r =>
            {
                if (!string.IsNullOrEmpty(r.Info?.Fullnamev2))
                {
                    var emp = new
                    {
                        ID = r.ID,
                        Fullname = r.Info?.Fullnamev2.Trim()
                    };
                    //var emp = new KeyValuePair<int, string>(r.ID, r.Info?.Fullnamev2);
                    emps.Add(emp);
                }
            });

            return new SelectList(emps.OrderBy(emp => emp.GetType().GetProperty("Fullname")?.GetValue(emp)), "ID", "Fullname");
        }

        public static SelectList NetworkListItems { get; set; }

        public static SelectList NetworkList {
            get
            {
                return NetworkListItems;
            }
            set
            {
                NetworkListItems = new NetworkType().ListPrefix(value.ToString());
            }
        }

        public SelectList NetworkPrefixList()
        {
            return new SelectList(new PrefixNo().PrefixNoList(), "Prefix", "NetworkProviderID");
        }

        public class DateLimitAttribute : ValidationAttribute
        {
            private readonly string _datePropertyName;
            private readonly bool _isUpperLimit;

            public DateLimitAttribute(string datePropertyName, bool isUpperLimit)
            {
                _datePropertyName = datePropertyName;
                _isUpperLimit = isUpperLimit;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var dateProperty = validationContext.ObjectType.GetProperty(_datePropertyName);
                var startDateValue = this._isUpperLimit ? (DateTime)dateProperty.GetValue(validationContext.ObjectInstance) : (DateTime)value;
                var endDateValue = this._isUpperLimit ? (DateTime)value : (DateTime)dateProperty.GetValue(validationContext.ObjectInstance);

                if (endDateValue < startDateValue)
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }

    }
}