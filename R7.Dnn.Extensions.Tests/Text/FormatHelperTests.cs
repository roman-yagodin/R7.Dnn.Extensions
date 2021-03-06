﻿//
//  FormatHelperTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Globalization;
using R7.Dnn.Extensions.Text;
using Xunit;

namespace R7.Dnn.Extensions.Tests.Text
{
    public static class FormatHelperTests
    {
        [Fact]
        public static void RemoveTrailingZeroesTest ()
        {
            var decimalSeparator = new CultureInfo ("en-US").NumberFormat.NumberDecimalSeparator;

            Assert.Equal ("12", FormatHelper.RemoveTrailingZeroes ("12.0", decimalSeparator));
            Assert.Equal ("0", FormatHelper.RemoveTrailingZeroes ("0.000", decimalSeparator));
            Assert.Equal ("12.345", FormatHelper.RemoveTrailingZeroes ("12.34500", decimalSeparator));
        }
    }
}
