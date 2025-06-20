USE [cenfocinemas-db]
GO

/****** Object:  StoredProcedure [dbo].[RET_USER_BY_CODE_PR]    Script Date: 19/06/2025 08:47:11 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_USER_BY_CODE_PR]
@P_CODE NVARCHAR(30) 
AS
BEGIN

SELECT Id,Created,Updated,UserCode,Name,Email,Password,BirthDate,Status
FROM TBL_User
WHERE UserCode = @P_CODE;
END
GO


