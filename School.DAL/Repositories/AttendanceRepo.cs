using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class AttendanceRepo : IAttendanceRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public Response GetAttendanceListing(AttendanceCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        var rs = dbcontext.USP_GetAttendanceDetail(objModel.SectionId, objModel.AttendanceDate).ToList();
                        if (rs.Count > 0)
                        {
                            //return rs;
                            response.message = "Update";
                            response.responseData = rs;
                        }
                        else
                        {
                            var rs2 = dbcontext.USP_BindStudentWithClassAndSection(objModel.SessionId, objModel.ClassId, objModel.SectionId, "").ToList();
                            //return rs2;
                            response.message = "Insert";
                            response.responseData = rs2;
                        }
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;                        
                        return response;
                    }
                }
                //return response;
            }
            
        }

        public Response SaveAttendanceDetails(AttendanceCustomModel objModel)
        {
            DateTime DateCheck = objModel.AttendanceDate.Add(new TimeSpan(5, 30, 0));

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        var rs = dbcontext.tblAttendances.FirstOrDefault(x => 
                        x.IsDeleted == false && x.AdmissionId == objModel.AdmissionId 
                        && x.AttendanceDate.Year == DateCheck.Year
                        && x.AttendanceDate.Month == DateCheck.Month
                        && x.AttendanceDate.Day == DateCheck.Day
                        && x.Attendance == objModel.Attendance 
                        && x.SectionId == objModel.SectionId
                        );
                        if (rs == null)
                        {
                            tblAttendance objAddNew = new tblAttendance
                            {
                                EnquiryId = objModel.EnquiryId,
                                AdmissionId = objModel.AdmissionId,
                                SectionId = objModel.SectionId ?? 0,
                                AttendanceDate = objModel.AttendanceDate.Add(new TimeSpan(5, 30, 0)),
                                Attendance = objModel.Attendance,
                                Reason = objModel.Reason,

                                IsActive = true,
                                IsDeleted = false,
                                CreatedBy = objModel.CreatedBy,
                                CreatedDate = DateTime.Now,
                                ModifiedBy = objModel.ModifiedBy,
                                ModifiedDate = DateTime.Now
                            };

                            dbcontext.tblAttendances.Add(objAddNew);
                            dbcontext.SaveChanges();
                            response.responseData = new { AttendanceId = objAddNew.AdmissionId, AdmissionDate = objAddNew.AttendanceDate };
                            response.message = "Record Added Successfully!";
                        }
                        else
                        {
                            response.success = false;
                            response.message = "Record Already Exists!";
                        }
                        
                        //var rs = dbcontext.tblAttendances.FirstOrDefault(x => x.IsDeleted == false && x.AttendanceDate == objModel.AttendanceDate && x.AttendanceId != objModel.AttendanceId);
                        //if (rs == null)
                        //{
                        //    var objUpdate = dbcontext.tblAttendances.FirstOrDefault(m => m.AttendanceId == objModel.AttendanceId);
                        //    if (objUpdate != null)
                        //    {
                        //        objUpdate.EnquiryId = objModel.EnquiryId;
                        //        objUpdate.AdmissionId = objModel.AdmissionId;
                        //        objUpdate.SectionId = objModel.SectionId ?? 0;
                        //        objUpdate.AttendanceDate = objModel.AttendanceDate;
                        //        objUpdate.Attendance = objModel.Attendance;
                        //        objUpdate.Reason = objModel.Reason;

                        //        objUpdate.ModifiedBy = objModel.ModifiedBy;
                        //        objUpdate.ModifiedDate = DateTime.Now;
                        //        dbcontext.SaveChanges();
                        //        response.responseData = new { AttendanceId = objUpdate.AdmissionId, AdmissionDate = objUpdate.AttendanceDate };
                        //        response.message = "Record Updated Successfully!";
                        //    }
                        //}
                        //else
                        //{
                        //    response.success = false;
                        //    response.message = "Record Already Exists!";
                        //}
                        
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public Response Update_tbAttendance(AttendanceCustomModel objModel)
        {
            DateTime DateCheck = objModel.AttendanceDate.Add(new TimeSpan(5, 30, 0));

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        tblAttendance cmd = dbcontext.tblAttendances.FirstOrDefault(x =>
                        x.IsDeleted == false && x.AdmissionId == objModel.AdmissionId
                        && x.AttendanceDate.Year == DateCheck.Year
                        && x.AttendanceDate.Month == DateCheck.Month
                        && x.AttendanceDate.Day == DateCheck.Day
                        && x.SectionId == objModel.SectionId
                        );
                        if (cmd != null)
                        {
                            cmd.Attendance = objModel.Attendance;
                            dbcontext.SaveChanges();
                            response.message = "Record Updated Successfully!!";
                        }
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public void Dispose()
        {
            dbcontext.Dispose();
            if (response != null)
            {
                response.Dispose();
            }

            GC.SuppressFinalize(this);
        }

    }
}
