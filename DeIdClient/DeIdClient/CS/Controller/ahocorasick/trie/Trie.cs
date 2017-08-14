
using System;
using System.Collections;
using System.Collections.Generic;
using WTextAnnotator.CS.Model;

namespace Aho
{
    public class Trie
    {

        public TrieConfig trieConfig;

        private State rootState;

        public Trie()
        {
            this.trieConfig = new TrieConfig();
            this.rootState = new State();
        }

        public Trie(TrieConfig trieConfig)
        {
            this.trieConfig = trieConfig;
            this.rootState = new State();
        }
        public void addKeyword(ArrayList keywords)
        {
            foreach (string term in keywords)
            {
                addKeyword(term);
            }
        }

        public void addKeyword(string keyword)
        {
            if (keyword == null || keyword.Length == 0)
            {
                return;
            }
            State currentState = this.rootState;
            foreach (char c in keyword.ToCharArray())
            {
                char character = c;
                if (trieConfig.isCaseInsensitive())
                {
                    character = Char.ToLower(c);
                }
                currentState = currentState.addState(character);
            }
            currentState.addEmit(trieConfig.isCaseInsensitive() ? keyword.ToLower() : keyword);
        }

        public ArrayList tokenize(string text)
        {

            ArrayList tokens = new ArrayList();

            ArrayList collectedEmits = parseText(text);
            int lastCollectedPosition = -1;
            foreach (Emit emit in collectedEmits)
            {
                if (emit.getStart() - lastCollectedPosition > 1)
                {
                    tokens.Add(createFragment(emit, text, lastCollectedPosition));
                }
                tokens.Add(createMatch(emit, text));
                lastCollectedPosition = emit.getEnd();
            }
            if (text.Length - lastCollectedPosition > 1)
            {
                tokens.Add(createFragment(null, text, lastCollectedPosition));
            }

            return tokens;
        }

        private Token createFragment(Emit emit, String text, int lastCollectedPosition)
        {
            return new FragmentToken(text.Substring(lastCollectedPosition + 1, emit == null ? text.Length : emit.getStart()));
        }

        private Token createMatch(Emit emit, String text)
        {
            return new MatchToken(text.Substring(emit.getStart(), emit.getEnd() + 1), emit);
        }


        public ArrayList parseText(string text)
        {
            DefaultEmitHandler emitHandler = new DefaultEmitHandler();
            parseText(text, emitHandler);

            ArrayList collectedEmits = emitHandler.getEmits();

            if (trieConfig.isOnlyWholeWords())
            {
                removePartialMatches(text, collectedEmits);
            }

            if (trieConfig.isOnlyWholeWordsWhiteSpaceSeparated())
            {
                removePartialMatchesWhiteSpaceSeparated(text, collectedEmits);
            }

            if (!trieConfig.isAllowOverlaps())
            {
                IntervalTree2 intervalTree = new IntervalTree2(collectedEmits);
                intervalTree.removeOverlaps(collectedEmits);
            }

            return collectedEmits;
        }
        public ArrayList annotateText(string text,String label)
        {
            ArrayList emits = parseText(text);
            ArrayList anns = new ArrayList();
            foreach (Emit e1 in emits)
            {
                Annotation ann = new Annotation(e1.getStart(), e1.getEnd() + 1, label, text.Substring(e1.getStart(), e1.getEnd() + 1 - e1.getStart()));
                anns.Add(ann);
            }
            return anns;
        }

        public bool containsMatch(string text)
        {
            Emit firstMatch1 = firstMatch(text);
            return firstMatch1 != null;
        }

        public void parseText(string text, EmitHandler emitHandler)
        {
            State currentState = this.rootState;
            for (int position = 0; position < text.Length; position++)
            {
                char character = text[position];
                if (trieConfig.isCaseInsensitive())
                {
                    character = Char.ToLower(character);
                }
                currentState = getState(currentState, character);
                if (storeEmits(position, currentState, emitHandler) && trieConfig.isStopOnHit())
                {
                    return;
                }
            }

        }

