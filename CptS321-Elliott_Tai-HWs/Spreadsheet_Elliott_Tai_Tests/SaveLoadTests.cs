// <copyright file="SaveLoadTests.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Elliott_Tai_Tests
{
    /// <summary>
    /// Contains test for saving and loading the spreadsheet.
    /// </summary>
    internal class SaveLoadTests
    {
        /// <summary>
        /// Test saving a file, then loading that file.
        /// </summary>
        [Test]
        public void SaveThenLoadTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                testCell.Text = "test";
                using (MemoryStream stream = new MemoryStream())
                {
                    testSheet.Save(stream);
                    stream.Position = 0;
                    testCell.Text = "test1";
                    testSheet.Load(stream);
                }

                Assert.That(testCell.Text, Is.EqualTo("test"));
            }
        }

        /// <summary>
        /// Test if file not in the exacte format saved can be loaded.
        /// </summary>
        [Test]
        public void ExtraTagsTest()
        {
            // Simulate xml with extra tags, not the way save() formats files.
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <Spreadsheet>
              <Cell TestAtribute=""abd"" Name=""A1"">
                <tag_I_do_not_write>blah</tag_I_do_not_write>
                <BGColor>4294967295</BGColor>
                <Text>test</Text>
                <tag_I_do_not_write2>blah</tag_I_do_not_write2>
              </Cell>
            </Spreadsheet>";

            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(0, 0);
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();
                stream.Position = 0;
                testSheet.Load(stream);
            }

            if (testCell != null)
            {
                Assert.That(testCell.Text, Is.EqualTo("test"));
            }
        }

        /// <summary>
        /// Test that exeption is thrown when xml is missing and element for a cell.
        /// </summary>
        [Test]
        public void MissingElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <Spreadsheet>
              <Cell Name=""A1"">
                <BGColor>4294967295</BGColor>
              </Cell>
            </Spreadsheet>";

            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();
                stream.Position = 0;
                Assert.Throws<InvalidDataException>(() => testSheet.Load(stream));
            }
        }
    }
}
