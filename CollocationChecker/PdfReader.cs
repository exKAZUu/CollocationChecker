using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WordFrequencyChecker {
	public class PdfReader {
		public static string[] ReadWordsFromPdfFile(FileInfo pdfFile) {
			var text = ReadTextFromPdfFile(pdfFile);
			return text.Split(
					new[] { ' ', '\t', '　', '\r', '\n' },
					StringSplitOptions.RemoveEmptyEntries);
		}

		public static string ReadTextFromPdfFile(FileInfo pdfFile) {
			var textFile = new FileInfo(Path.ChangeExtension(pdfFile.FullName, ".txt"));
			if (!textFile.Exists) {
				var info = new ProcessStartInfo {
						FileName = "pdftotext.exe",
						Arguments = "-enc UTF-8 \"" + pdfFile.FullName + "\"",
						CreateNoWindow = true,
						UseShellExecute = false,
				};
				var proc = Process.Start(info);
				proc.WaitForExit();
			}
			var text = File.ReadAllText(textFile.FullName, Encoding.UTF8);
			text = text.Replace("ﬁ", "fi")
					.Replace("ﬂ", "fl")
					.Replace("ﬀ", "ff")
					.Replace("ﬃ", "ffi")
					.Replace("’", "'");
			return text;
		}
	}
}