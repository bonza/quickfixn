﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using QuickFIX.NET;

namespace UnitTests
{
    [TestFixture]
    public class DataDictionaryTests
    {
        [Test]
        public void FieldEntryTest()
        {
            QuickFIX.NET.DataDictionary dd = new QuickFIX.NET.DataDictionary();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.MajorVersion, Is.EqualTo("4"));
            Assert.That(dd.MinorVersion, Is.EqualTo("4"));
            Assert.That(dd.Version, Is.EqualTo("FIX.4.4"));
        }

        [Test]
        public void MakeFromType()
        {
            DataDictionary dd = new DataDictionary();
            dd.Load("../../../spec/fix/FIX44.xml");
            Type ft = dd.GetFieldType(1);
            QuickFIX.NET.Fields.StringField field = (QuickFIX.NET.Fields.StringField)Activator.CreateInstance(dd.GetFieldType(1),1);
            field.Tag = 1;
            field.Obj = "2";
            Assert.That(field.Tag, Is.EqualTo(1));
            Assert.That(field.Obj, Is.EqualTo("2"));
        }

        [Test]
        public void ValidFieldTagTest()
        {
            DataDictionary dd = new DataDictionary();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.ValidFieldTag(1), Is.EqualTo(true));
            Assert.That(dd.ValidFieldTag(742), Is.EqualTo(true));
            Assert.That(dd.ValidFieldTag(203948), Is.EqualTo(false));
        }

        [Test]
        public void GetTagFromNameTest()
        {
            DataDictionary dd = new DataDictionary();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.GetFieldTagFromName("Account"), Is.EqualTo(1));
            Assert.That(dd.GetFieldTagFromName("OfferForwardPoints"), Is.EqualTo(191));
            Assert.Throws(typeof(FieldNotFoundException),
                delegate { dd.GetFieldTagFromName("wayner___"); });
        }
    }
}