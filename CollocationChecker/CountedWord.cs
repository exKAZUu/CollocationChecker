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

namespace WordFrequencyChecker {
	public class CountedWord<T> : IComparable<CountedWord<T>> {
		public int Count { get; set; }
		public T WordSet { get; set; }

		public int CompareTo(CountedWord<T> other) {
			return Count.CompareTo(other.Count);
		}
	}

	public static class CountedWord {
		public static CountedWord<Tuple<string, string>> Create(int count, Tuple<string, string> wordSet) {
			return new CountedWord<Tuple<string, string>> {
					Count = count,
					WordSet = wordSet,
			};
		}

		public static CountedWord<Tuple<string, string, string>> Create(
				int count, Tuple<string, string, string> wordSet) {
			return new CountedWord<Tuple<string, string, string>> {
					Count = count,
					WordSet = wordSet,
			};
		}

		public static CountedWord<string> Create(int count, string word) {
			return new CountedWord<string> {
					Count = count,
					WordSet = word,
			};
		}
	}
}