using System;
using System.Collections.Generic;

namespace ArchitectureTest1.Game
{
    internal sealed class Pile
    {
        internal IReadOnlyList<PlayingCard> Cards { get; }

        private int? _facedIndex;

        private readonly int _countToFace;

        internal Pile(int countToFace, IReadOnlyList<PlayingCard> cards)
        {
            _countToFace = countToFace;
            Cards = cards;
        }

        internal bool IsPileEmpty => Cards.Count == 0 || _facedIndex != null && _facedIndex.Value + _countToFace >= Cards.Count;

        internal List<PlayingCard> Faced
        {
            get
            {
                var result = new List<PlayingCard>();
                if (_facedIndex == null) return result;

                int startIndex = _facedIndex.Value;
                int endIndex = Math.Min(startIndex + _countToFace, Cards.Count);
                for (int i = startIndex; i < endIndex; i++)
                {
                    result.Add(Cards[i]);
                }
                return result;
            }
        }

        internal void FaceNext()
        {
            if (_facedIndex != null && _facedIndex.Value + _countToFace < Cards.Count)
            {
                _facedIndex += _countToFace;
            }
            else if (_facedIndex == null && Cards.Count > 0)
            {
                _facedIndex = 0;
            }
            else if (IsPileEmpty)
            {
                _facedIndex = null;
            }
        }
    }
}
