USE [WikiSurf]
GO
/****** Object:  StoredProcedure [dbo].[S_INSERT_WIKI_LINK]    Script Date: 2/20/2021 2:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[S_INSERT_WIKI_LINK]
(
	@WikiPageId uniqueidentifier,
	@Ns varchar(max),
	@Exists varchar(max),
	@Additional varchar(max)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		declare @id uniqueidentifier
	select 
		@id = WikiLinksId 
	from WikiLinks
	where Ns = @Ns and @Exists = [Exists] and @Additional = Additional
	--Each link will be linked to the word associated to the WikiPage. Additional words will be placed in the Word Bank for Pages that exists in Wikipedia
	if(@ns = '0' and @Ns not like '%?%')
	begin
		if(NOT EXISTS(select 1 from WordBanks where Word = @Additional))
		begin
			INSERT INTO WordBanks(Word) VALUES(@Additional)
		end
		if(@id is null)
		begin
			set @id = NEWID()
			INSERT INTO WikiLinks(
				WikiLinksId,
				WikiPageId,
				Ns,
				Additional,
				[Exists])
				VALUES(
					@id,
					@WikiPageId, 
					@Ns, 
					@Additional, 
					@Exists)
		end
		else
		begin
			UPDATE WikiLinks
			SET Additional = @Additional,
				Ns = @Ns,
				[Exists] = @Exists
			where WikiLinksId = @id
		end
			select * 
			from WikiLinks 
			where WikiLinksId =@id
			return
	end

	select null 
	return
END
