#region License

// Copyright (C) 2012-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Paraiba.Collections.Generic;

namespace WordFrequencyChecker {
	public class FrequencyCounter {
		private static Dictionary<string, int> _wordDict1;
		private static Dictionary<Tuple<string, string>, int> _wordDict2;
		private static Dictionary<Tuple<string, string, string>, int> _wordDict3;

		private static List<CountedWord<string>> _sortedWords;
		private static List<CountedWord<Tuple<string, string>>> _sortedWords2;
		private static List<CountedWord<Tuple<string, string, string>>> _sortedWords3;

		public FrequencyCounter() {
			_wordDict1 = new Dictionary<string, int>();
			_wordDict2 = new Dictionary<Tuple<string, string>, int>();
			_wordDict3 = new Dictionary<Tuple<string, string, string>, int>();
			_sortedWords = new List<CountedWord<string>>();
			_sortedWords2 = new List<CountedWord<Tuple<string, string>>>();
			_sortedWords3 = new List<CountedWord<Tuple<string, string, string>>>();
		}

		public List<CountedWord<string>> SortedWords {
			get { return _sortedWords; }
		}

		public List<CountedWord<Tuple<string, string>>> SortedWords2 {
			get { return _sortedWords2; }
		}

		public List<CountedWord<Tuple<string, string, string>>> SortedWords3 {
			get { return _sortedWords3; }
		}

		public void ConstructDictionaries(FileInfo pdfFile) {
			var words = PdfReader.ReadWordsFromPdfFile(pdfFile);
			var tail2 = String.Empty;
			var tail = String.Empty;
			foreach (var word in Where(words)) {
				_wordDict1.Increment(word);
				_wordDict2.Increment(Tuple.Create(tail, word));
				_wordDict3.Increment(Tuple.Create(tail2, tail, word));
				UpdateTails(word, ref tail, ref tail2);
			}
		}

		private static IEnumerable<string> Where(string[] words) {
			return words
				.Select(w => w.EndsWith(",") ? w.Substring(0, w.Length - 1) : w);
		}

		public void ConstructSortedWords(FileInfo pdfFile) {
			var wordSet1 = new HashSet<string>();
			var wordSet2 = new HashSet<Tuple<string, string>>();
			var wordSet3 = new HashSet<Tuple<string, string, string>>();

			var words = PdfReader.ReadWordsFromPdfFile(pdfFile);
			var tail2 = String.Empty;
			var tail = String.Empty;
			foreach (var word in Where(words)) {
				wordSet1.Add(word);
				if (!string.IsNullOrEmpty(tail)) {
					wordSet2.Add(Tuple.Create(tail, word));
					if (!string.IsNullOrEmpty(tail2)) {
						wordSet3.Add(Tuple.Create(tail2, tail, word));
					}
				}
				UpdateTails(word, ref tail, ref tail2);
			}
			foreach (var word in wordSet1) {
				SortedWords.Add(CountedWord.Create(_wordDict1.GetValueOrDefault(word), word));
			}
			foreach (var word in wordSet2) {
				SortedWords2.Add(CountedWord.Create(_wordDict2.GetValueOrDefault(word), word));
			}
			foreach (var word in wordSet3) {
				SortedWords3.Add(CountedWord.Create(_wordDict3.GetValueOrDefault(word), word));
			}
			SortedWords.Sort();
			SortedWords2.Sort();
			SortedWords3.Sort();
		}

		private static void UpdateTails(string word, ref string tail, ref string tail2) {
			if (word.EndsWith(".")) {
				tail2 = String.Empty;
				tail = String.Empty;
			} else {
				tail2 = tail;
				tail = word;
			}
		}
	}
}