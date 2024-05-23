using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace GrandoUniverse.Utilities {

	public static class CSVUtilities {

		public static string[][] GetCSVTable(string _csvRawData) {
			Regex regex = new Regex(",(?=(?:[^\\\"]*\\\"[^\\\"]*\\\")*[^\\\"]*$)");
			string[] rows = _csvRawData.Split("\n");
			int rowCount = rows.Length;
			string[][] table = new string[rowCount][];
			for (int i = 0; i < rowCount; i++) {
				string[] row = regex.Split(rows[i]);
				for (int j = 0; j < row.Length; j++) {
					row[j] = row[j].Replace("\r", "");
				}
				table[i] = row;

			}
			return table;
		}

	}

}