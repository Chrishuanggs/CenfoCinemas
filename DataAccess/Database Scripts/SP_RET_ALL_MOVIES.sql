USE [cenfocinemas-db]
GO

/****** Object:  StoredProcedure [dbo].[RET_ALL_MOVIES_PR]    Script Date: 19/06/2025 08:10:42 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_ALL_MOVIES_PR]
AS
BEGIN

	SELECT Id,Created,Updated,Title,Description,ReleaseDate,Genre,Director
	FROM TBL_Movie;
	END
GO


