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
using NFluent;
using NUnit.Framework;


namespace Drinks.Model
{
    [TestFixture]
    public class Test_DrinkId
    {
        public class Construction
        {
            [Test]
            public void Succeed_With_DefaultConstructor()
            {
                var id = new DrinkId();

                Check.That(id.Guid).IsEqualTo(Guid.Empty);
                Check.That(id.ToString()).IsEqualTo("{00000000-0000-0000-0000-000000000000}");
                Check.That(id.GetHashCode()).IsEqualTo(0);
                Check.That(id.DebuggerDisplay).IsEqualTo("{00000000-0000-0000-0000-000000000000}");
            }

            [Test]
            public void Succeed_With_Name()
            {
                var id = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));

                Check.That(id.Guid).IsEqualTo(Guid.Parse("83b8710937a84b818c829162c1b405b6"));
                Check.That(id.ToString()).IsEqualTo("{83B87109-37A8-4B81-8C82-9162C1B405B6}");
                Check.That(id.GetHashCode()).Not.IsEqualTo(0);
                Check.That(id.DebuggerDisplay).IsEqualTo("{83B87109-37A8-4B81-8C82-9162C1B405B6}");
            }
        }

        public class Equality
        {
            [Test]
            public void TestGetHashCode()
            {
                var id0 = new DrinkId();
                var id1 = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));
                var id2 = new DrinkId(Guid.Parse("83B8710937A84B818C829162C1B405B6"));
                var id3 = new DrinkId(Guid.Parse("5A1DF7E2450B4BE7B4F970A95D391316"));

                Check.That(id0.GetHashCode()).IsEqualTo(0);
                Check.That(id0.GetHashCode()).IsEqualTo(id0.GetHashCode());
                Check.That(id0.GetHashCode()).IsNotEqualTo(id1.GetHashCode());
                Check.That(id0.GetHashCode()).IsNotEqualTo(id2.GetHashCode());
                Check.That(id0.GetHashCode()).IsNotEqualTo(id3.GetHashCode());

                Check.That(id1.GetHashCode()).IsEqualTo(id1.GetHashCode());
                Check.That(id1.GetHashCode()).IsEqualTo(id2.GetHashCode());
                Check.That(id1.GetHashCode()).IsNotEqualTo(id3.GetHashCode());

                Check.That(id2.GetHashCode()).IsEqualTo(id2.GetHashCode());
                Check.That(id2.GetHashCode()).IsNotEqualTo(id3.GetHashCode());

                Check.That(id3.GetHashCode()).IsEqualTo(id3.GetHashCode());
            }

            [Test]
            public void TestEquals()
            {
                var id0 = new DrinkId();
                var id1 = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));
                var id2 = new DrinkId(Guid.Parse("83B8710937A84B818C829162C1B405B6"));
                var id3 = new DrinkId(Guid.Parse("5A1DF7E2450B4BE7B4F970A95D391316"));

                Check.That(id0.Equals(id0)).IsTrue();
                Check.That(id0.Equals(id1)).IsFalse();
                Check.That(id0.Equals(id2)).IsFalse();
                Check.That(id0.Equals(id3)).IsFalse();

                Check.That(id1.Equals(id1)).IsTrue();
                Check.That(id1.Equals(id2)).IsTrue();
                Check.That(id1.Equals(id3)).IsFalse();

                Check.That(id2.Equals(id2)).IsTrue();
                Check.That(id2.Equals(id3)).IsFalse();

                Check.That(id3.Equals(id3)).IsTrue();

                Check.That(id3.Equals(null)).IsFalse();
                Check.That(id3.Equals(new Object())).IsFalse();
            }

            [Test]
            public void TestObjectEquals()
            {
                Object id0 = new DrinkId();
                Object id1 = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));
                Object id2 = new DrinkId(Guid.Parse("83B8710937A84B818C829162C1B405B6"));
                Object id3 = new DrinkId(Guid.Parse("5A1DF7E2450B4BE7B4F970A95D391316"));
                Object id4 = null;

                Check.That(id0.Equals(id0)).IsTrue();
                Check.That(id0.Equals(id1)).IsFalse();
                Check.That(id0.Equals(id2)).IsFalse();
                Check.That(id0.Equals(id3)).IsFalse();
                Check.That(id0.Equals(id4)).IsFalse();

                Check.That(id1.Equals(id1)).IsTrue();
                Check.That(id1.Equals(id2)).IsTrue();
                Check.That(id1.Equals(id3)).IsFalse();
                Check.That(id1.Equals(id4)).IsFalse();

                Check.That(id2.Equals(id2)).IsTrue();
                Check.That(id2.Equals(id3)).IsFalse();
                Check.That(id2.Equals(id4)).IsFalse();

                Check.That(id3.Equals(id3)).IsTrue();
                Check.That(id3.Equals(id4)).IsFalse();
            }

            [Test]
            public void TestOperatorEqual()
            {
                var id0 = new DrinkId();
                var id1 = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));
                var id2 = new DrinkId(Guid.Parse("83B8710937A84B818C829162C1B405B6"));
                var id3 = new DrinkId(Guid.Parse("5A1DF7E2450B4BE7B4F970A95D391316"));

