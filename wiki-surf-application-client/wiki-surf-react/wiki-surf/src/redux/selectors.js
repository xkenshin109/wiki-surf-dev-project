export const getSessionState = store => store ? store.session : {};
export const getWikiSessionId = store => store.session.WikiSessionId !== ''? store.session.WikiSessionId : 'N/A';
export const getStartingWord = store => store.wikiEngine.wikiContent.startingWord !== undefined ? store.wikiEngine.wikiContent.startingWord.Word : 'N/A';
export const getEndingWord = store => store.wikiEngine.wikiContent.endingWord ? store.wikiEngine.wikiContent.endingWord : 'N/A';
export const getTotalClicks = store => store.wikiEngine.TotalClicks ? store.wikiEngine.TotalClicks : 0;
export const getPlayerName = store => store.session.PlayerName ? store.session.PlayerName : 'N/A';