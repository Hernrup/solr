﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;

namespace Tests
{
    [TestClass]
    public class AddRecords
    {
        [TestMethod]
        public void AddRecord()
        {
            Actions.addRecord(@"http://localhost:8983/solr/lime/", "classname", 001, "Some content");
        }
    }
}
