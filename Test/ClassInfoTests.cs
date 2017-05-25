using HelperExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperExpressionsTest
{
    [TestClass]
    public class ClassInfoTests
    {
        private int _loadTestLoopCount = 100000000;
        private ClassForTest _classForTest;

        [TestInitialize]
        public void Init()
        {
            _classForTest = new ClassForTest { A = 101, B = "qwertyuiop", C = 3.1415926f, D = DateTime.Today };
        }

        [TestMethod]
        public void GetPropertyFuncTestInt()
        {
            var info = new ClassInfos<ClassForTest>();
            var method = info.GetPropertyFunc<int>("A");
            var actual = (int)method(_classForTest);
            Assert.AreEqual(101, actual);
        }

        [TestMethod]
        public void GetPropertyGetMethodInfoLoadTestInt()
        {
            var info = new ClassInfos<ClassForTest>();
            var method = info.GetPropertyGetMethodInfo("A");
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                var actual = (int)method.Invoke(_classForTest, null);
                if (actual != 101)
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }

        //[TestMethod]
        //public void GetPropertyDelegateLoadTestInt()
        //{
        //    var info = new ClassInfos<ClassForTest>();
        //    var method = info.GetPropertyDelegate("A");
        //    for (int i = 0; i < _loadTestLoopCount; i++)
        //    {
        //        var actual = (int)method(_classForTest);
        //        if (actual != 101)
        //            throw new AssertFailedException($"Result was wrong {actual}");
        //    }
        //    //Assert.AreEqual(101, actual);
        //}
        [TestMethod]
        public void GetPropertyLoadTestInt()
        {
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                var actual = _classForTest.A;
                if (actual != 101)
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }

        [TestMethod]
        public void GetPropertyFuncLoadTestInt()
        {
            var info = new ClassInfos<ClassForTest>();
            var method = info.GetPropertyFunc<int>("A");
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                var actual = (int)method(_classForTest);
                if (actual != 101)
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }

    }


    public class ClassForTest
    {
        public int A { get; set; }
        public string B { get; set; }
        public float C { get; set; }
        public DateTime D { get; set; }
    }
}
