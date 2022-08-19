using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using EnsekAPI.DbInteraction;
using EnsekAPI;

namespace EnsekAPI.DbInteraction
{
    public class Services
    {
    }
}




/// <summary>
/// //This class will do all the required transactions with the database 
/// </summary>
public class DBInteraction
{

    #region Internal variables mapped to parameter. 
    /// <summary>
    /// This is the database connection to be used in all DB calls.
    /// </summary>
    private string _DBConnection = string.Empty;


    #endregion



    public static string GetConnStr()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

        IConfiguration Configuration = builder.Build();
        return Configuration["ConnectionStrings:DefaultConnection"];
    }


    /// <summary>
    /// This instantiation method will place the DB parameter value as the _DBConnection value.
    /// </summary>
    public DBInteraction()
    {
        _DBConnection = GetConnStr();
    }

    //This method does the work of actual insert
    public ComplexTypes.ReturnInsertMeterReading InsertMeterReading(string JsonString)
    {
        int MeterReadingId;
        DateTime MeterReadingDateTime;
        int MeterReadValue;
        int AccountId;



        int _MethodStatus = -1;
        ComplexTypes.ApplicationError _ApplicationError = new ComplexTypes.ApplicationError();
        List<ComplexTypes.SQLError> _SQLError = new List<ComplexTypes.SQLError>();

        DBInteraction _DBInteraction = new DBInteraction();



        if (string.IsNullOrEmpty(JsonString))
        {
            throw new Exception("file path is null or empty.");
        }
        else
        {
            List<MeterReading> meterReadings = new List<MeterReading>();


            //Calling method to read the csv file and load the data
            meterReadings = CommonFunctions.ReadFile(JsonString);


            if (String.IsNullOrEmpty(_DBConnection))
            {
                _ApplicationError.Message = "The database connection has not been set.";
                _ApplicationError.Source = "DBInteraction.ReturnInsertToolCheckOut";
                _MethodStatus = -1;
            }
            else
            {

                int returnParameter = 0;
                string jSon = string.Empty;
                             



                try
                {
                    using (SqlConnection _SqlConnection = new SqlConnection(GetConnStr()))
                    {
                        _SqlConnection.Open();

                        foreach (MeterReading item in meterReadings)
                        {
                            
                            MeterReadingId = item.MeterReadingId;
                            MeterReadingDateTime = item.MeterReadingDateTime;
                            MeterReadValue = item.MeterReadValue;
                            AccountId = item.AccountId;

                            SqlCommand _SqlCommand = new SqlCommand("InsertMeterReading", _SqlConnection);
                            _SqlCommand.CommandType = CommandType.StoredProcedure;

                            #region Set up the SQL Parameters
                            SqlParameter _SqlParameterReturnParameter = _SqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                            _SqlParameterReturnParameter.Direction = ParameterDirection.ReturnValue;

                            SqlParameter _SqlParameterjSonReturn = new SqlParameter();
                            _SqlParameterjSonReturn.ParameterName = "@ReturnDataSet";
                            _SqlParameterjSonReturn.SqlDbType = SqlDbType.NVarChar;
                            _SqlParameterjSonReturn.Size = -1;
                            _SqlParameterjSonReturn.Direction = ParameterDirection.Output;
                            _SqlCommand.Parameters.Add(_SqlParameterjSonReturn);

                            SqlParameter _SqlParameterAccountId = new SqlParameter();
                            _SqlParameterAccountId.ParameterName = "@AccountId";
                            _SqlParameterAccountId.SqlDbType = SqlDbType.Int;
                            _SqlParameterAccountId.Size = -1;
                            _SqlParameterAccountId.Direction = ParameterDirection.Input;
                            _SqlParameterAccountId.Value = AccountId;
                            _SqlCommand.Parameters.Add(_SqlParameterAccountId);


                            SqlParameter _SqlParameterDateTime = new SqlParameter();
                            _SqlParameterDateTime.ParameterName = "@MeterReadingDateTime";
                            _SqlParameterDateTime.SqlDbType = SqlDbType.DateTime;
                            _SqlParameterDateTime.Size = -1;
                            _SqlParameterDateTime.Direction = ParameterDirection.Input;
                            _SqlParameterDateTime.Value = MeterReadingDateTime;
                            _SqlCommand.Parameters.Add(_SqlParameterDateTime);


                     

                            SqlParameter _SqlParameterMeterReadValue = new SqlParameter();
                            _SqlParameterMeterReadValue.ParameterName = "@MeterReadValue";
                            _SqlParameterMeterReadValue.SqlDbType = SqlDbType.Int;
                            _SqlParameterMeterReadValue.Size = -1;
                            _SqlParameterMeterReadValue.Direction = ParameterDirection.Input;
                            _SqlParameterMeterReadValue.Value = MeterReadValue;
                            _SqlCommand.Parameters.Add(_SqlParameterMeterReadValue);



                            #endregion


                            _SqlCommand.ExecuteNonQuery();

                            #region Get the information back form the DB
                            returnParameter = (int)_SqlCommand.Parameters["@ReturnVal"].Value;
                            jSon = _SqlCommand.Parameters["@ReturnDataSet"].Value.ToString();
                            #endregion


                        }



                        if (returnParameter == 0)
                        {
                            //There was an error during the execution of the store procedure. 
                            _SQLError = JsonConvert.DeserializeObject<List<ComplexTypes.SQLError>>(jSon);
                            _MethodStatus = 0;
                        }
                        else
                        {
                            //Looks like there are no issues with the execution of the stored procedure.  
                            _MethodStatus = 1;
                        }


                    }
                }
                catch (Exception _Ex)
                {
                    _ApplicationError.Message = _Ex.Message.ToString();
                    _ApplicationError.Source = _Ex.Source.ToString();
                    _MethodStatus = -1;
                }


            }
            ComplexTypes.ReturnInsertMeterReading ReturnInsertToolCheckOut = new ComplexTypes.ReturnInsertMeterReading();
            ReturnInsertToolCheckOut.Status = _MethodStatus;
            ReturnInsertToolCheckOut.ErrorApplication = _ApplicationError;
            ReturnInsertToolCheckOut.ErrorSQL = _SQLError;


            return ReturnInsertToolCheckOut;




        }


    }
}