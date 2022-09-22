IF EXISTS (SELECT NULL FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSupportSoftwarePartnersDetails]') AND TYPE IN (N'P', N'PC'))
BEGIN;
	DROP PROCEDURE [dbo].[GetSupportSoftwarePartnersDetails];
END;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER OFF;
GO

CREATE PROCEDURE [dbo].[GetSupportSoftwarePartnersDetails]
   @searchText VARCHAR(MAX) = NULL,
   @isChecked BIT
AS
/*****************************************************************
///<spcomments>
///	<spname>[dbo].[GetSupportSoftwarePartnersDetails]</spname>
///	<summary>This procedures will return Software Partners Details</summary>
///	<callers>
///		<caller></caller>
///	</callers>
///	<calls>
///		<call>Database</call>
///	</calls>
///	<dependents>
///		
///	</dependents>
///	<history>
		Date:		By:		Comments:
		02/19/2017	Amar	Created
		03/03/2019	Amar	Update
///	</history>
///	<example>
///		<code>			
///		</code>
///	</example>
///</spcomments>
*****************************************************************/
SET NOCOUNT ON;
IF @isChecked ='True'
BEGIN
	IF @searchText =''
	BEGIN
		SELECT 
			CompanyId, 
			CompanyName, 
			CompanyUrl, 
			CompanyLogoUrl,
			IsCompanySCSLogo,
			ISNULL(CONVERT(varchar(max),Description),'') AS Description
		FROM [dbo].[tblCompany]
		WHERE [IsActive] = 1 AND IsCompanySCSLogo=1
		ORDER BY CompanyName;
	END
	ELSE
	BEGIN
		SELECT 
			CompanyId, 
			CompanyName, 
			CompanyUrl, 
			CompanyLogoUrl, 
			IsCompanySCSLogo,
			ISNULL(CONVERT(varchar(max),Description),'') AS Description
		FROM [dbo].[tblCompany]
		WHERE [IsActive] = 1 AND IsCompanySCSLogo=1
			AND CompanyName LIKE ('%'+ @searchText + '%') 
		ORDER BY CompanyName;
	END
END
ELSE
BEGIN
	IF @searchText =''
	BEGIN
		SELECT 
			CompanyId, 
			CompanyName, 
			CompanyUrl, 
			CompanyLogoUrl, 
			IsCompanySCSLogo,
			ISNULL(CONVERT(varchar(max),Description),'') AS Description
		FROM [dbo].[tblCompany]
		WHERE [IsActive] = 1
		ORDER BY CompanyName;
	END
	ELSE
	BEGIN
		SELECT 
			CompanyId, 
			CompanyName, 
			CompanyUrl, 
			CompanyLogoUrl, 
			IsCompanySCSLogo,
			ISNULL(CONVERT(varchar(max),Description),'') AS Description
		FROM [dbo].[tblCompany]
		WHERE [IsActive] = 1
			AND CompanyName LIKE ('%'+ @searchText + '%')
		ORDER BY CompanyName;
	END	
END
SET NOCOUNT OFF;