using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace School.Shared.CustomModels
{
    public class CommonMasterDataModel
    {

    }

    public class ClassMasterModel
    {
        public int ClassId { get; set; }
        public string Title { get; set; }
        public SelectList ClassList { get; set; }
    }

    public class SectionMasterModel
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public SelectList SectionList { get; set; }

    }

    public class SessionMasterModel
    {
        public int SessionId { get; set; }
        public string Title { get; set; }
        public SelectList SessionList { get; set; }

    }

    public class EmployeeMasterModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public SelectList EmployeeList { get; set; }

    }

    #region MessageModel
    public class MessageModel
    {
        public string MessageType { get; set; }
        public string Message { get; set; }

        public MessageModel()
        {

        }
        public MessageModel(string messageType, string message)
        {
            this.MessageType = messageType;
            this.Message = message;
        }
    }
    #endregion


    public sealed class CommonStrings
    {
        public const string UserSession = "UserSession";
        public const string DefaultSession = "DefaultSession";
        public const string Session_SubQuestionID = "SubQuestionID";
        public const string Session_LastQuestionIndx = "LastQuestionIndx";
        public const string Session_UserSelfProfilingRsponse = "UserSelfProfilingRsponse";
        public const string Session_AffiliateQuestions = "AffiliateQuestions";
        public const string Session_TestSource = "TestSource";
        public const string CompanySession = "CompanySession";
    }

    public class LoginModel
    {

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be less than equal to 100 characters and greater than 4 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        //[RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        public long ApplicationUserId { get; set; }
        public bool IsMobile { get; set; }
        public int DeviceType { get; set; }
        public string DeviceId { get; set; }
        public Nullable<int> OTP { get; set; }
    }

}