#pragma warning disable 1718
                Check.That(id0 == id0).IsTrue();
                Check.That(id0 == id1).IsFalse();
                Check.That(id0 == id2).IsFalse();
                Check.That(id0 == id3).IsFalse();

                Check.That(id1 == id1).IsTrue();
                Check.That(id1 == id2).IsTrue();
                Check.That(id1 == id3).IsFalse();

                Check.That(id2 == id2).IsTrue();
                Check.That(id2 == id3).IsFalse();

                Check.That(id3 == id3).IsTrue();
#pragma warning restore 1718
            }

            [Test]
            public void TestOperatorUnequal()
            {
                var id0 = new DrinkId();
                var id1 = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));
                var id2 = new DrinkId(Guid.Parse("83B8710937A84B818C829162C1B405B6"));
                var id3 = new DrinkId(Guid.Parse("5A1DF7E2450B4BE7B4F970A95D391316"));

#pragma warning disable 1718
                Check.That(id0 != id0).Not.IsTrue();
                Check.That(id0 != id1).Not.IsFalse();
                Check.That(id0 != id2).Not.IsFalse();
                Check.That(id0 != id3).Not.IsFalse();

                Check.That(id1 != id1).Not.IsTrue();
                Check.That(id1 != id2).Not.IsTrue();
                Check.That(id1 != id3).Not.IsFalse();

                Check.That(id2 != id2).Not.IsTrue();
                Check.That(id2 != id3).Not.IsFalse();

                Check.That(id3 != id3).Not.IsTrue();
#pragma warning restore 1718
            }
        }

        public class TestGuid
        {
            [Test]
            public void ReturnsEmptyGuid_For_DefaultConstructor()
            {
                var id = new DrinkId();

                Check.That(id.Guid).IsEqualTo(Guid.Empty);
            }

            [Test]
            public void ReturnsCustomGuid_For_CustomGuid()
            {
                var id = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));

                Check.That(id.Guid).IsEqualTo(Guid.Parse("{83B87109-37A8-4B81-8C82-9162C1B405B6}"));
            }
        }

        public class TestToString
        {
            [Test]
            public void ReturnsZeros_For_DefaultConstructor()
            {
                var id = new DrinkId();

                Check.That(id.ToString()).IsEqualTo("{00000000-0000-0000-0000-000000000000}");
            }

            [Test]
            public void ReturnsFormatted_For_CustomGuid()
            {
                var id = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));

                Check.That(id.ToString()).IsEqualTo("{83B87109-37A8-4B81-8C82-9162C1B405B6}");
            }
        }

        public class DebuggerDisplay
        {
            [Test]
            public void ReturnsZeros_For_DefaultConstructor()
            {
                var id = new DrinkId();

                Check.That(id.DebuggerDisplay).IsEqualTo("{00000000-0000-0000-0000-000000000000}");
            }

            [Test]
            public void ReturnsFormatted_For_CustomGuid()
            {
                var id = new DrinkId(Guid.Parse("83b8710937a84b818c829162c1b405b6"));

                Check.That(id.DebuggerDisplay).IsEqualTo("{83B87109-37A8-4B81-8C82-9162C1B405B6}");
            }
        }
    }
}
