USE [WikiSurf]
GO
/****** Object:  StoredProcedure [dbo].[S_NEW_GAME_WORDS]    Script Date: 2/20/2021 2:37:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[S_NEW_GAME_WORDS]
	-- Add the parameters for the stored procedure here
	@WikiSessionId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @wordBank Table(
		WordBankId uniqueidentifier,
		Word varchar(max)
	)
	INSERT INTO @wordBank(WordBankId, Word)
	SELECT TOP 2 WikiSessionWordBanks.WordBankId, WordBanks.Word
	FROM WikiSessionWordBanks 
	inner join WordBanks on WordBanks.WordBankId = WikiSessionWordBanks.WordBankId
	where WikiSessionId = @WikiSessionId
	declare @total int
	select @total = count(*) from @wordBank
	if(@total = 0)
	begin
		INSERT INTO @wordBank(WordBankId, Word)
		SELECT TOP 2 WordBankId, Word
		FROM WordBanks 
		WHERE Exclude = 0 
		order by NEWID()
	end
    --Set the current word 
	UPDATE WikiSessions SET CurrentWordBankId = (SELECT TOP 1 WordBankId from @wordBank)
	where WikiSessionId = @WikiSessionId

	INSERT INTO WikiSessionWordBanks(WikiSessionId,WordBankId)
	SELECT @WikiSessionId, WordBankId 
	from @wordBank

	select * 
	from @wordBank
END
