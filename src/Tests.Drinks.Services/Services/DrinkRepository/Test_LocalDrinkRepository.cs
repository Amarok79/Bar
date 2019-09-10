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

using System;
using System.Linq;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;


namespace Drinks.Services.DrinkRepository
{
	[TestFixture]
	public class Test_LocalDrinkRepository
	{
		[Test]
		public async Task GetAll()
		{
			var repo = new LocalDrinkRepository("testfiles");

			var drinks = (await repo.GetAll())
				.ToList();

			Check.That(drinks)
				.HasSize(2);

			Check.That(drinks[0].Name)
				.IsEqualTo("Bramble");
			Check.That(drinks[0].Teaser)
				.IsEqualTo("Zitrone, Gin, Brombeere");
			Check.That(drinks[0].ImageId.Guid)
				.IsEqualTo(new Guid("{41E7903D-086C-4E4B-A4BA-869248A2DB25}"));

			Check.That(drinks[1].Name)
				.IsEqualTo("Singapore Sling");
			Check.That(drinks[1].Teaser)
				.IsEqualTo("Limette, Gin, Kirschlikör, Cointreau, Ananas");
			Check.That(drinks[1].ImageId.Guid)
				.IsEqualTo(new Guid("{50B3EA34-2FE3-47D2-8498-C956687DE2FB}"));
		}
	}
}