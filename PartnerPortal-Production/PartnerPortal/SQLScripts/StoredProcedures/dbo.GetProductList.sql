IF EXISTS (SELECT NULL FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProductList]') AND TYPE IN (N'P', N'PC'))
BEGIN;
	DROP PROCEDURE [dbo].[GetProductList];
END;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER OFF;
GO

CREATE PROCEDURE [dbo].[GetProductList]
AS
/*****************************************************************
///<spcomments>
///	<spname>[dbo].[GetProductList]</spname>
///	<summary>This procedures will return Product List</summary>
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
		03/07/2017	Amar	Created
///	</history>
///	<example>
///		<code>	
		EXEC GetProductList		
///		</code>
///	</example>
///</spcomments>
*****************************************************************/
SET NOCOUNT ON;
SELECT 
	a.Id,
	a.ParentId,
	a.Title,
	a.PageTitle,
	a.Url,
	a.HasPage,
	a.PageId,
	a.SortOrder,
	a.IsActive
 FROM MegaMenu a, MegaMenu b WHERE a.ParentId=b.id 
  AND a.ParentId IS NOT NULL   
  AND a.Title <> ''  
  AND a.Title <> 'See All Hardware' 
  AND a.IsActive = 1
  AND (b.ParentId NOT IN (4, 59, 90, 157) OR b.id NOT IN (4, 59, 90, 157))
ORDER BY a.SortOrder;
	
SET NOCOUNT OFF;