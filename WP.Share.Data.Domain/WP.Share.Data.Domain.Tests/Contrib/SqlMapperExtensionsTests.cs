using Microsoft.VisualStudio.TestTools.UnitTesting;
using WP.Share.Data.Dapper.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WP.Share.Data.Domain.Orm.Repositories;

namespace WP.Share.Data.Dapper.Contrib.Tests
{
    [TestClass()]
    public class SqlMapperExtensionsTests
    {
        string connStr = "server=115.29.37.55,1440;database=OrderCenter;User ID =zsh; Password=L8%mVG^3hy^5V!*F#1V9mZ5FD*ezGeOn;Connect Timeout = 120";
        [TestMethod()]
        public void DeleteByIdTest()
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.DeleteById<Test2>("aaaa");
            }
        }

        [TestMethod()]
        public void GetTest2()
        {
            using (var conn = new SqlConnection(connStr))
            {
                var result = conn.Get<Test2>("bbbb");
            }
        }

        [TestMethod()]
        public void InsertTest1()
        {
            using (var conn = new SqlConnection(connStr))
            {
                var result = conn.Insert<Test2>(new Test2 { Id="ccccc",No=3323});
            }
        }

        [TestMethod()]
        public void InsertTest2()
        {
            using (var conn = new SqlConnection(connStr))
            {
                var result = conn.Insert<Test1>(new Test1 { Id = 33, No = 3323 });
            }
        }


        [TestMethod()]
        public void InsertTest3()
        {
            using (var conn = new SqlConnection(connStr))
            {
                var result = conn.Insert<List<Test3>>(new List<Test3> { new Test3 { No = "1111" },new Test3 { No="2222"} });
            }
        }


        [TestMethod()]
        public void UpdateTest()
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Update<Test2>(new Test2 { Id = "aaaa", No = 4 });
            }
        }

    }

    [Table("Test1")]
    public class Test1
    {
        [ExplicitKey]
        public int Id { get; set; }

        public int No { get; set; }
    }

    [Table("Test2"),KeyAlias("UserId")]
    public class Test2
    {
        [ExplicitKey]
        public string Id { get; set; }

        public int No { get; set; }
    }

    [Table("Test3")]
    public class Test3:Entity<int>
    {
        [Key]
        public override int Id { get; set; }

        public string No { get; set; }
    }
}