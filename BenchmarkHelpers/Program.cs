using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HelperExpressions;
using HelperExpressionsTest;
using System;

namespace BenchmarkHelpers
{
    class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<SimpleTest>();
        }
    }
    public class SimpleTest
    {
        private ClassInfos _infos;
        private ClassForTest _x, _y, _xA, _xB, _xC, _xD;
        private Func<ClassForTest, object> _deleA, _deleB, _deleC, _deleD;


        [Params( _x, _y, _xA, _xB, _xC, _xD ) 
        ClassForTest X;

        [Setup]
        public void SetupData()
        {
            _infos = new ClassInfos();
            _x = new ClassForTest { A = 101, B = "qwertyuiop", C = 3.1415926f, D = DateTime.Today };
            _xA = new ClassForTest { A = 10, B = "qwertyuiop", C = 3.1415926f, D = DateTime.Today };
            _xB = new ClassForTest { A = 101, B = "qwerty", C = 3.1415926f, D = DateTime.Today };
            _xC = new ClassForTest { A = 101, B = "qwertyuiop", C = 2.1415926f, D = DateTime.Today };
            _xD = new ClassForTest { A = 101, B = "qwertyuiop", C = 3.1415926f, D = DateTime.Now };
            _y = new ClassForTest { A = 101, B = "qwertyuiop", C = 3.1415926f, D = DateTime.Today };
            _deleA = _infos.GetPropertyFunc<ClassForTest>("A");
            _deleB = _infos.GetPropertyFunc<ClassForTest>("B");
            _deleC = _infos.GetPropertyFunc<ClassForTest>("C");
            _deleD = _infos.GetPropertyFunc<ClassForTest>("D");
        }

        [Benchmark]
        public void Test1()
        {
            var reA = _deleA(_x);
            var reB = _deleB(_x);
            var reC = _deleC(_x);
            var reD = _deleD(_x);
        }
        [Benchmark]
        public void TestDirect()
        {
            var reA = _x.A;
            var reB = _x.B;
            var reC = _x.C;
            var reD = _x.D;
        }
        [Benchmark]
        public void TestBoxUnbox()
        {
            int re = 101;
            object re1 = re;
            int re2 = (int)re1;

            string reB = "qwertyuiop";
            object reB1 = reB;
            string reB2 =(string)reB1;
        }
    }
}