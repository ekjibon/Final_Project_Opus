using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Attendance.Interfaces;

namespace OPUSERP.Areas.HRPMSAttendence.Controllers
{
    [Area("HRPMSAttendence")]
    public class UploadAttendenceController : Controller
    {
        private IAttendanceProcessService attendanceProcessService;

        public UploadAttendenceController(IHostingEnvironment hostingEnvironment, IAttendanceProcessService attendanceProcessService)
        {
            this.attendanceProcessService = attendanceProcessService;           
        }

        public async Task<IActionResult> Index()
        {
            UploadExcelViewModel model = new UploadExcelViewModel
            {
                attendenceUploads = await attendanceProcessService.GetAllAttendenceUpload()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] UploadExcelViewModel model)
        {           
            await attendanceProcessService.DeleteAttendenceUpload();

            for (int i = 0; i < model.headName.Length; i++)
            {
                AttendenceUpload data = new AttendenceUpload
                {
                    empCode = model.headName[i],
                    entryDate = model.col1[i],
                    // status= model.col2[i],   
                };
                await attendanceProcessService.SaveAttendenceUpload(data);
            }           
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult LoadFile()
        {
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    List<ExeclField> lstHead = new List<ExeclField>();
                    if (Request.Form.Files[0].Length > 0)
                    {
                        string fileExtension = Path.GetExtension(Request.Form.Files[0].FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            int _min = 10000;
                            int _max = 99999;
                            Random _rdm = new Random();
                            int rnd = _rdm.Next(_min, _max);

                            string filePath = string.Empty;
                            string fileName = string.Empty;
                            string fileType = string.Empty;

                            IFormFile file = Request.Form.Files[0];
                            fileType = file.ContentType;
                            fileName = rnd + file.FileName;
                            filePath = "wwwroot/Upload/CS/" + fileName;

                            var fileD = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                            using (var fileSrteam = new FileStream(fileD, FileMode.Create))
                            {
                                file.CopyTo(fileSrteam);
                            }

                            string excelConnectionString = string.Empty;
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();

                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                                excelConnection.Close();
                            }
                        }

                        ExeclField column = new ExeclField();
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            var columnName = ds.Tables[0].Columns[i].ColumnName.ToString();
                            if (i == 0) { column.col0 = columnName; }
                            else if (i == 1) { column.col1 = columnName; }
                            else if (i == 2) { column.col2 = columnName; }
                            else if (i == 3) { column.col3 = columnName; }
                            else if (i == 4) { column.col4 = columnName; }
                            else if (i == 5) { column.col5 = columnName; }
                            else if (i == 6) { column.col6 = columnName; }
                            else if (i == 7) { column.col7 = columnName; }
                            else if (i == 8) { column.col8 = columnName; }
                            else if (i == 9) { column.col9 = columnName; }
                            else if (i == 10) { column.col10 = columnName; }
                            else if (i == 11) { column.col11 = columnName; }
                            else if (i == 12) { column.col12 = columnName; }
                            else if (i == 13) { column.col13 = columnName; }
                        }
                        lstHead.Add(column);

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ExeclField head = new ExeclField();
                            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                            {
                                //var columnName = ds.Tables[0].Rows[i][j].ToString() != "" ? ds.Tables[0].Rows[i][j].ToString() : "-";
                                var columnName = ds.Tables[0].Rows[i][j].ToString();//.ToString() != "" ? ds.Tables[0].Rows[i][j].ToString() : "-";
                                if (j == 0) { head.col0 = columnName; }
                                else if (j == 1) { head.col1 = columnName; }
                                else if (j == 2) { head.col2 = columnName; }
                                else if (j == 3) { head.col3 = columnName; }
                                else if (j == 4) { head.col4 = columnName; }
                                else if (j == 5) { head.col5 = columnName; }
                                else if (j == 6) { head.col6 = columnName; }
                                else if (j == 7) { head.col7 = columnName; }
                                else if (j == 8) { head.col8 = columnName; }
                                else if (j == 9) { head.col9 = columnName; }
                                else if (j == 10) { head.col10 = columnName; }
                                else if (j == 11) { head.col11 = columnName; }
                                else if (j == 12) { head.col12 = columnName; }
                                else if (j == 13) { head.col13 = columnName; }
                            }
                            lstHead.Add(head);
                        }
                    }
                    return Json(lstHead);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public class ExeclField
        {
            public string col0 { get; set; }
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
            public string col5 { get; set; }
            public string col6 { get; set; }
            public string col7 { get; set; }
            public string col8 { get; set; }
            public string col9 { get; set; }
            public string col10 { get; set; }
            public string col11 { get; set; }
            public string col12 { get; set; }
            public string col13 { get; set; }
        }
    }
}