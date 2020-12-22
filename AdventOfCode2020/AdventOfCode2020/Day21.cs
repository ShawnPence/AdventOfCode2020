using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day21
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day21.txt");
			
			//day 21 puzzle - 
			//puzzle input is a list of made-up nonsense words ("ingredients") 
			//along with a list of "allergens" for each list of ingredients

			//go through list and figure out which ingredients cannot possibly be an allergen (part 1)

			//note: per puzzle rules, if the allergen is listed, the made-up ingredient that 
			//matches it will always appear in that same line, but an ingredient
			//that is an allergen may or may not have the allergen listed in that line

			Dictionary<string, HashSet<string>> allergens = new Dictionary<string, HashSet<string>>();
			Dictionary<string, int> ingredientCount = new Dictionary<string, int>();

			foreach(var line in input)
			{
				var lineIngredients = line.Substring(0,line.IndexOf("(")).Split(' ');
				var lineAllergens = line.Substring(line.IndexOf("contains") + 9).Replace(",", "")[0..^1].Split(' ');
				foreach(var ingredient in lineIngredients)
				{
					
					if (!ingredientCount.ContainsKey(ingredient)) ingredientCount[ingredient] = 0;
					ingredientCount[ingredient]++;
				}

				foreach(var allergen in lineAllergens)
				{
					if (!allergens.ContainsKey(allergen))
					{
						allergens[allergen] = new HashSet<string>(lineIngredients);
					}
					else
					{
						//remove any items from previous set that are not in current list
						allergens[allergen].IntersectWith(lineIngredients);
					}
				}

			}

			HashSet<string> possibleAllergenIngredients = new HashSet<string>();

			foreach(var allergen in allergens.Keys)
			{
				possibleAllergenIngredients.UnionWith(allergens[allergen]);
			}


			long result1 = 0;
			foreach (var ingredient in ingredientCount.Keys)
			{
				if (!possibleAllergenIngredients.Contains(ingredient)) result1 += ingredientCount[ingredient];
			}

			Console.WriteLine($"Part 1: {result1}");

			//part 2:

			Isolate(allergens);

			List<string> allergenList = new List<string>(allergens.Keys);
			allergenList.Sort();

			string result2 = "";
			foreach (var ingredient in allergens[allergenList[0]]) result2 += ingredient;
			for(int i = 1; i < allergenList.Count; i++)
			{
				foreach (var ingredient in allergens[allergenList[i]]) result2 += "," + ingredient;
			}

			Console.WriteLine($"Part 2: {result2}");
		}

		public static void Isolate(Dictionary<string, HashSet<string>> allergens)
		{
			var count = 1;
			HashSet<string> isolated = new HashSet<string>();
			isolated.Add("");
			while (count > 0)
			{
				count = 0;
				foreach (var allergen in allergens.Keys)
				{
					if (allergens[allergen].Count == 1)
					{
						//if there is only one item left, that must be the correct allergen and made up ingredient pair
						isolated.UnionWith(allergens[allergen]);
					}
					else
					{
						//remove any ingredient that has already been isolated to another allergen
						allergens[allergen].ExceptWith(isolated);
						count++;
					}
				}
			}

		}

	}

	
}
