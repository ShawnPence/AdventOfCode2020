using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day04
	{
		public static void Problems()
		{
			//both parts of day 4 need similar logic, so added part 2 logic to the part 1 method instead of creating 2 methods

			var inputs = FileReader.ReadLines("Inputs\\day4.txt");
			inputs.Add(""); //add a blank line at the end to make processing loop correctly process the last entry

			long result1 = 0;
			long result2 = 0;

			//test for all 7 required pieces for part 1
			bool b = false;
			bool i = false;
			bool e = false;
			bool h = false;
			bool hc = false;
			bool ec = false;
			bool p = false;

			//test for all 7 required pieces for part 2
			bool b2 = false;
			bool i2 = false;
			bool e2 = false;
			bool h2 = false;
			bool hc2 = false;
			bool ec2 = false;
			bool p2 = false;

			foreach (var line in inputs)
			{
				if (line.Trim() != "")
				{
					var fields = line.Split(' ');
					foreach (var field in fields)
					{
						var fieldType = field.Split(':')[0];
						var fieldValue = field.Split(':')[1].Trim();
						switch (fieldType)
						{
							case "byr":
								b = true;
								if (fieldValue.Length == 4 && int.TryParse(fieldValue, out _) && int.Parse(fieldValue) >= 1920 && int.Parse(fieldValue) <= 2002) b2 = true;
								break;
							case "iyr":
								i = true;
								if (fieldValue.Length == 4 && int.TryParse(fieldValue, out _) && int.Parse(fieldValue) >= 2010 && int.Parse(fieldValue) <= 2020) i2 = true;
								break;
							case "eyr":
								e = true;
								if (fieldValue.Length == 4 && int.TryParse(fieldValue, out _) && int.Parse(fieldValue) >= 2020 && int.Parse(fieldValue) <= 2030) e2 = true;
								break;
							case "hgt":
								h = true;
								var hNum = fieldValue.Substring(0, fieldValue.Length - 2);
								if (fieldValue.EndsWith("cm"))
								{
									if (int.TryParse(hNum, out _) && int.Parse(hNum) >= 150 && int.Parse(hNum) <= 193) h2 = true;
								}
								else if (fieldValue.EndsWith("in"))
								{
									if (int.TryParse(hNum, out _) && int.Parse(hNum) >= 59 && int.Parse(hNum) <= 76) h2 = true;
								}
								break;
							case "hcl":
								hc = true;

								if (fieldValue.Length == 7 && fieldValue[0] == '#')
								{
									bool valid = true;
									for (int j = 1; j < 7; j++)
									{
										if (!((fieldValue[j] - '0' >= 0 && fieldValue[j] - '0' <= 9) || (fieldValue[j] - 'a' >= 0 && fieldValue[j] - 'a' <= 6))) valid = false;
									}
									if (valid) hc2 = true;
								}
								break;
							case "ecl":
								ec = true;
								if (fieldValue == "amb" || fieldValue == "blu" || fieldValue == "brn" || fieldValue == "gry" || fieldValue == "grn" || fieldValue == "hzl" || fieldValue == "oth") ec2 = true;
								break;
							case "pid":
								p = true;
								if (fieldValue.Length == 9 && int.TryParse(fieldValue, out _)) p2 = true;
								break;
							default:
								break;

						} 

					} 

				}
				else
				{
				
					if (b && i && e && h && hc && ec && p) result1++;
					if (b2 && i2 && e2 && h2 && hc2 && ec2 && p2) result2++;

					b = false;
					i = false;
					e = false;
					h = false;
					hc = false;
					ec = false;
					p = false;

					b2 = false;
					i2 = false;
					e2 = false;
					h2 = false;
					hc2 = false;
					ec2 = false;
					p2 = false;
				}

			}

			Console.WriteLine($"Part 1: {result1}");
			Console.WriteLine($"Part 2: {result2}");
		}
	}
}
