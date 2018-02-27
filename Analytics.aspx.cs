using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Configuration;

// namespaces for Sql and AJAX
using System.Data.SqlClient;
using System.Web.Script.Serialization;

public partial class Analytics : System.Web.UI.Page
{

    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");


        if ((int)Session["AdminFlag"] == 0)
        {

            Response.Redirect("RewardFeed.aspx");
        }
    }


    // below is the server-side code for the dashboard
    // here, we respond to client side AJAX requests by querying the database and returning json-array results


    public static string queryToJSON(String query)
    {
        // create the JSON string of a 2D array that can be used as a Google visualization data tables

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            // connect to database perform query
            //conn.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;";
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            //conn.Open();
            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = query;
            SqlDataReader sdr = sc.ExecuteReader();

            // read data into a 2D array
            ArrayList data = new ArrayList();
            // get column headers
            object[] fieldnames = new object[sdr.FieldCount];
            for (int i = 0; i < sdr.FieldCount; i++)
            {
                fieldnames[i] = sdr.GetName(i);
            }
            data.Add(fieldnames);
            // get row values
            while (sdr.Read())
            {
                // create array from a row of data
                object[] values = new object[sdr.FieldCount];
                sdr.GetValues(values);
                data.Add(values);
            }
            // serialize to JSON
            JavaScriptSerializer jss = new JavaScriptSerializer();
            String jsonResult = jss.Serialize(data);



            // return the json string
            return jsonResult;

        }
    }


    [System.Web.Services.WebMethod]
    public static string DoQuery1()
    {
       
        String query =
         "SELECT   (Employee.FirstName + ' ' + Employee.LastName) As 'Name', (Sum(Reward.RewardAmount)) As 'Money'" +
" FROM Employee INNER JOIN" +
             " Reward ON Employee.EmployeeID = Reward.RewardReceiver" +

             " Group By (Employee.FirstName + ' ' + Employee.LastName)";
        return queryToJSON(query);

    }

    [System.Web.Services.WebMethod]
    public static string DoQuery2()
    {
        // this query returns the name, id, and sales territory of AW's salespeople
        String query =
                                "Select RewardCategory.CategoryName, Count(Reward.RewardID) from RewardCategory Inner Join Reward on Reward.CategoryID = RewardCategory.CategoryID group by RewardCategory.CategoryName";
        return queryToJSON(query);

    }

    [System.Web.Services.WebMethod]
    public static String DoQuery3()
    {
        //String query = "Select (FirstName + ' ' + LastName) As Name, Position from Employee"
        String query = "Select (FirstName + ' ' + LastName) As Name, EmployeeID, Position from Employee";
        return queryToJSON(query);
    }


    [System.Web.Services.WebMethod]
    public static string DoQuery4()
    {
        String query = "Select CompanyValues.ValueName, count(Reward.RewardID) As 'Count' from CompanyValues Inner Join Reward On Reward.ValueID = CompanyValues.ValueID Group by CompanyValues.ValueName";
        return queryToJSON(query);
    }

    [System.Web.Services.WebMethod]
    public static string DoQuery5(String employeeName)
    {
        //String query = "SELECT Gift.GiftName, Count(EmployeeGift.TransactionID) FROM Gift INNER JOIN EmployeeGift ON Gift.GiftID = EmployeeGift.GiftID INNER JOIN Employee ON EmployeeGift.EmployeeID = Employee.EmployeeID where (Employee.FirstName + ' ' + Employee.LastName) = '" + employeeName + "' group by Gift.GiftName";
        String query = "SELECT Gift.GiftName, Count(EmployeeGift.TransactionID) FROM Gift INNER JOIN EmployeeGift ON Gift.GiftID = EmployeeGift.GiftID INNER JOIN Employee ON EmployeeGift.EmployeeID = Employee.EmployeeID where Employee.EmployeeID ='" + employeeName + "' group by Gift.GiftName";
        return queryToJSON(query);
    }

    [System.Web.Services.WebMethod]
    public static string DoQuery6(String employeeName)
    {
        //String query = "SELECT Vendor.VendorName, Count(EmployeeGift.TransactionID) FROM Vendor INNER JOIN Gift ON Gift.VendorID = Vendor.VendorID INNER JOIN EmployeeGift ON Gift.GiftID = EmployeeGift.GiftID INNER JOIN Employee ON EmployeeGift.EmployeeID = Employee.EmployeeID where (Employee.FirstName + ' ' + Employee.LastName) = '" + employeeName + "' group by Vendor.VendorName";
        String query = "SELECT Vendor.VendorName, Count(EmployeeGift.TransactionID) FROM Vendor INNER JOIN Gift ON Gift.VendorID = Vendor.VendorID INNER JOIN EmployeeGift ON Gift.GiftID = EmployeeGift.GiftID INNER JOIN Employee ON EmployeeGift.EmployeeID = Employee.EmployeeID where Employee.EmployeeID='" + employeeName + "' group by Vendor.VendorName";
        return queryToJSON(query);
    }

    [System.Web.Services.WebMethod]
    public static string DoQuery8(String employeeName)
    {
        //String query = "SELECT (Employee.FirstName + ' ' + Employee.LastName) As 'Name', Count(Reward.RewardID) FROM Reward INNER JOIN Employee ON Reward.RewardSender = Employee.EmployeeID where Reward.RewardReceiver = (select EmployeeID from Employee where (Employee.FirstName + ' ' + Employee.LastName) = '" + employeeName + "') Group By (Employee.FirstName + ' ' + Employee.LastName)";
        String query = "SELECT (Employee.FirstName + ' ' + Employee.LastName) As 'Name', Count(Reward.RewardID) FROM Reward INNER JOIN Employee ON Reward.RewardSender = Employee.EmployeeID where Reward.RewardReceiver = '" + employeeName + "' Group By (Employee.FirstName + ' ' + Employee.LastName)";
        return queryToJSON(query);
    }

    [System.Web.Services.WebMethod]
    public static string DoQuery9(String employeeName)
    {
        //String query = "SELECT (Employee.FirstName + ' ' + Employee.LastName) As 'Name', Count(Reward.RewardID) FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID where Reward.RewardSender = (select EmployeeID from Employee where (Employee.FirstName + ' ' + Employee.LastName) = '" + employeeName + "') Group By (Employee.FirstName + ' ' + Employee.LastName)";
        String query = "SELECT (Employee.FirstName + ' ' + Employee.LastName) As 'Name', Count(Reward.RewardID) FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID where Reward.RewardSender ='" + employeeName + "' Group By (Employee.FirstName + ' ' + Employee.LastName)";

        return queryToJSON(query);
    }

    [System.Web.Services.WebMethod]
    public static string DoQuery11(String employeeName)
    {
        String query = "SELECT (Employee.FirstName + ' ' + Employee.LastName) As 'Name', Sum(Reward.RewardAmount) FROM Reward INNER JOIN Employee ON Reward.RewardSender = Employee.EmployeeID where Reward.RewardReceiver = '" + employeeName + "' Group By (Employee.FirstName + ' ' + Employee.LastName)";


        return queryToJSON(query);
    }

    [System.Web.Services.WebMethod]
    public static string DoQuery12(String employeeName)
    {
        String query = "SELECT (Employee.FirstName + ' ' + Employee.LastName) As 'Name', Sum(Reward.RewardAmount) FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID where Reward.RewardSender = '" + employeeName + "' Group By (Employee.FirstName + ' ' + Employee.LastName)";


        return queryToJSON(query);
    }
}