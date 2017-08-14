namespace Aho
{
    public class TrieConfig
    {

        private bool allowOverlaps = false;

        private bool onlyWholeWords = true;

        private bool onlyWholeWordsWhiteSpaceSeparated = false;

        private bool caseInsensitive = true;

        private bool stopOnHit = false;

        public bool isStopOnHit() { return stopOnHit; }

        public void setStopOnHit(bool stopOnHit) { this.stopOnHit = stopOnHit; }

        public bool isAllowOverlaps()
        {
            return allowOverlaps;
        }

        public void setAllowOverlaps(bool allowOverlaps)
        {
            this.allowOverlaps = allowOverlaps;
        }

        public bool isOnlyWholeWords()
        {
            return onlyWholeWords;
        }

        public void setOnlyWholeWords(bool onlyWholeWords)
        {
            this.onlyWholeWords = onlyWholeWords;
        }

        public bool isOnlyWholeWordsWhiteSpaceSeparated() { return onlyWholeWordsWhiteSpaceSeparated; }

        public void setOnlyWholeWordsWhiteSpaceSeparated(bool onlyWholeWordsWhiteSpaceSeparated)
        {
            this.onlyWholeWordsWhiteSpaceSeparated = onlyWholeWordsWhiteSpaceSeparated;
        }

        public bool isCaseInsensitive()
        {
            return caseInsensitive;
        }

        public void setCaseInsensitive(bool caseInsensitive)
        {
            this.caseInsensitive = caseInsensitive;
        }
    }

}

