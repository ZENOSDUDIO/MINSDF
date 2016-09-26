using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using SGM.ECountJQ.UPG.BLL;

namespace SGM.ECountJQ.UPG.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int total = 0;
            int pageCount = 0;
            DateTime dtStart = DateTime.Now;
            //List<StocktakeDetails> list = StocktakeDetails.FindAll(out total, 10000, 1, out pageCount, string.Empty, null);
            List<StocktakeResultSimple> list = StocktakeResultSimple.FindAll(1933, 0, int.MaxValue);
            Console.WriteLine(list.Count);
            DateTime dtEnd = DateTime.Now;

            TimeSpan ts = dtEnd - dtStart;
            Console.WriteLine("开始时间：" + dtStart.ToLongTimeString());
            Console.WriteLine("执行时间：" + ts.TotalMilliseconds.ToString() + "毫秒");
            Console.WriteLine("结束时间：" + dtEnd.ToLongTimeString());

            Console.Read();
        }
    }
}
