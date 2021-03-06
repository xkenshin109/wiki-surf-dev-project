USE [WikiSurf]
GO
/****** Object:  StoredProcedure [dbo].[S_INSERT_WIKI_PAGE]    Script Date: 2/20/2021 2:37:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[S_INSERT_WIKI_PAGE] 
	-- Add the parameters for the stored procedure here
	(
		@WordBankId uniqueidentifier,
		@PageId bigint,
		@RevId bigint,
		@Title varchar(200),
		@DisplayTitle varchar(200),
		@PlainTextContent varchar(max)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @id uniqueidentifier, @existingId uniqueidentifier;
	set @id = NEWID()

	select @existingId = WikiPageId 
		from WikiPages
		where @WordBankId = WordBankId and @PageId = PageId and RevId = @RevId
	if(@existingId is null)
	begin
    -- Insert statements for procedure here
	INSERT INTO dbo.WikiPages(
		WikiPageId,
		WordBankId,
		Title,
		RevId,
		PageId,
		DisplayTitle,
		PlainTextContent)
	VALUES(@id,@WordBankId, @Title, @RevId, @PageId, @DisplayTitle, @PlainTextContent)
	set @existingId = @id
	end
	else
	begin
		UPDATE WikiPages 
		SET Title = @Title,
			RevId = @RevId,
			PageId = @PageId,
			DisplayTitle = @DisplayTitle,
			PlainTextContent = @PlainTextContent
		where WikiPageId = @existingId
	end
		SELECT WikiPageId,
		WordBankId,
		Title,
		RevId,
		PageId,
		DisplayTitle,
		PlainTextContent,
		ImportDate
		from WikiPages where @existingId = WikiPageId

END
