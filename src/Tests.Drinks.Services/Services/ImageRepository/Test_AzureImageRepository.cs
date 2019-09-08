// Copyright © Anton Paar GmbH, 2019

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
				.IsEqualTo(new Uri("https://amarok.blob.core.windows.net/drinks/{E8608B08-4309-47EE-88FF-582301A48222}.jpg"));
		}
	}
}
