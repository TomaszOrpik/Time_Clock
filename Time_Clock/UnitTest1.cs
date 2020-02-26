using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using Time_TimePeriod;

namespace Clock_Stopper_Test
{
    [TestFixture]
    public class TimeTest
    {
        
        Time tInit;
        Time t1;
        Time t2;
        TimePeriod tp;

        [Test]
        [Category("pass")]
        [TestCase(12, 12, 21)]
        //Test correct value input
        public void TestInitFull(byte a, byte b, byte c)
        {
            tInit = new Time(a, b, c);
            NUnit.Framework.Assert.AreEqual("12:12:21",tInit.ToString());
        }
        public void TestInitMinSec(byte a, byte b)
        {
            tInit = new Time(minutes: a, seconds: b);
            NUnit.Framework.Assert.AreEqual("00:12:12", tInit.ToString());
        }
        public void TestSecOnly(byte a)
        {
            tInit = new Time(seconds: a);
            NUnit.Framework.Assert.AreEqual("00:00:12", tInit.ToString());
        }
        public void TestHourOnly(byte a)
        {
            tInit = new Time(a);
            NUnit.Framework.Assert.AreEqual("12:00:00", tInit.ToString());
        }
        [Test]
        [Category("pass")]
        [TestCase(220)]
        //secs and mins bigger than 60
        public void SecondsBiggerThan60(byte a)
        {
            tInit = new Time(seconds: a);
            NUnit.Framework.Assert.AreEqual("00:03:40", tInit.ToString());
        }
        public void MinutesBiggerThan60(byte a)
        {
            tInit = new Time(minutes: a);
            NUnit.Framework.Assert.AreEqual("03:40:00", tInit.ToString());
        }

        [Test]
        [Category("pass")]
        [TestCase(12,12,12)]
        //compare with == operator
        public void CompareTime(byte a, byte b, byte c)
        {
            t1 = new Time(a, b, c);
            t2 = new Time(a, b, c);
            NUnit.Framework.Assert.AreEqual(true, t1 == t2);
        }
        //compare with CompareTo
        public void CompareToTime(byte a, byte b, byte c)
        {
            t1 = new Time(a, b, c);
            t2 = new Time(a, b, c);
            NUnit.Framework.Assert.AreEqual(true, t1.CompareTo(t2));
        }
        [Test]
        [Category("pass")]
        [TestCase(12, 12, 12, 13, 13, 13)]
        //compare with != operator
        public void CompareTimeFalse(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            t1 = new Time(a, b, c);
            t2 = new Time(d, e, f);
            NUnit.Framework.Assert.AreEqual(true, t1 != t2);
        }
        //compare with CompareTo
        public void CompareToTimFalse(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            t1 = new Time(a, b, c);
            t2 = new Time(d, e, f);
            NUnit.Framework.Assert.AreEqual("false", t1.CompareTo(t2));
        }
        //greater/lower compare
        public void isGreater(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            t1 = new Time(a, b, c);
            t2 = new Time(d, e, f);
            NUnit.Framework.Assert.AreEqual("true", t1 < t2);
        }
        public void isLower(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            t1 = new Time(a, b, c);
            t2 = new Time(d, e, f);
            NUnit.Framework.Assert.AreEqual("false", t1 > t2);
        }
        //time+timePeriod
        [Test]
        [Category("pass")]
        [TestCase(12,12,12,1,1,1)]
        
        public void AddTimePeriod(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            t1 = new Time(a, b, c);
            tp = new TimePeriod(d, e, f);
            Time result;
            result = t1 + tp;
            NUnit.Framework.Assert.AreEqual("13:13:13", result.ToString());
        }
        public void AddTimePeriodMethod(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            t1 = new Time(a, b, c);
            tp = new TimePeriod(d, e, f);
            Time result;
            result = t1.Plus(tp);
            NUnit.Framework.Assert.AreEqual("13:13:13", result.ToString());
        }
        //exceptions test
        [Test]
        [Category("fail")]
        [TestCase(60,12,12)]
        [ExpectedException(typeof(Exception))]
         public void TooBig(byte a, byte b, byte c)
        {
            NUnit.Framework.Assert.Throws<Exception>(() => tInit = new Time(a, b, c));
        }
        [Test]
        [Category("fail")]
        [TestCase(12, 12, 256)]
        [ExpectedException(typeof(OverflowException))]
        public void NotByte(int a, int b, int c)
        {
            NUnit.Framework.Assert.Throws<OverflowException>(() => tInit = new Time(Convert.ToByte(a), Convert.ToByte(b), Convert.ToByte(c)));
            NUnit.Framework.Assert.Throws<OverflowException>(() => tInit = new Time(Convert.ToByte(a), Convert.ToByte(c), Convert.ToByte(b)));
            NUnit.Framework.Assert.Throws<OverflowException>(() => tInit = new Time(Convert.ToByte(c), Convert.ToByte(b), Convert.ToByte(a)));
        }

    }
}

