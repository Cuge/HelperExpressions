using System.Text;
using HelperExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
            var info = new ClassInfos();
            var method = info.GetPropertyFunc<ClassForTest>("A");
            var actual = (int)method((object)_classForTest);
            Assert.AreEqual(101, actual);
        }

        // Slow Took 12 secs
        // [TestMethod]
        public void GetPropertyGetMethodInfoLoadTestInt()
        {
            var info = new ClassInfos();
            var method = info.GetPropertyGetMethodInfo<ClassForTest>("A");
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                var actual = (int)method.Invoke(_classForTest, null);
                if (actual != 101)
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }

        [TestMethod]
        public void GetPropertyDelegateLoadTestInt()
        {
            var info = new ClassInfos();
            var method = info.GetPropertyDelegate<ClassForTest>("A") as Func<ClassForTest, int>;
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                //var me = Convert.ChangeType(method, typeof(Func<ClassForTest, int>));
                var actual = method(_classForTest);
                if ((int)actual != 101)
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }

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
            var info = new ClassInfos();
            var method = info.GetPropertyFunc<ClassForTest>("A");
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                var actual = (int)method(_classForTest);
                if (actual != 101)
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }
        [TestMethod]
        public void GetPropertyFuncLoadTestString()
        {
            var info = new ClassInfos();
            var method = info.GetPropertyFunc<ClassForTest>("B");
            for (int i = 0; i < _loadTestLoopCount; i++)
            {
                var actual = method(_classForTest);
                if (!"qwertyuiop".Equals(actual))
                    throw new AssertFailedException($"Result was wrong {actual}");
            }
            //Assert.AreEqual(101, actual);
        }


        // Too Slow 13Sec for 100,000,000
        //[TestMethod]
        //public void GetPropertyInfoTest()
        //{
        //    var info = new ClassInfos<ClassForTest>();
        //    var propInfo = info.GetPropertyInfo("A");
        //    for (int i = 0; i < _loadTestLoopCount; i++)
        //    {
        //        var actual =(int)propInfo.GetValue(_classForTest);
        //        if (actual != 101)
        //            throw new AssertFailedException($"Result was wrong {actual}");
        //    }
        //    //Assert.AreEqual(101, actual);
        //}

    }


    public class ClassForTest
    {
        public int A { get; set; }
        public string B { get; set; }
        public float C { get; set; }
        public DateTime D { get; set; }
    }
}
