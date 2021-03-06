﻿using System;
using System.Collections.Generic;

namespace R7.Text
{
    /// <summary>
    /// Class to calulate Damerau-Levenshtein distance between two strings.
    /// </summary>
    public class DamerauLevenshteinDistance: LevenshteinDistanceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.Dnn.Extensions.Text.Levenstein.DamerauLevenshteinDistance"/> class.
        /// </summary>
        /// <param name="s1">First string.</param>
        /// <param name="s2">Second string.</param>
        public DamerauLevenshteinDistance (string s1, string s2) : base (s1, s2)
        {
        }

        // TODO: Add tests
        /// <summary>
        /// Calculates the Damerau-Levenstein distance between two strings.
        /// </summary>
        /// <value>The Damerau-Levenstein distance.</value>
        public override int Distance {
            get {
                // border case processing
                if (string.IsNullOrEmpty (s1)) {
                    if (string.IsNullOrEmpty (s2)) {
                        return 0;
                    }
                    return s2.Length;
                }

                if (string.IsNullOrEmpty (s2)) {
                    return s1.Length;
                }

                var D = new int [s1.Length + 1, s2.Length + 1]; // dynamics

                // induction base
                D [0, 0] = int.MaxValue;
                for (var i = 0; i <= s1.Length; i++) {
                    D [i + 1, 1] = i;
                    D [i + 1, 0] = int.MaxValue;
                }

                for (var j = 0; j <= s2.Length; j++) {
                    D [1, j + 1] = j;
                    D [0, j + 1] = int.MaxValue;
                }

                var lastPosition = new Dictionary<char, int> ();
                foreach (var letter in (s1 + s2)) {
                    if (!lastPosition.ContainsKey (letter)) {
                        lastPosition.Add (letter, 0);
                    }
                }

                for (var i = 1; i <= s1.Length; i++) {
                    var last = 0;
                    for (var j = 1; j <= s2.Length; j++) {
                        var i2 = lastPosition [s2 [j]];
                        var j2 = last;

                        if (s1 [i] == s2 [j]) {
                            D [i + 1, j + 1] = D [i, j];
                            last = j;
                        }
                        else {
                            D [i + 1, j + 1] = Math.Min (Math.Min (D [i, j], D [i + 1, j]), D [i, j + 1] + 1);
                            D [i + 1, j + 1] = Math.Min (D [i + 1, j + 1], D [i2 + 1, j2 + 1] + (i - i2 - 1) + 1 + (j - j2 - 1));
                            lastPosition [s1 [i]] = i;
                        }
                    }
                }
                return D [s1.Length + 1, s2.Length + 1];
            }
        }
    }
}
