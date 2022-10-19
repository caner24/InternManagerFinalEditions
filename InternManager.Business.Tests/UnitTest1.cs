using NUnit.Framework;
using System.Data.SqlClient;

namespace InternManager.Business.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Test1()
        {
            SqlConnection conn = new SqlConnection("Server=tcp:internmanager.database.windows.net,1433;Initial Catalog=InternManager;Persist Security Info=False;User ID=myAdmin;Password=123456-Admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"); // assert
            conn.Open();
            SqlCommand komut = new SqlCommand("Select * from Persons",conn); 


            string id="";
            SqlDataReader dr = komut.ExecuteReader();   
            while (dr.Read())
            {
                id = dr["Id"].ToString();                     // act
            }

            Assert.IsNotEmpty(id.ToString()); // arrange
        }
    }
}