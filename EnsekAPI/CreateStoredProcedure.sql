CREATE PROCEDURE [dbo].[InsertMeterReading]
	@AccountId int,
	@MeterReadingDateTime datetime,
	@MeterReadValue int,
	@ReturnDataSet nvarchar(max) out

	AS

declare @Return int	
set @Return = 0 
     


	    begin transaction 

		begin try

	Insert into Meter_Reading([AccountId],[MeterReadingDateTime],[MeterReadValue])  values (@AccountId, @MeterReadingDateTime, @MeterReadValue)
	
   commit		    

			set @Return = 1

		end try

		begin catch 
		    set @ReturnDataSet = (
    		    SELECT  ERROR_NUMBER() AS ErrorNumber  ,ERROR_SEVERITY() AS ErrorSeverity  ,ERROR_STATE() AS ErrorState  ,ERROR_PROCEDURE() AS ErrorProcedure  ,ERROR_LINE() AS ErrorLine  ,ERROR_MESSAGE() AS ErrorMessage
		        for json path
		    )

    		set @Return = 0

			rollback transaction
        end catch

	    return @Return
