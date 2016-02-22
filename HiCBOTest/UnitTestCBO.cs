using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HiCBO;

namespace HiCBOTest
{
    [TestClass]
    public class UnitTestCBO
    {
        class CCBOTest
        {
            public string Str { get; set; }
            public DateTime DT_val { get; set; }
            public DateTime? DTNull_val { get; set; }
            public Int32 Int32_val { get; set; }
            public Int32? Int32Null_val { get; set; }
            public Int16 Int16_val { get; set; }
            public Int64 Int64_val { get; set; }
            public UInt32 UInt32_val { get; set; }
            public UInt16 UInt16_val { get; set; }
            public UInt64 UInt64_val { get; set; }
            public double Double_val { get; set; }
            public decimal Decimal_val { get; set; }
            public float Float_val { get; set; }
            public string NotContains { get; set; }
        }

        private DataRow GetDataRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Str");
            dt.Columns.Add("DT_val");
            dt.Columns.Add("DTNull_val");
            dt.Columns.Add("Int32_val");
            dt.Columns.Add("Int32Null_val");
            dt.Columns.Add("Int16_val");
            dt.Columns.Add("Int64_val");
            dt.Columns.Add("UInt32_val");
            dt.Columns.Add("UInt16_val");
            dt.Columns.Add("UInt64_val");
            dt.Columns.Add("Double_val");
            dt.Columns.Add("Decimal_val");
            dt.Columns.Add("Float_val");
            return dt.NewRow();
        }
        [TestMethod]
        public void TestMethod_FillObjectDataRow_1()
        {
            DataRow dr = GetDataRow();
            CCBOTest obj = new CCBOTest();
            CBO.FillObject(obj, dr);
            Assert.IsTrue(true);

            dr["str"] = "test";
            dr["dTNull_val"] = "";
            dr["Int32_val"] = 3;
            dr["int32Null_val"] = "";
            dr["Int16_val"] = 2;
            dr["Int64_val"] = "1";
            dr["UInt32_val"] = 5;
            dr["UInt16_val"] = 7;
            dr["UInt64_val"] = 23;
            dr["Double_val"] = 9;
            dr["Decimal_val"] = "7";
            dr["Float_val"] = "67";

            dr["DT_val"] = "2015-02-34";
            try
            {
                CBO.FillObject(obj, dr);
                Assert.IsFalse(true);
            }
            catch(Exception ex)
            {
                ex.ToString();
                Assert.IsTrue(true);
            }
            dr["DT_val"] = "2015-02-03";
            CBO.FillObject(obj, dr);
            Assert.IsTrue(true);

            dr["UInt32_val"] = -5;
            try
            {
                CBO.FillObject(obj, dr);
                Assert.IsFalse(true);
            }
            catch (Exception ex)
            {
                ex.ToString();
                Assert.IsTrue(true);
            }
            dr["UInt32_val"] = "abc";
            try
            {
                CBO.FillObject(obj, dr);
                Assert.IsFalse(true);
            }
            catch (Exception ex)
            {
                ex.ToString();
                Assert.IsTrue(true);
            }
            dr["UInt32_val"] = "3";
            CBO.FillObject(obj, dr);

            dr["DTNull_val"] = "2015-02-04 16:00";
            Assert.IsTrue(obj.DTNull_val == null &&
                obj.Int32_val == 3 && obj.Int16_val == 2 && obj.Int64_val == 1 && obj.UInt32_val == 3 &&
                obj.UInt16_val == 7 && obj.UInt64_val == 23 && obj.Double_val == 9 && obj.Decimal_val == 7 &&
                obj.Float_val == 67 && obj.Int32Null_val == null &&
                obj.DT_val == Convert.ToDateTime("2015-02-03"));
        }
    }
}
