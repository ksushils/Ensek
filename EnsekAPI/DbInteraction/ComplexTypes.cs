using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EnsekAPI.DbInteraction
{
    public class ComplexTypes
    {


        public class ReturnInsertMeterReading
        {
            /// <summary>
            /// -1 : There was an application error. 
            /// 0 : There was a SQL error.
            /// 1 : No Errors where returned from the stroed procedure.
            /// </summary>
            /// <remarks>
            /// -1 : There was an application error. 
            /// 0 : There was a SQL error.
            /// 1 : No Errors where returned from the stroed procedure.
            /// </remarks>
            /// <value>
            /// -1 : There was an application error. 
            /// 0 : There was a SQL error.
            /// 1 : No Errors where returned from the stroed procedure.
            /// </value>
            public int Status { get; set; }
            /// <summary>
            /// The application error object.
            /// </summary>
            public ApplicationError ErrorApplication { get; set; }
            /// <summary>
            /// The SQL error object.
            /// </summary>
            public List<SQLError> ErrorSQL { get; set; }
        }



        public class ApplicationError
        {
            /// <summary>
            /// The text taken from the exception.source should there be an application error.
            /// </summary>
            public string Source { get; set; }
            /// <summary>
            /// The text taken from the exception.message should there be an application error.
            /// </summary>
            public string Message { get; set; }
        }

        public class SQLError
        {
            /// <summary>
            /// SQL Server stroed procedure issue : Error number.
            /// </summary>
            public int ErrorNumber { get; set; }
            /// <summary>
            /// SQL Server stroed procedure issue : Error severity
            /// </summary>
            public int ErrorSeverity { get; set; }
            /// <summary>
            /// SQL Server stroed procedure issue : Error state
            /// </summary>
            public int ErrorState { get; set; }
            /// <summary>
            /// SQL Server stroed procedure issue : Name of the stroed procedure with the issue.
            /// </summary>
            public string ErrorProcedure { get; set; }
            /// <summary>
            /// SQL Server stroed procedure issue : Line number.
            /// </summary>
            public int ErrorLine { get; set; }
            /// <summary>
            /// SQL Server stroed procedure issue : Error message.
            /// </summary>
            public string ErrorMessage { get; set; }
        }





    }
}
