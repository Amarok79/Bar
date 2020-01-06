/* MIT License
 * 
 * Copyright (c) 2019, Olaf Kober
 * https://github.com/Amarok79/Bar
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
using System.Threading.Tasks;
using Drinks.Model;
using NFluent;
using NUnit.Framework;


namespace Drinks.Services.ImageRepository
{
	[TestFixture]
	public class Test_AzureImageRepository
	{
		[Test]
		public async Task GetById()
		{
			var repo = new AzureImageRepository();
			var id = new ImageId(new Guid("{E8608B08-4309-47EE-88FF-582301A48222}"));

			Check.That(await repo.GetById(id))
				.IsEqualTo(
					new Uri("https://amarok.blob.core.windows.net/drinks/{E8608B08-4309-47EE-88FF-582301A48222}.jpg")
				);
		}
	}
}
