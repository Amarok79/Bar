﻿/* MIT License
 * 
 * Copyright (c) 2019, Olaf Kober
 * https://bitbucket.org/Amarok/bar
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System.Linq;
using System.Threading.Tasks;
using Drinks.Model;
using NFluent;
using NUnit.Framework;


namespace Drinks.Services.DrinkRepository
{
	[TestFixture]
	public class Test_AzureBlobDrinkRepository
	{
		[Test]
		public async Task GetAll()
		{
			var repo = new AzureBlobDrinkRepository();

			var drinks = (await repo.GetAll())
				.ToList();

			Check.That(drinks.Count)
				.IsStrictlyGreaterThan(10);

			foreach (var drink in drinks)
			{
				Check.That(drink.BarId)
					.IsEqualTo(default(BarId));
				Check.That(drink.Description)
					.IsNull();
				Check.That(drink.Id)
					.IsNotEqualTo(default(DrinkId));
				Check.That(drink.Name)
					.Not.IsEmpty();
				Check.That(drink.Teaser)
					.Not.IsEmpty();
			}
		}
	}
}