        public Emit firstMatch(string text)
        {
            if (!trieConfig.isAllowOverlaps())
            {
                // Slow path. Needs to find all the matches to detect overlaps.
                ArrayList parseText1 = parseText(text);
                if (parseText1 != null && parseText1.Count != 0)
                {
                    return (Emit)parseText1[0];
                }
            }
            else
            {
                // Fast path. Returns first match found.
                State currentState = this.rootState;
                for (int position = 0; position < text.Length; position++)
                {
                    char character = text[position];
                    if (trieConfig.isCaseInsensitive())
                    {
                        character = Char.ToLower(character);
                    }
                    currentState = getState(currentState, character);
                    HashSet<string> emitStrs = currentState.emit();
                    if (emitStrs != null && emitStrs.Count != 0)
                    {
                        foreach (String emitStr in emitStrs)
                        {
                            Emit emit = new Emit(position - emitStr.Length + 1, position, emitStr);
                            if (trieConfig.isOnlyWholeWords())
                            {
                                if (!isPartialMatch(text, emit))
                                {
                                    return emit;
                                }
                            }
                            else
                            {
                                return emit;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private bool isPartialMatch(string searchText, Emit emit)
        {
            return (emit.getStart() != 0 &&
                Char.IsLetter(searchText[emit.getStart() - 1])) ||
                (emit.getEnd() + 1 != searchText.Length &&
                Char.IsLetter(searchText[emit.getEnd() + 1]));
        }

        private void removePartialMatches(string searchText, ArrayList collectedEmits)
        {
            ArrayList removeEmits = new ArrayList();
            foreach (Emit emit in collectedEmits)
            {
                if (isPartialMatch(searchText, emit))
                {
                    removeEmits.Add(emit);
                }
            }
            foreach (Emit removeEmit in removeEmits)
            {
                collectedEmits.Remove(removeEmit);
            }
        }

        private void removePartialMatchesWhiteSpaceSeparated(string searchText, ArrayList collectedEmits)
        {
            long size = searchText.Length;
            ArrayList removeEmits = new ArrayList();
            foreach (Emit emit in collectedEmits)
            {
                if ((emit.getStart() == 0 || Char.IsWhiteSpace(searchText[(emit.getStart() - 1)])) &&
                    (emit.getEnd() + 1 == size || Char.IsWhiteSpace(searchText[(emit.getEnd() + 1)])))
                {
                    continue;
                }
                removeEmits.Add(emit);
            }
            foreach (Emit removeEmit in removeEmits)
            {
                collectedEmits.Remove(removeEmit);
            }
        }

        private State getState(State currentState, char character)
        {
            State newCurrentState = currentState.nextState(character);
            while (newCurrentState == null)
            {
                currentState = currentState.getFailure();
                newCurrentState = currentState.nextState(character);
            }
            return newCurrentState;
        }

        public void finalize()
        {
            Queue<State> queue = new Queue<State>();

            // First, set the fail state of all depth 1 states to the root state
            foreach (State depthOneState in this.rootState.getStates())
            {
                depthOneState.setFailure(this.rootState);
                queue.Enqueue(depthOneState);
            }

            // Second, determine the fail state for all depth > 1 state
            while (queue.Count != 0)
            {
                State currentState = queue.Dequeue();

                foreach (char transition in currentState.getTransitions())
                {
                    State targetState = currentState.nextState(transition);
                    queue.Enqueue(targetState);

                    State traceFailureState = currentState.getFailure();
                    while (traceFailureState.nextState(transition) == null)
                    {
                        traceFailureState = traceFailureState.getFailure();
                    }
                    State newFailureState = traceFailureState.nextState(transition);
                    targetState.setFailure(newFailureState);
                    targetState.addEmit(newFailureState.emit());
                }
            }
        }

        private bool storeEmits(int position, State currentState, EmitHandler emitHandler)
        {
            bool emitted = false;
            HashSet<String> emits = currentState.emit();
            if (emits != null && emits.Count != 0)
            {
                foreach (String emit in emits)
                {
                    emitHandler.emit(new Emit(position - emit.Length + 1, position, emit));
                    emitted = true;
                }
            }
            return emitted;
        }

        public static TrieBuilder builder()
        {
            return new TrieBuilder();
        }

        public class TrieBuilder
        {

            private TrieConfig trieConfig = new TrieConfig();

            private Trie trie = new Trie(new TrieConfig());

            public TrieBuilder() { }

            public TrieBuilder caseInsensitive()
            {
                this.trieConfig.setCaseInsensitive(true);
                return this;
            }

            public TrieBuilder removeOverlaps()
            {
                this.trieConfig.setAllowOverlaps(false);
                return this;
            }

            public TrieBuilder onlyWholeWords()
            {
                this.trieConfig.setOnlyWholeWords(true);
                return this;
            }

            public TrieBuilder onlyWholeWordsWhiteSpaceSeparated()
            {
                this.trieConfig.setOnlyWholeWordsWhiteSpaceSeparated(true);
                return this;
            }

            public TrieBuilder addKeyword(String keyword)
            {
                trie.addKeyword(keyword);
                return this;
            }

            public TrieBuilder stopOnHit()
            {
                trie.trieConfig.setStopOnHit(true);
                return this;
            }

            public Trie build()
            {
                trie.finalize();
                return trie;
            }
        }
    }
}

