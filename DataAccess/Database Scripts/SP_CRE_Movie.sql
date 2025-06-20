USE [cenfocinemas-db]
GO

-- Para crear una película
CREATE PROCEDURE CRE_MOVIE_PR
    @P_Title nvarchar(50),
    @P_Description nvarchar(100),
    @P_ReleaseDate datetime,
    @P_Genre nvarchar(50),
    @P_Director nvarchar(50)
AS
BEGIN
    INSERT INTO TBL_Movie (Created, Title, Description, ReleaseDate, Genre, Director)
    VALUES (GETDATE(), @P_Title, @P_Description,@P_ReleaseDate, @P_Genre, @P_Director);
END
