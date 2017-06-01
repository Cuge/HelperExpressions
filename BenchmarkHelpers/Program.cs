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
        private Func<ClassForTest, int> _deleAG;
        private Func<ClassForTest, string> _deleBG;
        private Func<ClassForTest, float> _deleCG;
        private Func<ClassForTest, DateTime> _deleDG;


        //[Params( new ClassForTest[] { _x, _y, _xA, _xB, _xC, _xD })]
        ClassForTest X {get; set;}

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

            _deleAG = (Func<ClassForTest, int>)_infos.GetPropertyDelegate<ClassForTest>("A");
            _deleBG = (Func<ClassForTest, string>)_infos.GetPropertyDelegate<ClassForTest>("B");
            _deleCG = (Func<ClassForTest, float>)_infos.GetPropertyDelegate<ClassForTest>("C");
            _deleDG = (Func<ClassForTest, DateTime>)_infos.GetPropertyDelegate<ClassForTest>("D");

        }



        [Benchmark]
        public void TestDelegateGet()
        {
            _infos = new ClassInfos();
            _deleAG = (Func<ClassForTest, int>)_infos.GetPropertyDelegate<ClassForTest>("A");
            _deleBG = (Func<ClassForTest, string>)_infos.GetPropertyDelegate<ClassForTest>("B");
            _deleCG = (Func<ClassForTest, float>)_infos.GetPropertyDelegate<ClassForTest>("C");
            _deleDG = (Func<ClassForTest, DateTime>)_infos.GetPropertyDelegate<ClassForTest>("D");

            var reA = _deleAG(_x);
            var reB = _deleBG(_x);
            var reC = _deleCG(_x);
            var reD = _deleDG(_x);
            reA = _deleAG(_xA);
            reB = _deleBG(_xA);
            reC = _deleCG(_xA);
            reD = _deleDG(_xA);

            reA = _deleAG(_xB);
            reB = _deleBG(_xB);
            reC = _deleCG(_xB);
            reD = _deleDG(_xB);

            reA = _deleAG(_xC);
            reB = _deleBG(_xC);
            reC = _deleCG(_xC);
            reD = _deleDG(_xC);

            reA = _deleAG(_xD);
            reB = _deleBG(_xD);
            reC = _deleCG(_xD);
            reD = _deleDG(_xD);

            reA = _deleAG(_y);
            reB = _deleBG(_y);
            reC = _deleCG(_y);
            reD = _deleDG(_y);

        }

        [Benchmark]
        public void Test1()
        {
            _infos = new ClassInfos();
            var reA = _deleA(_x);
            var reB = _deleB(_x);
            var reC = _deleC(_x);
            var reD = _deleD(_x);
            reA =_deleA(_xA);
            reB =_deleB(_xA);
            reC =_deleC(_xA);
            reD =_deleD(_xA);

            reA =_deleA(_xB);
            reB =_deleB(_xB);
            reC =_deleC(_xB);
            reD =_deleD(_xB);

            reA =_deleA(_xC);
            reB =_deleB(_xC);
            reC =_deleC(_xC);
            reD =_deleD(_xC);

            reA =_deleA(_xD);
            reB =_deleB(_xD);
            reC =_deleC(_xD);
            reD =_deleD(_xD);

            reA =_deleA(_y);
            reB =_deleB(_y);
            reC =_deleC(_y);
            reD =_deleD(_y);

        }
        [Benchmark(Baseline = true)]
        public void TestDirect()
        {
            var reA = _x.A;
            var reB = _x.B;
            var reC = _x.C;
            var reD = _x.D;
            reA = _xA.A;
            reB = _xA.B;
            reC = _xA.C;
            reD = _xA.D;
            reA = _xB.A;
            reB = _xB.B;
            reC = _xB.C;
            reD = _xB.D;
            reA = _xC.A;
            reB = _xC.B;
            reC = _xC.C;
            reD = _xC.D;
            reB = _xD.B;
            reA = _xD.A;
            reC = _xD.C;
            reD = _xD.D;
            reA = _y.A;
            reB = _y.B;
            reC = _y.C;
            reD = _y.D;

        }
//        [Benchmark]
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