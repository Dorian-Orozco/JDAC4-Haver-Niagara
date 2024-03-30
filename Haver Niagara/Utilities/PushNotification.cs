using Haver_Niagara.Models;
using Haver_Niagara.ViewModels;
using System.Text.Json;
using WebPush;

namespace Haver_Niagara.Utilities
{
    /// <summary>
    /// Helper Utility for sending Push Notificaitons
    /// </summary>
    public static class PushNotification
    {
        /// <summary>
        /// Send Push Notification to selected Engineer Employees when an NCR is created
        /// </summary>
        /// <param name="vapidDetails">Vapid Details with values from the configuration</param>
        /// <param name="employees">List of Employees with Push Subscriptions</param>
        /// <param name="Title">Title for the Notification Message</param>
        /// <param name="Message">Body of the Message</param>
        /// <returns>Message summarizing the results.</returns>
        public static string Send(VapidDetails vapidDetails, List<Employee> employees, string Title, string Message)
        {
            //Make sure you have included all subscriptions for each employee
            string ReturnMessage = "";

            //Built the message and serialize it to Json
            PushMessageVM pushMessageVM = new PushMessageVM()
            {
                title = Title,
                message = Message
            };

            string payload = JsonSerializer.Serialize(pushMessageVM);

            int totalCount = 0;
            int employeeCount = 0;
            int subscriptionCount = 0;
            foreach (Employee employee in employees)
            {
                employeeCount++;
                foreach (Subscription sub in employee.Subscriptions)
                {
                    subscriptionCount = 0;
                    var pushSubscription = new PushSubscription(sub.PushEndpoint, sub.PushP256DH, sub.PushAuth);

                    try
                    {
                        var webPushClient = new WebPushClient();
                        webPushClient.SendNotification(pushSubscription, payload, vapidDetails);
                        subscriptionCount++;
                        totalCount++;
                    }
                    catch (WebPushException ex)
                    {
                        var statusCode = ex.StatusCode;
                        ReturnMessage += "Error Sending Notification to " + employee.FullName +
                            ". Failed with Status Code " + (int)statusCode + "<br />";
                    }
                }
                ReturnMessage += "Sent Notification to " + subscriptionCount +
                    " Subscription" + ((subscriptionCount > 1) ? "s" : "") +
                    " for " + employee.FullName + "." + "<br />";
            }
            ReturnMessage += "<strong>Total of  " + totalCount + " Push Notification" +
                ((totalCount > 1) ? "s" : "") + " sent to " + employeeCount +
                " Employee" + ((employeeCount > 1) ? "s" : "") + "." + "</strong>";

            return ReturnMessage;
        }
    }
}
