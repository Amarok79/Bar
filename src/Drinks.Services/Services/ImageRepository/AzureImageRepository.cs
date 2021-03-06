﻿/* MIT License
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
using System.Globalization;
using System.Threading.Tasks;
using Drinks.Model;


namespace Drinks.Services.ImageRepository
{
    /// <summary>
    /// An implementation that loads images from Azure.
    /// </summary>
    public sealed class AzureImageRepository : IImageRepository
    {
        /// <summary>
        /// Gets the source URI for the given image.
        /// </summary>
        public Task<Uri> GetById(ImageId id)
        {
            var fileName = id.Guid.ToString("D", CultureInfo.InvariantCulture).ToUpperInvariant();

            var uri = new Uri($"https://amarok.blob.core.windows.net/drinks/{fileName}.jpg");

            return Task.FromResult(uri);
        }
    }
}
