using Microgroove;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        List<List<string>> normalcsvFile = new List<List<string>>();
        List<List<string>> csvFileWithWrongLineTypes = new List<List<string>>();
        List<List<string>> csvFileWithWordsWhereDatesShouldBe = new List<List<string>>();
        
        [Test]
        public void NormalCsvFile()
        {
            Utility util = new Utility();
            normalcsvFile = util.ReadAllLinesIntoAList("file.csv");
            string json = util.ProcessLines(normalcsvFile);

            string previousJson = @"{
  ""date"": ""2018-08-04T00:00:00"",
  ""type"": "" By Batch #"",
  ""orders"": [
    {
      ""date"": ""2018-08-04T00:00:00"",
      ""code"": ""ONF002793300"",
      ""number"": ""080427bd1"",
      ""buyer"": {
        ""name"": ""Brett Nagy"",
        ""street"": ""5825 221st Place S.E."",
        ""zip"": ""98027""
      },
      ""items"": [
        {
          ""sku"": ""602527788265"",
          ""qty"": 2
        },
        {
          ""sku"": ""602517642850"",
          ""qty"": 1
        }
      ],
      ""timings"": {
        ""start"": 3,
        ""stop"": 3,
        ""gap"": 0,
        ""offset"": 2,
        ""pause"": 0
      }
    },
    {
      ""date"": ""2018-08-07T00:00:00"",
      ""code"": ""ONF002793399"",
      ""number"": ""080427b99"",
      ""buyer"": {
        ""name"": ""Steven Cox"",
        ""street"": ""123 Main St."",
        ""zip"": ""98188""
      },
      ""items"": [
        {
          ""sku"": ""602527788999"",
          ""qty"": 2
        },
        {
          ""sku"": ""602517642999"",
          ""qty"": 5
        }
      ],
      ""timings"": {
        ""start"": 1,
        ""stop"": 2,
        ""gap"": 3,
        ""offset"": 4,
        ""pause"": 5
      }
    }
  ],
  ""ender"": {
    ""Process"": 1,
    ""Paid"": 2,
    ""Created"": 9
  }
}";
            Assert.AreEqual(previousJson, json);
        }

        [Test]
        public void CsvFileWithWrongLineTypes()
        {
            Utility util = new Utility();
            csvFileWithWrongLineTypes = util.ReadAllLinesIntoAList("filewithincorrectlinetype.csv");
            var ex = Assert.Throws<WrongLineTypeException>(() => util.ProcessLines(csvFileWithWrongLineTypes));

            //Line 4 is where I put the Q
            Assert.That(ex.Message == "Wrong type of line at line number 4");
        }

        [Test]
        public void CsvFileWithWordsWhereDatesShouldBeInOrderLine()
        {
            Utility util = new Utility();
            csvFileWithWordsWhereDatesShouldBe = util.ReadAllLinesIntoAList("filewithwordswheredatesshouldbe.csv");
            var ex = Assert.Throws<Exception>(() => util.ProcessLines(csvFileWithWordsWhereDatesShouldBe));

            //Where I put NotADate
            Assert.That(ex.Message == "Date parsing error at order line number 7");
        }

    }
}