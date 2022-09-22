IF EXISTS (SELECT NULL FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.GenerateAndNotifyPassword') AND TYPE IN (N'P', N'PC'))
BEGIN;
	DROP PROCEDURE dbo.GenerateAndNotifyPassword;
END;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[GenerateAndNotifyPassword]
AS
/*****************************************************************
///<spcomments>
///	<spname>dbo.GenerateAndNotifyPassword</spname>
///	<summary></summary>
///	<callers>
///		<caller></caller>
///	</callers>
///	<calls>
///		<call>Database</call>
///	</calls>
///	<dependents>
///		<dependent></dependent>
///	</dependents>
///	<history>
		Date:		By:		Comments:
		06/14/2018	Amit	CREATE
///	</history>
///	<example>
///		<code>
///			exec dbo.GenerateAndNotifyPassword
///		</code>
///	</example>
///</spcomments>
*****************************************************************/
BEGIN 
SET NOCOUNT ON  
DECLARE @LENGTH INT,
		@CharPool VARCHAR(26),
		@PoolLength VARCHAR(26),
		@LoopCount  INT,
		@GeneratedPassword VARCHAR(MAX)
DECLARE @RandomString VARCHAR(10),@CHARPOOLINT VARCHAR(9)  
  
    
SET @CharPool = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'  
DECLARE @TMPSTR VARCHAR(3)  



	DECLARE @tab TABLE(ID INT IDENTITY(1,1),AcId INT,AcName VARCHAR(MAX),PartnerNumber VARCHAR(MAX),AcEmail VARCHAR(MAX),PartnerType VARCHAR(MAX))

	INSERT INTO @tab(AcId,AcName,PartnerNumber,AcEmail,PartnerType)
	SELECT Id,AccountName,PartnerNumber,AccountEmail,PartnerType
	FROM SFAccounts
	WHERE		ISNULL(AccountPassword,'') = ''
			AND ISNULL(PartnerNumber,'') <> ''
			AND ISNULL(AccountEmail,'') <> ''
			AND PartnerProgramStatus = 'Signed'

	DECLARE @count INT;
	SELECT @count = 1;

	WHILE(@count <= (SELECT MAX(ID) FROM @tab))
	BEGIN
		
		--Generate Password
		SET @PoolLength = DataLength(@CharPool)  
		SET @LoopCount = 0  
		SET @RandomString = ''  
  
			WHILE (@LoopCount <10)  
			BEGIN  
				SET @TMPSTR =   SUBSTRING(@Charpool, CONVERT(int, RAND() * @PoolLength), 1)           
				SELECT @RandomString  = @RandomString + CONVERT(VARCHAR(5), CONVERT(INT, RAND() * 10))  
				IF(DATALENGTH(@TMPSTR) > 0)  
				BEGIN   
					SELECT @RandomString = @RandomString + @TMPSTR    
					SELECT @LoopCount = @LoopCount + 1  
				END  
			END  
			SET @LOOPCOUNT = 0    
			SET @GeneratedPassword = @RandomString  

			--SELECT @GeneratedPassword

			---End Generate Password
			--Update Table
			DECLARE @PNbr VARCHAR(MAX),
					@acId INT,
					@AcEmail VARCHAR(MAX),
					@AcName VARCHAR(MAX),
					@PartnerType VARCHAR(MAX)

			SELECT @AcName= AcName, @PNbr = PartnerNumber,@acId = AcId,@AcEmail = AcEmail,@PartnerType =  REPLACE(REPLACE(LOWER(PartnerType),'partner',''),' ' ,'')
			FROM @tab WHERE ID = @count

			UPDATE SFAccounts SET AccountPassword = @GeneratedPassword,PartnerProgramStatus = 'Credentials Sent',IsActive = 1
			WHERE Id = @acId
			AND PartnerNumber = @PNbr
			AND AccountEmail = @AcEmail

			INSERT INTO PasswordGeneraionLog(PartnerNumber,PasswordGenerationDate,LastUpdateDate)
			SELECT @PNbr,GETDATE(),GETDATE()

			DECLARE @subject VARCHAR(MAX),@body VARCHAR(MAX)


			SELECT @subject = ConfigValue FROM SystemConfig WHERE ConfigKey= 'PasswordEmailSubject_' + @PartnerType;
			SELECT @body = ConfigValue FROM SystemConfig WHERE ConfigKey= 'PasswordEmailBody_' + @PartnerType;

			IF ISNULL(@body,'') = ''
			BEGIN
				SELECT @subject = ConfigValue FROM SystemConfig WHERE ConfigKey= 'PasswordEmailSubject';
				SELECT @body = ConfigValue FROM SystemConfig WHERE ConfigKey= 'PasswordEmailBody';
			END

			SELECT @body = REPLACE(@body,'[Name]',@AcName)
			SELECT @body = REPLACE(@body,'[UserName]',@PNbr)
			SELECT @body = REPLACE(@body,'[Password]',@GeneratedPassword)

			Select @AcEmail = 'amit@nightstarpartners.com;tguella@starmicronics.com;PChirinos@starmicronics.com';
			--Send Email
			BEGIN TRY										
				EXEC msdb.dbo.sp_send_dbmail 
					@profile_name = 'Amit',--'Star Marketing'
					@recipients = @AcEmail,
					@subject = @Subject,
					@body = @body,
					@body_format = 'HTML';												

			END TRY
			BEGIN CATCH
			END CATCH
			SELECT @count = @count + 1;
	END;

END  