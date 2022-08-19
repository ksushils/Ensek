using Microsoft.AspNetCore.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using EnsekAPI.DbInteraction;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnsekAPI.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {

         



        // POST api/<MeterReadingController>
        [HttpPost]
        public ComplexTypes.ReturnInsertMeterReading Post([FromBody] string JsonString)
        {
            ComplexTypes.ReturnInsertMeterReading _Return = new ComplexTypes.ReturnInsertMeterReading();
            DBInteraction _DBInteraction = new DBInteraction();
            try
            {
                _Return = _DBInteraction.InsertMeterReading(JsonString);
            }
            catch (Exception _Ex)
            {
                //Convert the returned data set to the correct class. 
                _Return.ErrorApplication.Source = _Ex.Source;
                _Return.ErrorApplication.Message = _Ex.Message;
                _Return.Status = -1;
            }


            return _Return;
        }

    }

        
     
    }

