using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNet.SignalR.Hubs;
using System.Data.SqlClient;

namespace HR.Models
{
    public class notificationHub : Hub
    {


        //public void RegisterNotification(DateTime currentTime)
        //{
            //    string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    string sqlCommand = @"SELECT [ID],[number] from [dbo].[EOS_Request] where [Requset_date] < @AddedOn";
            //    //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
            //    using (SqlConnection con = new SqlConnection(conStr))
            //    {
            //        SqlCommand cmd = new SqlCommand(sqlCommand, con);
            //        cmd.Parameters.AddWithValue("@AddedOn", currentTime);
            //        if (con.State != System.Data.ConnectionState.Open)
            //        {
            //            con.Open();
            //        }
            //        cmd.Notification = null;
            //        SqlDependency sqlDep = new SqlDependency(cmd);
            //        sqlDep.OnChange += sqlDep_OnChange;
            //        //we must have to execute the command here
            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            // nothing need to add here now
            //        }
            //    }
            //}

            //void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
            //{
            //    //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record
            //    if (e.Type == SqlNotificationType.Change)
            //    {
            //        SqlDependency sqlDep = sender as SqlDependency;
            //        sqlDep.OnChange -= sqlDep_OnChange;

            //        //from here we will send notification message to client
            //        var notificationHub = GlobalHost.ConnectionManager.GetHubContext<notificationHub>();
            //        notificationHub.Clients.All.notify("added");
            //        //re-register notification
            //        RegisterNotification(DateTime.Now);
            //    }
            //}


            //[HubMethodName("sendMessages")]
            //public static void SendMessages()
            //{
            //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<notificationHub>();
            //    context.Clients.All.updateMessages();
            //}
            //public notificationHub()
            //{
            //    var taskTimer = Task.Factory.StartNew(async () =>
            //{
            //    while (true)
            //    {
            //        DateTime timeNow = DateTime.Now;
            //        //Sending the server time to all the connected clients on the client method SendServerTime()
            //        RegisterNotification(timeNow);
            //         //Delaying by 2 seconds.
            //         await Task.Delay(2000);
            //    }
            //}, TaskCreationOptions.LongRunning
            //       );

            //}
       // }
    }
}