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
using System.IO;
using System.Linq;

namespace WordFrequencyChecker {
	internal class Program {
		private static void Main(string[] args) {
			args = new[] {
					@"C:\Users\exKAZUu\Downloads\toolsicst2013_submission_41 (3).pdf",
					@"C:\Users\exKAZUu\Dropbox\Private\Tools\English\pdfs",
			};

			var coutner = new FrequencyCounter();

			var pdfFiles = args.Skip(1).SelectMany(a => new DirectoryInfo(a).EnumerateFiles("*.pdf"));
			foreach (var pdfFile in pdfFiles) {
				Console.Write("Reading: " + pdfFile.FullName);
				coutner.ConstructDictionaries(pdfFile);
				Console.WriteLine(" ... Done");
			}
			coutner.ConstructSortedWords(new FileInfo(args[0]));

			foreach (var freqAndWord in coutner.SortedWords.Take(100)) {
				var freq = freqAndWord.Count;
				var wordSet = freqAndWord.WordSet;
				Console.WriteLine(freq + ":'" + wordSet + "'");
			}
		}
	}
}